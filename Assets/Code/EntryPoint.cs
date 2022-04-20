using System;
using Code.Managers;
using UnityEngine;

namespace Code
{
    public class EntryPoint : MonoBehaviour
    {
        #region Fields
        private ManagerDirectory 			_managerDirectory;
        #endregion
		
        #region Unity Methods

        private void Start()
        {
            _managerDirectory 				= new ManagerDirectory();
        }

        private void Update()
        {
            _managerDirectory.UpdateUpdatableManagers();
        }

        private void LateUpdate()
        {
            _managerDirectory.UpdateLateUpdatableManagers();
        }

        private void FixedUpdate()
        {
            _managerDirectory?.UpdateFixedUpdatableManagers();
        }

        private void OnApplicationQuit()
        {
        }
        
        #endregion
    }
}