using System.IO;
using Code.Utilities.FileSystem;
using UnityEngine;

namespace Code.View
{
    public class GlobalUser : MonoBehaviour
    {
        #region Fields
        [SerializeField] private SupportedUserTypes 	_userType;
        private GameObject 								_loadedUser;
        private Camera                                  _camera;
        #endregion
		
        #region Unity Methods
        private void Awake()
        {
            switch (_userType)
            {
                case SupportedUserTypes.Standard:
                    LoadStandardUser();
                    break;
            }

            GetGlobalCamera();
        }

        #endregion


        #region Properties

        #endregion

        #region Methods

        public Camera GetCamera()
        {
            return _camera;
        }
        
        #endregion

        #region Implementation

        private void GetGlobalCamera()
        {
            _camera = _loadedUser.GetComponent<Camera>();
        }   

        private void LoadStandardUser()
        {
            var user 		= Resources.Load<GameObject>(
                Path.Combine(FilePaths.PrefabDirectories.USERS, FileNames.Prefabs.GlobalStandardPlayer));
            _loadedUser 		= Instantiate(user, transform);
        }
        
        #endregion

        
    }
}