using Code.Managers;
using Code.Model;
using Code.Utilities;

namespace Code.ContextSystem
{
	public class MainMenu : Context
	{
		#region Fields
		#endregion
		
		#region Methods
		public override void Enter()
		{
			DebugLogger.LogMessage("Entered Main Menu Context");

			RegisterEventListeners();
			
			HasEntered		= true;
		}

		public override void Exit()
		{
			DeregisterEventListeners();
			
			CanExit 		= false;
			HasEntered 		= false;
			DebugLogger.LogMessage("Exited Main Menu Context");
		}
		
		public override void SetCanExitAsSoonAsPossible()
		{
			CanExit 		= true;
		}
		#endregion
		
		#region Implementation
		private void OnStartGameRequested()
		{
			if (EventDataStore.HasData(EventType.StartGameRequested))
			{
				var startGameRequest 	= EventDataStore.RemoveData<GameDescription>(EventType.StartGameRequested);
				ManagerDirectory.GameManager.SetCurrentGameDescription(startGameRequest);
				ManagerDirectory.ContextManager.RequestContextChange(ContextType.LoadingGameWorld);
				
				CanExit 	= true;
			}
		}

		private void RegisterEventListeners()
		{
			ManagerDirectory.EventManager.RegisterListener(EventType.StartGameRequested, OnStartGameRequested);
		}

		private void DeregisterEventListeners()
		{
			ManagerDirectory.EventManager.DeregisterListener(EventType.StartGameRequested, OnStartGameRequested);
		}
		#endregion
	}
}