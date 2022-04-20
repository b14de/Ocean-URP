using Code.Managers;

namespace Code.Model
{
    public class GameDescription
    {
        #region Fields
        public enum GameType
        {
            SinglePlayer,
            MultiPlayer
        }
        
        public enum GameDifficulty
        {
            Easy,
            Medium,
            Hard
        }

        public GameType Type;
        public GameDifficulty Difficulty;
        #endregion


        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Implementation

        #endregion

        
    }
}