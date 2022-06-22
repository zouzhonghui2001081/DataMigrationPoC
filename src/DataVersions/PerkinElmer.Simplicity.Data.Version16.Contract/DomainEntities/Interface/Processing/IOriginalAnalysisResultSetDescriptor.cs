using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IOriginalAnalysisResultSetDescriptor : IPersistable, IEquatable<IOriginalAnalysisResultSetDescriptor>
	{
		bool Imported { get; set; } // Based on BatchResultSet
		string SystemName { get; set; } // Dynamic based on BatchResultSet
		bool AutoProcessed { get; set; } // Tells if Queue or Import still busy with this result set
        string ReviewedBy { get; set; }
        DateTime? ReviewedTimeStamp { get; set; }
        string ApprovedBy { get; set; }
        DateTime? ApprovedTimeStamp { get; set; }
    }
}