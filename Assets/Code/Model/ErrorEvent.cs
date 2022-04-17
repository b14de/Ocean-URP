using System;

namespace Code.Model
{
    public class ErrorEvent
    {
        #region Properties
        public Action 		RecoverAction 		{ get; }
        public string 		Message 			{ get; }
        #endregion

        #region Constructor
        public ErrorEvent(string message, Action recoverAction = null)
        {
            RecoverAction 	= recoverAction;
            Message 		= message;
        }
        #endregion
    }
}