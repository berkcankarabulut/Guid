namespace Project.Utility.Runtime.Save
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}