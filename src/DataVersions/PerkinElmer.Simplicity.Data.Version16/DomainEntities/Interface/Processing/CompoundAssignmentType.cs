namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public enum CompoundAssignmentType
    {
        Automatic, //by algorithm
        ManuallyAssigned, //by user
        AffectedByManualAssignment //affected by user assignment of another compound to another peak
    }
}