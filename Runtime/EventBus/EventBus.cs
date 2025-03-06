using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility.Runtime
{
    public class EventBus : Singleton<EventBus>
    {  
        private Dictionary<Type, object> _eventDictionary = new Dictionary<Type, object>();
        private ActionEventHandler _actionHandler;
        private FuncEventHandler _funcHandler;
  
        protected override void Awake()
        { 
            base.Awake();
            _actionHandler = new ActionEventHandler(_eventDictionary);
            _funcHandler = new FuncEventHandler(_eventDictionary);
        } 
        
        #region Action Subscriptions (void return type)
        
        public void Subscribe<T>(Action handler) where T : struct
        {
            _actionHandler.Subscribe<T>(handler);
        }
 
        public void Subscribe<T>(Action<T> handler) where T : struct
        {
            _actionHandler.Subscribe<T>(handler);
        }
        
        public void Unsubscribe<T>(Action handler) where T : struct
        {
            _actionHandler.Unsubscribe<T>(handler);
        }
        
        public void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            _actionHandler.Unsubscribe<T>(handler);
        }
        
        public void Raise<T>() where T : struct
        {
            _actionHandler.Raise<T>();
        }
 
        public void Raise<T>(T eventData) where T : struct
        {
            _actionHandler.Raise<T>(eventData);
        }
        
        #endregion
        
        #region Func Subscriptions (with return type)
        
        public void Subscribe<T, TResult>(Func<TResult> handler) where T : struct
        {
            _funcHandler.Subscribe<T, TResult>(handler);
        }
        
        public void Subscribe<T, TParam, TResult>(Func<TParam, TResult> handler) where T : struct
        {
            _funcHandler.Subscribe<T, TParam, TResult>(handler);
        }
        
        public void Unsubscribe<T, TResult>(Func<TResult> handler) where T : struct
        {
            _funcHandler.Unsubscribe<T, TResult>(handler);
        }
        
        public void Unsubscribe<T, TParam, TResult>(Func<TParam, TResult> handler) where T : struct
        {
            _funcHandler.Unsubscribe<T, TParam, TResult>(handler);
        }
        
        public List<TResult> RaiseWithResults<T, TResult>() where T : struct
        {
            return _funcHandler.RaiseWithResults<T, TResult>();
        }
        
        public List<TResult> RaiseWithResults<T, TParam, TResult>(TParam param) where T : struct
        {
            return _funcHandler.RaiseWithResults<T, TParam, TResult>(param);
        }
        
        #endregion
 
        public void ClearAllSubscriptions()
        {
            int count = _eventDictionary.Count;
            _eventDictionary.Clear(); 
        }
         
        public void ClearSubscriptions<T>() where T : struct
        {
            _actionHandler.ClearSubscriptions<T>();
            _funcHandler.ClearSubscriptions<T>();
        }
         
        public int GetSubscriberCount<T>() where T : struct
        {
            int actionCount = _actionHandler.GetSubscriberCount<T>();
            int funcCount = _funcHandler.GetSubscriberCount<T>();
            return actionCount + funcCount;
        }
    }
}