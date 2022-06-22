using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IErrorIndicator
    {
        IErrorIndicatorDetails ErrorIndicatorDetails { get; }
        event EventHandler<ErrorIndicatorChangedEventArgs> ErrorIndicatorChangedEvent;
    }
}