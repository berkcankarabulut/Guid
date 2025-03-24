using System.Collections.Generic;

namespace Project.Save.Runtime
{
    public struct SaveGameEvent {}
    public struct RequestSaveablesEvent 
    {
        public List<SaveableEntity> Saveables;
    }
}