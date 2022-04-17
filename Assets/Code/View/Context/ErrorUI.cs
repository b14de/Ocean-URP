using Code.Managers;
using Code.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View.Context
{
    public class ErrorUI : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TMP_Text 		_errorMessage;
        [SerializeField] private Button 	    _exitAppButton;
        #endregion
		
        #region Methods
        public void SetError(ErrorEvent error)
        {
            _errorMessage.text		 = error.Message;

            if (error.RecoverAction != null)
            {
                _exitAppButton.onClick.AddListener( () => error.RecoverAction());
            }
            else
            {
                _exitAppButton.onClick.AddListener( () => 
                    ManagerDirectory.EventManager.Trigger(EventType.AppExitRequested)
                    );
            }
        }
        #endregion
    }
}