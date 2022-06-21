namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
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