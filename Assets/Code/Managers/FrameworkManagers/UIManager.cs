using Code.Managers.UI;
using Code.Model;

namespace Code.Managers
{
    public class UIManager : ICoreManager, IUpdatable
    {
        #region Fields
        
        private ContextualUIController 		_contextualUIController;
        private const int 					PERCENTAGE_MULTIPLIER 					= 100;

        #endregion


        #region Properties

        #endregion

        #region Methods

        public void Start()
        {
            ManagerDirectory.EventManager.RegisterListener(EventType.ContextChanging, () =>
            {
                _contextualUIController.ContextChanging(ManagerDirectory.ContextManager.CurrentContextType);
            });
        }
		
        public void SetLoadingProgress(float progress)
        {
            _contextualUIController.SetLoadingProgress(progress * PERCENTAGE_MULTIPLIER);
        }
        
        public void SetError(ErrorEvent error)
        {
            _contextualUIController.SetError(error);
        }
        public void Update()
        {
            
        }

        #endregion

        #region Implementation

        #endregion

    }
}