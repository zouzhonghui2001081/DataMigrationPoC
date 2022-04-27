﻿namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface ICalibrationParameterError
    {
        string ParameterName { get; set; }

        //TODO: Convert string to enum ErrorCode
        string ErrorCode { get; set; }
    }
}
