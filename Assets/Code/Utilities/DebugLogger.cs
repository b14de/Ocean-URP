using Code.Utilities.View;
using UnityEngine;

namespace Code.Utilities
{
    public class DebugLogger
    {
        #region Fields
        private const string 					UserActionPrefix 		= "UserAction:";
        private const string 					MessagePrefix 			= "\tMessage: ";
        private const string 					WarningPrefix 			= "\t\tWarning: ";
        private const string 					ErrorPrefix 			= "\t\t\tError: ";
		
        private static readonly HexColor		MessageColor 			= new HexColor(Color.green);
        private static readonly HexColor		WarningColor 			= new HexColor(new Color(255, 165, 0));
        private static readonly HexColor		ErrorColor 				= new HexColor(Color.red);
        private static readonly HexColor 		UserActionColor 		= new HexColor(Color.blue);
        #endregion
		
        #region Methods
        public static void LogMessage(string message, bool logToFile = false)
        {
            Debug.Log(MessageColor.ColorString(message));

            if (logToFile)
            {
                LogToFile(MessagePrefix + message);
            }
        }

        public static void LogWarning(string message, bool logToFile = false)
        {
            Debug.LogWarning(WarningColor.ColorString(message));

            if (logToFile)
            {
                LogToFile(WarningPrefix + message);
            }
        }

        public static void LogError(string message, bool logToFile = false)
        {
            Debug.LogError(ErrorColor.ColorString(message));

            if (logToFile)
            {
                LogToFile(ErrorPrefix + message);
            }
        }

        public static void LogUserAction(string message, bool logToFile = false)
        {
            Debug.Log(UserActionColor.ColorString(message));

            if (logToFile)
            {
                LogToFile(UserActionPrefix + message);
            }
        }
        #endregion
		
        #region Implementation
        private static void LogToFile(string message)
        {
            // DO TO FILE APPENDING
        }
        #endregion
    }
}