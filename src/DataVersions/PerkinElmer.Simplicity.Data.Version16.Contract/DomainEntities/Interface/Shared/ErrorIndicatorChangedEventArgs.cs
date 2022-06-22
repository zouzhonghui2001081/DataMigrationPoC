namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public class ErrorIndicatorChangedEventArgs
    {
        public ErrorIndicatorChangedEventArgs(IErrorIndicatorDetails errorIndicatorDetails)
        {
            ErrorIndicatorDetails = errorIndicatorDetails;
        }

        public IErrorIndicatorDetails ErrorIndicatorDetails{ get; }
    }
}