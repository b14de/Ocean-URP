using System.Collections.Generic;
using Code.Utilities;

namespace Code.ContextSystem
{
	public static class ContextMapper
	{
		#region Fields
		private static readonly Dictionary<ContextType, Context> 	ContextMap 		= new Dictionary<ContextType, Context>
		{
			{ContextType.Startup, 					new Startup()},
			{ContextType.MainMenu, 					new MainMenu()},
			{ContextType.Error, 					new Error()},
			{ContextType.LoadingGameWorld, 			new LoadingGameWorld()},
			{ContextType.InGame, 			        new InGame()}

		};
		#endregion

		#region Methods
		public static Context GetContextImplementation(ContextType contextType)
		{
			if (ContextMap.ContainsKey(contextType))
			{
				return ContextMap[contextType];
			}

			DebugLogger.LogError("Context requested not known by ContextMapper: " + contextType);
			return ContextMap[ContextType.Error];
		}
		#endregion
	}
}