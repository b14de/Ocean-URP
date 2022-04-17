using System.Collections.Generic;
using Code.ContextSystem;
using Code.Utilities;
using Code.Utilities.FileSystem;

namespace Code.Scene
{
    public class SceneMapper
    {
        #region Fields
        private static readonly Dictionary<ContextType, string> 		_contextScenes 		= new Dictionary<ContextType, string>
        {
            {ContextType.InGame, 		        FileNames.Scenes.MAINWORLD},
            {ContextType.MainMenu, 				FileNames.Scenes.MAINMENU},
            {ContextType.Error, 				FileNames.Scenes.ERROR}
        };
        #endregion
		
        #region
        public static bool HasAssociatedScene(ContextType context)
        {
            return _contextScenes.ContainsKey(context);
        }
		
        public static string GetSceneName(ContextType context)
        {
            if (_contextScenes.ContainsKey(context))
            {
                return _contextScenes[context];
            }

            DebugLogger.LogError("Scene not found for " + context);
            return "";
        }
        #endregion
    }
}