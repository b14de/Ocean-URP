using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scene
{
    public class SceneController
    {
        #region Properties

        private AsyncOperation _asyncSceneLoad;
        #endregion
		
        #region Methods
        public void LoadSceneAsync(string sceneName, bool setActive)
        {
            _asyncSceneLoad                      = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _asyncSceneLoad.allowSceneActivation = true;
            
            _asyncSceneLoad.completed += (AsyncOperation obj) =>
            {
                if (setActive)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                }
            };
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
        
        
        public float SceneRequestProgress()
        {
            if (_asyncSceneLoad?.progress != null) return (float)_asyncSceneLoad?.progress;
            return 0;
        }

        #endregion
    }
}