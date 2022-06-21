namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
    public enum CompoundAssignmentType
    {
        Automatic, //by algorithm
        ManuallyAssigned, //by user
        AffectedByManualAssignment //affected by user assignment of another compound to another peak
    }
}