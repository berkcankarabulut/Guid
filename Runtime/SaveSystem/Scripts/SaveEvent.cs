using System.Collections.Generic;

namespace Project.Utility.Runtime.Save
{
    public struct SaveGameEvent {}
    public struct RequestSaveablesEvent 
    {
        public List<SaveableEntity> Saveables;
    }
}