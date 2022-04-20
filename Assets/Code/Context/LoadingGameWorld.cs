using Code.Managers;
using Code.Utilities;

namespace Code.ContextSystem
{
	public class LoadingGameWorld : Context
	{
		#region Fields
		#endregion
		
		#region Methods
		public override void Enter()
		{
			DebugLogger.LogMessage("Entered LoadingGameWorld Context");
			HasEntered 							= true;
			ManagerDirectory.UIManager.SetLoadingWorldText();
			ManagerDirectory.EventManager.RegisterListener(EventType.CancelLoadingRequested, CancelLoading);
		}

		public override void Update()
		{
			if (!CanExit)
			{
				ManagerDirectory.UIManager.SetLoadingProgress(ManagerDirectory.ContextManager.GetSceneRequestProgress());
				MoveOnIfRetrievalsComplete();
			}
		}

		public override void Exit()
		{
			ManagerDirectory.EventManager.DeregisterListener(EventType.CancelLoadingRequested, CancelLoading);
			HasEntered 				= false;
			CanExit 				= false;
			
			DebugLogger.LogMessage("Exited LoadingGameWorld Context");
		}
		
		public override void SetCanExitAsSoonAsPossible()
		{
			CanExit 			= true;
		}
		#endregion
		
		#region Implementation
		private void CancelLoading()
		{
			ManagerDirectory.ContextManager.RequestContextChange(ContextType.MainMenu);
			CanExit 			= true;
		}
		
		private void MoveOnIfRetrievalsComplete()
		{
			if (!ManagerDirectory.ContextManager.GetSceneRequestHasCompleted()) return;

			RequestNextContext();
		}
		
		private void RequestNextContext()
		{
			ManagerDirectory.ContextManager.RequestContextChange(ContextType.InGame);
			CanExit 				= true;
		}
		#endregion
	}
}