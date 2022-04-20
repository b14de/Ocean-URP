using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Utilities;
using UnityEngine.PlayerLoop;

namespace Code.Managers
{
    public class ManagerDirectory
    {
        #region Fields

        private static Dictionary<Type, ICoreManager>                  _coreManagers;
        private static Dictionary<Type, IRuntimeManager> 		       _runtimeManagers;

        private static List<IUpdatable> 							   _updatables;
        private static List<ILateUpdatable> 						   _lateUpdatables;
        private static List<IFixedUpdatable> 						   _fixedUpdatables;

        #endregion


        #region Properties

        public static EventManager EventManager => GetCoreManager<EventManager>();
        public static ContextManager ContextManager => GetCoreManager<ContextManager>();
        public static UIManager UIManager => GetCoreManager<UIManager>();
        public static GameManager GameManager => GetCoreManager<GameManager>();

        #endregion

        
        #region Methods

        public ManagerDirectory()
        {
            _updatables                     = new List<IUpdatable>();
            _lateUpdatables                 = new List<ILateUpdatable>();
            _fixedUpdatables                = new List<IFixedUpdatable>();
            _runtimeManagers 				= new Dictionary<Type, IRuntimeManager>();

            CreateAndStartCoreManagers();
            AddCoreUpdateables();
        }

        public void UpdateUpdatableManagers()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }
        
        public void UpdateLateUpdatableManagers()
        {
            foreach (var lateUpdatable in _lateUpdatables)
            {
                lateUpdatable.LateUpdate();
            }
        }
                
        public void UpdateFixedUpdatableManagers()
        {
            foreach (var fixedUpdatable in _fixedUpdatables)
            {
                fixedUpdatable.FixedUpdate();
            }
        }
        
        public static void NoRuntimeManagersRequired()
        {
            if (_runtimeManagers.Count <= 0) return;
            
            ExitNonContinuingManagers(_runtimeManagers);
            _runtimeManagers 			= new Dictionary<Type, IRuntimeManager>();
        }

        public static void RequestRuntimeManagersBeMadeAvailable(params Type[] managers)
        {
            if (managers.Length > 0)
            {
                SetNewRequiredManagers(managers);
            }
        }
        
        public static IRuntimeManager GetRuntimeManager(Type managerType)
        {
            if (_runtimeManagers.ContainsKey(managerType))
            {
                return _runtimeManagers[managerType];
            }
			
            DebugLogger.LogWarning("Manager " + managerType + " is not available.");
            return null;
        }

        #endregion


        #region Implemnentation

        private void CreateAndStartCoreManagers()
        {
            if(_coreManagers != null)
            {
                DebugLogger.LogError("Core managers already created");
                return;
            }
            
            _coreManagers = new Dictionary<Type, ICoreManager>
            {
                {typeof(EventManager), 		    new EventManager()},
                {typeof(ContextManager), 		new ContextManager()},
                {typeof(UIManager), 			new UIManager()},
                {typeof(GameManager), 			new GameManager()}
            };
        }

        private void AddCoreUpdateables()
        {
            foreach (var coreManager in _coreManagers)
            {
                coreManager.Value.Start();

                if (coreManager.Value is IUpdatable updatable)
                {
                    _updatables.Add(updatable);
                }
                
                if (coreManager.Value is ILateUpdatable lateUpdatable)
                {
                    _lateUpdatables.Add(lateUpdatable);
                }
                
                if (coreManager.Value is IFixedUpdatable fixedUpdatable)
                {
                    _fixedUpdatables.Add(fixedUpdatable);
                }
            }
        }
        
        private static void SetNewRequiredManagers(Type[] requiredManagers)
        {
            var previousManagers = _runtimeManagers;
            _runtimeManagers 				= new Dictionary<Type, IRuntimeManager>();
			
            foreach (var manager in requiredManagers)
            {
                if (previousManagers.ContainsKey(manager))
                {
                    _runtimeManagers.Add(manager, previousManagers[manager]);
                    previousManagers.Remove(manager);
                }
                else
                {
                    CreateAndStartNewRuntimeManager(manager);
                }
            }

            ExitNonContinuingManagers(previousManagers);
        }

        private static void CreateAndStartNewRuntimeManager(Type manager)
        {
            var constructors = manager.GetConstructors(BindingFlags.Public);

            var managerInstance = constructors[0].Invoke(new object[] { });

            if (managerInstance != null)
            {
                if (!(managerInstance is IRuntimeManager runtimeManager))
                {
                    DebugLogger.LogError($"{manager.Name} does not implement IRuntimeManager");
                    return;
                }
                
                _runtimeManagers.Add(manager, runtimeManager);
                runtimeManager.Start();

                if (managerInstance is IUpdatable updateable)
                {
                    _updatables.Add(updateable);
                }
            }
            else
            {
                DebugLogger.LogError($"RuntimeManager {manager.Name} failed to construct");
            }
        }
        
        private static void ExitNonContinuingManagers(Dictionary<Type, IRuntimeManager> previousManagers)
        {
            foreach (var manager in previousManagers.Values)
            {
                if (manager is IUpdatable)
                {
                    if ( _updatables.Contains(manager as IUpdatable))
                    {
                        _updatables.Remove(manager as IUpdatable);
                    }
                }
                
                if (manager is ILateUpdatable)
                {
                    if (_lateUpdatables.Contains(manager as ILateUpdatable))
                    {
                        _lateUpdatables.Remove(manager as ILateUpdatable);
                    }
                }
                
                if (manager is IFixedUpdatable)
                {
                    if (_fixedUpdatables.Contains(manager as IFixedUpdatable))
                    {
                        _fixedUpdatables.Remove(manager  as IFixedUpdatable);
                    }
                }
				
                manager.Exit();
            }
        }

        private static T GetCoreManager<T>() where T : class
        {
            return (T)_coreManagers[typeof(T)];
        }
        
        #endregion
    }
}