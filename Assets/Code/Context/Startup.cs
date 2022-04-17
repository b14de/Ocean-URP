using Code.Managers;
using Code.Utilities;

namespace Code.ContextSystem
{
	public class Startup : Context
	{
		#region Methods

		public override void Enter()
		{
			DebugLogger.LogMessage("Entered Startup Context");
			HasEntered = true;

			SetEnvironmentSettings();
			DoStartupInitialisation();
			ManagerDirectory.EventManager.RegisterListener(EventType.AppExitRequested, ExitApp);
		}

		public override void Exit()
		{
			HasEntered = false;
			CanExit = false;
			DebugLogger.LogMessage("Exited Startup Context");
		}

		public override void SetCanExitAsSoonAsPossible()
		{
		}

		#endregion

		#region Implementation

		private void DoStartupInitialisation()
		{
			CanExit 	= true;
			ManagerDirectory.ContextManager.RequestContextChange(ContextType.MainMenu);
		}

		private void ExitApp()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
		}


		private void SetEnvironmentSettings()
		{
#if UNITY_EDITOR
			Settings.RunningInEditor = true;
#endif
		}

		#endregion
	}
}