using System.Collections.Generic;

namespace Code.Utilities
{
    public static class EventDataStore
    {
        #region Fields
        private static readonly Dictionary<EventType, object> 		eventData 		= new Dictionary<EventType, object>();
        #endregion
		
        #region Methods
        public static void AddData(EventType eventType, object data)
        {
            if (!eventData.ContainsKey(eventType))
            {
                eventData.Add(eventType, data);
            }
            else
            {
                DebugLogger.LogError("Transient data for " + eventType + " already exists");
            }
        }
		
        public static bool HasData(EventType eventType)
        {
            return eventData.ContainsKey(eventType);
        }

        public static T RemoveData<T>(EventType eventType)
        {
            if (HasData(eventType))
            {
                if (eventData[eventType] is T)
                {
                    var returnData 		= eventData[eventType];
                    eventData.Remove(eventType);
				
                    return (T)returnData;
                }
				
                DebugLogger.LogError("Data requested for " + eventType + " is not a " + typeof(T) + 
                                ". Found: " + eventData[eventType].GetType());
            }
            else
            {
                DebugLogger.LogError("No data in store for " + eventType);
            }
			
            return default;
        }
        #endregion
    }
}