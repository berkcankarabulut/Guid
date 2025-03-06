using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility.Runtime
{
    public class ActionEventHandler
    {
        private Dictionary<Type, object> _eventDictionary;
        
        public ActionEventHandler(Dictionary<Type, object> eventDictionary)
        {
            _eventDictionary = eventDictionary;
        }
        
        public void Subscribe<T>(Action handler) where T : struct
        {
            Type eventType = typeof(T);
            if (!_eventDictionary.TryGetValue(eventType, out object eventHandler))
            {
                _eventDictionary[eventType] = handler; 
            }
            else
            {
                _eventDictionary[eventType] = (Action)eventHandler + handler; 
            }
        }
 
        public void Subscribe<T>(Action<T> handler) where T : struct
        {
            Type eventType = typeof(T);
            if (!_eventDictionary.TryGetValue(eventType, out object eventHandler))
            {
                _eventDictionary[eventType] = handler; 
            }
            else
            {
                _eventDictionary[eventType] = (Action<T>)eventHandler + handler; 
            }
        }
        
        public void Unsubscribe<T>(Action handler) where T : struct
        {
            Type eventType = typeof(T);
            if (_eventDictionary.TryGetValue(eventType, out object eventHandler))
            {
                _eventDictionary[eventType] = (Action)eventHandler - handler;
                
                // Event handler boş ise dictionary'den kaldır
                if (_eventDictionary[eventType] == null)
                {
                    _eventDictionary.Remove(eventType); 
                } 
            }
        } 
        
        public void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            Type eventType = typeof(T);
            if (_eventDictionary.TryGetValue(eventType, out object eventHandler))
            {
                _eventDictionary[eventType] = (Action<T>)eventHandler - handler;
                
                // Event handler boş ise dictionary'den kaldır
                if (_eventDictionary[eventType] == null)
                {
                    _eventDictionary.Remove(eventType); 
                } 
            }
        }
        
        public void Raise<T>() where T : struct
        {
            Type eventType = typeof(T);
            if (_eventDictionary.TryGetValue(eventType, out object eventHandler))
            { 
                try
                {
                    ((Action)eventHandler)?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus] Event handler hatası: {e.Message}\n{e.StackTrace}");
                }
            } 
        }
 
        public void Raise<T>(T eventData) where T : struct
        {
            Type eventType = typeof(T);
            if (_eventDictionary.TryGetValue(eventType, out object eventHandler))
            { 
                try
                {
                    ((Action<T>)eventHandler)?.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus] Event handler hatası ({typeof(T).Name}): {e.Message}\n{e.StackTrace}");
                }
            } 
        }
        
        public void ClearSubscriptions<T>() where T : struct
        {
            Type eventType = typeof(T);
            if (_eventDictionary.Remove(eventType))
            {
                Debug.Log($"[EventBus] {typeof(T).Name} için Action abonelikleri temizlendi.");
            }
        }
        
        public int GetSubscriberCount<T>() where T : struct
        {
            Type eventType = typeof(T);
            if (!_eventDictionary.TryGetValue(eventType, out object eventHandler))
            {
                return 0;
            }
            
            if (eventHandler is Action action)
            {
                return action.GetInvocationList().Length;
            }
            else if (eventHandler is Action<T> actionWithParam)
            {
                return actionWithParam.GetInvocationList().Length;
            }
            
            return 0;
        }
    }
}