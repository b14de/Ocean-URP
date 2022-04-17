using System;
using Code.Managers;
using Code.Utilities;
using Code.View.Interfaces;
using Code.View.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View.Context
{
    public class InGameUI : MonoBehaviour, IUIScreen
    {
        #region Fields
        [SerializeField] private GameObject _healthBarGameObject;
        private HealthBarUI _healthBar;
        #endregion


        #region Properties

        #endregion

        #region Methods

        public void Setup()
        {
            GetComponentsForUI();
            ManagerDirectory.EventManager.RegisterListener(EventType.PlayerHealthChanged, SetHealthBar);
        }

        #endregion

        #region Implementation

        private void GetComponentsForUI()
        {
            _healthBarGameObject.GetComponent<HealthBarUI>();
        }

        private void SetHealthBar()
        {
            var health = EventDataStore.RemoveData<float>(EventType.PlayerHealthChanged);
            _healthBar.SetHealth(health);
        }

        private void OnDestroy()
        {
            ManagerDirectory.EventManager.DeregisterListener(EventType.PlayerHealthChanged, SetHealthBar);

        }

        #endregion

        
    }
}