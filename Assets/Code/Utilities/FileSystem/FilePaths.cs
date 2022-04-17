using System.IO;

namespace Code.Utilities.FileSystem
{
    public static class FilePaths
    {
        public static class PrefabDirectories
        {
            private const string 				ROOT 						= "Prefabs";
            public static readonly string 		NAVIGATION 					= Path.Combine(ROOT, "Navigation");
            public static readonly string 		UI 							= Path.Combine(ROOT, "UI");
            public static readonly string 		USERS 						= Path.Combine(ROOT, "Users");
            public static readonly string 		SYSTEMS 					= Path.Combine(ROOT, "Systems");
        }
        
        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Implementation

        #endregion

        
    }
}