using System;
using System.Collections.Generic;
using Code.Utilities;

namespace Code.Managers
{
    public class EventManager : ICoreManager
    {
        #region Fields
        private Dictionary<EventType, Action> 		eventsMap;
        #endregion
		
        #region Methods
        public void Start()
        {
            DebugLogger.LogMessage("Started EventManager");
            eventsMap 		= new Dictionary<EventType, Action>();
        }

        public void RegisterListener(EventType eventType, Action action)
        {
            if (eventsMap.ContainsKey(eventType))
            {
                eventsMap[eventType] += action;
            }
            else
            {
                eventsMap.Add(eventType, action);
            }
        }

        public void Trigger(EventType eventType)
        {
            if (eventsMap.ContainsKey(eventType))
            {
                eventsMap[eventType]?.Invoke();
            }
        }

        public void DeregisterListener(EventType eventType, Action action)
        {
            if (eventsMap.ContainsKey(eventType))
            {
                eventsMap[eventType] -= action;
                RemoveEventIfNoListeners(eventType);
            }
        }
        #endregion
		
        #region Implementation
        private void RemoveEventIfNoListeners(EventType eventType)
        {
            if (eventsMap.ContainsKey(eventType))
            {
                if (eventsMap[eventType] == null)
                {
                    eventsMap.Remove(eventType);
                }
            }
        }
        #endregion
    }
}