using System.Collections.Generic;
using Code.Model;
using Code.Utilities;

namespace Code.Managers
{
    public class GameManager : ICoreManager
    {
        #region Properties

        public GameDescription CurrentGameDescription { get; private set; }
        #endregion

        #region Methods
        public void Start()
        {
            DebugLogger.LogMessage("Started Game Manager");
        }

        public void SetCurrentGameDescription(GameDescription gameDescription)
        {
            CurrentGameDescription = gameDescription;
        }

        public void ClearAll()
        {
            CurrentGameDescription 		= null;
        }
        #endregion
    }
}