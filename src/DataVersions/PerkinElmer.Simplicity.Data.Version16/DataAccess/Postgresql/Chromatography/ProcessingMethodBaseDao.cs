using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography
{
	internal abstract class ProcessingMethodBaseDao
	{
		protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string TableName { get; } = "ProcessingMethod";
		public static string IdColumn { get; } = "Id";
		public static string IsDefaultColumn { get; } = "IsDefault";
		public static string NameColumn { get; } = "Name";
        public static string VersionNumberColumn { get; } = "VersionNumber";
        public static string GuidColumn { get; } = "Guid";
		public static string ModifiedDateColumn { get; } = "ModifiedDate";
		public static string CreatedDateColumn { get; } = "CreatedDate";
		public static string CreatedUserIdColumn { get; } = "CreatedUserId";
		public static string ModifiedUserIdColumn { get; } = "ModifiedUserId";
        internal static string CreatedUserNameColumn { get; } = "CreatedUserName";
        internal static string ModifiedUserNameColumn { get; } = "ModifiedUserName";
        public static string NumberOfLevelsColumn { get; } = "NumberOfLevels";
		public static string AmountUnitsColumn { get; } = "AmountUnits";
		public static string UnidentifiedPeakCalibrationTypeColumn { get; } = "UnidentifiedPeakCalibrationType";
		public static string UnidentifiedPeakCalibrationFactorColumn { get; } = "UnidentifiedPeakCalibrationFactor";
		public static string UnidentifiedPeakReferenceCompoundGuidColumn { get; } = "UnidentifiedPeakReferenceCompoundGuid";
		public static string ModifiedFromOriginalColumn { get; } = "ModifiedFromOriginal";
		public static string OriginalReadOnlyMethodGuidColumn { get; } = "OriginalReadOnlyMethodGuid";
		public static string DescriptionColumn { get; } = "Description";
        public static string ReviewApproveStateColumn { get; } = "ReviewApproveState";

        protected readonly string insertSql = $"INSERT INTO {TableName} " +
											  $"({NameColumn}," +
                                              $"{VersionNumberColumn}," +
                                              $"{IsDefaultColumn}," +
											  $"{GuidColumn}," +
											  $"{CreatedDateColumn}," +
											  $"{CreatedUserIdColumn}," +
											  $"{ModifiedDateColumn}," +
											  $"{ModifiedUserIdColumn}," +
                                              $"{CreatedUserNameColumn}," +
                                              $"{ModifiedUserNameColumn}," +
                                              $"{ModifiedFromOriginalColumn}," +
											  $"{OriginalReadOnlyMethodGuidColumn}," +
											  $"{NumberOfLevelsColumn}," +
											  $"{AmountUnitsColumn}," +
											  $"{UnidentifiedPeakCalibrationTypeColumn}," +
											  $"{UnidentifiedPeakCalibrationFactorColumn}," +
											  $"{DescriptionColumn}," +
											  $"{ReviewApproveStateColumn}," +
											  $"{UnidentifiedPeakReferenceCompoundGuidColumn})" +
											  "VALUES " +
											  $"(@{NameColumn}," +
                                              $"@{VersionNumberColumn}," +
                                              $"@{IsDefaultColumn}," +
											  $"@{GuidColumn}," +
											  $"@{CreatedDateColumn}," +
											  $"@{CreatedUserIdColumn}," +
											  $"@{ModifiedDateColumn}," +
											  $"@{ModifiedUserIdColumn}," +
                                              $"@{CreatedUserNameColumn}," +
                                              $"@{ModifiedUserNameColumn}," +
                                              $"@{ModifiedFromOriginalColumn}," +
											  $"@{OriginalReadOnlyMethodGuidColumn}," +
											  $"@{NumberOfLevelsColumn}," +
											  $"@{AmountUnitsColumn}," +
											  $"@{UnidentifiedPeakCalibrationTypeColumn}," +
											  $"@{UnidentifiedPeakCalibrationFactorColumn}," +
											  $"@{DescriptionColumn}," +
											  $"@{ReviewApproveStateColumn}," +
                                              $"@{UnidentifiedPeakReferenceCompoundGuidColumn}) ";

		protected readonly string selectSql = "SELECT " +
											  $"{TableName}.{IdColumn}," +
											  $"{TableName}.{IsDefaultColumn}," +
											  $"{TableName}.{GuidColumn}," +
											  $"{TableName}.{NameColumn}," +
                                              $"{TableName}.{VersionNumberColumn}," +
                                              $"{TableName}.{CreatedDateColumn}," +
											  $"{TableName}.{CreatedUserIdColumn}," +
											  $"{TableName}.{ModifiedDateColumn}," +
											  $"{TableName}.{ModifiedUserIdColumn}," +
                                              $"{TableName}.{CreatedUserNameColumn}," +
                                              $"{TableName}.{ModifiedUserNameColumn}," +
                                              $"{TableName}.{ModifiedFromOriginalColumn}," +
											  $"{TableName}.{OriginalReadOnlyMethodGuidColumn}," +
											  $"{TableName}.{NumberOfLevelsColumn}," +
											  $"{TableName}.{AmountUnitsColumn}," +
											  $"{TableName}.{UnidentifiedPeakCalibrationTypeColumn}," +
											  $"{TableName}.{UnidentifiedPeakCalibrationFactorColumn}," +
											  $"{TableName}.{DescriptionColumn}," +
											  $"{TableName}.{ReviewApproveStateColumn}," +
                                              $"{TableName}.{UnidentifiedPeakReferenceCompoundGuidColumn} ";

		protected readonly string updateSql = $"UPDATE {TableName} SET " +
                                              $"{NameColumn} = @{NameColumn}," +
                                              $"{VersionNumberColumn} = @{VersionNumberColumn}," +
                                              $"{IsDefaultColumn} = @{IsDefaultColumn}," +
                                              $"{GuidColumn} = @{GuidColumn}," +
                                              $"{CreatedDateColumn} = @{CreatedDateColumn}," +
                                              $"{CreatedUserIdColumn} = @{CreatedUserIdColumn}," +
                                              $"{ModifiedDateColumn} = @{ModifiedDateColumn}," +
                                              $"{ModifiedUserIdColumn} = @{ModifiedUserIdColumn}," +
                                              $"{CreatedUserNameColumn} = @{CreatedUserNameColumn}," +
                                              $"{ModifiedUserNameColumn} = @{ModifiedUserNameColumn}," +
                                              $"{ModifiedFromOriginalColumn} = @{ModifiedFromOriginalColumn}," +
                                              $"{OriginalReadOnlyMethodGuidColumn} = @{OriginalReadOnlyMethodGuidColumn}," +
                                              $"{NumberOfLevelsColumn} = @{NumberOfLevelsColumn}," +
                                              $"{AmountUnitsColumn} = @{AmountUnitsColumn}," +
                                              $"{UnidentifiedPeakCalibrationTypeColumn} = @{UnidentifiedPeakCalibrationTypeColumn}," +
                                              $"{UnidentifiedPeakCalibrationFactorColumn} = @{UnidentifiedPeakCalibrationFactorColumn}," +
                                              $"{DescriptionColumn} = @{DescriptionColumn}," +
                                              $"{ReviewApproveStateColumn} = @{ReviewApproveStateColumn}," +
                                              $"{UnidentifiedPeakReferenceCompoundGuidColumn} = @{UnidentifiedPeakReferenceCompoundGuidColumn} ";


		public virtual void Create(IDbConnection connection, ProcessingMethod processingMethod)
		{
			try
			{
				processingMethod.Id = connection.ExecuteScalar<long>($"{insertSql} RETURNING {IdColumn}", processingMethod);
				//Save ProcessingDeviceMethod
				foreach (var processingDeviceMethod in processingMethod.ProcessingDeviceMethods)
				{
					processingDeviceMethod.ProcessingMethodId = processingMethod.Id;
					SaveProcessingDeviceMethod(connection, processingDeviceMethod);
				}
				CreateChildren(connection, processingMethod);
			}
			catch (Exception ex)
			{
				Log.Error("Error in Create method", ex);
				throw;
			}
		}

		protected void SaveProcessingDeviceMethod(IDbConnection connection, ProcessingDeviceMethod processingDeviceMethod)
		{
			ProcessingDeviceMethodDao processingDeviceMethodDao = new ProcessingDeviceMethodDao();
			processingDeviceMethodDao.Create(connection, processingDeviceMethod);
		}

		protected void CreateChildren(IDbConnection connection, ProcessingMethod processingMethod)
		{
			var suitabilityParametersDao = new SuitabilityParametersDao();
			var channelMethodDao = new ChannelMethodDao();
			var spectrumMethodDao = new SpectrumMethodDao();
			var pdaWavelengthMaxParametersDao = new PdaWavelengthMaxParametersDao();
			var pdaPeakPurityParametersDao = new PdaPeakPurityParametersDao();
			var integrationEventDao = new IntegrationEventDao();
			var compoundDao = new CompoundDao();
			var levelAmountsDao = new LevelAmountsDao();
			var compoundGuidsDao = new CompoundGuidsDao();
			var compoundCalibrationResultsDao = new CompoundCalibrationResultsDao();
			var pointResponseBatchResultDao = new CalibrationPointResponseDao();
			var compCalibResultCoefficientDao = new CompCalibResultCoefficientDao();
			var invalidAmountsDao = new InvalidAmountsDao();
			var pdaAbsorbanceRatioParametersDao = new PdaAbsorbanceRatioParametersDao();
			var pdaBaselineCorrectionParametersDao = new PdaBaselineCorrectionParametersDao();
			var pdaStandardConfirmationParametersDao = new PdaStandardConfirmationParametersDao();
			var pdaApexOptimizedParametersDao = new PdaApexOptimizedParametersDao();
			var pdaLibrarySearchParametersDao = new PdaLibrarySearchParametersDao();
			var pdaLibrarySearchSelectedLibrariesDao = new PdaLibrarySearchSelectedLibrariesDao();
			var pdaLibraryConfirmationParametersDao = new PdaLibraryConfirmationParametersDao();
			var pdaLibraryConfirmationSelectedLibrariesDao = new PdaLibraryConfirmationSelectedLibrariesDao();
			var calibrationBatchRunInfoDao = new CalibrationBatchRunInfoDao();
			var suitabilityMethodDao = new SuitabilityMethodDao();

			// PdaApexOptimizedParameters
			if (processingMethod.PdaApexOptimizedParameters != null)
			{
				processingMethod.PdaApexOptimizedParameters.ProcessingMethodId = processingMethod.Id;
				pdaApexOptimizedParametersDao.Create(connection, processingMethod.PdaApexOptimizedParameters);
			}

			if (processingMethod.CalibrationBatchRunInfos != null)
			{
				processingMethod.CalibrationBatchRunInfos.ForEach(c => c.ProcessingMethodId = processingMethod.Id);
				calibrationBatchRunInfoDao.Create(connection, processingMethod.CalibrationBatchRunInfos);
			}

			if (processingMethod.SuitabilityMethod != null)
			{
				processingMethod.SuitabilityMethod.ProcessingMethodId = processingMethod.Id;
				suitabilityMethodDao.Create(connection, processingMethod.SuitabilityMethod);
			}

			// ChannelMethod
			foreach (var channelMethod in processingMethod.ChannelMethods)
			{
				channelMethod.ProcessingMethodId = processingMethod.Id;
				channelMethodDao.Create(connection, channelMethod);

				foreach (var integrationEvent in channelMethod.IntegrationEvents)
				{
					integrationEvent.ChannelMethodId = channelMethod.Id;
					integrationEventDao.Create(connection, integrationEvent);
				}

				if (channelMethod.PdaPeakPurityParameters != null)
				{
					channelMethod.PdaPeakPurityParameters.ChannelMethodId = channelMethod.Id;
					pdaPeakPurityParametersDao.Create(connection, channelMethod.PdaPeakPurityParameters);
				}

				if (channelMethod.PdaWavelengthMaxParameters != null)
				{
					channelMethod.PdaWavelengthMaxParameters.ChannelMethodId = channelMethod.Id;
					pdaWavelengthMaxParametersDao.Create(connection, channelMethod.PdaWavelengthMaxParameters);
				}

				if (channelMethod.PdaAbsorbanceRatioParameters != null)
				{
					channelMethod.PdaAbsorbanceRatioParameters.ChannelMethodId = channelMethod.Id;
					pdaAbsorbanceRatioParametersDao.Create(connection, channelMethod.PdaAbsorbanceRatioParameters);
				}

				if (channelMethod.PdaBaselineCorrectionParameters != null)
				{
					channelMethod.PdaBaselineCorrectionParameters.ChannelMethodId = channelMethod.Id;
					pdaBaselineCorrectionParametersDao.Create(connection, channelMethod.PdaBaselineCorrectionParameters);
				}

				if (channelMethod.PdaStandardConfirmationParameters != null)
				{
					channelMethod.PdaStandardConfirmationParameters.ChannelMethodId = channelMethod.Id;
					pdaStandardConfirmationParametersDao.Create(connection, channelMethod.PdaStandardConfirmationParameters);
				}

				if (channelMethod.PdaLibrarySearchParameters != null)
				{
					channelMethod.PdaLibrarySearchParameters.ChannelMethodId = channelMethod.Id;
					pdaLibrarySearchParametersDao.Create(connection, channelMethod.PdaLibrarySearchParameters);
					channelMethod.PdaLibrarySearchParameters.SelectedLibraries.ForEach(c => c.PdaLibrarySearchParameterId = channelMethod.PdaLibrarySearchParameters.Id);
					pdaLibrarySearchSelectedLibrariesDao.Create(connection, channelMethod.PdaLibrarySearchParameters.SelectedLibraries);
				}

                if (channelMethod.PdaLibraryConfirmationParameters != null)
                {
                    channelMethod.PdaLibraryConfirmationParameters.ChannelMethodId = channelMethod.Id;
                    pdaLibraryConfirmationParametersDao.Create(connection, channelMethod.PdaLibraryConfirmationParameters);
                    channelMethod.PdaLibraryConfirmationParameters.SelectedLibraries.ForEach(c => c.PdaLibraryConfirmationParameterId = channelMethod.PdaLibraryConfirmationParameters.Id);
                    pdaLibraryConfirmationSelectedLibrariesDao.Create(connection, channelMethod.PdaLibraryConfirmationParameters.SelectedLibraries);
				}

				//if (channelMethod.SuitabilityParameters != null)
				//{
				//	channelMethod.SuitabilityParameters.ChannelMethodId = channelMethod.Id;
				//	suitabilityParametersDao.Create(connection, channelMethod.SuitabilityParameters);
				//}

			}

			// Compound
			foreach (var compound in processingMethod.Compounds)
			{
				compound.ProcessingMethodId = processingMethod.Id;
				compoundDao.Create(connection, compound);

				// CompoundGuids
				compound.CompoundGuids.ForEach(c => c.CompoundId = compound.Id);
				compoundGuidsDao.Create(connection, compound.CompoundGuids);

				foreach (var levelAmount in compound.LevelAmounts)
				{
					levelAmount.CompoundId = compound.Id;
				}

				levelAmountsDao.Create(connection, compound.LevelAmounts);
			}

			if (processingMethod.SpectrumMethods != null)
			{
				foreach (var spectrumMethod in processingMethod.SpectrumMethods)
				{
					spectrumMethod.ProcessingMethodId = processingMethod.Id;
					spectrumMethodDao.Create(connection, spectrumMethod);
				}
			}

			// CompCalibResults
			processingMethod.CompoundCalibrationResults.ForEach(c => c.ProcessingMethodId = processingMethod.Id);
			compoundCalibrationResultsDao.Create(connection, processingMethod.CompoundCalibrationResults);

			foreach (var compoundCalibrationResults in processingMethod.CompoundCalibrationResults)
			{
				compoundCalibrationResults.CalibrationPointResponses.ForEach(c => c.CompoundCalibrationResultsId = compoundCalibrationResults.Id);
				pointResponseBatchResultDao.Create(connection, compoundCalibrationResults.CalibrationPointResponses);

				compoundCalibrationResults.CompCalibResultCoefficients.ForEach(c => c.CompoundCalibrationResultsId = compoundCalibrationResults.Id);
				compCalibResultCoefficientDao.Create(connection, compoundCalibrationResults.CompCalibResultCoefficients);

				compoundCalibrationResults.InvalidAmounts.ForEach(c => c.CompoundCalibrationResultsId = compoundCalibrationResults.Id);
				invalidAmountsDao.Create(connection, compoundCalibrationResults.InvalidAmounts);
			}
		}

		public void UpdateChildren(IDbConnection connection, ProcessingMethod processingMethod)
		{
			var channelMethodDao = new ChannelMethodDao();
			channelMethodDao.Delete(connection, processingMethod.Id);

			var spectrumMethodDao = new SpectrumMethodDao();
			spectrumMethodDao.Delete(connection, processingMethod.Id);

			var compoundDao = new CompoundDao();
			compoundDao.Delete(connection, processingMethod.Id);

			var compoundCalibrationResultsDao = new CompoundCalibrationResultsDao();
			compoundCalibrationResultsDao.Delete(connection, processingMethod.Id);

		    var pdaApexOptimizedParametersDao = new PdaApexOptimizedParametersDao();
		    pdaApexOptimizedParametersDao.Delete(connection,processingMethod.Id);

		    var calibrationBatchRunInfoDao = new CalibrationBatchRunInfoDao();
			calibrationBatchRunInfoDao.Delete(connection, processingMethod.Id);

            var suitabilityMethodDao = new SuitabilityMethodDao();
            suitabilityMethodDao.Delete(connection, processingMethod.Id);

            CreateChildren(connection, processingMethod);
		}
		protected ProcessingMethod GetProcessingMethodChildren(IDbConnection connection, ProcessingMethod processingMethod)
		{
			if (processingMethod != null)
			{
				var suitabilityParametersDao = new SuitabilityParametersDao();
				var channelMethodDao = new ChannelMethodDao();
				var spectrumMethodDao = new SpectrumMethodDao();
				var integrationEventDao = new IntegrationEventDao();
				var pdaWavelengthMaxParametersDao = new PdaWavelengthMaxParametersDao();
				var pdaPeakPurityParametersDao = new PdaPeakPurityParametersDao();
				var compoundDao = new CompoundDao();
				var levelAmountsDao = new LevelAmountsDao();
				var compoundGuidsDao = new CompoundGuidsDao();
				var compoundCalibrationResultsDao = new CompoundCalibrationResultsDao();
				var calibrationPointResponseDao = new CalibrationPointResponseDao();
				var calibResultCoefficientDao = new CompCalibResultCoefficientDao();
				var invalidAmountsDao = new InvalidAmountsDao();
				var pdaAbsorbanceRatioParametersDao = new PdaAbsorbanceRatioParametersDao();
				var pdaBaselineCorrectionParametersDao = new PdaBaselineCorrectionParametersDao();
				var pdaStandardConfirmationParametersDao = new PdaStandardConfirmationParametersDao();
				var pdaApexOptimizedParametersDao = new PdaApexOptimizedParametersDao();
				var pdaLibrarySearchParametersDao = new PdaLibrarySearchParametersDao();
				var pdaLibrarySearchSelectedLibrariesDao = new PdaLibrarySearchSelectedLibrariesDao();
				var pdaLibraryConfirmationParametersDao = new PdaLibraryConfirmationParametersDao();
				var pdaLibraryConfirmationSelectedLibrariesDao = new PdaLibraryConfirmationSelectedLibrariesDao();
				var calibrationBatchRunInfoDao = new CalibrationBatchRunInfoDao();
				var processingDeviceMethodDao = new ProcessingDeviceMethodDao();
				var suitabilityMethodDao = new SuitabilityMethodDao();

				processingMethod.ProcessingDeviceMethods =
					processingDeviceMethodDao.GetProcessingDeviceMethods(connection, processingMethod.Id);

				// ChannelMethods
				processingMethod.ChannelMethods = channelMethodDao.GetChannelMethods(connection, processingMethod.Id);

				processingMethod.SpectrumMethods = spectrumMethodDao.GetSpectrumMethods(connection, processingMethod.Id);

				processingMethod.PdaApexOptimizedParameters = pdaApexOptimizedParametersDao.GetByProcessingMethodId(connection, processingMethod.Id);

				processingMethod.CalibrationBatchRunInfos =
					calibrationBatchRunInfoDao.Get(connection, processingMethod.Id);

				processingMethod.SuitabilityMethod = suitabilityMethodDao.Get(connection, processingMethod.Id);

				foreach (var channelMethod in processingMethod.ChannelMethods)
				{
					channelMethod.IntegrationEvents = integrationEventDao.GetIntegrationEventsByChannelMethodId(connection, channelMethod.Id);
					channelMethod.PdaPeakPurityParameters =
						pdaPeakPurityParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					channelMethod.PdaWavelengthMaxParameters =
						pdaWavelengthMaxParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					channelMethod.PdaAbsorbanceRatioParameters =
						pdaAbsorbanceRatioParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					channelMethod.PdaBaselineCorrectionParameters =
						pdaBaselineCorrectionParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					channelMethod.PdaStandardConfirmationParameters =
						pdaStandardConfirmationParametersDao.GetByChannelMethodId(connection, channelMethod.Id);

                    channelMethod.PdaLibraryConfirmationParameters = pdaLibraryConfirmationParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					if (channelMethod.PdaLibraryConfirmationParameters != null)
					{
						channelMethod.PdaLibraryConfirmationParameters.SelectedLibraries =
							pdaLibraryConfirmationSelectedLibrariesDao.GetByPdaLibraryConfirmationParameterId(connection,
								channelMethod.PdaLibraryConfirmationParameters.Id);
					}

					channelMethod.PdaLibrarySearchParameters = pdaLibrarySearchParametersDao.GetByChannelMethodId(connection, channelMethod.Id);
					if (channelMethod.PdaLibrarySearchParameters != null)
					{
						channelMethod.PdaLibrarySearchParameters.SelectedLibraries =
							pdaLibrarySearchSelectedLibrariesDao.GetByPdaLibrarySearchParameterId(connection,
								channelMethod.PdaLibrarySearchParameters.Id);
					}

				//	channelMethod.SuitabilityParameters = suitabilityParametersDao.GetSuitabilityParametersByChannelMethodId(connection, channelMethod.Id);
				}

				// Compound
				processingMethod.Compounds = compoundDao.GetCompoundsByProcessingMethodId(connection, processingMethod.Id);


				foreach (var compound in processingMethod.Compounds)
				{
					compound.LevelAmounts = levelAmountsDao.GetLevelAmountsByCompoundId(connection, compound.Id);
					compound.CompoundGuids = compoundGuidsDao.GetCompondGuidsByCompoundId(connection, compound.Id);
				}

				// CompoundCalibrationResults
				processingMethod.CompoundCalibrationResults = compoundCalibrationResultsDao.GetCompoundCalibrationResultsByProcessingMethodId(connection, processingMethod.Id).ToList();

				foreach (var compoundCalibrationResults in processingMethod.CompoundCalibrationResults)
				{
					// CalibrationPointResponse
					compoundCalibrationResults.CalibrationPointResponses =
						calibrationPointResponseDao.GetCalibrationPointResponseByCompoundCalibrationResultId(connection, compoundCalibrationResults.Id).ToList();

					// CompCalibResultsCoeff
					compoundCalibrationResults.CompCalibResultCoefficients = calibResultCoefficientDao.GetCompCalibResultCoeff(connection, compoundCalibrationResults.Id);

					// InvalidAmounts
					compoundCalibrationResults.InvalidAmounts = invalidAmountsDao.GetInvalidAmounts(connection, compoundCalibrationResults.Id);
				}
			}

			return processingMethod;
		}
	}
}
