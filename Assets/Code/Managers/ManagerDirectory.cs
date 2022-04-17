using System;
using System.Collections.Generic;
using Code.Utilities;

namespace Code.Managers
{
    public class ManagerDirectory
    {
        #region Fields

        private static Dictionary<Type, ICoreManager>                  _coreManagers;
        private static Dictionary<Type, IRuntimeManager> 		       _runtimeManagers;

        private static List<IUpdatable> 							   _updatables;
        private static List<ILateUpdatable> 						   _lateUpdatables;

        #endregion


        #region Properties

        public static EventManager EventManager => GetCoreManager<EventManager>();
        public static ContextManager ContextManager => GetCoreManager<ContextManager>();
        public static UIManager UIManager => GetCoreManager<UIManager>();

        #endregion

        
        #region Methods

        public ManagerDirectory()
        {
            _updatables = new List<IUpdatable>();
            _lateUpdatables = new List<ILateUpdatable>();
            
            CreateAndStartCoreManagers();
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

        #endregion


        #region Implemnentation

        private void CreateAndStartCoreManagers()
        {
            if(_coreManagers != null)
            {
                DebugLogger.LogError("Core managers can only be create once");
                return;
            }
            
            _coreManagers = new Dictionary<Type, ICoreManager>
            {
                {typeof(EventManager), 		    new EventManager()},
                {typeof(ContextManager), 		new ContextManager()},
                {typeof(UIManager), 			new UIManager()}
            };

            foreach (var coreManager in _coreManagers)
            {
                coreManager.Value.Start();
            }
        }

        private static T GetCoreManager<T>() where T : class
        {
            return (T)_coreManagers[typeof(T)];
        }
        
        #endregion
    }
}