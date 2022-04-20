using Code.Utilities;

namespace Code.ContextSystem
{
    public class InGame : Context
    {
        #region Fields

        #endregion

        #region Methods

        public override void Enter()
        {
            DebugLogger.LogMessage("Entered InGame Context");
            HasEntered = true;
        }

        public override void Update()
        {
            if (!CanExit)
            {
            }
        }

        public override void Exit()
        {
            HasEntered = false;
            CanExit = false;

            DebugLogger.LogMessage("Exited LoadingGameWorld Context");
        }

        public override void SetCanExitAsSoonAsPossible()
        {
            CanExit = true;
        }

        #endregion

        #region Implementation

        #endregion
    }
}