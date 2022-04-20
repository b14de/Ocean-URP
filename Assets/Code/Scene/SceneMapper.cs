using System.Collections.Generic;
using Code.ContextSystem;
using Code.Utilities;
using Code.Utilities.FileSystem;

namespace Code.Scene
{
    public class SceneMapper
    {
        #region Fields
        private struct SceneMap
        {
            public string SceneName;
            public bool   SetAsActive;
        }
        
        private static readonly Dictionary<ContextType, SceneMap> 		_contextScenes 		= new Dictionary<ContextType, SceneMap>
        {
            {ContextType.InGame, 		        new SceneMap{ SceneName = FileNames.Scenes.MAINWORLD, SetAsActive = true}},
            {ContextType.MainMenu, 				new SceneMap{ SceneName = FileNames.Scenes.MAINMENU, SetAsActive  = false}},
            {ContextType.LoadingGameWorld, 		new SceneMap{ SceneName = FileNames.Scenes.LOADING, SetAsActive   = false}},
            {ContextType.Error, 				new SceneMap{ SceneName = FileNames.Scenes.ERROR, SetAsActive     = false}},
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
                return _contextScenes[context].SceneName;
            }

            DebugLogger.LogError("Scene not found for " + context);
            return "";
        }
        
        public static bool GetSetAsActive(ContextType context)
        {
            if (_contextScenes.ContainsKey(context))
            {
                return _contextScenes[context].SetAsActive;
            }

            DebugLogger.LogError("Scene not found for " + context);
            return false;
        }
        #endregion
    }
}