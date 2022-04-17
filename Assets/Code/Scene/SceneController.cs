using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scene
{
    public class SceneController
    {
        #region Fields
        private AsyncOperation 		_asyncSceneLoad;
        #endregion
		
        #region Methods
        public void LoadSceneAsync(string sceneName)
        {
            _asyncSceneLoad 		= SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public static void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        public bool SceneRequestHasLoaded()
        {
            return _asyncSceneLoad != null &&
                   _asyncSceneLoad.isDone;
        }
        #endregion
    }
}