﻿

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IStatusParameter
    {
        /// <summary>
        /// Parameter Id
        /// </summary>
        string ParameterId { get; }

        /// <summary>
        /// 
        /// </summary>
        string Unit { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsParameterInError { get; }
    }
}
