using Code.Managers;
using Code.View.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.View.Context
{
    public class LoadingUI : MonoBehaviour, IUIScreen
    {
        [SerializeField] private TextMeshProUGUI 		_subtext;
        [SerializeField] private TextMeshProUGUI 		_progress;
        [SerializeField] private Button 				_cancelButton;

        private const string 							_progressHeader 			= "Loading: ";
        private bool									_progressing;
        private float                                   _currentProgress;
        #region Unity Methods

        private void Update()
        {
            if (_progressing)
            {
                _progress.text	 = _progressHeader + _currentProgress.ToString("0") + "%";
            }
        }
        #endregion
		
        #region Methods
        
        public void Setup()
        {
            SetupCancelButton();
        }
        
        public void SetSubtext(string subtext)
        {
            _subtext.text 		= subtext;
        }

        public void SetNewProgress(float currentProgress)
        {
            _progressing 		= true;
            _currentProgress 	= currentProgress;
        }

        public void ShowCancelButton()
        {
            _cancelButton.gameObject.SetActive(true);	
        }

        public void HideCancelButton()
        {
            _cancelButton.gameObject.SetActive(false);
        }
        #endregion
		
        #region Implementation
        private void SetupCancelButton()
        {
            _cancelButton.onClick.AddListener(() =>
            {
                ManagerDirectory.EventManager.Trigger(EventType.CancelDownloadRequested);   //TODO: Use cancellation token instead
            });
        }
        #endregion
    }
}