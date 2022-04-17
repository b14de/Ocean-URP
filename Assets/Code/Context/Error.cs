using Code.Managers;
using Code.Model;
using Code.Utilities;

namespace Code.ContextSystem
{
    public class Error : Context
    {
        #region Methods
        public override void Enter()
        {
            DebugLogger.LogMessage("Entered Error Context");

            if (EventDataStore.HasData(EventType.Error))
            {
                var error 	= EventDataStore.RemoveData<ErrorEvent>(EventType.Error);
                DebugLogger.LogError(error.Message);
                ManagerDirectory.UIManager.SetError(error);
            }
			
            HasEntered 		= true;
        }

        public override void Exit()
        {
            CanExit 	= false;
            HasEntered 	= false;
            DebugLogger.LogMessage("Exited Error Context");
        }
		
        public override void SetCanExitAsSoonAsPossible()
        {
            CanExit 	= true;
        }
        #endregion
    }
}