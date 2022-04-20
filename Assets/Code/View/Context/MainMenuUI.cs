using Code.Managers;
using Code.Model;
using Code.Utilities;
using Code.View.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View.Context
{
    public class MainMenuUI : MonoBehaviour, IUIScreen
    {
        #region Fields
        [SerializeField] private GameObject _playButtonGameObject;
        private Button _playButton;
        #endregion


        #region Properties

        #endregion

        #region Methods

        private void GetComponentsForUI()
        {
            _playButton = _playButtonGameObject.GetComponent<Button>();

            SetActions();
        }


        private void SetActions()
        {
            _playButton.onClick.AddListener(() =>
            {
                EventDataStore.AddData(EventType.StartGameRequested, new GameDescription()
                {
                    Type = GameDescription.GameType.SinglePlayer,
                    Difficulty = GameDescription.GameDifficulty.Medium
                });
                
                ManagerDirectory.EventManager.Trigger(EventType.StartGameRequested);
            });
        }

        #endregion

        #region Implementation

        public void Setup()
        {
            GetComponentsForUI();
        }

        #endregion

        
    }
}