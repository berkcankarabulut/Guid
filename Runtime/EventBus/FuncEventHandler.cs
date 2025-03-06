using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Utility.Runtime
{
    public class FuncEventHandler
    {
        private Dictionary<Type, object> _eventDictionary;
        private readonly Type _keyType = typeof(KeyValuePair<string, Type>);
        
        public FuncEventHandler(Dictionary<Type, object> eventDictionary)
        {
            _eventDictionary = eventDictionary;
        }
        
        public void Subscribe<T, TResult>(Func<TResult> handler) where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_Func";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            
            if (!_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                _eventDictionary[_keyType] = new Dictionary<KeyValuePair<string, Type>, object>();
            }
            
            var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)_eventDictionary[_keyType];
            
            if (!funcDict.TryGetValue(dictKey, out object funcHandler))
            {
                funcDict[dictKey] = handler;
            }
            else
            {
                // You can't combine Func delegates directly with + operator like Action
                // We'll store them in a list
                if (funcHandler is List<Func<TResult>> funcList)
                {
                    funcList.Add(handler);
                }
                else if (funcHandler is Func<TResult> existingFunc)
                {
                    var newList = new List<Func<TResult>> { existingFunc, handler };
                    funcDict[dictKey] = newList;
                }
            }
        }
        
        public void Subscribe<T, TParam, TResult>(Func<TParam, TResult> handler) where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_FuncParam";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            
            if (!_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                _eventDictionary[_keyType] = new Dictionary<KeyValuePair<string, Type>, object>();
            }
            
            var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)_eventDictionary[_keyType];
            
            if (!funcDict.TryGetValue(dictKey, out object funcHandler))
            {
                funcDict[dictKey] = handler;
            }
            else
            {
                if (funcHandler is List<Func<TParam, TResult>> funcList)
                {
                    funcList.Add(handler);
                }
                else if (funcHandler is Func<TParam, TResult> existingFunc)
                {
                    var newList = new List<Func<TParam, TResult>> { existingFunc, handler };
                    funcDict[dictKey] = newList;
                }
            }
        }
        
        public void Unsubscribe<T, TResult>(Func<TResult> handler) where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_Func";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            
            if (_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)eventHandler;
                
                if (funcDict.TryGetValue(dictKey, out object funcHandler))
                {
                    if (funcHandler is List<Func<TResult>> funcList)
                    {
                        funcList.Remove(handler);
                        if (funcList.Count == 0)
                        {
                            funcDict.Remove(dictKey);
                        }
                    }
                    else if (funcHandler is Func<TResult> existingFunc && existingFunc.Equals(handler))
                    {
                        funcDict.Remove(dictKey);
                    }
                    
                    if (funcDict.Count == 0)
                    {
                        _eventDictionary.Remove(_keyType);
                    }
                }
            }
        }
        
        public void Unsubscribe<T, TParam, TResult>(Func<TParam, TResult> handler) where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_FuncParam";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            
            if (_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)eventHandler;
                
                if (funcDict.TryGetValue(dictKey, out object funcHandler))
                {
                    if (funcHandler is List<Func<TParam, TResult>> funcList)
                    {
                        funcList.Remove(handler);
                        if (funcList.Count == 0)
                        {
                            funcDict.Remove(dictKey);
                        }
                    }
                    else if (funcHandler is Func<TParam, TResult> existingFunc && existingFunc.Equals(handler))
                    {
                        funcDict.Remove(dictKey);
                    }
                    
                    if (funcDict.Count == 0)
                    {
                        _eventDictionary.Remove(_keyType);
                    }
                }
            }
        }
        
        public List<TResult> RaiseWithResults<T, TResult>() where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_Func";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            var results = new List<TResult>();
            
            if (_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)eventHandler;
                
                if (funcDict.TryGetValue(dictKey, out object funcHandler))
                {
                    try
                    {
                        if (funcHandler is List<Func<TResult>> funcList)
                        {
                            foreach (var func in funcList)
                            {
                                results.Add(func());
                            }
                        }
                        else if (funcHandler is Func<TResult> singleFunc)
                        {
                            results.Add(singleFunc());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[EventBus] Event handler hatası (Func<{typeof(TResult).Name}>): {e.Message}\n{e.StackTrace}");
                    }
                }
            }
            
            return results;
        }
        
        public List<TResult> RaiseWithResults<T, TParam, TResult>(TParam param) where T : struct
        {
            Type eventType = typeof(T);
            string key = $"{eventType.FullName}_FuncParam";
            var dictKey = new KeyValuePair<string, Type>(key, typeof(TResult));
            var results = new List<TResult>();
            
            if (_eventDictionary.TryGetValue(_keyType, out object eventHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)eventHandler;
                
                if (funcDict.TryGetValue(dictKey, out object funcHandler))
                {
                    try
                    {
                        if (funcHandler is List<Func<TParam, TResult>> funcList)
                        {
                            foreach (var func in funcList)
                            {
                                results.Add(func(param));
                            }
                        }
                        else if (funcHandler is Func<TParam, TResult> singleFunc)
                        {
                            results.Add(singleFunc(param));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[EventBus] Event handler hatası (Func<{typeof(TParam).Name}, {typeof(TResult).Name}>): {e.Message}\n{e.StackTrace}");
                    }
                }
            }
            
            return results;
        }
        
        public void ClearSubscriptions<T>() where T : struct
        {
            Type eventType = typeof(T);
            
            // Func abonelikleri için temizleme
            if (_eventDictionary.TryGetValue(_keyType, out object dictHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)dictHandler;
                string keyBase = $"{eventType.FullName}_";
                
                // Remove all keys that start with our event type
                var keysToRemove = funcDict.Keys
                    .Where(k => k.Key.StartsWith(keyBase))
                    .ToList();
                    
                foreach (var key in keysToRemove)
                {
                    funcDict.Remove(key);
                }
                
                if (funcDict.Count == 0)
                {
                    _eventDictionary.Remove(_keyType);
                }
                
                if (keysToRemove.Count > 0)
                {
                    Debug.Log($"[EventBus] {typeof(T).Name} için Func abonelikleri temizlendi.");
                }
            }
        }
        
        public int GetSubscriberCount<T>() where T : struct
        {
            int count = 0;
            Type eventType = typeof(T);
            
            // Count Func subscribers
            if (_eventDictionary.TryGetValue(_keyType, out object dictHandler))
            {
                var funcDict = (Dictionary<KeyValuePair<string, Type>, object>)dictHandler;
                string keyBase = $"{eventType.FullName}_";
                
                foreach (var kv in funcDict)
                {
                    if (kv.Key.Key.StartsWith(keyBase))
                    {
                        if (kv.Value is IList<object> list)
                        {
                            count += list.Count;
                        }
                        else
                        {
                            count += 1; // Single delegate
                        }
                    }
                }
            }
            
            return count;
        }
    }
}