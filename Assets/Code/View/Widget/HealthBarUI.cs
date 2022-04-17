using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View.Widget
{
    public class HealthBarUI : MonoBehaviour
    {

        #region Fields
        
        public Image healthBar;
        public Image healthBarBackground;
        public TMP_Text healthText;
        
        #endregion


        #region Properties

        #endregion

        #region Methods

        public void SetHealth(float health)
        {
            healthBar.fillAmount = health;
            healthText.text = health.ToString("0.0");
        }

        #endregion

        #region Implementation

        #endregion

        
    }
}