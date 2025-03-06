using System;
using System.Collections.Generic; 

namespace Project.Utility.Runtime
{
    public static class EventBusExtensions
    { 
        public static TResult RaiseSingle<T, TResult>() where T : struct
        {
            List<TResult> results = EventBus.Instance.RaiseWithResults<T, TResult>();
            return results.Count > 0 ? results[0] : default;
        }
         
        public static TResult RaiseSingle<T, TParam, TResult>(TParam param) where T : struct
        {
            List<TResult> results = EventBus.Instance.RaiseWithResults<T, TParam, TResult>(param);
            return results.Count > 0 ? results[0] : default;
        }
         
        public static TResult RaiseAggregate<T, TResult>() where T : struct
        {
            List<TResult> results = EventBus.Instance.RaiseWithResults<T, TResult>();
            if (results.Count == 0)
                return default;
                
            if (typeof(TResult) == typeof(int))
            {
                int sum = 0;
                foreach (var result in results)
                {
                    sum += Convert.ToInt32(result);
                }
                return (TResult)(object)sum;
            }
            else if (typeof(TResult) == typeof(float))
            {
                float sum = 0;
                foreach (var result in results)
                {
                    sum += Convert.ToSingle(result);
                }
                return (TResult)(object)sum;
            }
            else if (typeof(TResult) == typeof(bool))
            {
                bool any = false;
                foreach (var result in results)
                {
                    any |= Convert.ToBoolean(result);
                }
                return (TResult)(object)any;
            }
            else if (typeof(TResult) == typeof(string))
            {
                string combined = "";
                foreach (var result in results)
                {
                    combined += result.ToString();
                }
                return (TResult)(object)combined;
            }
             
            return results[0];
        }
         
        public static void CreateEvent<T>() where T : struct
        { 
        }
    }
}