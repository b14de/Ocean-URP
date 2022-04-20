using Code.ContextSystem;
using Code.Model;
using Code.Utilities;
using Code.View;
using Code.View.Context;
using UnityEngine;

namespace Code.Managers.UI
{
    public class ContextualUIController
    {
        #region Fields

        // Startup ------------------------------------------------------

        private readonly GlobalUser 	_globalUser;
        
        // --------------------------------------------------------------
        // Error --------------------------------------------------------
		
        private ErrorUI					_errorUI;
        
        // --------------------------------------------------------------
        // Loading ------------------------------------------------------
        
        private LoadingUI               _loadingUI;

        // --------------------------------------------------------------
        // InGame -------------------------------------------------------
        
        private InGameUI               _inGameUI;

        // --------------------------------------------------------------
        // MainMenuUI ---------------------------------------------------
        
        private MainMenuUI             _mainMenuUI;
        
        
        
        private ContextType 			_currentContext;

        #endregion
        
        #region Properties

        #endregion

        #region Methods
        public ContextualUIController()
        {
            _currentContext 	= ContextType.Startup;
            _globalUser 		= GameObject.FindObjectOfType<GlobalUser>();
        }
        
        public void SetLoadingProgress(float loadingProgress)
        {
            if (_currentContext == ContextType.LoadingGameWorld)
            {
                _loadingUI.SetNewProgress(loadingProgress);
            }
        }
        public void ContextChanging(ContextType currentContext)
        {	
            _currentContext 	= currentContext;
            SetUI();
        }
        
        public void SetError(ErrorEvent error)
        {
            _errorUI.SetError(error);
        }
        
        public void SetMessage(string message)
        {
            switch (_currentContext)
            {
                case ContextType.LoadingGameWorld:
                    _loadingUI.SetSubtext(message);
                    break;
                default:
                    // Other UIs dont have a message
                    break;
            }
        }
        #endregion

        #region Implementation
        private void SetUI()
        {
            switch (_currentContext)
            {
                case ContextType.MainMenu:
                    _mainMenuUI 			= GameObject.FindObjectOfType<MainMenuUI>();
                    _mainMenuUI.Setup();
                    break;
                case ContextType.LoadingGameWorld:
                    _loadingUI 					= GameObject.FindObjectOfType<LoadingUI>();
                    _loadingUI.Setup();
                    _loadingUI.HideCancelButton();
                    break;
                case ContextType.InGame:
                    _inGameUI 			        = GameObject.FindObjectOfType<InGameUI>();
                    _inGameUI.Setup();
                    break;
                case ContextType.Error:
                    _errorUI 					= GameObject.FindObjectOfType<ErrorUI>();
                    break;
                default:
                    DebugLogger.LogWarning(_currentContext + " has not set UI");
                    break;
            }
        }
        #endregion
    }
}