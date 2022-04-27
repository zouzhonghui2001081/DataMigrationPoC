namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public enum InstrumentSystemState
    {
        Ready,
        NotReady,
        PreRun,
        Injection,
        Running,
        PostRun,
        CollectingData,
        Fault,
        Offline
    }
}