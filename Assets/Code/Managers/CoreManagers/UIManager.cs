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
            _contextualUIController = new ContextualUIController();
            
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
        		
        public void SetLoadingWorldText()
        {
            _contextualUIController.SetMessage("Loading World for " + ManagerDirectory.GameManager.CurrentGameDescription.Type + "Game Mode");
        }


        #endregion

        #region Implementation

        #endregion

    }
}