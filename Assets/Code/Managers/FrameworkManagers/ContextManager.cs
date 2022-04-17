using Code.ContextSystem;
using Code.Scene;
using Code.Utilities;

namespace Code.Managers
{
    public class ContextManager : IUpdatable, ICoreManager
    {
        #region Fields
		private ContextType	 			_targetContext;
		private SceneController 		_sceneController;
		private bool 					_currentContextHasNoScene;
		#endregion
		
		#region Properties
		public ContextType 				CurrentContextType 				{ get; private set; }
		public Context					CurrentContext 					{ get; private set; }
		#endregion

		
		#region Methods
		public void Start()
		{
			DebugLogger.LogMessage("Started ContextManager");
			_sceneController 				= new SceneController();
			SetStartupAsFirstContext();
		}
		
		public void Update()
		{
			CheckIfContextShouldChange();

			var currentContextHasEntered 	= EnterCurrentContextIfNeeded();

			if (currentContextHasEntered)
			{
				CurrentContext.Update();
			}
		}

		public void RequestContextChange(ContextType targetContext)
		{
			_targetContext 					= targetContext;
		}
		#endregion

		
		#region Implementation
		private void SetStartupAsFirstContext()
		{
			_targetContext 					= ContextType.Startup;
			SetTargetContextAsCurrent();
			CurrentContext.Enter();
		}
		
		private void CheckIfContextShouldChange()
		{
			if (_targetContext != CurrentContextType)
			{
				if (CurrentContext.CanExit)
				{
					CurrentContext.Exit();
					RemoveSceneForCurrentContext();
					SetTargetContextAsCurrent();
					FireSceneLoadForNewContext();
				}
				else
				{
					CurrentContext.SetCanExitAsSoonAsPossible();
				}
			}
		}

		private void SetTargetContextAsCurrent()
		{
			CurrentContext 				= ContextMapper.GetContextImplementation(_targetContext);
			CurrentContextType 			= _targetContext;
		}

		private bool EnterCurrentContextIfNeeded()
		{
			if (!CurrentContext.HasEntered)
			{
				if (RequiredSceneHasLoaded())
				{
					ManagerDirectory.EventManager.Trigger(EventType.ContextChanging);
					CurrentContext.Enter();
					return true;
				}
				
				return false;
			}
			
			return true;
		}

		private bool RequiredSceneHasLoaded()
		{
			return _currentContextHasNoScene ||
			       _sceneController.SceneRequestHasLoaded();
		}

		private void RemoveSceneForCurrentContext()
		{
			if (SceneMapper.HasAssociatedScene(CurrentContextType))
			{
				SceneController.UnloadScene(SceneMapper.GetSceneName(CurrentContextType));
			}
		}
		
		private void FireSceneLoadForNewContext()
		{
			_currentContextHasNoScene 		= false;
			
			if (SceneMapper.HasAssociatedScene(CurrentContextType))
			{
				_sceneController.LoadSceneAsync(SceneMapper.GetSceneName(CurrentContextType));
			}
			else
			{
				_currentContextHasNoScene 	= true;
			}
		}
		#endregion
    }
}