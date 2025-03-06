using System; 
using System.Collections.Generic; 

namespace Project.Utility.Runtime
{
    public class Notifier<T>
    {
        private List<Action<T>> observers = new List<Action<T>>();

        public void Subscribe(Action<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Unsubscribe(Action<T> observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void Notify(T data)
        {
            foreach (var observer in observers)
            {
                observer.Invoke(data);
            }
        }
    }
}
