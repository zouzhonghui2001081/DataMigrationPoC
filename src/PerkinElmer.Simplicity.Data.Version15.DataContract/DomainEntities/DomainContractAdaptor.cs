using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.Spectral;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;
using AcquisitionMethod = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod.AcquisitionMethod;
using BatchResultSet = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.BatchResultSet;
using BatchRun = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.BatchRun;
using DeviceMethod = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod.DeviceMethod;
using IDeviceMethod = PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition.IDeviceMethod;
using ReviewApproveState = PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.ReviewApprove.ReviewApproveState;
using SampleType = PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition.SampleType;
using Sequence = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.Sequence;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities
{
	internal static class DomainContractAdaptor
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private const string ChromatographicChannelDescriptorType = "IChromatographicChannelDescriptor";

		internal static void PopulateBatchResultSetInfo(BatchResultSet batchResultSetEntity,
			IBatchResultSetInfo batchResultSetInfo)
		{
			if (batchResultSetInfo.CreatedByUser == null)
				batchResultSetInfo.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (batchResultSetInfo.ModifiedByUser == null)
				batchResultSetInfo.ModifiedByUser = DomainFactory.Create<IUserInfo>();
			try
			{
				if (batchResultSetEntity != null)
				{
					batchResultSetInfo.Guid = batchResultSetEntity.Guid;
					batchResultSetInfo.Name = batchResultSetEntity.Name;
					batchResultSetInfo.CreatedByUser.UserId = batchResultSetEntity.CreatedUserId;
					batchResultSetInfo.CreatedDateUtc = batchResultSetEntity.CreatedDate;
					batchResultSetInfo.ModifiedByUser.UserId = batchResultSetEntity.ModifiedUserId;
					batchResultSetInfo.ModifiedDateUtc = batchResultSetEntity.ModifiedDate;
					batchResultSetInfo.IsCompleted = batchResultSetEntity.IsCompleted;
					batchResultSetInfo.DataSourceType = (DataSourceType)batchResultSetEntity.DataSourceType;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateBatchResultSetInfo() method", ex);
				throw;
			}
		}

		internal static void PopulateOriginalAnalysisResultSetDescriptor(AnalysisResultSet analysisResultSetEntity,
			IOriginalAnalysisResultSetDescriptor originalAnalysisResultSetDescriptor, string systemName)
		{
			originalAnalysisResultSetDescriptor.AutoProcessed = analysisResultSetEntity.AutoProcessed;
			originalAnalysisResultSetDescriptor.Imported = analysisResultSetEntity.Imported;
			originalAnalysisResultSetDescriptor.Name = analysisResultSetEntity.Name;
            originalAnalysisResultSetDescriptor.SystemName = systemName;
			originalAnalysisResultSetDescriptor.Guid = analysisResultSetEntity.Guid;
			originalAnalysisResultSetDescriptor.CreatedDateUtc = analysisResultSetEntity.CreatedDate;
			var userInfo = DomainFactory.Create<IUserInfo>();
			userInfo.UserId = analysisResultSetEntity.CreatedUserId;
			userInfo.UserFullName = analysisResultSetEntity.CreatedUserName;
			originalAnalysisResultSetDescriptor.CreatedByUser = userInfo;
			originalAnalysisResultSetDescriptor.ModifiedDateUtc = analysisResultSetEntity.ModifiedDate;
			userInfo = DomainFactory.Create<IUserInfo>();
			userInfo.UserId = analysisResultSetEntity.ModifiedUserId;
			userInfo.UserFullName = analysisResultSetEntity.ModifiedUserName;
			originalAnalysisResultSetDescriptor.ModifiedByUser = userInfo;
            originalAnalysisResultSetDescriptor.ReviewedBy = analysisResultSetEntity.ReviewedBy;
            originalAnalysisResultSetDescriptor.ReviewedTimeStamp = analysisResultSetEntity.ReviewedTimeStamp?.ToLocalTime();
            originalAnalysisResultSetDescriptor.ApprovedBy = analysisResultSetEntity.ApprovedBy;
            originalAnalysisResultSetDescriptor.ReviewedTimeStamp = analysisResultSetEntity.ApprovedTimeStamp?.ToLocalTime();

        }
		internal static void PopulateAnalysisResultSetDescriptor(AnalysisResultSet analysisResultSetEntity, 
			IOriginalAnalysisResultSetDescriptor originalAnalysisResultSetDescriptor,
			IAnalysisResultSetDescriptor analysisResultSetDescriptor)
		{
			analysisResultSetDescriptor.OriginalDescriptor = originalAnalysisResultSetDescriptor;
			analysisResultSetDescriptor.OnlyOriginalExists = analysisResultSetEntity.OnlyOriginalExists;
			analysisResultSetDescriptor.ReviewApproveState = (ReviewApproveState)analysisResultSetEntity.ReviewApproveState;
			analysisResultSetDescriptor.Partial = analysisResultSetEntity.Partial;
			analysisResultSetDescriptor.Name = analysisResultSetEntity.Name;
			analysisResultSetDescriptor.Guid = analysisResultSetEntity.Guid;
			analysisResultSetDescriptor.CreatedDateUtc = analysisResultSetEntity.CreatedDate;
            analysisResultSetDescriptor.ReviewedBy = analysisResultSetEntity.ReviewedBy;
            analysisResultSetDescriptor.ReviewedTimeStamp = analysisResultSetEntity.ReviewedTimeStamp?.ToLocalTime();
            analysisResultSetDescriptor.ApprovedBy = analysisResultSetEntity.ApprovedBy;
            analysisResultSetDescriptor.ApprovedTimeStamp = analysisResultSetEntity.ApprovedTimeStamp?.ToLocalTime();
            var userInfo = DomainFactory.Create<IUserInfo>();
			userInfo.UserId = analysisResultSetEntity.CreatedUserId;
			userInfo.UserFullName = analysisResultSetEntity.CreatedUserName;
			analysisResultSetDescriptor.CreatedByUser = userInfo;
			analysisResultSetDescriptor.ModifiedDateUtc = analysisResultSetEntity.ModifiedDate;
			userInfo = DomainFactory.Create<IUserInfo>();
			userInfo.UserId = analysisResultSetEntity.ModifiedUserId;
			userInfo.UserFullName = analysisResultSetEntity.ModifiedUserName;
			analysisResultSetDescriptor.ModifiedByUser = userInfo;
			analysisResultSetDescriptor.IsCopy = analysisResultSetEntity.IsCopy;
		}

        internal static void PopulateBatchRunInfo(BatchRun batchRunEntity, IBatchRunInfo batchRunInfo)
		{
			try
            {
                if (batchRunInfo.AcquisitionRunInfo == null)
                    batchRunInfo.AcquisitionRunInfo = DomainFactory.Create<IAcquisitionRunInfo>();

				batchRunInfo.AcquisitionRunInfo.AcquisitionCompletionState = (AcquisitionCompletionState)batchRunEntity.AcquisitionCompletionState;

				if (batchRunInfo.CreatedByUser == null)
					batchRunInfo.CreatedByUser = DomainFactory.Create<IUserInfo>();
				if (batchRunInfo.ModifiedByUser == null)
					batchRunInfo.ModifiedByUser = DomainFactory.Create<IUserInfo>();

				batchRunInfo.CreatedDateUtc = batchRunEntity.CreatedDate;
				batchRunInfo.ModifiedDateUtc = batchRunEntity.ModifiedDate;
				batchRunInfo.Name = batchRunEntity.Name;
				batchRunInfo.Guid = batchRunEntity.Guid;
				batchRunInfo.RepeatIndex = batchRunEntity.RepeatIndex;
				batchRunInfo.AcquisitionRunInfo.AcquisitionTime = batchRunEntity.AcquisitionTime;
				batchRunInfo.CreatedByUser.UserId = batchRunEntity.CreatedUserId;
				batchRunInfo.ModifiedByUser.UserId = batchRunEntity.ModifiedUserId;
				batchRunInfo.DataSourceType = (DataSourceType)batchRunEntity.DataSourceType;
				batchRunInfo.AcquisitionRunInfo.IsModifiedAfterSubmission = batchRunEntity.IsModifiedAfterSubmission;
				batchRunInfo.AcquisitionRunInfo.AcquisitionCompletionStateDetails = batchRunEntity.AcquisitionCompletionStateDetails;
            }
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateBatchRunInfo() method", ex);
				throw;
			}
		}

		internal static void PopulateBatchRunAnalysisResultInfo(BatchRunAnalysisResult batchRunAnalysisResultEntity, BatchRun originalBatchRunEntity, IBatchRunInfo modifiableBatchRunId)
		{
			try
			{
				if (modifiableBatchRunId.CreatedByUser == null)
					modifiableBatchRunId.CreatedByUser = DomainFactory.Create<IUserInfo>();
				if (modifiableBatchRunId.ModifiedByUser == null)
					modifiableBatchRunId.ModifiedByUser = DomainFactory.Create<IUserInfo>();
                if (modifiableBatchRunId.AcquisitionRunInfo == null)
                    modifiableBatchRunId.AcquisitionRunInfo = DomainFactory.Create<IAcquisitionRunInfo>();

				modifiableBatchRunId.CreatedDateUtc = batchRunAnalysisResultEntity.BatchRunCreatedDate;
				modifiableBatchRunId.ModifiedDateUtc = batchRunAnalysisResultEntity.BatchRunModifiedDate;
				modifiableBatchRunId.Name = batchRunAnalysisResultEntity.BatchRunName;
				modifiableBatchRunId.Guid = batchRunAnalysisResultEntity.ModifiableBatchRunInfoGuid;

				modifiableBatchRunId.CreatedByUser.UserId = batchRunAnalysisResultEntity.BatchRunCreatedUserId;
				modifiableBatchRunId.ModifiedByUser.UserId = batchRunAnalysisResultEntity.BatchRunModifiedUserId;
				modifiableBatchRunId.DataSourceType = (DataSourceType)batchRunAnalysisResultEntity.DataSourceType;

				if (originalBatchRunEntity != null)
				{
					modifiableBatchRunId.AcquisitionRunInfo.AcquisitionCompletionState = (AcquisitionCompletionState)originalBatchRunEntity.AcquisitionCompletionState;
					modifiableBatchRunId.RepeatIndex = originalBatchRunEntity.RepeatIndex;
					modifiableBatchRunId.AcquisitionRunInfo.AcquisitionTime = originalBatchRunEntity.AcquisitionTime;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateBatchRunAnalysisResultInfo() method", ex);
				throw;
			}
		}

		internal static void PopulateProcessingMethod(ProcessingMethod processingMethodEntity, IProcessingMethod processingMethod)
		{
			if (processingMethodEntity == null)
				return;
			try
			{
				// Common assignments
				processingMethod.Info = DomainFactory.Create<IProcessingMethodInfo>();
				processingMethod.Info.Name = processingMethodEntity.Name;
                processingMethod.Info.VersionNumber = processingMethodEntity.VersionNumber;
                processingMethod.Info.Guid = processingMethodEntity.Guid;
				processingMethod.Info.IsDefault = processingMethodEntity.IsDefault;
				processingMethod.Info.Description = processingMethodEntity.Description;
				processingMethod.Info.CreatedByUser = DomainFactory.Create<IUserInfo>();
				processingMethod.Info.CreatedByUser.UserId = processingMethodEntity.CreatedUserId;
				processingMethod.Info.CreatedByUser.UserFullName = processingMethodEntity.CreatedUserName;
				processingMethod.Info.CreatedDateUtc = processingMethodEntity.CreatedDate.ToLocalTime();
				processingMethod.Info.ModifiedDateUtc = processingMethodEntity.ModifiedDate.ToLocalTime();
				processingMethod.Info.ModifiedByUser = DomainFactory.Create<IUserInfo>();
				processingMethod.Info.ModifiedByUser.UserId = processingMethodEntity.ModifiedUserId;
				processingMethod.Info.ModifiedByUser.UserFullName = processingMethodEntity.ModifiedUserName;
				processingMethod.Info.Name = processingMethodEntity.Name;
				processingMethod.Info.ReviewApproveState = (ReviewApproveState)processingMethodEntity.ReviewApproveState;

                processingMethod.CalibrationGlobalParameters = DomainFactory.Create<ICalibrationGlobalParameters>();
				processingMethod.CalibrationGlobalParameters.NumberOfLevels = processingMethodEntity.NumberOfLevels;
				//				processingMethod.CalibrationGlobalParameters.UseInternalStandard =
				//					processingMethodEntity.UseInternalStandard;
				processingMethod.CalibrationGlobalParameters.AmountUnits = processingMethodEntity.AmountUnits;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationType = (UnidentifiedPeakCalibrationType)processingMethodEntity.UnidentifiedPeakCalibrationType;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationFactor = processingMethodEntity.UnidentifiedPeakCalibrationFactor;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakReferenceCompoundGuid = processingMethodEntity.UnidentifiedPeakReferenceCompoundGuid;

				if (processingMethod is IModifiableProcessingMethod modifiableProcessingMethod)
				{
					modifiableProcessingMethod.ModifiedFromOriginal = processingMethodEntity.ModifiedFromOriginal;
					modifiableProcessingMethod.OriginalReadOnlyMethodGuid = processingMethodEntity.OriginalReadOnlyMethodGuid.HasValue ? processingMethodEntity.OriginalReadOnlyMethodGuid.Value : Guid.Empty;
				}
                if (processingMethod.SuitabilityMethod != null)
                {
                    processingMethod.SuitabilityMethod = DomainFactory.Create<ISuitabilityMethod>();
                    PopulateSuitabilityParameters(processingMethodEntity.ChannelMethods[1].SuitabilityParameters, processingMethod.SuitabilityMethod);
                }
                //todo: uncomment below code after channel restructuring is done from manager/provider
                ////populate ProcessingDeviceMethod
                //foreach (var processingDeviceMethodEntity in processingMethodEntity.ProcessingDeviceMethods)
                //{
                //	IProcessingDeviceMethod processingDeviceMethod = DomainFactory.Create<IProcessingDeviceMethod>();
                //	PopulateProcessingDeviceMethod(processingDeviceMethod,processingDeviceMethodEntity);
                //	processingMethod.ProcessingDeviceMethods.Add(processingDeviceMethod);
                //}

                //SpectrumMethods
                if (processingMethodEntity.SpectrumMethods != null)
				{
					processingMethod.SpectrumMethods = new List<ISpectrumMethod>();
					PopulateSpectrumMethod(processingMethodEntity.SpectrumMethods, processingMethod.SpectrumMethods);
				}

				if (processingMethodEntity.PdaApexOptimizedParameters != null)
				{
					processingMethod.ApexOptimizedParameters = DomainFactory.Create<IPdaApexOptimizedParameters>();
					PopulatePdaApexOptimizedParameters(processingMethodEntity.PdaApexOptimizedParameters, processingMethod.ApexOptimizedParameters);
				}

				if (processingMethodEntity.CalibrationBatchRunInfos != null)
				{
					processingMethod.CalibrationBatchRunInfos = new Dictionary<Guid, ICalibrationBatchRunInfo>();

					foreach (var calibrationBatchRunInfoEntity in processingMethodEntity.CalibrationBatchRunInfos)
					{
						var calibrationBatchRunInfo = DomainFactory.Create<ICalibrationBatchRunInfo>();
						PopulateCalibrationBatchRunInfo(calibrationBatchRunInfoEntity, out var key, calibrationBatchRunInfo);
						processingMethod.CalibrationBatchRunInfos.Add(key, calibrationBatchRunInfo);
					}
				}

				if (processingMethodEntity.SuitabilityMethod != null)
				{
					var suitabilityMethod = DomainFactory.Create<ISuitabilityMethod>();
					PopulateSuitabilityMethod(processingMethodEntity.SuitabilityMethod, suitabilityMethod);
					processingMethod.SuitabilityMethod = suitabilityMethod;
				}              

                // ChannelMethod
                if (processingMethodEntity.ChannelMethods != null)
				{
					processingMethod.ChannelMethods = new List<IChannelMethod>();

					foreach (var channelMethodEntity in processingMethodEntity.ChannelMethods)
					{
						IChannelMethod channelMethod = DomainFactory.Create<IChannelMethod>();
						PopulateChannelMethod(channelMethodEntity, channelMethod);
						channelMethod.TimedIntegrationParameters = new List<IIntegrationEvent>();

						// IntegrationEvent
						foreach (var integrationEventEntity in channelMethodEntity.IntegrationEvents)
						{
							IIntegrationEvent integrationEvent = DomainFactory.Create<IIntegrationEvent>();
							PopulateIntegrationEvent(integrationEventEntity, integrationEvent);
							channelMethod.TimedIntegrationParameters.Add(integrationEvent);
						}

						channelMethod.PdaParameters = DomainFactory.Create<IPdaParameters>();

						if (channelMethodEntity.PdaPeakPurityParameters != null)
						{
							channelMethod.PdaParameters.PeakPurityParameters =
								DomainFactory.Create<IPdaPeakPurityParameters>();
							PopulatePdaPeakPurityParameters(channelMethodEntity.PdaPeakPurityParameters, channelMethod.PdaParameters.PeakPurityParameters);
						}

						if (channelMethodEntity.PdaWavelengthMaxParameters != null)
						{
							channelMethod.PdaParameters.WavelengthParameters =
								DomainFactory.Create<IPdaWavelengthMaxParameters>();
							PopulatePdaWavelengthMaxParameters(channelMethodEntity.PdaWavelengthMaxParameters, channelMethod.PdaParameters.WavelengthParameters);
						}

						if (channelMethodEntity.PdaAbsorbanceRatioParameters != null)
						{
							channelMethod.PdaParameters.AbsorbanceRatioParameters =
								DomainFactory.Create<IPdaAbsorbanceRatioParameters>();
							PopulatePdaAbsorbanceRatioParameters(channelMethodEntity.PdaAbsorbanceRatioParameters, channelMethod.PdaParameters.AbsorbanceRatioParameters);
						}

						if (channelMethodEntity.PdaBaselineCorrectionParameters != null)
						{
							channelMethod.PdaParameters.BaselineCorrectionParameters =
								DomainFactory.Create<IPdaBaselineCorrectionParameters>();
							PopulatePdaBaselineCorrectionParameters(channelMethodEntity.PdaBaselineCorrectionParameters, channelMethod.PdaParameters.BaselineCorrectionParameters);
						}

						if (channelMethodEntity.PdaStandardConfirmationParameters != null)
						{
							channelMethod.PdaParameters.StandardConfirmationParameters =
								DomainFactory.Create<IPdaStandardConfirmationParameters>();
							PopulatePdaStandardConfirmationParameters(channelMethodEntity.PdaStandardConfirmationParameters, channelMethod.PdaParameters.StandardConfirmationParameters);
						}

                        if (channelMethodEntity.PdaLibrarySearchParameters != null)
                        {
                            channelMethod.PdaParameters.PeakLibrarySearchParameters =
                                DomainFactory.Create<IPdaLibrarySearchParameters>();
                            PopulatePdaLibrarySearchParameters(channelMethodEntity.PdaLibrarySearchParameters,
                                channelMethod.PdaParameters.PeakLibrarySearchParameters);
                        }

                        if (channelMethodEntity.PdaLibraryConfirmationParameters != null)
                        {
                            channelMethod.PdaParameters.LibraryConfirmationParameters =
                                DomainFactory.Create<IPdaLibraryConfirmationParameters>();
                            PopulatePdaLibraryConfirmationParameters(
                                channelMethodEntity.PdaLibraryConfirmationParameters,
                                channelMethod.PdaParameters.LibraryConfirmationParameters);
                        }
						// if (channelMethodEntity.SuitabilityParameters != null)
						// {
						// 	channelMethod.SuitabilityParameters = DomainFactory.Create<ISuitabilityParameters>();
						// 	PopulateSuitabilityParameters(channelMethodEntity.SuitabilityParameters, channelMethod.SuitabilityParameters);
						// }

                        processingMethod.ChannelMethods.Add(channelMethod);
					}
				}

				// Compound
				if (processingMethodEntity.Compounds != null)
				{
					processingMethod.Compounds = new List<ICompound>();

					foreach (var compoundEntity in processingMethodEntity.Compounds)
					{
						ICompound compound = compoundEntity.IsCompoundGroup ? DomainFactory.Create<ICompoundGroup>() : DomainFactory.Create<ICompound>();
						PopulateCompound(compoundEntity, compound);
						processingMethod.Compounds.Add(compound);

						// CalibrationParameters
						compound.CalibrationParameters = DomainFactory.Create<ICalibrationParameters>();
						PopulateCalibrationParameters(compoundEntity, compound.CalibrationParameters);

						// LevelAmount
						if (compoundEntity.LevelAmounts != null)
						{
							compound.CalibrationParameters.LevelAmounts = new Dictionary<int, double?>();

							foreach (var levelAmount in compoundEntity.LevelAmounts)
							{
								PopulateLevelAmounts(levelAmount, compound.CalibrationParameters.LevelAmounts);
							}
						}
					}
				}

				// CompCalibResults
				if (processingMethodEntity.CompoundCalibrationResults != null)
				{
					processingMethod.CompoundCalibrationResultsMap = new Dictionary<Guid, ICompoundCalibrationResults>();

					foreach (var compoundCalibrationResultsEntity in processingMethodEntity.CompoundCalibrationResults)
					{
						ICompoundCalibrationResults compoundCalibrationResults = DomainFactory.Create<ICompoundCalibrationResults>();
						PopulateCompoundCalibrationResults(compoundCalibrationResultsEntity, compoundCalibrationResults);

						List<double> compCoeffs = new List<double>();

						foreach (var compCalibResultCoefficientEntity in compoundCalibrationResultsEntity.CompCalibResultCoefficients)
						{
							compCoeffs.Add(compCalibResultCoefficientEntity.Coefficients);
						}

						compoundCalibrationResults.RegressionEquation.Coefficients = compCoeffs.ToArray();

						if (processingMethod.CompoundCalibrationResultsMap.ContainsKey(compoundCalibrationResults.CompoundGuid))
						{
							processingMethod.CompoundCalibrationResultsMap[compoundCalibrationResults.CompoundGuid] = compoundCalibrationResults;
						}
						else
						{
							processingMethod.CompoundCalibrationResultsMap.Add(compoundCalibrationResults.CompoundGuid, compoundCalibrationResults);
						}

						// CalibrationPointResponse
						compoundCalibrationResults.LevelResponses = new Dictionary<int, List<ICalibrationPointResponse>>();


						var compound = processingMethod.Compounds.FirstOrDefault(x => x.Guid == compoundCalibrationResultsEntity.Guid);

						if (compound != null)
							foreach (var compoundLevel in compound.CalibrationParameters.LevelAmounts)
							{
								var calibrationPointResponseList = new List<ICalibrationPointResponse>();

								//Select compoundCalibrationResultId based on compoundGuid
								var calibrationResultBatchResultId = processingMethodEntity.CompoundCalibrationResults.Where(x => x.Guid == compound.Guid).Select(x => x.Id).First();

								//Select the List of CalibrationPointResponse for the level
								var calibrationPointResponseProjLevelWiseList = compoundCalibrationResultsEntity.CalibrationPointResponses.Where(x => x.Level == compoundLevel.Key && x.CompoundCalibrationResultsId == calibrationResultBatchResultId).ToList();

								foreach (var calibrationPointResponseProjLevelWise in calibrationPointResponseProjLevelWiseList)
								{
									//Convert to DomainContract
									ICalibrationPointResponse calibrationPointResponse1 = DomainFactory.Create<ICalibrationPointResponse>();
									PopulateCalibrationPointResponseInfo(calibrationPointResponseProjLevelWise, calibrationPointResponse1);
									calibrationPointResponseList.Add(calibrationPointResponse1);
								}

								if (calibrationPointResponseList.Count > 0)
								{
									compoundCalibrationResults.LevelResponses.Add(compoundLevel.Key, calibrationPointResponseList);
								}
							}

						// InvalidAmounts
						compoundCalibrationResults.InvalidAmounts = new List<double>();

						foreach (var invalidAmountsEntity in compoundCalibrationResultsEntity.InvalidAmounts)
						{
							compoundCalibrationResults.InvalidAmounts.Add(invalidAmountsEntity.Amount);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateProcessingMethod() method", ex);
				throw;
			}
		}

		private static void PopulateSpectrumMethod(List<SpectrumMethod> spectrumMethodsEntity, IList<ISpectrumMethod> spectrumMethods)
		{

			foreach (var spectrumMethodEntity in spectrumMethodsEntity)
			{
				ISpectrumMethod spectrumMethod = DomainFactory.Create<ISpectrumMethod>();
				spectrumMethod.Guid = spectrumMethodEntity.Guid;
				spectrumMethod.BaselineCorrectionType = (BaselineCorrectionType)spectrumMethodEntity.BaselineCorrectionType;
				spectrumMethod.StartRetentionTime = spectrumMethodEntity.StartRetentionTime;
				spectrumMethod.EndRetentionTime = spectrumMethodEntity.EndRetentionTime;
				spectrumMethod.BaselineStartRetentionTime = spectrumMethodEntity.BaselineStartRetentionTime;
				spectrumMethod.BaselineEndRetentionTime = spectrumMethodEntity.BaselineEndRetentionTime;
				spectrumMethods.Add(spectrumMethod);
			}
		}

		internal static void PopulateChannelMethodEntity(IChannelMethod channelMethod, ChannelMethod channelMethodEntity)
		{
			// Smoothing
			if (channelMethod.SmoothParams != null)
			{
				channelMethodEntity.SmoothCycles = channelMethod.SmoothParams.Cycles;
				channelMethodEntity.SmoothFunction = (int)channelMethod.SmoothParams.Function;
				channelMethodEntity.SmoothOrder = channelMethod.SmoothParams.Order;
				channelMethodEntity.SmoothPasses = channelMethod.SmoothParams.Passes;
				channelMethodEntity.SmoothWidth = channelMethod.SmoothParams.Width;
			}

			channelMethodEntity.TimeAdjustment = channelMethod.TimeAdjustment;
			channelMethodEntity.TangentSkimWidth = channelMethod.TangentSkimWidth;
			channelMethodEntity.RrtReferenceCompound = channelMethod.RrtReferenceCompound;
			channelMethodEntity.RrtReferenceType = (short)channelMethod.RrtReferenceType;
			channelMethodEntity.BunchingFactor = channelMethod.BunchingFactor;
			channelMethodEntity.NoiseThreshold = channelMethod.NoiseThreshold;
			channelMethodEntity.AreaThreshold = channelMethod.AreaThreshold;
			channelMethodEntity.WidthRatio = channelMethod.WidthRatio;
			channelMethodEntity.ValleyToPeakRatio = channelMethod.ValleyToPeakRatio;
			channelMethodEntity.PeakHeightRatio = channelMethod.PeakHeightRatio;
			channelMethodEntity.AdjustedHeightRatio = channelMethod.AdjustedHeightRatio;
			channelMethodEntity.ValleyHeightRatio = channelMethod.ValleyHeightRatio;
			channelMethodEntity.VoidTime = channelMethod.VoidTime;
			channelMethodEntity.VoidTimeType = (short)channelMethod.VoidTimeType;
			channelMethodEntity.IsPdaMethod = channelMethod.IsPdaMethod;
			channelMethodEntity.AutoGeneratedFromData = channelMethod.AutoGeneratedFromData;

			//todo: uncomment below code after channel restructuring is done from manager/provider 
			channelMethodEntity.ChannelGuid = channelMethod.ChannelGuid;
			channelMethodEntity.ParentChannelGuid = channelMethod.ParentChannelGuid;
			if (channelMethod.ChannelDescriptor != null)
				channelMethodEntity.ChromatographicChannelDescriptor = JsonConverter.JsonConverter.ToJson(channelMethod.ChannelDescriptor);

			if (channelMethod.ChannelIdentifier != null)
				channelMethodEntity.ProcessingMethodChannelIdentifier = JsonConverter.JsonConverter.ToJson(channelMethod.ChannelIdentifier);
		}

		internal static void PopulateChannelMethod(ChannelMethod channelMethodEntity, IChannelMethod channelMethod)
		{
			if (channelMethod.SmoothParams == null)
				channelMethod.SmoothParams = DomainFactory.Create<ISmoothParameters>();

			channelMethod.SmoothParams.Cycles = channelMethodEntity.SmoothCycles;
			channelMethod.SmoothParams.Function = (SmoothType)channelMethodEntity.SmoothFunction;
			channelMethod.SmoothParams.Order = channelMethodEntity.SmoothOrder;
			channelMethod.SmoothParams.Passes = channelMethodEntity.SmoothPasses;
			channelMethod.SmoothParams.Width = channelMethodEntity.SmoothWidth;
			channelMethod.TimeAdjustment = channelMethodEntity.TimeAdjustment;
			channelMethod.TangentSkimWidth = channelMethodEntity.TangentSkimWidth;
			channelMethod.RrtReferenceCompound = channelMethodEntity.RrtReferenceCompound;
			channelMethod.RrtReferenceType = (RrtReferenceType)channelMethodEntity.RrtReferenceType;
			channelMethod.BunchingFactor = channelMethodEntity.BunchingFactor;
			channelMethod.NoiseThreshold = channelMethodEntity.NoiseThreshold;
			channelMethod.AreaThreshold = channelMethodEntity.AreaThreshold;
			channelMethod.WidthRatio = channelMethodEntity.WidthRatio;
			channelMethod.ValleyToPeakRatio = channelMethodEntity.ValleyToPeakRatio;
			channelMethod.PeakHeightRatio = channelMethodEntity.PeakHeightRatio;
			channelMethod.AdjustedHeightRatio = channelMethodEntity.AdjustedHeightRatio;
			channelMethod.ValleyHeightRatio = channelMethodEntity.ValleyHeightRatio;
			channelMethod.VoidTime = channelMethodEntity.VoidTime;
			channelMethod.VoidTimeType = (VoidTimeType)channelMethodEntity.VoidTimeType;
			channelMethod.IsPdaMethod = channelMethodEntity.IsPdaMethod;
			channelMethod.AutoGeneratedFromData = channelMethodEntity.AutoGeneratedFromData;

			////todo: uncomment below code after channel restructuring is done from manager/provider 
			channelMethod.ChannelGuid = channelMethodEntity.ChannelGuid;
			channelMethod.ParentChannelGuid = channelMethodEntity.ParentChannelGuid;
			if (channelMethodEntity.ChromatographicChannelDescriptor != null)
				channelMethod.ChannelDescriptor = JsonConverter.JsonConverter.FromJson<IChromatographicChannelDescriptor>(channelMethodEntity.ChromatographicChannelDescriptor);

			if (channelMethodEntity.ProcessingMethodChannelIdentifier != null)
				channelMethod.ChannelIdentifier = JsonConverter.JsonConverter.FromJson<IProcessingMethodChannelIdentifier>(channelMethodEntity.ProcessingMethodChannelIdentifier);

		}

		private static void PopulatePdaPeakPurityParametersEntity(IPdaPeakPurityParameters pdaPeakPurityParameters,
			PdaPeakPurityParameters pdaPeakPurityParametersEntity)
		{
			pdaPeakPurityParametersEntity.MinWavelength = pdaPeakPurityParameters.MinWavelength;
			pdaPeakPurityParametersEntity.MaxWavelength = pdaPeakPurityParameters.MaxWavelength;
			pdaPeakPurityParametersEntity.MinimumDataPoints = pdaPeakPurityParameters.MinimumDataPoints;
			pdaPeakPurityParametersEntity.ApplyBaselineCorrection = pdaPeakPurityParameters.ApplyBaselineCorrection;
			pdaPeakPurityParametersEntity.PurityLimit = pdaPeakPurityParameters.PurityLimit;
			pdaPeakPurityParametersEntity.PercentOfPeakHeightForSpectra =
				pdaPeakPurityParameters.PercentOfPeakHeightForSpectra;
			pdaPeakPurityParametersEntity.UseAutoAbsorbanceThreshold = pdaPeakPurityParameters.UseAutoAbsorbanceThreshold;
			pdaPeakPurityParametersEntity.ManualAbsorbanceThreshold = pdaPeakPurityParameters.ManualAbsorbanceThreshold;
		}
		private static void PopulatePdaPeakPurityParameters(PdaPeakPurityParameters pdaPeakPurityParametersEntity, IPdaPeakPurityParameters pdaPeakPurityParameters)
		{
			pdaPeakPurityParameters.MinWavelength = pdaPeakPurityParametersEntity.MinWavelength;
			pdaPeakPurityParameters.MaxWavelength = pdaPeakPurityParametersEntity.MaxWavelength;
			pdaPeakPurityParameters.MinimumDataPoints = pdaPeakPurityParametersEntity.MinimumDataPoints;
			pdaPeakPurityParameters.ApplyBaselineCorrection = pdaPeakPurityParametersEntity.ApplyBaselineCorrection;
			pdaPeakPurityParameters.PurityLimit = pdaPeakPurityParametersEntity.PurityLimit;
			pdaPeakPurityParameters.PercentOfPeakHeightForSpectra = pdaPeakPurityParametersEntity.PercentOfPeakHeightForSpectra;
			pdaPeakPurityParameters.UseAutoAbsorbanceThreshold = pdaPeakPurityParametersEntity.UseAutoAbsorbanceThreshold;
			pdaPeakPurityParameters.ManualAbsorbanceThreshold = pdaPeakPurityParametersEntity.ManualAbsorbanceThreshold;
		}
		private static void PopulatePdaAbsorbanceRatioParametersEntity(IPdaAbsorbanceRatioParameters pdaAbsorbanceRatioParameters, PdaAbsorbanceRatioParameters pdaAbsorbanceRatioParametersEntity)
		{
			pdaAbsorbanceRatioParametersEntity.WavelengthA = pdaAbsorbanceRatioParameters.WavelengthA;
			pdaAbsorbanceRatioParametersEntity.WavelengthB = pdaAbsorbanceRatioParameters.WavelengthB;
			pdaAbsorbanceRatioParametersEntity.ApplyBaselineCorrection = pdaAbsorbanceRatioParameters.ApplyBaselineCorrection;
			pdaAbsorbanceRatioParametersEntity.UseAutoAbsorbanceThreshold = pdaAbsorbanceRatioParameters.UseAutoAbsorbanceThreshold;
			pdaAbsorbanceRatioParametersEntity.ManualAbsorbanceThreshold = pdaAbsorbanceRatioParameters.ManualAbsorbanceThreshold;
		}
		private static void PopulatePdaBaselineCorrectionParametersEntity(IPdaBaselineCorrectionParameters pdaBaselineCorrectionParameters, PdaBaselineCorrectionParameters pdaBaselineCorrectionParametersEntity)
		{
			pdaBaselineCorrectionParametersEntity.CorrectionType = (short)pdaBaselineCorrectionParameters.CorrectionType;
			pdaBaselineCorrectionParametersEntity.SelectedSpectrumTime = pdaBaselineCorrectionParameters.SelectedSpectrumTimeInSeconds;
			pdaBaselineCorrectionParametersEntity.RangeStart = pdaBaselineCorrectionParameters.RangeStartInSeconds;
			pdaBaselineCorrectionParametersEntity.RangeEnd = pdaBaselineCorrectionParameters.RangeEndInSeconds;
		}
		private static void PopulatePdaBaselineCorrectionParameters(PdaBaselineCorrectionParameters pdaBaselineCorrectionParametersEntity, IPdaBaselineCorrectionParameters pdaBaselineCorrectionParameters)
		{
			pdaBaselineCorrectionParameters.CorrectionType = (BaselineCorrectionType)pdaBaselineCorrectionParametersEntity.CorrectionType;
			pdaBaselineCorrectionParameters.SelectedSpectrumTimeInSeconds = pdaBaselineCorrectionParametersEntity.SelectedSpectrumTime;
			pdaBaselineCorrectionParameters.RangeStartInSeconds = pdaBaselineCorrectionParametersEntity.RangeStart;
			pdaBaselineCorrectionParameters.RangeEndInSeconds = pdaBaselineCorrectionParametersEntity.RangeEnd;
		}
		private static void PopulatePdaAbsorbanceRatioParameters(PdaAbsorbanceRatioParameters pdaAbsorbanceRatioParametersEntity, IPdaAbsorbanceRatioParameters pdaAbsorbanceRatioParameters)
		{
			pdaAbsorbanceRatioParameters.WavelengthA = pdaAbsorbanceRatioParametersEntity.WavelengthA;
			pdaAbsorbanceRatioParameters.WavelengthB = pdaAbsorbanceRatioParametersEntity.WavelengthB;
			pdaAbsorbanceRatioParameters.ApplyBaselineCorrection = pdaAbsorbanceRatioParametersEntity.ApplyBaselineCorrection;
			pdaAbsorbanceRatioParameters.UseAutoAbsorbanceThreshold = pdaAbsorbanceRatioParametersEntity.UseAutoAbsorbanceThreshold;
			pdaAbsorbanceRatioParameters.ManualAbsorbanceThreshold = pdaAbsorbanceRatioParametersEntity.ManualAbsorbanceThreshold;
		}
		private static void PopulatePdaWavelengthMaxParametersEntity(IPdaWavelengthMaxParameters pdaWavelengthMaxParameters,
			PdaWavelengthMaxParameters pdaWavelengthMaxParametersEntity)
		{
			pdaWavelengthMaxParametersEntity.ApplyBaselineCorrection =
				pdaWavelengthMaxParameters.ApplyBaselineCorrection;
			pdaWavelengthMaxParametersEntity.ManualAbsorbanceThreshold =
				pdaWavelengthMaxParameters.ManualAbsorbanceThreshold;
			pdaWavelengthMaxParametersEntity.MaxWavelength = pdaWavelengthMaxParameters.MaxWavelength;
			pdaWavelengthMaxParametersEntity.MinWavelength = pdaWavelengthMaxParameters.MinWavelength;
			pdaWavelengthMaxParametersEntity.UseAutoAbsorbanceThreshold =
				pdaWavelengthMaxParameters.UseAutoAbsorbanceThreshold;
		}
		private static void PopulatePdaWavelengthMaxParameters(PdaWavelengthMaxParameters pdaWavelengthMaxParametersEntity,
			IPdaWavelengthMaxParameters pdaWavelengthMaxParameters)
		{
			pdaWavelengthMaxParameters.ApplyBaselineCorrection = pdaWavelengthMaxParametersEntity.ApplyBaselineCorrection;
			pdaWavelengthMaxParameters.ManualAbsorbanceThreshold = pdaWavelengthMaxParametersEntity.ManualAbsorbanceThreshold;
			pdaWavelengthMaxParameters.MaxWavelength = pdaWavelengthMaxParametersEntity.MaxWavelength;
			pdaWavelengthMaxParameters.MinWavelength = pdaWavelengthMaxParametersEntity.MinWavelength;
			pdaWavelengthMaxParameters.UseAutoAbsorbanceThreshold = pdaWavelengthMaxParametersEntity.UseAutoAbsorbanceThreshold;
		}
		private static void PopulatePdaStandardConfirmationParametersEntity(IPdaStandardConfirmationParameters pdaStandardConfirmationParameters,
			PdaStandardConfirmationParameters pdaStandardConfirmationParametersEntity)
		{
            pdaStandardConfirmationParametersEntity.PdaStandardConfirmationGuid = pdaStandardConfirmationParameters.PdaStandardConfirmationGuid;
            pdaStandardConfirmationParametersEntity.MinWavelength = pdaStandardConfirmationParameters.MinWavelength;
			pdaStandardConfirmationParametersEntity.MaxWavelength = pdaStandardConfirmationParameters.MaxWavelength;
			pdaStandardConfirmationParametersEntity.MinimumDataPoints = pdaStandardConfirmationParameters.MinimumDataPoints;
			pdaStandardConfirmationParametersEntity.PassThreshold = pdaStandardConfirmationParameters.PassThreshold;
			pdaStandardConfirmationParametersEntity.ApplyBaselineCorrection = pdaStandardConfirmationParameters.ApplyBaselineCorrection;
			pdaStandardConfirmationParametersEntity.UseAutoAbsorbanceThresholdForSample = pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForSample;
			pdaStandardConfirmationParametersEntity.ManualAbsorbanceThresholdForSample = pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForSample;
			pdaStandardConfirmationParametersEntity.UseAutoAbsorbanceThresholdForStandard = pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForStandard;
			pdaStandardConfirmationParametersEntity.ManualAbsorbanceThresholdForStandard = pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForStandard;
			pdaStandardConfirmationParametersEntity.StandardType = (short)pdaStandardConfirmationParameters.StandardType;
		}
		private static void PopulatePdaStandardConfirmationParameters(PdaStandardConfirmationParameters pdaStandardConfirmationParametersEntity, IPdaStandardConfirmationParameters pdaStandardConfirmationParameters)
		{
			pdaStandardConfirmationParameters.PdaStandardConfirmationGuid = pdaStandardConfirmationParametersEntity.PdaStandardConfirmationGuid;
			pdaStandardConfirmationParameters.MinWavelength = pdaStandardConfirmationParametersEntity.MinWavelength;
			pdaStandardConfirmationParameters.MaxWavelength = pdaStandardConfirmationParametersEntity.MaxWavelength;
			pdaStandardConfirmationParameters.MinimumDataPoints = pdaStandardConfirmationParametersEntity.MinimumDataPoints;
			pdaStandardConfirmationParameters.PassThreshold = pdaStandardConfirmationParametersEntity.PassThreshold;
			pdaStandardConfirmationParameters.ApplyBaselineCorrection = pdaStandardConfirmationParametersEntity.ApplyBaselineCorrection;
			pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForSample = pdaStandardConfirmationParametersEntity.UseAutoAbsorbanceThresholdForSample;
			pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForSample = pdaStandardConfirmationParametersEntity.ManualAbsorbanceThresholdForSample;
			pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForStandard = pdaStandardConfirmationParametersEntity.UseAutoAbsorbanceThresholdForStandard;
			pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForStandard = pdaStandardConfirmationParametersEntity.ManualAbsorbanceThresholdForStandard;
			pdaStandardConfirmationParameters.StandardType = (StandardType)pdaStandardConfirmationParametersEntity.StandardType;
		}

		private static void PopulateCalibrationBatchRunInfoEntity(Guid key, ICalibrationBatchRunInfo calibrationBatchRunInfo,
			CalibrationBatchRunInfo calibrationBatchRunInfoEntity)
		{
			calibrationBatchRunInfoEntity.BatchResultSetGuid = calibrationBatchRunInfo.BatchResultSetGuid;
			calibrationBatchRunInfoEntity.BatchRunAcquisitionTime = calibrationBatchRunInfo.BatchRunAcquisitionTime;
			calibrationBatchRunInfoEntity.BatchRunGuid = calibrationBatchRunInfo.BatchRunGuid;
			calibrationBatchRunInfoEntity.BatchRunName = calibrationBatchRunInfo.BatchRunName;
			calibrationBatchRunInfoEntity.Key = key;
			calibrationBatchRunInfoEntity.ResultSetName = calibrationBatchRunInfo.ResultSetName;
		}
		private static void PopulateCalibrationBatchRunInfo(CalibrationBatchRunInfo calibrationBatchRunInfoEntity, out Guid key, 
			ICalibrationBatchRunInfo calibrationBatchRunInfo)
		{
			calibrationBatchRunInfo.BatchResultSetGuid = calibrationBatchRunInfoEntity.BatchResultSetGuid;
			calibrationBatchRunInfo.BatchRunAcquisitionTime = calibrationBatchRunInfoEntity.BatchRunAcquisitionTime;
			calibrationBatchRunInfo.BatchRunGuid = calibrationBatchRunInfoEntity.BatchRunGuid;
			calibrationBatchRunInfo.BatchRunName = calibrationBatchRunInfoEntity.BatchRunName;
			key = calibrationBatchRunInfoEntity.Key;
			calibrationBatchRunInfo.ResultSetName = calibrationBatchRunInfoEntity.ResultSetName;
		}
		private static void PopulateSuitabilityMethodEntity(ISuitabilityMethod suitabilityMethod,
			SuitabilityMethod suitabilityMethodEntity)
		{
			suitabilityMethodEntity.Enabled = suitabilityMethod.Enabled;
			suitabilityMethodEntity.SelectedPharmacopeiaType = (short)suitabilityMethod.SelectedPharmacopeiaType;
			suitabilityMethodEntity.IsEfficiencyInPlates = suitabilityMethod.IsEfficiencyInPlates;
			suitabilityMethodEntity.ColumnLength = suitabilityMethod.ColumnLength;
			suitabilityMethodEntity.SignalToNoiseWindowStart = suitabilityMethod.SignalToNoiseWindowStart;
			suitabilityMethodEntity.SignalToNoiseWindowEnd = suitabilityMethod.SignalToNoiseWindowEnd;
			suitabilityMethodEntity.SignalToNoiseEnabled = suitabilityMethod.SignalToNoiseWindowEnabled;
			suitabilityMethodEntity.AnalyzeAdjacentPeaks = suitabilityMethod.AnalyzeAdjacentPeaks;
			suitabilityMethodEntity.VoidTimeType = (short)suitabilityMethod.VoidTimeType;
			suitabilityMethodEntity.VoidTimeCustomValueInSeconds = suitabilityMethod.VoidTimeCustomValueInSeconds;

            if (suitabilityMethod.CompoundPharmacopeiaDefinitions != null)
				suitabilityMethodEntity.CompoundPharmacopeiaDefinitions =
                    JsonConverter.JsonConverter.ToJson(suitabilityMethod.CompoundPharmacopeiaDefinitions);
		}
		private static void PopulateSuitabilityMethod(SuitabilityMethod suitabilityMethodEntity, ISuitabilityMethod suitabilityMethod)
		{
			suitabilityMethod.Enabled = suitabilityMethodEntity.Enabled;
			suitabilityMethod.SelectedPharmacopeiaType = (PharmacopeiaType)suitabilityMethodEntity.SelectedPharmacopeiaType;
			suitabilityMethod.IsEfficiencyInPlates = suitabilityMethodEntity.IsEfficiencyInPlates;
			suitabilityMethod.ColumnLength = suitabilityMethodEntity.ColumnLength;
			suitabilityMethod.SignalToNoiseWindowStart = suitabilityMethodEntity.SignalToNoiseWindowStart;
			suitabilityMethod.SignalToNoiseWindowEnd = suitabilityMethodEntity.SignalToNoiseWindowEnd;
			suitabilityMethod.SignalToNoiseWindowEnabled = suitabilityMethodEntity.SignalToNoiseEnabled;
			suitabilityMethod.AnalyzeAdjacentPeaks = suitabilityMethodEntity.AnalyzeAdjacentPeaks;
            suitabilityMethod.VoidTimeType = (VoidTimeType)suitabilityMethodEntity.VoidTimeType;
            suitabilityMethod.VoidTimeCustomValueInSeconds = suitabilityMethodEntity.VoidTimeCustomValueInSeconds;

            if (string.IsNullOrEmpty(suitabilityMethodEntity.CompoundPharmacopeiaDefinitions) == false)
			{
				suitabilityMethod.CompoundPharmacopeiaDefinitions =
                    JsonConverter.JsonConverter
						.FromJson<IDictionary<Guid, IDictionary<PharmacopeiaType,
							IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>>(suitabilityMethodEntity
							.CompoundPharmacopeiaDefinitions);
			}
		}
        private static void PopulatePdaApexOptimizedParametersEntity(IPdaApexOptimizedParameters pdaApexOptimizedParameters,
			PdaApexOptimizedParameters pdaApexOptimizedParametersEntity)
		{
			pdaApexOptimizedParametersEntity.MinWavelength = pdaApexOptimizedParameters.MinWavelength;
			pdaApexOptimizedParametersEntity.MaxWavelength = pdaApexOptimizedParameters.MaxWavelength;
			pdaApexOptimizedParametersEntity.WavelengthBandwidth = pdaApexOptimizedParameters.WavelengthBandwidth;
			pdaApexOptimizedParametersEntity.UseReference = pdaApexOptimizedParameters.UseReference;
			pdaApexOptimizedParametersEntity.ReferenceWavelength = pdaApexOptimizedParameters.ReferenceWavelength;
			pdaApexOptimizedParametersEntity.ReferenceWavelengthBandwidth = pdaApexOptimizedParameters.ReferenceWavelengthBandwidth;
			pdaApexOptimizedParametersEntity.ApplyBaselineCorrection = pdaApexOptimizedParameters.ApplyBaselineCorrection;
			pdaApexOptimizedParametersEntity.UseAutoAbsorbanceThreshold = pdaApexOptimizedParameters.UseAutoAbsorbanceThreshold;
			pdaApexOptimizedParametersEntity.ManualAbsorbanceThreshold = pdaApexOptimizedParameters.ManualAbsorbanceThreshold;
		}
		private static void PopulatePdaApexOptimizedParameters(PdaApexOptimizedParameters pdaApexOptimizedParametersEntity, IPdaApexOptimizedParameters pdaApexOptimizedParameters)
		{
			pdaApexOptimizedParameters.MinWavelength = pdaApexOptimizedParametersEntity.MinWavelength;
			pdaApexOptimizedParameters.MaxWavelength = pdaApexOptimizedParametersEntity.MaxWavelength;
			pdaApexOptimizedParameters.WavelengthBandwidth = pdaApexOptimizedParametersEntity.WavelengthBandwidth;
			pdaApexOptimizedParameters.UseReference = pdaApexOptimizedParametersEntity.UseReference;
			pdaApexOptimizedParameters.ReferenceWavelength = pdaApexOptimizedParametersEntity.ReferenceWavelength;
			pdaApexOptimizedParameters.ReferenceWavelengthBandwidth = pdaApexOptimizedParametersEntity.ReferenceWavelengthBandwidth;
			pdaApexOptimizedParameters.ApplyBaselineCorrection = pdaApexOptimizedParametersEntity.ApplyBaselineCorrection;
			pdaApexOptimizedParameters.UseAutoAbsorbanceThreshold = pdaApexOptimizedParametersEntity.UseAutoAbsorbanceThreshold;
			pdaApexOptimizedParameters.ManualAbsorbanceThreshold = pdaApexOptimizedParametersEntity.ManualAbsorbanceThreshold;
		}

		internal static void PopulateIntegrationEventEntity(IIntegrationEvent integrationEvent,
			IntegrationEventBase integrationEventEntity)
		{
			integrationEventEntity.EndTime = integrationEvent.EndTime;
			integrationEventEntity.EventId = integrationEvent.EventId;
			integrationEventEntity.StartTime = integrationEvent.StartTime;
			integrationEventEntity.Value = integrationEvent.Value;
			integrationEventEntity.EventType = (int)integrationEvent.EventType;
		}

		private static void PopulatePdaLibrarySearchParameters(PdaLibrarySearchParameters pdaLibrarySearchParametersEntity, IPdaLibrarySearchParameters pdaLibrarySearchParameters)
		{
			//TODO: --will populate from entity
			//pdaLibrarySearchParameters.MinimumWavelength = 190;
			//pdaLibrarySearchParameters.MaximumWavelength = 700;
			//pdaLibrarySearchParameters.IsBaselineCorrectionEnabled = false;
			//pdaLibrarySearchParameters.IsMatchRetentionTimeWindowEnabled = false;
			//pdaLibrarySearchParameters.MatchRetentionTimeWindow = 5;
			//pdaLibrarySearchParameters.HitDistanceThreshold = 0.050;
			//pdaLibrarySearchParameters.IsPeakLibrarySearch = false;
			//pdaLibrarySearchParameters.UseWavelengthLimits = true;
			//pdaLibrarySearchParameters.SelectedLibraries = new List<string>();
			//pdaLibrarySearchParameters.MaxNumberOfResults = 1;

			pdaLibrarySearchParameters.MinimumWavelength = pdaLibrarySearchParametersEntity.MinWavelength;
			pdaLibrarySearchParameters.MaximumWavelength = pdaLibrarySearchParametersEntity.MaxWavelength;
			pdaLibrarySearchParameters.IsBaselineCorrectionEnabled = pdaLibrarySearchParametersEntity.BaselineCorrectionEnabled;
			pdaLibrarySearchParameters.IsMatchRetentionTimeWindowEnabled = pdaLibrarySearchParametersEntity.MatchRetentionTimeWindowEnabled;
			pdaLibrarySearchParameters.MatchRetentionTimeWindow = pdaLibrarySearchParametersEntity.MatchRetentionTimeWindow;
			pdaLibrarySearchParameters.HitDistanceThreshold = pdaLibrarySearchParametersEntity.HitDistanceThreshold;
			pdaLibrarySearchParameters.IsPeakLibrarySearch = pdaLibrarySearchParametersEntity.PeakLibrarySearch;
			pdaLibrarySearchParameters.UseWavelengthLimits = pdaLibrarySearchParametersEntity.UseWavelengthLimits;
			pdaLibrarySearchParameters.MaxNumberOfResults = pdaLibrarySearchParametersEntity.MaxNumberOfResults;
			pdaLibrarySearchParameters.SelectedLibraries = new List<string>();
			PopulateSelectedLibraries(pdaLibrarySearchParameters.SelectedLibraries, pdaLibrarySearchParametersEntity);
		}

        private static void PopulatePdaLibraryConfirmationParameters(PdaLibraryConfirmationParameters pdaLibraryConfirmationParametersEntity, IPdaLibraryConfirmationParameters pdaLibraryConfirmationParameters)
        {
            pdaLibraryConfirmationParameters.MinimumWavelength = pdaLibraryConfirmationParametersEntity.MinWavelength;
            pdaLibraryConfirmationParameters.MaximumWavelength = pdaLibraryConfirmationParametersEntity.MaxWavelength;
            pdaLibraryConfirmationParameters.IsBaselineCorrectionEnabled = pdaLibraryConfirmationParametersEntity.BaselineCorrectionEnabled;
            pdaLibraryConfirmationParameters.HitDistanceThreshold = pdaLibraryConfirmationParametersEntity.HitDistanceThreshold;
			//--todo
	        pdaLibraryConfirmationParameters.SelectedLibraries = new List<string>();
			PopulateLibraryConfirmationSelectedLibraries(pdaLibraryConfirmationParameters.SelectedLibraries, pdaLibraryConfirmationParametersEntity);
        }

        private static void PopulatePdaLibraryConfirmationParametersEntity(PdaLibraryConfirmationParameters pdaLibraryConfirmationParametersEntity, IPdaLibraryConfirmationParameters pdaLibraryConfirmationParameters)
        {
            pdaLibraryConfirmationParametersEntity.MinWavelength = pdaLibraryConfirmationParameters.MinimumWavelength;
             pdaLibraryConfirmationParametersEntity.MaxWavelength = pdaLibraryConfirmationParameters.MaximumWavelength;
              pdaLibraryConfirmationParametersEntity.BaselineCorrectionEnabled = pdaLibraryConfirmationParameters.IsBaselineCorrectionEnabled;
             pdaLibraryConfirmationParametersEntity.HitDistanceThreshold = pdaLibraryConfirmationParameters.HitDistanceThreshold;
             if (pdaLibraryConfirmationParametersEntity.SelectedLibraries == null)
	             pdaLibraryConfirmationParametersEntity.SelectedLibraries = new List<PdaLibraryConfirmationSelectedLibraries>();
             PopulateLibraryConfirmationSelectedLibrariesEntity(pdaLibraryConfirmationParameters, pdaLibraryConfirmationParametersEntity.SelectedLibraries);
		}

        internal static void PopulateSelectedLibraries(IList<string> selectedLibraries,PdaLibrarySearchParameters pdaLibrarySearchParametersEntity)
		{
			foreach (PdaLibrarySearchSelectedLibraries selectedLibrary in pdaLibrarySearchParametersEntity.SelectedLibraries)
			{
				selectedLibraries.Add(selectedLibrary.SelectedLibraries);
			}
		}

        internal static void PopulateLibraryConfirmationSelectedLibraries(IList<string> selectedLibraries, PdaLibraryConfirmationParameters pdaLibraryConfirmationParametersEntity)
        {
	        foreach (PdaLibraryConfirmationSelectedLibraries selectedLibrary in pdaLibraryConfirmationParametersEntity.SelectedLibraries)
	        {
		        selectedLibraries.Add(selectedLibrary.SelectedLibraries);
	        }
        }

		internal static void PopulateStreamDataEntity(IStreamDataInfo streamDataInfo, string deviceId, StreamDataBatchResult streamDataBatchResultEntity)
		{
			streamDataBatchResultEntity.StreamIndex = streamDataInfo.StreamIndex;
			streamDataBatchResultEntity.MetaData = streamDataInfo.MetaData;
			streamDataBatchResultEntity.MetaDataType = streamDataInfo.MetaDataType;
			streamDataBatchResultEntity.DeviceId = deviceId;
			streamDataBatchResultEntity.YData = new byte[0];
			streamDataBatchResultEntity.UseLargeObjectStream = streamDataInfo.UseLargeObjectStream;

			if (streamDataInfo.DeviceInformation != null)
			{
				streamDataBatchResultEntity.FirmwareVersion = streamDataInfo.DeviceInformation.FirmwareVersion;
				streamDataBatchResultEntity.SerialNumber = streamDataInfo.DeviceInformation.SerialNumber;
				streamDataBatchResultEntity.ModelName = streamDataInfo.DeviceInformation.ModelName;
				streamDataBatchResultEntity.UniqueIdentifier = streamDataInfo.DeviceInformation.UniqueIdentifier;
				streamDataBatchResultEntity.InterfaceAddress = streamDataInfo.DeviceInformation.InterfaceAddress;
			}
		}

		internal static void PopulateStreamData(StreamDataBatchResult streamDataBatchResultEntity, IStreamDataInfo streamDataInfo)
		{
			streamDataInfo.StreamIndex = streamDataBatchResultEntity.StreamIndex;
			streamDataInfo.MetaData = streamDataBatchResultEntity.MetaData;
			streamDataInfo.MetaDataType = streamDataBatchResultEntity.MetaDataType;
			streamDataInfo.DeviceDriverId = streamDataBatchResultEntity.DeviceId;
			streamDataBatchResultEntity.UseLargeObjectStream = streamDataInfo.UseLargeObjectStream;
			streamDataInfo.DeviceInformation = new DeviceInformation
			{
				FirmwareVersion = streamDataBatchResultEntity.FirmwareVersion,
				ModelName = streamDataBatchResultEntity.ModelName,
				SerialNumber = streamDataBatchResultEntity.SerialNumber,
				UniqueIdentifier = streamDataBatchResultEntity.UniqueIdentifier,
				InterfaceAddress = streamDataBatchResultEntity.InterfaceAddress

			};
		}

		internal static void PopulateSequenceSample(SequenceSampleBase sequenceSampleEntity,
			ISequenceSampleInfo sequenceSampleObject)
		{
			try
			{
				sequenceSampleObject.Guid = sequenceSampleEntity.Guid;
				sequenceSampleObject.BaselineCorrection =
					(BaselineCorrection)sequenceSampleEntity.BaselineCorrection;

				if (sequenceSampleEntity.InjectionTypeAsIntegerDeviceName != null)
					sequenceSampleObject.InjectionTypeAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.InjectionTypeAsIntegerDeviceName, sequenceSampleEntity.InjectionTypeAsInteger);

				sequenceSampleObject.InjectionType = sequenceSampleEntity.InjectionType;


				if (sequenceSampleEntity.InjectionVolumeDeviceName != null)
				{
					sequenceSampleObject.InjectionVolume = new DoubleValueWithDeviceName(sequenceSampleEntity.InjectionVolumeDeviceName, sequenceSampleEntity.InjectionVolume);

				}


				sequenceSampleObject.InternalStandardAmountAdjustment = sequenceSampleEntity.InternalStandardAmountAdjustment;
				sequenceSampleObject.Level = sequenceSampleEntity.Level;
				sequenceSampleObject.Multiplier = sequenceSampleEntity.Multiplier;
                sequenceSampleObject.Divisor = sequenceSampleEntity.Divisor;
				sequenceSampleObject.NumberOfRepeats = sequenceSampleEntity.NumberOfRepeats;
				sequenceSampleObject.PlateCode = sequenceSampleEntity.PlateCode;
				sequenceSampleObject.PlatePosition = sequenceSampleEntity.PlatePosition;
				sequenceSampleObject.BaselineCorrection =
					(BaselineCorrection)sequenceSampleEntity.BaselineCorrection;
				sequenceSampleObject.RackCode = sequenceSampleEntity.RackCode;
				sequenceSampleObject.SampleId = sequenceSampleEntity.SampleId;
				sequenceSampleObject.SampleName = sequenceSampleEntity.SampleName;
				sequenceSampleObject.UserComments = sequenceSampleEntity.UserComments;
				sequenceSampleObject.SampleType = (SampleType)sequenceSampleEntity.SampleType;
				//sequenceSampleObject.UnknownAmountAdjustment = sequenceSampleEntity.UnknownAmountAdjustment;
				sequenceSampleObject.RackPosition = sequenceSampleEntity.RackPosition;
				sequenceSampleObject.VialPosition = sequenceSampleEntity.VialPosition;
				sequenceSampleObject.DestinationVial = sequenceSampleEntity.DestinationVial;
				sequenceSampleObject.SampleAmount = sequenceSampleEntity.SampleAmount;
				sequenceSampleObject.DilutionFactor = sequenceSampleEntity.DilutionFactor;
				sequenceSampleObject.Addend = sequenceSampleEntity.Addend;
				sequenceSampleObject.NormalizationFactor = sequenceSampleEntity.NormalizationFactor;
				sequenceSampleObject.StandardAmountAdjustment = sequenceSampleEntity.StandardAmountAdjustment;
				sequenceSampleObject.Selected = sequenceSampleEntity.Selected;
                sequenceSampleObject.Selected = sequenceSampleEntity.Selected; sequenceSampleObject.Selected = sequenceSampleEntity.Selected;
                if (sequenceSampleEntity.DestinationVialAsIntegerDeviceName != null)
					sequenceSampleObject.DestinationVialAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.DestinationVialAsIntegerDeviceName, sequenceSampleEntity.DestinationVialAsInteger);

				if (sequenceSampleEntity.VialPositionAsIntegerDeviceName != null)
					sequenceSampleObject.VialPositionAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.VialPositionAsIntegerDeviceName, sequenceSampleEntity.VialPositionAsInteger);

				if (sequenceSampleEntity.PlateCodeAsIntegerDeviceName != null)
					sequenceSampleObject.PlateCodeAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.PlateCodeAsIntegerDeviceName, sequenceSampleEntity.PlateCodeAsInteger);

				if (sequenceSampleEntity.PlatePositionAsIntegerDeviceName != null)
					sequenceSampleObject.PlatePositionAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.PlatePositionAsIntegerDeviceName, sequenceSampleEntity.PlatePositionAsInteger);


				sequenceSampleObject.BaselineRunGuid = sequenceSampleEntity.BaselineRunGuid;
				sequenceSampleObject.InjectionPort = sequenceSampleEntity.InjectionPort;

				if (sequenceSampleEntity.InjectionPortAsIntegerDeviceName != null)
					sequenceSampleObject.InjectionPortAsInteger = new IntValueWithDeviceName(sequenceSampleEntity.InjectionPortAsIntegerDeviceName, sequenceSampleEntity.InjectionPortAsInteger);

				sequenceSampleObject.AcquisitionMethodName = sequenceSampleEntity.AcquisitionMethodName;
                sequenceSampleObject.AcquisitionMethodVersionNumber = sequenceSampleEntity.AcquisitionMethodVersionNumber;
                sequenceSampleObject.CalibrationMethodName = sequenceSampleEntity.CalibrationCurveName;
				sequenceSampleObject.ProcessingMethodName = sequenceSampleEntity.ProcessingMethodName;
                sequenceSampleObject.ProcessingMethodVersionNumber = sequenceSampleEntity.ProcessingMethodVersionNumber;
                sequenceSampleObject.SampleReportTemplate = sequenceSampleEntity.SampleReportTemplate;
                sequenceSampleObject.SummaryReportGroup = sequenceSampleEntity.SummaryReportGroup;
                sequenceSampleObject.SuitabilitySampleType = (SuitabilitySampleType)sequenceSampleEntity.SuitabilitySampleType;
			}
            catch (Exception ex)
			{
				Log.Error("Error occured in PopulateSequenceSample() method", ex);
				throw;
			}
		}

		internal static void PopulateSequenceSampleInfoEntity(ISequenceSampleInfo sequenceSampleObject,
			SequenceSampleBase sequenceSampleEntity)
		{
			try
			{
				sequenceSampleEntity.Guid = sequenceSampleObject.Guid;
				sequenceSampleEntity.BaselineCorrection = (int)sequenceSampleObject.BaselineCorrection;



				if (sequenceSampleObject.InjectionTypeAsInteger != null)
				{
					sequenceSampleEntity.InjectionTypeAsInteger = sequenceSampleObject.InjectionTypeAsInteger.Value;
					sequenceSampleEntity.InjectionTypeAsIntegerDeviceName = sequenceSampleObject.InjectionTypeAsInteger.DeviceName;
				}

				sequenceSampleEntity.InjectionType = sequenceSampleObject.InjectionType;

				if (sequenceSampleObject.InjectionVolume != null)
				{
					sequenceSampleEntity.InjectionVolume = sequenceSampleObject.InjectionVolume.Value;
					sequenceSampleEntity.InjectionVolumeDeviceName = sequenceSampleObject.InjectionVolume.DeviceName;
				}

				sequenceSampleEntity.InternalStandardAmountAdjustment = sequenceSampleObject.InternalStandardAmountAdjustment;
				sequenceSampleEntity.Level = sequenceSampleObject.Level;
				sequenceSampleEntity.Multiplier = sequenceSampleObject.Multiplier;
				sequenceSampleEntity.Divisor = sequenceSampleObject.Divisor;
				sequenceSampleEntity.NumberOfRepeats = sequenceSampleObject.NumberOfRepeats;
				sequenceSampleEntity.PlateCode = sequenceSampleObject.PlateCode;
				sequenceSampleEntity.BaselineCorrection = (int)sequenceSampleObject.BaselineCorrection;
				sequenceSampleEntity.PlatePosition = sequenceSampleObject.PlatePosition;
				sequenceSampleEntity.RackCode = sequenceSampleObject.RackCode;
				sequenceSampleEntity.SampleId = sequenceSampleObject.SampleId;
				sequenceSampleEntity.SampleName = sequenceSampleObject.SampleName;
				sequenceSampleEntity.UserComments = sequenceSampleObject.UserComments;
				sequenceSampleEntity.SampleType = (int)sequenceSampleObject.SampleType;
				//sequenceSampleEntity.UnknownAmountAdjustment = sequenceSampleObject.UnknownAmountAdjustment;
				sequenceSampleEntity.RackPosition = sequenceSampleObject.RackPosition;
				sequenceSampleEntity.VialPosition = sequenceSampleObject.VialPosition;
				sequenceSampleEntity.DestinationVial = sequenceSampleObject.DestinationVial;
				sequenceSampleEntity.SampleAmount = sequenceSampleObject.SampleAmount;
				sequenceSampleEntity.DilutionFactor = sequenceSampleObject.DilutionFactor;
				sequenceSampleEntity.Addend = sequenceSampleObject.Addend;
				sequenceSampleEntity.NormalizationFactor = sequenceSampleObject.NormalizationFactor;
				sequenceSampleEntity.StandardAmountAdjustment = sequenceSampleObject.StandardAmountAdjustment;
				sequenceSampleEntity.Selected = sequenceSampleObject.Selected;

				if (sequenceSampleObject.VialPositionAsInteger != null)
				{
					sequenceSampleEntity.VialPositionAsInteger = sequenceSampleObject.VialPositionAsInteger.Value;
					sequenceSampleEntity.VialPositionAsIntegerDeviceName = sequenceSampleObject.VialPositionAsInteger.DeviceName;
				}
				if (sequenceSampleObject.DestinationVialAsInteger != null)
				{
					sequenceSampleEntity.DestinationVialAsInteger = sequenceSampleObject.DestinationVialAsInteger.Value;
					sequenceSampleEntity.DestinationVialAsIntegerDeviceName = sequenceSampleObject.DestinationVialAsInteger.DeviceName;
				}
				if (sequenceSampleObject.PlateCodeAsInteger != null)
				{
					sequenceSampleEntity.PlateCodeAsInteger = sequenceSampleObject.PlateCodeAsInteger.Value;
					sequenceSampleEntity.PlateCodeAsIntegerDeviceName = sequenceSampleObject.PlateCodeAsInteger.DeviceName;
				}
				if (sequenceSampleObject.PlatePositionAsInteger != null)
				{
					sequenceSampleEntity.PlatePositionAsInteger = sequenceSampleObject.PlatePositionAsInteger.Value;
					sequenceSampleEntity.PlatePositionAsIntegerDeviceName = sequenceSampleObject.PlatePositionAsInteger.DeviceName;
				}
				sequenceSampleEntity.BaselineRunGuid = sequenceSampleObject.BaselineRunGuid;
				sequenceSampleEntity.InjectionPort = sequenceSampleObject.InjectionPort;

				if (sequenceSampleObject.InjectionPortAsInteger != null)
				{
					sequenceSampleEntity.InjectionPortAsInteger = sequenceSampleObject.InjectionPortAsInteger.Value;
					sequenceSampleEntity.InjectionPortAsIntegerDeviceName = sequenceSampleObject.InjectionPortAsInteger.DeviceName;
				}

				if (sequenceSampleObject.AcquisitionMethodName != null)
				{
					sequenceSampleEntity.AcquisitionMethodName = sequenceSampleObject.AcquisitionMethodName;
                    sequenceSampleEntity.AcquisitionMethodVersionNumber = sequenceSampleObject.AcquisitionMethodVersionNumber;
                }

				sequenceSampleEntity.CalibrationCurveName = sequenceSampleObject.CalibrationMethodName;
				sequenceSampleEntity.ProcessingMethodName = sequenceSampleObject.ProcessingMethodName;
                sequenceSampleEntity.ProcessingMethodVersionNumber = sequenceSampleObject.ProcessingMethodVersionNumber;
                sequenceSampleEntity.SampleReportTemplate = sequenceSampleObject.SampleReportTemplate;
                sequenceSampleEntity.SummaryReportGroup = sequenceSampleObject.SummaryReportGroup;
                sequenceSampleEntity.SuitabilitySampleType = (short)sequenceSampleObject.SuitabilitySampleType;
			}
            catch (Exception ex)
			{
				Log.Error("Error occured in PopulateSequenceSampleInfoEntity() method", ex);
				throw;
			}
		}

		internal static void PopulateSequenceInfo(Sequence sequenceEntity, ISequenceInfo sequenceInfoObject)
		{
			try
			{
				if (sequenceInfoObject.CreatedByUser == null)
					sequenceInfoObject.CreatedByUser = DomainFactory.Create<IUserInfo>();
				if (sequenceInfoObject.ModifiedByUser == null)
					sequenceInfoObject.ModifiedByUser = DomainFactory.Create<IUserInfo>();

				sequenceInfoObject.Guid = sequenceEntity.Guid;

				sequenceInfoObject.CreatedByUser.UserId = sequenceEntity.CreatedUserId;
                sequenceInfoObject.CreatedByUser.UserFullName = sequenceEntity.CreatedUserName;
                sequenceInfoObject.ModifiedByUser.UserId = sequenceEntity.ModifiedUserId;
                sequenceInfoObject.ModifiedByUser.UserFullName = sequenceEntity.ModifiedUserName;

                sequenceInfoObject.CreatedDateUtc = sequenceEntity.CreatedDate;
				sequenceInfoObject.ModifiedDateUtc = sequenceEntity.ModifiedDate;
				sequenceInfoObject.Name = sequenceEntity.Name;
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateSequenceInfo() method", ex);
				throw;
			}
		}

		internal static void PopulateRunPeakResult(RunPeakResult runPeakResultEntity, IRunPeakResult runPeakResultObject)
		{
			try
			{
				runPeakResultObject.CompoundGuid = runPeakResultEntity.CompoundGuid;
				runPeakResultObject.BatchRunChannelGuid = runPeakResultEntity.BatchRunChannelGuid;
				runPeakResultObject.PeakGuid = runPeakResultEntity.PeakGuid;
				runPeakResultObject.PeakNumber = runPeakResultEntity.PeakNumber;
				runPeakResultObject.Area = runPeakResultEntity.Area;
				runPeakResultObject.Height = runPeakResultEntity.Height;
				runPeakResultObject.InternalStandardAreaRatio = runPeakResultEntity.InternalStandardAreaRatio;
				runPeakResultObject.InternalStandardHeightRatio = runPeakResultEntity.InternalStandardHeightRatio;
				runPeakResultObject.AreaPercent = runPeakResultEntity.AreaPercent;
				runPeakResultObject.RetentionTime = runPeakResultEntity.RetentionTime;
				runPeakResultObject.StartPeakTime = runPeakResultEntity.StartPeakTime;
				runPeakResultObject.EndPeakTime = runPeakResultEntity.EndPeakTime;
				runPeakResultObject.BaselineSlope = runPeakResultEntity.BaselineSlope;
				runPeakResultObject.BaselineIntercept = runPeakResultEntity.BaselineIntercept;
				runPeakResultObject.SignalToNoiseRatio = runPeakResultEntity.SignalToNoiseRatio;
				runPeakResultObject.Amount = runPeakResultEntity.Amount;
				runPeakResultObject.InternalStandardAmountRatio = runPeakResultEntity.InternalStandardAmountRatio;
				runPeakResultObject.AreaToAmountRatio = runPeakResultEntity.AreaToAmountRatio;
				runPeakResultObject.AreaToHeightRatio = runPeakResultEntity.AreaToHeightRatio;
				runPeakResultObject.BaselineCode = (BaselineCode)runPeakResultEntity.BaselineCode;
				runPeakResultObject.CalibrationInRange = (HittingCalibrationRange?)runPeakResultEntity.CalibrationInRange;
				//runPeakResultObject.KPrime = runPeakResultEntity.KPrime;
				runPeakResultObject.NormalizedAmount = runPeakResultEntity.NormalizedAmount;
				runPeakResultObject.RelativeRetentionTime = runPeakResultEntity.RelativeRetentionTime;
				runPeakResultObject.RawAmount = runPeakResultEntity.RawAmount;
				runPeakResultObject.CompoundType = (CompoundType)runPeakResultEntity.CompoundType;
				runPeakResultObject.PeakName = runPeakResultEntity.PeakName;
				runPeakResultObject.Overlapped = runPeakResultEntity.Overlapped;
				runPeakResultObject.IsBaselineExpo = runPeakResultEntity.IsBaselineExpo;
				runPeakResultObject.ExpoA = runPeakResultEntity.ExpoA;
				runPeakResultObject.ExpoB = runPeakResultEntity.ExpoB;
				runPeakResultObject.ExpoCorrection = runPeakResultEntity.ExpoCorrection;
				runPeakResultObject.ExpoDecay = runPeakResultEntity.ExpoDecay;
				runPeakResultObject.RetTimeReferenceGuid = runPeakResultEntity.RetTimeReferenceGuid;
				runPeakResultObject.RrtReferenceGuid = runPeakResultEntity.RrtReferenceGuid;
				runPeakResultObject.ReferenceInternalStandardPeakGuid = runPeakResultEntity.InternalStandardGuid;
				runPeakResultObject.ExpoHeight = runPeakResultEntity.ExpoHeight;
				//runPeakResultObject.TailingFactor = runPeakResultEntity.TailingFactor;
				//runPeakResultObject.Resolution = runPeakResultEntity.Resolution;
				//runPeakResultObject.PeakWidth = runPeakResultEntity.PeakWidth;
				//runPeakResultObject.PeakWidthAtHalfHeight = runPeakResultEntity.PeakWidthAtHalfHeight;
				//runPeakResultObject.PlatesDorseyFoley = runPeakResultEntity.PlatesDorseyFoley;
				//runPeakResultObject.PlatesTangential = runPeakResultEntity.PlatesTangential;
				//runPeakResultObject.PeakWidthAt5PercentHeight = runPeakResultEntity.PeakWidthAt5PercentHeight;
				//runPeakResultObject.PeakWidthAt10PercentHeight = runPeakResultEntity.PeakWidthAt10PercentHeight;
				//runPeakResultObject.RelativeRetTimeSuit = runPeakResultEntity.RelativeRetTimeSuit;
				//runPeakResultObject.Signal = runPeakResultEntity.Signal;
				runPeakResultObject.InternalStandardAmount = runPeakResultEntity.InternalStandardAmount;
				runPeakResultObject.ReferenceInternalStandardCompoundGuid = runPeakResultEntity.ReferenceInternalStandardCompoundGuid;
				runPeakResultObject.AmountError = (AmountResultError)runPeakResultEntity.AmountError;
				runPeakResultObject.AbsorbanceRatio = runPeakResultEntity.AbsorbanceRatio;
				runPeakResultObject.WavelengthMax = runPeakResultEntity.WavelengthMax;
				runPeakResultObject.AbsorbanceAtWavelengthMax = runPeakResultEntity.AbsorbanceAtWavelengthMax;
				runPeakResultObject.WavelengthMaxError = (WavelengthMaxError)runPeakResultEntity.WavelengthMaxError;
				runPeakResultObject.PeakPurity = runPeakResultEntity.PeakPurity;
				runPeakResultObject.PeakPurityPassed = runPeakResultEntity.PeakPurityPassed;
				runPeakResultObject.PeakPurityError = (PeakPurityError)runPeakResultEntity.PeakPurityError;
				runPeakResultObject.DataSourceType = runPeakResultEntity.DataSourceType;
				runPeakResultObject.AbsorbanceRatioError = (AbsorbanceRatioError)runPeakResultEntity.AbsorbanceRatioError;
                runPeakResultObject.ModifiedByManualEvent = runPeakResultEntity.ManuallyOverriden;
				runPeakResultObject.MidIndex = runPeakResultEntity.MidIndex;
				runPeakResultObject.StartIndex = runPeakResultEntity.StartIndex;
				runPeakResultObject.StopIndex = runPeakResultEntity.StopIndex;
				runPeakResultObject.LibraryCompound = runPeakResultEntity.LibraryCompound;
                runPeakResultObject.SearchLibraryCompound = runPeakResultEntity.SearchLibraryCompound;
				runPeakResultObject.HitQualityValue = runPeakResultEntity.HitQualityValue;
                runPeakResultObject.SearchMatch = runPeakResultEntity.SearchMatch;
				runPeakResultObject.LibraryName = runPeakResultEntity.LibraryName;
                runPeakResultObject.SearchLibrary = runPeakResultEntity.SearchLibrary;
				runPeakResultObject.LibraryGuid = runPeakResultEntity.LibraryGuid;
				//TODO: LibGuidInResults: Add LibraryGuid to RunPeakResult persistence object, use it below
				runPeakResultObject.LibraryGuid = Guid.Empty; //runPeakResultEntity.LibraryGuid;
				runPeakResultObject.LibraryConfirmation = runPeakResultEntity.LibraryConfirmation;
				runPeakResultObject.CompoundAssignmentType = (CompoundAssignmentType)runPeakResultEntity.CompoundAssignmentType;
				runPeakResultObject.StandardConfirmationIndex = runPeakResultEntity.StandardConfirmationIndex;
				runPeakResultObject.StandardConfirmationPassed = runPeakResultEntity.StandardConfirmationPassed;
				runPeakResultObject.StandardConfirmationError =(StandardConfirmationError) runPeakResultEntity.StandardConfirmationError;

            }
            catch (Exception ex)
			{
				Log.Error("Error occured in PopulateRunPeakResult() method", ex);
				throw;
			}
		}

		internal static void PopulateBatchRunAnalysisResultEntity(BatchRunAnalysisResult batchRunAnalysisResultEntity, IVirtualBatchRun virtualBatchRunObject)
		{
			IBatchRunInfo batchRunInfo = virtualBatchRunObject.OriginalBatchRun.Info; // original BatchRun Info along with guid
			IBatchRunInfo modifiableBatchRunInfo = virtualBatchRunObject.ModifiableBatchRunInfo; // original BatchRun Info along with guid
			IBatchResultSetInfo batchResultSetInfo = virtualBatchRunObject.OriginalBatchResultSetInfo; // original BatchResultSet Info along with guid
																									   //IBatchRun batchRun = virtualBatchRunObject.OriginalBatchRun; //

			batchRunAnalysisResultEntity.OriginalBatchRunInfoGuid = batchRunInfo.Guid;

			batchRunAnalysisResultEntity.OriginalBatchResultSetGuid = batchResultSetInfo.Guid;
			if (modifiableBatchRunInfo != null)
			{
				batchRunAnalysisResultEntity.ModifiableBatchRunInfoGuid = modifiableBatchRunInfo.Guid;
				batchRunAnalysisResultEntity.BatchRunName = modifiableBatchRunInfo.Name;
				batchRunAnalysisResultEntity.DataSourceType = (short)modifiableBatchRunInfo.DataSourceType;
			}
			else
			{
				batchRunAnalysisResultEntity.BatchRunName = batchRunInfo.Name;
				batchRunAnalysisResultEntity.ModifiableBatchRunInfoGuid = virtualBatchRunObject.OriginalBatchRun.Info.Guid;
			}

			batchRunAnalysisResultEntity.BatchRunCreatedDate = batchRunInfo.CreatedDateUtc;
			batchRunAnalysisResultEntity.BatchRunCreatedUserId = batchRunInfo.CreatedByUser.UserId;

			batchRunAnalysisResultEntity.BatchRunModifiedDate = batchRunInfo.ModifiedDateUtc;
			batchRunAnalysisResultEntity.BatchRunModifiedUserId = batchRunInfo.ModifiedByUser.UserId;
		}

		internal static void PopulateAcquisitionMethod(AcquisitionMethod acquisitionMethodEntity, IAcquisitionMethod acquisitionMethod)
		{
			acquisitionMethod.ReconciledRunTime = acquisitionMethodEntity.ReconciledRunTime;
			acquisitionMethod.Info = DomainFactory.Create<IAcquisitionMethodInfo>();
			acquisitionMethod.Info.Guid = acquisitionMethodEntity.Guid;
			acquisitionMethod.Info.Name = acquisitionMethodEntity.MethodName;
			acquisitionMethod.Info.VersionNumber = acquisitionMethodEntity.VersionNumber;
            acquisitionMethod.Info.CreatedByUser = DomainFactory.Create<IUserInfo>();
			acquisitionMethod.Info.CreatedByUser.UserId = acquisitionMethodEntity.CreateUserId;
			acquisitionMethod.Info.CreatedByUser.UserFullName = acquisitionMethodEntity.CreateUserName;

			acquisitionMethod.Info.ModifiedByUser = DomainFactory.Create<IUserInfo>();
			acquisitionMethod.Info.ModifiedByUser.UserId = acquisitionMethodEntity.ModifyUserId;
			acquisitionMethod.Info.ModifiedByUser.UserFullName = acquisitionMethodEntity.ModifyUserName;
			acquisitionMethod.Info.ReviewApproveState = (ReviewApproveState)acquisitionMethodEntity.ReviewApproveState;
            acquisitionMethod.Info.CreatedDateUtc = acquisitionMethodEntity.CreateDate;
            acquisitionMethod.Info.ModifiedDateUtc = acquisitionMethodEntity.ModifyDate;
            IList<IDeviceMethod> deviceMethods = new List<IDeviceMethod>();

			// Populate DeviceMethod
			foreach (DeviceMethod deviceMethodEntity in acquisitionMethodEntity.DeviceMethods)
			{
				IDeviceMethod deviceMethod = DomainFactory.Create<IDeviceMethod>();
				deviceMethod.DeviceDriverItemDetails = DomainFactory.Create<IDeviceDriverItemDetailsModifiable>();
				deviceMethod.Content = deviceMethodEntity.Content == null ? null : Encoding.UTF8.GetString(deviceMethodEntity.Content);

				var driverItemDetails = DomainFactory.Create<IDeviceDriverItemDetailsModifiable>();
				var instrumentCompleteId = new InstrumentCompleteId(new Id(deviceMethodEntity.InstrumentMasterId), new Id(deviceMethodEntity.InstrumentId));
				var deviceDriverItemCompleteId = new DeviceDriverItemCompleteId(instrumentCompleteId, new Id(deviceMethodEntity.DeviceDriverItemId));
				driverItemDetails.Set(deviceDriverItemCompleteId, deviceMethodEntity.Name);
				var configuration = deviceMethodEntity.Configuration == null
					? null
					: Encoding.UTF8.GetString(deviceMethodEntity.Configuration);
				driverItemDetails.SetConfiguration(configuration);
				deviceMethod.DeviceDriverItemDetails = driverItemDetails;
				deviceMethod.DeviceModuleDetails = new List<IDeviceModuleDetails>();

				// Populate DeviceModule
				foreach (var deviceModuleEntity in deviceMethodEntity.DeviceModules)
				{
					instrumentCompleteId = new InstrumentCompleteId(new Id(deviceModuleEntity.InstrumentMasterId),
						new Id(deviceModuleEntity.InstrumentId));
					deviceDriverItemCompleteId = new DeviceDriverItemCompleteId(instrumentCompleteId, new Id(deviceModuleEntity.DeviceDriverItemId));
					var deviceModuleCompleteId = new DeviceModuleCompleteId(deviceDriverItemCompleteId,
						new Id(deviceModuleEntity.DeviceModuleId),
						deviceModuleEntity.IsDisplayDriver);
					var deviceModule = DomainFactory.Create<IDeviceModuleModifiable>();
					deviceModule.Set(deviceModuleCompleteId, deviceModuleEntity.Name, (DeviceType)deviceModuleEntity.DeviceType);

					// DeviceModuleDetail
					var deviceModuleDetails = DomainFactory.Create<IDeviceModuleDetailsModifiable>();
                    deviceModuleDetails.Set(deviceModule);
					deviceModuleDetails.SetSettingsUserInterfaceSupported(deviceModuleEntity.SettingsUserInterfaceSupported);
					deviceModuleDetails.SetSimulation(deviceModuleEntity.Simulation);
					var deviceInformation = new DeviceInformation()
					{
						FirmwareVersion = deviceModuleEntity.FirmwareVersion,
						SerialNumber = deviceModuleEntity.SerialNumber,
						UniqueIdentifier = deviceModuleEntity.UniqueIdentifier,
						ModelName = deviceModuleEntity.ModelName,
						InterfaceAddress = deviceModuleEntity.InterfaceAddress
					};
					deviceModuleDetails.SetCommunicationTestedSuccessfully(deviceModuleEntity.CommunicationTestedSuccessfully, deviceInformation);

					deviceMethod.DeviceModuleDetails.Add(deviceModuleDetails);
				}

				// IDeviceChannelDescriptor
				deviceMethod.ExpectedDeviceChannelDescriptors = new List<IDeviceChannelDescriptor>();

				foreach (var expectedDeviceChannelDescriptorEntity in deviceMethodEntity.ExpectedDeviceChannelDescriptors)
				{
					var deviceChannelDescriptor = JsonConverter.JsonConverter.FromJson<IDeviceChannelDescriptor>(expectedDeviceChannelDescriptorEntity.DeviceChannelDescriptor);
					deviceMethod.ExpectedDeviceChannelDescriptors.Add(deviceChannelDescriptor);
				}

				deviceMethods.Add(deviceMethod);
			}

			acquisitionMethod.DeviceMethods = deviceMethods.ToArray();
		}

		internal static void PopulateAcquisitionMethodEntity(IAcquisitionMethod acquisitionMethod, AcquisitionMethod acquisitionMethodEntity)
		{
			acquisitionMethodEntity.Guid = acquisitionMethod.Info.Guid;
			acquisitionMethodEntity.CreateDate = acquisitionMethod.Info.CreatedDateUtc;
			acquisitionMethodEntity.MethodName = acquisitionMethod.Info.Name;
			acquisitionMethodEntity.VersionNumber = acquisitionMethod.Info.VersionNumber;
            acquisitionMethodEntity.ReconciledRunTime = acquisitionMethod.ReconciledRunTime;
			acquisitionMethodEntity.ModifyDate = DateTime.UtcNow;
			acquisitionMethodEntity.CreateUserId = acquisitionMethod.Info.CreatedByUser.UserId;
			acquisitionMethodEntity.ModifyUserId = acquisitionMethod.Info.ModifiedByUser.UserId;
			acquisitionMethodEntity.ReviewApproveState = (short)acquisitionMethod.Info.ReviewApproveState;
			acquisitionMethodEntity.CreateUserName = acquisitionMethod.Info.CreatedByUser.UserFullName;
			acquisitionMethodEntity.ModifyUserName = acquisitionMethod.Info.ModifiedByUser.UserFullName;

			var deviceMethodEntities = new List<DeviceMethod>();

			foreach (var deviceMethod in acquisitionMethod.DeviceMethods)
			{
				var deviceMethodEntity = new DeviceMethod();
				PopulateDeviceMethodEntity(deviceMethod, deviceMethodEntity);
				deviceMethodEntities.Add(deviceMethodEntity);
			}

			acquisitionMethodEntity.DeviceMethods = deviceMethodEntities.ToArray();
		}
        //todo sure we need to fix it
		internal static void PopulateDeviceMethodEntity(IDeviceMethod deviceMethod, DeviceMethod deviceMethodEntity)
		{
			deviceMethodEntity.Content = string.IsNullOrEmpty(deviceMethod.Content) ? null : Encoding.UTF8.GetBytes(deviceMethod.Content);

			if (deviceMethod.DeviceDriverItemDetails != null)
			{
				deviceMethodEntity.Configuration = 
					string.IsNullOrEmpty(deviceMethod.DeviceDriverItemDetails.Configuration) ? null : 
						Encoding.UTF8.GetBytes(deviceMethod.DeviceDriverItemDetails.Configuration);
				deviceMethodEntity.DeviceType = deviceMethod.DeviceDriverItemDetails.DeviceType.HasValue
					? (short)deviceMethod.DeviceDriverItemDetails.DeviceType.Value
					: null as short?;
				deviceMethodEntity.Name = deviceMethod.DeviceDriverItemDetails.Name;
				deviceMethodEntity.DeviceDriverItemId = deviceMethod.DeviceDriverItemDetails.Id.DeviceDriverItemId.ToString();
				deviceMethodEntity.InstrumentId = deviceMethod.DeviceDriverItemDetails.Id.InstrumentCompleteId.InstrumentId.ToString();
				deviceMethodEntity.InstrumentMasterId = deviceMethod.DeviceDriverItemDetails.Id.InstrumentCompleteId.InstrumentMasterId.ToString();
			}

			// DeviceModule
			var deviceModuleEntities = new List<DeviceModuleDetails>();

			foreach (var deviceModule in deviceMethod.DeviceModuleDetails)
			{
				var deviceModuleDetailsEntity = new DeviceModuleDetails();
				PopulateDeviceModuleDetailsEntity(deviceModule, deviceModuleDetailsEntity);
				deviceModuleEntities.Add(deviceModuleDetailsEntity);
			}

			var expectedDeviceChannelDescriptorsEntity = new List<ExpectedDeviceChannelDescriptor>();

			if (deviceMethod.ExpectedDeviceChannelDescriptors != null)
			{
				foreach (var expectedDeviceChannelDescriptor in deviceMethod.ExpectedDeviceChannelDescriptors)
				{
					var expectedDeviceChannelDescriptorEntity = new ExpectedDeviceChannelDescriptor()
					{
						DeviceChannelDescriptor = JsonConverter.JsonConverter.ToJson(expectedDeviceChannelDescriptor)
					};

					expectedDeviceChannelDescriptorsEntity.Add(expectedDeviceChannelDescriptorEntity);
				}
			}

			deviceMethodEntity.DeviceModules = deviceModuleEntities.ToArray();
			deviceMethodEntity.ExpectedDeviceChannelDescriptors = expectedDeviceChannelDescriptorsEntity.ToArray();
		}

		internal static void PopulateDeviceModuleDetailsEntity(IDeviceModuleDetails deviceModuleDetails,
			DeviceModuleDetailsBase deviceModuleDetailsEntity)
		{
			deviceModuleDetailsEntity.Name = deviceModuleDetails.Name;
			deviceModuleDetailsEntity.DeviceType = (short) deviceModuleDetails.DeviceType;
			deviceModuleDetailsEntity.IsDisplayDriver = deviceModuleDetails.Id.IsDisplayDriver;
			deviceModuleDetailsEntity.DeviceModuleId = deviceModuleDetails.Id.DeviceModuleId.ToString();
			deviceModuleDetailsEntity.DeviceDriverItemId =
				deviceModuleDetails.Id.DeviceDriverItemCompleteId.DeviceDriverItemId.ToString();
			deviceModuleDetailsEntity.InstrumentId = deviceModuleDetails.Id.DeviceDriverItemCompleteId
				.InstrumentCompleteId.InstrumentId.ToString();
			deviceModuleDetailsEntity.InstrumentMasterId = deviceModuleDetails.Id.DeviceDriverItemCompleteId
				.InstrumentCompleteId.InstrumentMasterId.ToString();
			deviceModuleDetailsEntity.CommunicationTestedSuccessfully =
				deviceModuleDetails.CommunicationTestedSuccessfully;
			deviceModuleDetailsEntity.SettingsUserInterfaceSupported =
				deviceModuleDetails.SettingsUserInterfaceSupported;
			deviceModuleDetailsEntity.Simulation = deviceModuleDetails.Simulation;
			deviceModuleDetailsEntity.FirmwareVersion = deviceModuleDetails.DeviceInformation?.FirmwareVersion;
			deviceModuleDetailsEntity.UniqueIdentifier = deviceModuleDetails.DeviceInformation?.UniqueIdentifier;
			deviceModuleDetailsEntity.ModelName = deviceModuleDetails.DeviceInformation?.ModelName;
			deviceModuleDetailsEntity.SerialNumber = deviceModuleDetails.DeviceInformation?.SerialNumber;
			deviceModuleDetailsEntity.InterfaceAddress = deviceModuleDetails.DeviceInformation?.InterfaceAddress;
		}
		internal static void PopulateDeviceModuleDetails(DeviceModuleDetailsBase deviceModuleDetailsEntity, IDeviceModuleDetailsModifiable deviceModuleDetails)
		{
			IDeviceModuleModifiable deviceModule = DomainFactory.Create<IDeviceModuleModifiable>();
			deviceModule.Set(new DeviceModuleCompleteId(new DeviceDriverItemCompleteId(new InstrumentCompleteId(new Id(deviceModuleDetailsEntity.InstrumentMasterId),
				new Id(deviceModuleDetailsEntity.InstrumentId)), 
				new Id(deviceModuleDetailsEntity.DeviceDriverItemId)), 
				new Id(deviceModuleDetailsEntity.DeviceModuleId), deviceModuleDetailsEntity.IsDisplayDriver), deviceModuleDetailsEntity.Name, (DeviceType)deviceModuleDetailsEntity.DeviceType);
			deviceModuleDetails.Set(deviceModule);
			var deviceInformation = (DeviceInformation)DomainFactory.Create<IDeviceInformation>();
			deviceInformation.FirmwareVersion = deviceModuleDetailsEntity.FirmwareVersion;
			deviceInformation.InterfaceAddress = deviceModuleDetailsEntity.InterfaceAddress;
			deviceInformation.ModelName = deviceModuleDetailsEntity.ModelName;
			deviceInformation.SerialNumber = deviceModuleDetailsEntity.SerialNumber;
			deviceInformation.UniqueIdentifier = deviceModuleDetailsEntity.UniqueIdentifier;
			deviceModuleDetails.SetCommunicationTestedSuccessfully(deviceModuleDetailsEntity.CommunicationTestedSuccessfully, deviceInformation);
			deviceModuleDetails.SetSettingsUserInterfaceSupported(deviceModuleDetailsEntity.SettingsUserInterfaceSupported);
		    deviceModuleDetails.SetSimulation(deviceModuleDetailsEntity.Simulation);
		}
		internal static void PopulateDeviceDriverItemDetailsEntity(IDeviceDriverItemDetails deviceDriverItemDetails,
			DeviceDriverItemDetails deviceDriverItemDetailsEntity)
		{
			deviceDriverItemDetailsEntity.Configuration = deviceDriverItemDetails.Configuration;
			deviceDriverItemDetailsEntity.DeviceType = deviceDriverItemDetails.DeviceType == null ? null as short? : (short)deviceDriverItemDetails.DeviceType;
			deviceDriverItemDetailsEntity.Name = deviceDriverItemDetails.Name;
			deviceDriverItemDetailsEntity.IsDisplayDriver = deviceDriverItemDetails.IsDisplayDriver;
			deviceDriverItemDetailsEntity.InstrumentMasterId = deviceDriverItemDetails.Id.InstrumentCompleteId.InstrumentMasterId.ToString();
			deviceDriverItemDetailsEntity.InstrumentId = deviceDriverItemDetails.Id.InstrumentCompleteId.InstrumentId.ToString();
			deviceDriverItemDetailsEntity.DeviceDriverItemId = deviceDriverItemDetails.Id.DeviceDriverItemId.ToString();
		}
		internal static void PopulateDeviceDriverItemDetails(DeviceDriverItemDetails deviceDriverItemDetailsEntity, IDeviceDriverItemDetailsModifiable deviceDriverItemDetails)
		{
			deviceDriverItemDetails.SetConfiguration(deviceDriverItemDetailsEntity.Configuration);
			deviceDriverItemDetails.SetDeviceType(deviceDriverItemDetailsEntity.DeviceType == null ? null as DeviceType? : (DeviceType)deviceDriverItemDetailsEntity.DeviceType, 
				deviceDriverItemDetailsEntity.IsDisplayDriver);
			deviceDriverItemDetails.Set(new DeviceDriverItemCompleteId(new InstrumentCompleteId(new Id(deviceDriverItemDetailsEntity.InstrumentMasterId),
				new Id(deviceDriverItemDetailsEntity.InstrumentId)), 
				new Id(deviceDriverItemDetailsEntity.DeviceDriverItemId)), deviceDriverItemDetailsEntity.Name);
		}
		internal static void PopulateProcessingMethodEntity(IProcessingMethod processingMethod, ProcessingMethod processingMethodEntity)
		{
			Log.Info($"PopulateProcessingMethodEntity() called");

			// Common assignments
			processingMethodEntity.IsDefault = processingMethod.Info.IsDefault;
			processingMethodEntity.Name = processingMethod.Info.Name;
            processingMethodEntity.VersionNumber = processingMethod.Info.VersionNumber;
            processingMethodEntity.Guid = processingMethod.Info.Guid;
			processingMethodEntity.ModifiedDate = DateTime.UtcNow;
			processingMethodEntity.CreatedDate = DateTime.UtcNow;
			processingMethodEntity.ReviewApproveState = (short)processingMethod.Info.ReviewApproveState;
            if (processingMethod.Info.ModifiedByUser != null)
			{
				processingMethodEntity.ModifiedUserId = processingMethod.Info.ModifiedByUser?.UserId;
                processingMethodEntity.ModifiedUserName = processingMethod.Info.ModifiedByUser?.UserFullName;
            }
            if (processingMethod.Info.CreatedByUser != null)
            {
                processingMethodEntity.CreatedUserId = processingMethod.Info.CreatedByUser?.UserId;
                processingMethodEntity.CreatedUserName = processingMethod.Info.CreatedByUser?.UserFullName;
            }
            processingMethodEntity.Description = processingMethod.Info.Description;
			if (processingMethod.CalibrationGlobalParameters == null)
				processingMethod.CalibrationGlobalParameters = DomainFactory.Create<ICalibrationGlobalParameters>();

			processingMethodEntity.NumberOfLevels = processingMethod.CalibrationGlobalParameters.NumberOfLevels;
			processingMethodEntity.AmountUnits = processingMethod.CalibrationGlobalParameters.AmountUnits;
			processingMethodEntity.UnidentifiedPeakCalibrationType = (int)processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationType;
			processingMethodEntity.UnidentifiedPeakCalibrationFactor = processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationFactor;
			processingMethodEntity.UnidentifiedPeakReferenceCompoundGuid = processingMethod.CalibrationGlobalParameters.UnidentifiedPeakReferenceCompoundGuid;

			if (processingMethod is IModifiableProcessingMethod modifiableProcessingMethod)
			{
				processingMethodEntity.ModifiedFromOriginal = modifiableProcessingMethod.ModifiedFromOriginal;
				processingMethodEntity.OriginalReadOnlyMethodGuid = modifiableProcessingMethod.OriginalReadOnlyMethodGuid;
			}

			//todo: temporary hack to be removed Start ++
			processingMethodEntity.ProcessingDeviceMethods = new List<ProcessingDeviceMethod>();
			ProcessingDeviceMethod processingDeviceMethod = new ProcessingDeviceMethod()
			{
				DeviceClass = "GC",
				DeviceIndex = 0
			};
			processingMethodEntity.ProcessingDeviceMethods.Add(processingDeviceMethod);
			//todo: temporary hack to be removed End --

			//todo: uncomment below code after channel restructuring is done from manager/provider
			////Populate ProcessingDeviceMethod
			//foreach (var processingDeviceMethod in processingMethod.ProcessingDeviceMethods)
			//{
			//	ProcessingDeviceMethod processingDeviceMethodEntity = new ProcessingDeviceMethod();
			//	PopulateProcessingDeviceMethodEntity(processingDeviceMethod, processingDeviceMethodEntity);
			//	processingMethodEntity.ProcessingDeviceMethods.Add(processingDeviceMethodEntity);
			//}

			//SpectrumMethod
			if (processingMethod.SpectrumMethods != null)
			{
				processingMethodEntity.SpectrumMethods = new List<SpectrumMethod>();
				PopulateSpectrumMethodEntity(processingMethod.SpectrumMethods, processingMethodEntity.SpectrumMethods);
			}

			if (processingMethod.ApexOptimizedParameters != null)
			{
				var pdaApexOptimizedParametersEntity = new PdaApexOptimizedParameters();
				PopulatePdaApexOptimizedParametersEntity(processingMethod.ApexOptimizedParameters, pdaApexOptimizedParametersEntity);
				processingMethodEntity.PdaApexOptimizedParameters = pdaApexOptimizedParametersEntity;
			}

			if (processingMethod.CalibrationBatchRunInfos != null)
			{
				processingMethodEntity.CompoundCalibrationResults = new List<CompoundCalibrationResults>();

				foreach (var calibrationBatchRunInfo in processingMethod.CalibrationBatchRunInfos)
				{
					var calibrationBatchRunInfoEntity = new CalibrationBatchRunInfo();
					PopulateCalibrationBatchRunInfoEntity(calibrationBatchRunInfo.Key, calibrationBatchRunInfo.Value, calibrationBatchRunInfoEntity);
					processingMethodEntity.CalibrationBatchRunInfos.Add(calibrationBatchRunInfoEntity);
				}
			}

			if (processingMethod.SuitabilityMethod != null)
			{
				var suitabilityMethodEntity = new SuitabilityMethod();
				DomainContractAdaptor.PopulateSuitabilityMethodEntity(processingMethod.SuitabilityMethod, suitabilityMethodEntity);
				processingMethodEntity.SuitabilityMethod = suitabilityMethodEntity;
			}

			// ChannelMethod
			if (processingMethod.ChannelMethods != null)
			{
				foreach (var channelMethod in processingMethod.ChannelMethods)
				{
					var channelMethodEntity = new ChannelMethod();
					PopulateChannelMethodEntity(channelMethod, channelMethodEntity);

					foreach (var integrationEvent in channelMethod.TimedIntegrationParameters)
					{
						var integrationEventEntity = new IntegrationEvent();
						PopulateIntegrationEventEntity(integrationEvent, integrationEventEntity);
						channelMethodEntity.IntegrationEvents.Add(integrationEventEntity);
					}

					if (channelMethod.PdaParameters != null)
					{
						if (channelMethod.PdaParameters.WavelengthParameters != null)
						{
							var pdaWavelengthMaxParametersEntity = new PdaWavelengthMaxParameters();
							PopulatePdaWavelengthMaxParametersEntity(channelMethod.PdaParameters.WavelengthParameters, pdaWavelengthMaxParametersEntity);
							channelMethodEntity.PdaWavelengthMaxParameters = pdaWavelengthMaxParametersEntity;
						}

						if (channelMethod.PdaParameters.PeakPurityParameters != null)
						{
							var pdaPeakPurityParametersEntity = new PdaPeakPurityParameters();
							PopulatePdaPeakPurityParametersEntity(channelMethod.PdaParameters.PeakPurityParameters, pdaPeakPurityParametersEntity);
							channelMethodEntity.PdaPeakPurityParameters = pdaPeakPurityParametersEntity;
						}

						if (channelMethod.PdaParameters.AbsorbanceRatioParameters != null)
						{
							var pdaAbsorbanceRatioParametersEntity = new PdaAbsorbanceRatioParameters();
							PopulatePdaAbsorbanceRatioParametersEntity(channelMethod.PdaParameters.AbsorbanceRatioParameters, pdaAbsorbanceRatioParametersEntity);
							channelMethodEntity.PdaAbsorbanceRatioParameters = pdaAbsorbanceRatioParametersEntity;
						}

						if (channelMethod.PdaParameters.BaselineCorrectionParameters != null)
						{
							var pdaBaselineCorrectionParametersEntity = new PdaBaselineCorrectionParameters();
							PopulatePdaBaselineCorrectionParametersEntity(channelMethod.PdaParameters.BaselineCorrectionParameters, pdaBaselineCorrectionParametersEntity);
							channelMethodEntity.PdaBaselineCorrectionParameters = pdaBaselineCorrectionParametersEntity;
						}

						if (channelMethod.PdaParameters.StandardConfirmationParameters != null)
						{
							var pdaStandardConfirmationParametersEntity = new PdaStandardConfirmationParameters();
							PopulatePdaStandardConfirmationParametersEntity(channelMethod.PdaParameters.StandardConfirmationParameters, pdaStandardConfirmationParametersEntity);
							channelMethodEntity.PdaStandardConfirmationParameters =
								pdaStandardConfirmationParametersEntity;
						}

						if (channelMethod.PdaParameters.PeakLibrarySearchParameters != null)
						{
							var PdaLibrarySearchParametersEntity = new PdaLibrarySearchParameters();
							PopulatePdaLibrarySearchParametersEntity(channelMethod.PdaParameters.PeakLibrarySearchParameters, PdaLibrarySearchParametersEntity);
							channelMethodEntity.PdaLibrarySearchParameters = PdaLibrarySearchParametersEntity;
						}

                        if (channelMethod.PdaParameters.LibraryConfirmationParameters != null)
                        {
                            var pdaLibraryConfirmationParametersEntity = new PdaLibraryConfirmationParameters();
                            PopulatePdaLibraryConfirmationParametersEntity(pdaLibraryConfirmationParametersEntity,
                                channelMethod.PdaParameters.LibraryConfirmationParameters);
                            channelMethodEntity.PdaLibraryConfirmationParameters = pdaLibraryConfirmationParametersEntity;
                        }

						// if (channelMethod.SuitabilityParameters != null)
						// {
						// 	var suitabilityParametersEntity = new SuitabilityParameters();
						// 	PopulateSuitabilityParametersEntity(channelMethod.SuitabilityParameters, suitabilityParametersEntity);
						// 	channelMethodEntity.SuitabilityParameters = suitabilityParametersEntity;
						//
						// }
					}

					processingMethodEntity.ChannelMethods.Add(channelMethodEntity);
				}
			}

			// Compound
			if (processingMethod.Compounds != null)
			{
				foreach (var compound in processingMethod.Compounds)
				{
					Compound compoundEntity = new Compound();
					PopulateCompoundEntity(compound, compoundEntity);
					processingMethodEntity.Compounds.Add(compoundEntity);
				}
			}

			// CompoundCalibrationResults
			if (processingMethod.CompoundCalibrationResultsMap != null)
			{
				foreach (var calibrationResult in processingMethod.CompoundCalibrationResultsMap)
				{
					CompoundCalibrationResults compoundCalibrationResultsEntity = new CompoundCalibrationResults();
					PopulateCompoundCalibrationResultsEntity(calibrationResult.Value, compoundCalibrationResultsEntity);

					// LevelResponse
					foreach (var levelResponse in calibrationResult.Value.LevelResponses)
					{
						foreach (var calibrationPointResponse in levelResponse.Value)
						{
							CalibrationPointResponse calibrationPointResponseEntity = new CalibrationPointResponse();
							calibrationPointResponseEntity.Level = levelResponse.Key;
							PopulateCalibrationPointResponseEntity(calibrationPointResponse, calibrationPointResponseEntity);
							compoundCalibrationResultsEntity.CalibrationPointResponses.Add(calibrationPointResponseEntity);
						}
					}

					// InvalidAmounts
					if (calibrationResult.Value.InvalidAmounts != null)
					{
						foreach (var invalidAmount in calibrationResult.Value.InvalidAmounts)
						{
							InvalidAmounts invalidAmountsEntity = new InvalidAmounts();
							invalidAmountsEntity.Amount = invalidAmount;
							compoundCalibrationResultsEntity.InvalidAmounts.Add(invalidAmountsEntity);
						}
					}

					processingMethodEntity.CompoundCalibrationResults.Add(compoundCalibrationResultsEntity);
				}
			}
		}

		//private static void PopulateSuitabilityParametersEntity(ISuitabilityMethod suitabilityMethod,
  //          SuitabilityParameters suitabilityParametersEntity)
		//{
		//	suitabilityParametersEntity.ComplianceStandard = (short)suitabilityMethod.ComplianceStandard;
		//	suitabilityParametersEntity.EfficiencyReporting = (short)suitabilityMethod.EfficiencyReporting;
		//	suitabilityParametersEntity.ColumnLength= suitabilityMethod.ColumnLength;
		//	suitabilityParametersEntity.SignalToNoiseStartTime= suitabilityMethod.SignalToNoiseStartTime;
		//	suitabilityParametersEntity.SignalToNoiseEndTime= suitabilityParameters.SignalToNoiseEndTime;
		//	suitabilityParametersEntity.NumberOfSigmas= suitabilityParameters.NumberOfSigmas;
		//	suitabilityParametersEntity.AnalyzeMode= (short)suitabilityParameters.AnalyzeMode;
		//	suitabilityParametersEntity.TailingFactorCalculation= (short)suitabilityParameters.TailingFactorCalculation;

  //          //foreach (var suitabilityLimitsPerComplianceStandard in suitabilityParameters.SuitabilityLimitsPerComplianceStandards)
  //          //{
  //          //    if (suitabilityLimitsPerComplianceStandard.Value.Area != null)
  //          //    {
  //          //        suitabilityParametersEntity.AreaLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.Area.IsUsed;
  //          //        suitabilityParametersEntity.AreaLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.Area.LowerLimit;
  //          //        suitabilityParametersEntity.AreaLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.Area.UpperLimit;
  //          //        suitabilityParametersEntity.AreaLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.Area.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.Height != null)
  //          //    {
  //          //        suitabilityParametersEntity.HeightLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.Height.IsUsed;
  //          //        suitabilityParametersEntity.HeightLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.Height.LowerLimit;
  //          //        suitabilityParametersEntity.HeightLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.Height.UpperLimit;
  //          //        suitabilityParametersEntity.HeightLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.Height.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.NTan != null)
  //          //    {
  //          //        suitabilityParametersEntity.NTanLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.NTan.IsUsed;
  //          //        suitabilityParametersEntity.NTanLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.NTan.LowerLimit;
  //          //        suitabilityParametersEntity.NTanLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.NTan.UpperLimit;
  //          //        suitabilityParametersEntity.NTanLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.NTan.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.NFoley != null)
  //          //    {
  //          //        suitabilityParametersEntity.NFoleyLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.NFoley.IsUsed;
  //          //        suitabilityParametersEntity.NFoleyLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.NFoley.LowerLimit;
  //          //        suitabilityParametersEntity.NFoleyLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.NFoley.UpperLimit;
  //          //        suitabilityParametersEntity.NFoleyLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.NFoley.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.TailingFactorSymmetry != null)
  //          //    {
  //          //        suitabilityParametersEntity.TailingFactorSymmetryLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.TailingFactorSymmetry.IsUsed;
  //          //        suitabilityParametersEntity.TailingFactorSymmetryLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.TailingFactorSymmetry.LowerLimit;
  //          //        suitabilityParametersEntity.TailingFactorSymmetryLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.TailingFactorSymmetry.UpperLimit;
  //          //        suitabilityParametersEntity.TailingFactorSymmetryLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.TailingFactorSymmetry.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.UspResolution != null)
  //          //    {
  //          //        suitabilityParametersEntity.UspResolutionLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.UspResolution.IsUsed;
  //          //        suitabilityParametersEntity.UspResolutionLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.UspResolution.LowerLimit;
  //          //        suitabilityParametersEntity.UspResolutionLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.UspResolution.UpperLimit;
  //          //        suitabilityParametersEntity.UspResolutionLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.UspResolution.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.KPrime != null)
  //          //    {
  //          //        suitabilityParametersEntity.KPrimeLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.KPrime.IsUsed;
  //          //        suitabilityParametersEntity.KPrimeLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.KPrime.LowerLimit;
  //          //        suitabilityParametersEntity.KPrimeLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.KPrime.UpperLimit;
  //          //        suitabilityParametersEntity.KPrimeLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.KPrime.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.Resolution != null)
  //          //    {
  //          //        suitabilityParametersEntity.ResolutionLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.Resolution.IsUsed;
  //          //        suitabilityParametersEntity.ResolutionLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.Resolution.LowerLimit;
  //          //        suitabilityParametersEntity.ResolutionLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.Resolution.UpperLimit;
  //          //        suitabilityParametersEntity.ResolutionLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.Resolution.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.Alpha != null)
  //          //    {
  //          //        suitabilityParametersEntity.AlphaLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.Alpha.IsUsed;
  //          //        suitabilityParametersEntity.AlphaLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.Alpha.LowerLimit;
  //          //        suitabilityParametersEntity.AlphaLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.Alpha.UpperLimit;
  //          //        suitabilityParametersEntity.AlphaLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.Alpha.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.SignalToNoise != null)
  //          //    {
  //          //        suitabilityParametersEntity.SignalToNoiseLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.SignalToNoise.IsUsed;
  //          //        suitabilityParametersEntity.SignalToNoiseLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.SignalToNoise.LowerLimit;
  //          //        suitabilityParametersEntity.SignalToNoiseLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.SignalToNoise.UpperLimit;
  //          //        suitabilityParametersEntity.SignalToNoiseLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.SignalToNoise.RelativeStDevPercent;
  //          //    }

  //          //    if (suitabilityLimitsPerComplianceStandard.Value.PeakWidth != null)
  //          //    {
  //          //        suitabilityParametersEntity.PeakWidthLimitIsUsed = suitabilityLimitsPerComplianceStandard.Value.PeakWidth.IsUsed;
  //          //        suitabilityParametersEntity.PeakWidthLimitLowerLimit = suitabilityLimitsPerComplianceStandard.Value.PeakWidth.LowerLimit;
  //          //        suitabilityParametersEntity.PeakWidthLimitUpperLimit = suitabilityLimitsPerComplianceStandard.Value.PeakWidth.UpperLimit;
  //          //        suitabilityParametersEntity.PeakWidthLimitRelativeStDevPercent = suitabilityLimitsPerComplianceStandard.Value.PeakWidth.RelativeStDevPercent;
  //          //    }

  //          //   // suitabilityParametersEntity.SuitabilityLimitsPerComplianceStandards.Add(suitabilityLimitsPerComplianceStandard.Key, suitabilityLimitsPerComplianceStandard.Value);
  //          //}
			
		//}

		private static void PopulateSuitabilityParameters(SuitabilityParameters suitabilityParametersEntity, ISuitabilityMethod suitabilityParameters)
		{
			suitabilityParameters.SelectedPharmacopeiaType= (PharmacopeiaType)suitabilityParametersEntity.ComplianceStandard;
            suitabilityParameters.IsEfficiencyInPlates = true; // (EfficiencyReportingMode)suitabilityParametersEntity.EfficiencyReporting;
			suitabilityParameters.ColumnLength= suitabilityParametersEntity.ColumnLength;
			suitabilityParameters.SignalToNoiseWindowStart= suitabilityParametersEntity.SignalToNoiseStartTime;
			suitabilityParameters.SignalToNoiseWindowEnd = suitabilityParametersEntity.SignalToNoiseEndTime;
            suitabilityParameters.AnalyzeAdjacentPeaks = true; // (AnalyzeMode)suitabilityParametersEntity.AnalyzeMode;
			//suitabilityParameters.TailingFactorCalculation= (TailingFactorCalculationMode)suitabilityParametersEntity.TailingFactorCalculation;
            //suitabilityParameters.SuitabilityLimitsPerComplianceStandards = DomainFactory.Create<IDictionary<SuitabilityComplianceStandard, ISuitabilityLimits>>();
          //  suitabilityParameters.SuitabilityLimitsPerComplianceStandards=new Dictionary<SuitabilityComplianceStandard, ISuitabilityLimits>();

            var suitabilityLimits = DomainFactory.Create<ISuitabilityLimits>();
            suitabilityLimits.Area = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.AreaLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.AreaLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.AreaLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.AreaLimitRelativeStDevPercent
            };

            suitabilityLimits.Height = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.HeightLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.HeightLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.HeightLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.HeightLimitRelativeStDevPercent
            };
            
            suitabilityLimits.NTan = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.NTanLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.NTanLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.NTanLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.NTanLimitRelativeStDevPercent
            };

            suitabilityLimits.NFoley = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.NFoleyLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.NFoleyLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.NFoleyLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.NFoleyLimitRelativeStDevPercent
            };

            suitabilityLimits.TailingFactorSymmetry = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.TailingFactorSymmetryLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.TailingFactorSymmetryLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.TailingFactorSymmetryLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.TailingFactorSymmetryLimitRelativeStDevPercent,
            };
            suitabilityLimits.KPrime = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.KPrimeLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.KPrimeLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.KPrimeLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.KPrimeLimitRelativeStDevPercent
            };

            suitabilityLimits.Resolution = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.ResolutionLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.ResolutionLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.ResolutionLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.ResolutionLimitRelativeStDevPercent
            };

            suitabilityLimits.Alpha = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.AlphaLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.AlphaLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.AlphaLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.AlphaLimitRelativeStDevPercent
            };

            suitabilityLimits.UspResolution = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.UspResolutionLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.UspResolutionLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.UspResolutionLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.UspResolutionLimitRelativeStDevPercent
            };

            suitabilityLimits.SignalToNoise = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.SignalToNoiseLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.SignalToNoiseLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.SignalToNoiseLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.SignalToNoiseLimitRelativeStDevPercent
            };

            suitabilityLimits.PeakWidth = new SuitabilityLimit()
            {
                IsUsed = suitabilityParametersEntity.PeakWidthLimitIsUsed,
                LowerLimit = suitabilityParametersEntity.PeakWidthLimitLowerLimit,
                UpperLimit = suitabilityParametersEntity.PeakWidthLimitUpperLimit,
                RelativeStDevPercent = suitabilityParametersEntity.PeakWidthLimitRelativeStDevPercent
            };

            //suitabilityParameters.SuitabilityLimitsPerComplianceStandards.Add(SuitabilityComplianceStandard.UsPharmacopeia32, suitabilityLimits);

            ISuitabilityLimits suitabilityLimitsForBPEPJP = DomainFactory.Create<ISuitabilityLimits>(); ;
            suitabilityLimitsForBPEPJP.Area = suitabilityLimits.Area;
            suitabilityLimitsForBPEPJP.Height = suitabilityLimits.Height;
            suitabilityLimitsForBPEPJP.NFoley = suitabilityLimits.NFoley;
            suitabilityLimitsForBPEPJP.TailingFactorSymmetry = suitabilityLimits.TailingFactorSymmetry;
            suitabilityLimitsForBPEPJP.KPrime = suitabilityLimits.KPrime;
            suitabilityLimitsForBPEPJP.Resolution = suitabilityLimits.Resolution;
            suitabilityLimitsForBPEPJP.Alpha = suitabilityLimits.Alpha;
            suitabilityLimitsForBPEPJP.SignalToNoise = suitabilityLimits.SignalToNoise;
            suitabilityLimitsForBPEPJP.PeakWidth = suitabilityLimits.PeakWidth;
            //suitabilityParameters.SuitabilityLimitsPerComplianceStandards.Add(SuitabilityComplianceStandard.BritishPharmacopeia, suitabilityLimitsForBPEPJP);
            //suitabilityParameters.SuitabilityLimitsPerComplianceStandards.Add(SuitabilityComplianceStandard.EuropeanPharmacopeia, suitabilityLimitsForBPEPJP);
            //suitabilityParameters.SuitabilityLimitsPerComplianceStandards.Add(SuitabilityComplianceStandard.JapanesePharmacopeia, suitabilityLimitsForBPEPJP);
        }

		private static void PopulateSpectrumMethodEntity(IList<ISpectrumMethod> spectrumMethods, List<SpectrumMethod> spectrumMethodsEntity)
		{
			foreach (var spectrumMethod in spectrumMethods)
			{
				SpectrumMethod spectrumMethodEntity = new SpectrumMethod();
				spectrumMethodEntity.Guid = spectrumMethod.Guid;
				spectrumMethodEntity.BaselineCorrectionType = (int)spectrumMethod.BaselineCorrectionType;
				spectrumMethodEntity.BaselineStartRetentionTime = spectrumMethod.BaselineStartRetentionTime;
				spectrumMethodEntity.BaselineEndRetentionTime = spectrumMethod.BaselineEndRetentionTime;
				spectrumMethodEntity.StartRetentionTime = spectrumMethod.StartRetentionTime;
				spectrumMethodEntity.EndRetentionTime = spectrumMethod.EndRetentionTime;
				spectrumMethodsEntity.Add(spectrumMethodEntity);
			}
		}

		internal static void PopulatePdaLibrarySearchParametersEntity(IPdaLibrarySearchParameters pdaLibrarySearchParameters, PdaLibrarySearchParameters pdaLibrarySearchParametersEntity)
		{
			pdaLibrarySearchParametersEntity.BaselineCorrectionEnabled = pdaLibrarySearchParameters.IsBaselineCorrectionEnabled;
			pdaLibrarySearchParametersEntity.HitDistanceThreshold = pdaLibrarySearchParameters.HitDistanceThreshold;
			pdaLibrarySearchParametersEntity.MatchRetentionTimeWindow = pdaLibrarySearchParameters.MatchRetentionTimeWindow;
			pdaLibrarySearchParametersEntity.MatchRetentionTimeWindowEnabled = pdaLibrarySearchParameters.IsMatchRetentionTimeWindowEnabled;
			pdaLibrarySearchParametersEntity.MaxNumberOfResults = pdaLibrarySearchParameters.MaxNumberOfResults;
			pdaLibrarySearchParametersEntity.MaxWavelength = pdaLibrarySearchParameters.MaximumWavelength;
			pdaLibrarySearchParametersEntity.MinWavelength = pdaLibrarySearchParameters.MinimumWavelength;
			pdaLibrarySearchParametersEntity.PeakLibrarySearch = pdaLibrarySearchParameters.IsPeakLibrarySearch;
			pdaLibrarySearchParametersEntity.UseWavelengthLimits = pdaLibrarySearchParameters.UseWavelengthLimits;
			if (pdaLibrarySearchParametersEntity.SelectedLibraries ==null)
				pdaLibrarySearchParametersEntity.SelectedLibraries = new List<PdaLibrarySearchSelectedLibraries>();
			PopulateSelectedLibrariesEntity(pdaLibrarySearchParameters, pdaLibrarySearchParametersEntity.SelectedLibraries);
		}

		internal static void PopulateSelectedLibrariesEntity(IPdaLibrarySearchParameters pdaLibrarySearchParameters, List<PdaLibrarySearchSelectedLibraries> selectedLibraries)
		{
			foreach (var selectedLibrary in pdaLibrarySearchParameters.SelectedLibraries)
			{
				PdaLibrarySearchSelectedLibraries pdaLibrarySearchSelectedLibraries =
					new PdaLibrarySearchSelectedLibraries {SelectedLibraries = selectedLibrary};
				selectedLibraries.Add(pdaLibrarySearchSelectedLibraries);
			}
		}

		internal static void PopulateLibraryConfirmationSelectedLibrariesEntity(IPdaLibraryConfirmationParameters pdaLibraryConfirmationParameters, List<PdaLibraryConfirmationSelectedLibraries> selectedLibraries)
		{
			foreach (string selectedLibrary in pdaLibraryConfirmationParameters.SelectedLibraries)
			{
				PdaLibraryConfirmationSelectedLibraries pdaLibraryConfirmationSelectedLibraries = new PdaLibraryConfirmationSelectedLibraries();
				pdaLibraryConfirmationSelectedLibraries.SelectedLibraries = selectedLibrary;
				selectedLibraries.Add(pdaLibraryConfirmationSelectedLibraries);
			}
		}


		internal static void PopulateProcessingMethodInfo(ProcessingMethod processingMethodEntity,
			 IProcessingMethod processingMethod)
		{
			Log.Info($"PopulateProcessingMethodInfo() called");
			try
			{
				if (processingMethod.Info == null)
					processingMethod.Info = DomainFactory.Create<IProcessingMethodInfo>();

				if (processingMethod.Info.CreatedByUser == null)
					processingMethod.Info.CreatedByUser = DomainFactory.Create<IUserInfo>();

				if (processingMethod.Info.ModifiedByUser == null)
					processingMethod.Info.ModifiedByUser = DomainFactory.Create<IUserInfo>();

				if (processingMethod.CalibrationGlobalParameters == null)
					processingMethod.CalibrationGlobalParameters = DomainFactory.Create<ICalibrationGlobalParameters>();

				processingMethod.Info.Name = processingMethodEntity.Name;
                processingMethod.Info.VersionNumber = processingMethodEntity.VersionNumber;

                processingMethod.Info.Guid = processingMethodEntity.Guid;
				processingMethod.Info.IsDefault = processingMethodEntity.IsDefault;
				processingMethod.Info.Description = processingMethodEntity.Description;
				processingMethod.Info.CreatedByUser.UserId = processingMethodEntity.CreatedUserId;
                processingMethod.Info.CreatedByUser.UserFullName = processingMethodEntity.CreatedUserName;
                processingMethod.Info.CreatedDateUtc = processingMethodEntity.CreatedDate.ToLocalTime();
				processingMethod.Info.ModifiedDateUtc = processingMethodEntity.ModifiedDate.ToLocalTime();
				processingMethod.Info.ModifiedByUser.UserId = processingMethodEntity.ModifiedUserId;
                processingMethod.Info.ModifiedByUser.UserFullName = processingMethodEntity.ModifiedUserName;
                Enum.TryParse<ReviewApproveState>(processingMethodEntity.ReviewApproveState.ToString(), out var state);
				processingMethod.Info.ReviewApproveState = state;

				processingMethod.CalibrationGlobalParameters.AmountUnits = processingMethodEntity.AmountUnits;
				processingMethod.CalibrationGlobalParameters.NumberOfLevels = processingMethodEntity.NumberOfLevels;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationType =
					(UnidentifiedPeakCalibrationType)processingMethodEntity.UnidentifiedPeakCalibrationType;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakCalibrationFactor =
					processingMethodEntity.UnidentifiedPeakCalibrationFactor;
				processingMethod.CalibrationGlobalParameters.UnidentifiedPeakReferenceCompoundGuid =
					processingMethodEntity.UnidentifiedPeakReferenceCompoundGuid;


				if (processingMethod is IModifiableProcessingMethod modifiableProcessingMethod &&
					processingMethodEntity is ProcessingMethod processingMethodModifiableEntity)
				{
					modifiableProcessingMethod.ModifiedFromOriginal = processingMethodModifiableEntity.ModifiedFromOriginal;
					modifiableProcessingMethod.OriginalReadOnlyMethodGuid =
						processingMethodModifiableEntity.OriginalReadOnlyMethodGuid.HasValue ? processingMethodModifiableEntity.OriginalReadOnlyMethodGuid.Value : Guid.Empty;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateProcessingMethodInfo() method", ex);
				throw;
			}
		}
		internal static void PopulateAnalysisResultSetEntity(AnalysisResultSet analysisResultSetEntity, string projectName, string analysisResultSetName, IAnalysisResultSetDescriptor descriptor)
		{
			try
			{
				analysisResultSetEntity.ProjectName = projectName;
				analysisResultSetEntity.Name = analysisResultSetName;
                analysisResultSetEntity.OnlyOriginalExists = descriptor.OnlyOriginalExists;
                analysisResultSetEntity.Partial = descriptor.Partial;
                analysisResultSetEntity.Imported = descriptor.OriginalDescriptor.Imported;
                analysisResultSetEntity.CreatedDate = descriptor.CreatedDateUtc;
				analysisResultSetEntity.ModifiedDate = descriptor.ModifiedDateUtc;
				analysisResultSetEntity.CreatedUserId = descriptor.CreatedByUser.UserId;
				analysisResultSetEntity.CreatedUserName = descriptor.CreatedByUser.UserFullName;
				analysisResultSetEntity.ModifiedUserId = descriptor.ModifiedByUser.UserId;
				analysisResultSetEntity.ModifiedUserName = descriptor.ModifiedByUser.UserFullName;
				analysisResultSetEntity.ReviewApproveState = (short)descriptor.ReviewApproveState;

			}
			catch (Exception ex)
			{

				Log.Error("Error occured in PopulateAnalysisResultSet() method", ex);
				throw;
			}
		}

		//internal static void PopulateChannelMethodModifiableEntity(IChannelMethod channelMethod, ChannelMethodModifiable channelMethodModifiableEntity)
		//{
		//	//dkt todo gokul: modify schema
		//	//            channelMethodModifiableEntity.DataType = (short)channelMethod.ChannelIdentifier.DataType;
		//	//            channelMethodModifiableEntity.ChannelType = (short)channelMethod.ChannelIdentifier.ChannelType;
		//	channelMethodModifiableEntity.ChannelIndex = channelMethod.ChannelIndex;
		//	channelMethodModifiableEntity.TimeAdjustment = channelMethod.TimeAdjustment;
		//	channelMethodModifiableEntity.BunchingFactor = channelMethod.BunchingFactor;
		//	channelMethodModifiableEntity.NoiseThreshold = channelMethod.NoiseThreshold;
		//	channelMethodModifiableEntity.AreaThreshold = channelMethod.AreaThreshold;
		//	channelMethodModifiableEntity.RrtReferenceCompound = channelMethod.RrtReferenceCompound;
		//	channelMethodModifiableEntity.SmoothFunction = (int)channelMethod.SmoothParams.Function;
		//	channelMethodModifiableEntity.SmoothWidth = channelMethod.SmoothParams.Width;
		//	channelMethodModifiableEntity.SmoothPasses = channelMethod.SmoothParams.Passes;
		//	channelMethodModifiableEntity.SmoothOrder = channelMethod.SmoothParams.Order;
		//	channelMethodModifiableEntity.SmoothCycles = channelMethod.SmoothParams.Cycles;
		//	channelMethodModifiableEntity.PeakHeightRatio = channelMethod.PeakHeightRatio;
		//	channelMethodModifiableEntity.AdjustedHeightRatio = channelMethod.AdjustedHeightRatio;
		//	channelMethodModifiableEntity.ValleyHeightRatio = channelMethod.ValleyHeightRatio;
		//	channelMethodModifiableEntity.ValleyToPeakRatio = channelMethod.ValleyToPeakRatio;
		//	channelMethodModifiableEntity.VoidTime = channelMethod.VoidTime;
		//	channelMethodModifiableEntity.VoidTimeType = (short)channelMethod.VoidTimeType;
		//	channelMethodModifiableEntity.RrtReferenceType = (short)channelMethod.RrtReferenceType;
		//	channelMethodModifiableEntity.WidthRatio = channelMethod.WidthRatio;
		//	//channelMethodModifiableEntity.RetentionTimeReferenceCompound = channelMethod.RetentionTimeReferenceCompound; //dkt todo:delete
		//	channelMethodModifiableEntity.TangentSkimWidth = channelMethod.TangentSkimWidth;

		//}

		internal static void PopulateIntegrationEvent(IntegrationEventBase integrationEventModifiableEntity, IIntegrationEvent integrationEvent)
		{
			integrationEvent.EndTime = integrationEventModifiableEntity.EndTime;
			integrationEvent.EventId = integrationEventModifiableEntity.EventId;
			integrationEvent.StartTime = integrationEventModifiableEntity.StartTime;
			integrationEvent.Value = integrationEventModifiableEntity.Value;
			integrationEvent.EventType = (IntegrationEventType)integrationEventModifiableEntity.EventType;
		}
		internal static void PopulateRunPeakResultEntity(IRunPeakResult runPeakResultObject,
			RunPeakResult runPeakResultEntity)
		{
			runPeakResultEntity.BatchRunChannelGuid = runPeakResultObject.BatchRunChannelGuid;
			runPeakResultEntity.CompoundGuid = runPeakResultObject.CompoundGuid;
			runPeakResultEntity.PeakNumber = runPeakResultObject.PeakNumber;
			runPeakResultEntity.Area = runPeakResultObject.Area;
			runPeakResultEntity.Height = runPeakResultObject.Height;
			runPeakResultEntity.InternalStandardAreaRatio = runPeakResultObject.InternalStandardAreaRatio;
			runPeakResultEntity.InternalStandardHeightRatio = runPeakResultObject.InternalStandardHeightRatio;
			runPeakResultEntity.AreaPercent = runPeakResultObject.AreaPercent;
			runPeakResultEntity.RetentionTime = runPeakResultObject.RetentionTime;
			runPeakResultEntity.StartPeakTime = runPeakResultObject.StartPeakTime;
			runPeakResultEntity.EndPeakTime = runPeakResultObject.EndPeakTime;
			runPeakResultEntity.BaselineSlope = runPeakResultObject.BaselineSlope;
			runPeakResultEntity.BaselineIntercept = runPeakResultObject.BaselineIntercept;
			runPeakResultEntity.SignalToNoiseRatio = runPeakResultObject.SignalToNoiseRatio;
			runPeakResultEntity.Amount = runPeakResultObject.Amount;
			runPeakResultEntity.InternalStandardAmountRatio = runPeakResultObject.InternalStandardAmountRatio;
			runPeakResultEntity.AreaToHeightRatio = runPeakResultObject.AreaToHeightRatio;
			runPeakResultEntity.AreaToAmountRatio = runPeakResultObject.AreaToAmountRatio;
			runPeakResultEntity.BaselineCode = (int)runPeakResultObject.BaselineCode;
			runPeakResultEntity.CalibrationInRange = (int?)runPeakResultObject.CalibrationInRange;
			//runPeakResultEntity.KPrime = runPeakResultObject.KPrime;
			runPeakResultEntity.NormalizedAmount = runPeakResultObject.NormalizedAmount;
			runPeakResultEntity.RelativeRetentionTime = runPeakResultObject.RelativeRetentionTime;
			runPeakResultEntity.RawAmount = runPeakResultObject.RawAmount;
			//runPeakResultEntity.TailingFactor = runPeakResultObject.TailingFactor;
			//runPeakResultEntity.Resolution = runPeakResultObject.Resolution;
			//runPeakResultEntity.PeakWidth = runPeakResultObject.PeakWidth;

			//runPeakResultEntity.PeakWidthAtHalfHeight = runPeakResultObject.PeakWidthAtHalfHeight;
			runPeakResultEntity.CompoundType = (short)runPeakResultObject.CompoundType;
			runPeakResultEntity.PeakName = runPeakResultObject.PeakName;
			runPeakResultEntity.Overlapped = runPeakResultObject.Overlapped;
			runPeakResultEntity.IsBaselineExpo = runPeakResultObject.IsBaselineExpo;
			runPeakResultEntity.ExpoA = runPeakResultObject.ExpoA;
			runPeakResultEntity.ExpoB = runPeakResultObject.ExpoB;
			runPeakResultEntity.ExpoCorrection = runPeakResultObject.ExpoCorrection;
			runPeakResultEntity.ExpoDecay = runPeakResultObject.ExpoDecay;
			runPeakResultEntity.ExpoHeight = runPeakResultObject.ExpoHeight;
			runPeakResultEntity.RetTimeReferenceGuid = runPeakResultObject.RetTimeReferenceGuid;
			runPeakResultEntity.RrtReferenceGuid = runPeakResultObject.RrtReferenceGuid;

			runPeakResultEntity.InternalStandardGuid = runPeakResultObject.ReferenceInternalStandardPeakGuid;
			//runPeakResultEntity.PlatesDorseyFoley = runPeakResultObject.PlatesDorseyFoley;
			//runPeakResultEntity.PlatesTangential = runPeakResultObject.PlatesTangential;
			//runPeakResultEntity.PeakWidthAt5PercentHeight = runPeakResultObject.PeakWidthAt5PercentHeight;
			//runPeakResultEntity.PeakWidthAt10PercentHeight = runPeakResultObject.PeakWidthAt10PercentHeight;
			//runPeakResultEntity.RelativeRetTimeSuit = runPeakResultObject.RelativeRetTimeSuit;
			//runPeakResultEntity.Signal = runPeakResultObject.Signal;
			runPeakResultEntity.PeakGuid = runPeakResultObject.PeakGuid;
			runPeakResultEntity.InternalStandardAmount = runPeakResultObject.InternalStandardAmount;
			runPeakResultEntity.ReferenceInternalStandardCompoundGuid = runPeakResultObject.ReferenceInternalStandardCompoundGuid;
			runPeakResultEntity.AmountError = (int)runPeakResultObject.AmountError;
			runPeakResultEntity.AbsorbanceRatio = runPeakResultObject.AbsorbanceRatio;
			runPeakResultEntity.WavelengthMax = runPeakResultObject.WavelengthMax;
			runPeakResultEntity.AbsorbanceAtWavelengthMax = runPeakResultObject.AbsorbanceAtWavelengthMax;
			runPeakResultEntity.WavelengthMaxError = (int)runPeakResultObject.WavelengthMaxError;
			runPeakResultEntity.PeakPurity = runPeakResultObject.PeakPurity;
			runPeakResultEntity.PeakPurityPassed = runPeakResultObject.PeakPurityPassed;
			runPeakResultEntity.PeakPurityError = (int)runPeakResultObject.PeakPurityError;
			runPeakResultEntity.DataSourceType = runPeakResultObject.DataSourceType;
			runPeakResultEntity.AbsorbanceRatioError = (int)runPeakResultObject.AbsorbanceRatioError;
			runPeakResultEntity.ManuallyOverriden = runPeakResultObject.ModifiedByManualEvent;
			runPeakResultEntity.MidIndex = runPeakResultObject.MidIndex;
			runPeakResultEntity.StartIndex = runPeakResultObject.StartIndex;
			runPeakResultEntity.StopIndex = runPeakResultObject.StopIndex;
			runPeakResultEntity.LibraryCompound = runPeakResultObject.LibraryCompound;
            runPeakResultEntity.SearchLibraryCompound = runPeakResultObject.SearchLibraryCompound;
			runPeakResultEntity.LibraryConfirmation = runPeakResultObject.LibraryConfirmation;
			runPeakResultEntity.LibraryName = runPeakResultObject.LibraryName;
            runPeakResultEntity.SearchLibrary = runPeakResultObject.SearchLibrary;
			runPeakResultEntity.LibraryGuid = runPeakResultObject.LibraryGuid;
			runPeakResultEntity.HitQualityValue = runPeakResultObject.HitQualityValue;
            runPeakResultEntity.SearchMatch = runPeakResultObject.SearchMatch;
			runPeakResultEntity.CompoundAssignmentType = (short)runPeakResultObject.CompoundAssignmentType;
			runPeakResultEntity.StandardConfirmationIndex = runPeakResultObject.StandardConfirmationIndex;
			runPeakResultEntity.StandardConfirmationPassed = runPeakResultObject.StandardConfirmationPassed;
			runPeakResultEntity.StandardConfirmationError = (short)runPeakResultObject.StandardConfirmationError;
		}
		internal static void PopulateSuitabilityResultEntity(ISuitabilityResult suitabilityResult,
			SuitabilityResult suitabilityResultEntity)
		{
			suitabilityResultEntity.PeakGuid = suitabilityResult.PeakGuid;
			suitabilityResultEntity.CompoundGuid = suitabilityResult.CompoundGuid;
			suitabilityResultEntity.PeakName = suitabilityResult.PeakName;
			suitabilityResultEntity.PeakRetentionTime = suitabilityResult.PeakRetentionTime;
			suitabilityResultEntity.Noise = suitabilityResult.Noise;
			suitabilityResultEntity.NoiseStart = suitabilityResult.NoiseStart;
			suitabilityResultEntity.NoiseGapStart = suitabilityResult.NoiseGapStart;
			suitabilityResultEntity.NoiseGapEnd = suitabilityResult.NoiseGapEnd;
			suitabilityResultEntity.NoiseEnd = suitabilityResult.NoiseEnd;
			suitabilityResultEntity.SstFlag = suitabilityResult.SstFlag;

			if (suitabilityResult.Area != null)
			{
				suitabilityResultEntity.AreaPassed = suitabilityResult.Area.Passed;
				suitabilityResultEntity.Area = suitabilityResult.Area.Value;
				suitabilityResultEntity.AreaFailureReason = (short)suitabilityResult.Area.FailureReason;
			}

			if (suitabilityResult.Height != null)
			{
				suitabilityResultEntity.HeightPassed = suitabilityResult.Height.Passed;
				suitabilityResultEntity.Height = suitabilityResult.Height.Value;
				suitabilityResultEntity.HeightFailureReason = (short)suitabilityResult.Height.FailureReason;
			}

			if (suitabilityResult.TheoreticalPlatesN != null)
			{
				suitabilityResultEntity.TheoreticalPlatesNPassed = suitabilityResult.TheoreticalPlatesN.Passed;
				suitabilityResultEntity.TheoreticalPlatesN = suitabilityResult.TheoreticalPlatesN.Value;
				suitabilityResultEntity.TheoreticalPlatesNFailureReason = (short)suitabilityResult.TheoreticalPlatesN.FailureReason;
			}

			if (suitabilityResult.TheoreticalPlatesNTan != null)
			{
				suitabilityResultEntity.TheoreticalPlatesNTanPassed = suitabilityResult.TheoreticalPlatesNTan.Passed;
				suitabilityResultEntity.TheoreticalPlatesNTan = suitabilityResult.TheoreticalPlatesNTan.Value;
				suitabilityResultEntity.TheoreticalPlatesNTanFailureReason = (short)suitabilityResult.TheoreticalPlatesNTan.FailureReason;
			}

			if (suitabilityResult.TailingFactorSymmetry != null)
			{
				suitabilityResultEntity.TailingFactorSymmetryPassed = suitabilityResult.TailingFactorSymmetry.Passed;
				suitabilityResultEntity.TailingFactorSymmetry = suitabilityResult.TailingFactorSymmetry.Value;
				suitabilityResultEntity.TailingFactorSymmetryFailureReason = (short)suitabilityResult.TailingFactorSymmetry.FailureReason;
			}

			if (suitabilityResult.RelativeRetention != null)
			{
				suitabilityResultEntity.RelativeRetentionPassed = suitabilityResult.RelativeRetention.Passed;
				suitabilityResultEntity.RelativeRetention = suitabilityResult.RelativeRetention.Value;
				suitabilityResultEntity.RelativeRetentionFailureReason = (short)suitabilityResult.RelativeRetention.FailureReason;
			}

            if (suitabilityResult.RelativeRetentionTime != null)
            {
                suitabilityResultEntity.RelativeRetentionTimePassed = suitabilityResult.RelativeRetentionTime.Passed;
                suitabilityResultEntity.RelativeRetentionTime = suitabilityResult.RelativeRetentionTime.Value;
                suitabilityResultEntity.RelativeRetentionTimeFailureReason = (short)suitabilityResult.RelativeRetentionTime.FailureReason;
            }

            if (suitabilityResult.RetentionTime != null)
            {
                suitabilityResultEntity.RetentionTimePassed = suitabilityResult.RetentionTime.Passed;
                suitabilityResultEntity.RetentionTime = suitabilityResult.RetentionTime.Value;
                suitabilityResultEntity.RetentionTimeFailureReason = (short)suitabilityResult.RetentionTime.FailureReason;
            }

            if (suitabilityResult.CapacityFactorKPrime != null)
			{
				suitabilityResultEntity.CapacityFactorKPrimePassed = suitabilityResult.CapacityFactorKPrime.Passed;
				suitabilityResultEntity.CapacityFactorKPrime = suitabilityResult.CapacityFactorKPrime.Value;
				suitabilityResultEntity.CapacityFactorKPrimeFailureReason = (short)suitabilityResult.CapacityFactorKPrime.FailureReason;
			}

			if (suitabilityResult.Resolution != null)
			{
				suitabilityResultEntity.ResolutionPassed = suitabilityResult.Resolution.Passed;
				suitabilityResultEntity.Resolution = suitabilityResult.Resolution.Value;
				suitabilityResultEntity.ResolutionFailureReason = (short)suitabilityResult.Resolution.FailureReason;
			}

			if (suitabilityResult.UspResolution != null)
			{
				suitabilityResultEntity.UspResolutionPassed = suitabilityResult.UspResolution.Passed;
				suitabilityResultEntity.UspResolution = suitabilityResult.UspResolution.Value;
				suitabilityResultEntity.UspResolutionFailureReason = (short)suitabilityResult.UspResolution.FailureReason;
			}

			if (suitabilityResult.SignalToNoise != null)
			{
				suitabilityResultEntity.SignalToNoisePassed = suitabilityResult.SignalToNoise.Passed;
				suitabilityResultEntity.SignalToNoise = suitabilityResult.SignalToNoise.Value;
				suitabilityResultEntity.SignalToNoiseFailureReason = (short)suitabilityResult.SignalToNoise.FailureReason;
			}

			if (suitabilityResult.PeakWidthAtBase != null)
			{
				suitabilityResultEntity.PeakWidthAtBasePassed = suitabilityResult.PeakWidthAtBase.Passed;
				suitabilityResultEntity.PeakWidthAtBase = suitabilityResult.PeakWidthAtBase.Value;
				suitabilityResultEntity.PeakWidthAtBaseFailureReason = (short)suitabilityResult.PeakWidthAtBase.FailureReason;
			}

			if (suitabilityResult.PeakWidthAt5Pct != null)
			{
				suitabilityResultEntity.PeakWidthAt5PctPassed = suitabilityResult.PeakWidthAt5Pct.Passed;
				suitabilityResultEntity.PeakWidthAt5Pct = suitabilityResult.PeakWidthAt5Pct.Value;
				suitabilityResultEntity.PeakWidthAt5PctFailureReason = (short)suitabilityResult.PeakWidthAt5Pct.FailureReason;
			}

			if (suitabilityResult.PeakWidthAt10Pct != null)
			{
				suitabilityResultEntity.PeakWidthAt10PctPassed = suitabilityResult.PeakWidthAt10Pct.Passed;
				suitabilityResultEntity.PeakWidthAt10Pct = suitabilityResult.PeakWidthAt10Pct.Value;
				suitabilityResultEntity.PeakWidthAt10PctFailureReason = (short)suitabilityResult.PeakWidthAt10Pct.FailureReason;
			}

			if (suitabilityResult.PeakWidthAt50Pct != null)
			{
				suitabilityResultEntity.PeakWidthAt50PctPassed = suitabilityResult.PeakWidthAt50Pct.Passed;
				suitabilityResultEntity.PeakWidthAt50Pct = suitabilityResult.PeakWidthAt50Pct.Value;
				suitabilityResultEntity.PeakWidthAt50PctFailureReason = (short)suitabilityResult.PeakWidthAt50Pct.FailureReason;
			}
		}

		internal static void PopulateSuitabilityResult(SuitabilityResult suitabilityResultEntity,
			ISuitabilityResult suitabilityResult)
		{
			suitabilityResult.PeakGuid = suitabilityResultEntity.PeakGuid;
			suitabilityResult.CompoundGuid = suitabilityResultEntity.CompoundGuid;
			suitabilityResult.PeakName = suitabilityResultEntity.PeakName;
			suitabilityResult.PeakRetentionTime = suitabilityResultEntity.PeakRetentionTime;
			suitabilityResult.Noise = suitabilityResultEntity.Noise;
			suitabilityResult.NoiseStart = suitabilityResultEntity.NoiseStart;
			suitabilityResult.NoiseGapStart = suitabilityResultEntity.NoiseGapStart;
			suitabilityResult.NoiseGapEnd = suitabilityResultEntity.NoiseGapEnd;
			suitabilityResult.NoiseEnd = suitabilityResultEntity.NoiseEnd;
			suitabilityResult.SstFlag = suitabilityResultEntity.SstFlag;

			suitabilityResult.Area = new SuitabilityParameterResult(suitabilityResultEntity.Area,
					(SuitabilityParameterFailureReason)suitabilityResultEntity.AreaFailureReason,
					suitabilityResultEntity.AreaPassed);

			suitabilityResult.Height = new SuitabilityParameterResult(suitabilityResultEntity.Height,
					(SuitabilityParameterFailureReason)suitabilityResultEntity.HeightFailureReason, 
					suitabilityResultEntity.HeightPassed);

            suitabilityResult.TheoreticalPlatesN =
					new SuitabilityParameterResult(suitabilityResultEntity.TheoreticalPlatesN,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.TheoreticalPlatesNFailureReason,
						suitabilityResultEntity.TheoreticalPlatesNPassed);

            suitabilityResult.TheoreticalPlatesNTan =
					new SuitabilityParameterResult(suitabilityResultEntity.TheoreticalPlatesNTan,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.TheoreticalPlatesNTanFailureReason,
						suitabilityResultEntity.TheoreticalPlatesNTanPassed);

            suitabilityResult.TailingFactorSymmetry = new SuitabilityParameterResult(
					suitabilityResultEntity.TailingFactorSymmetry,
					(SuitabilityParameterFailureReason) suitabilityResultEntity.TailingFactorSymmetryFailureReason,
					suitabilityResultEntity.TailingFactorSymmetryPassed);

            suitabilityResult.RelativeRetention =
					new SuitabilityParameterResult(suitabilityResultEntity.RelativeRetention,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.RelativeRetentionFailureReason,
						suitabilityResultEntity.RelativeRetentionPassed);

			suitabilityResult.RelativeRetentionTime =
                    new SuitabilityParameterResult(suitabilityResultEntity.RelativeRetentionTime,
                        (SuitabilityParameterFailureReason)suitabilityResultEntity.RelativeRetentionTimeFailureReason,
                        suitabilityResultEntity.RelativeRetentionTimePassed);

            suitabilityResult.RetentionTime =
                new SuitabilityParameterResult(suitabilityResultEntity.RetentionTime,
                    (SuitabilityParameterFailureReason)suitabilityResultEntity.RetentionTimeFailureReason,
                    suitabilityResultEntity.RetentionTimePassed);

            suitabilityResult.CapacityFactorKPrime =
					new SuitabilityParameterResult(suitabilityResultEntity.CapacityFactorKPrime,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.CapacityFactorKPrimeFailureReason,
						suitabilityResultEntity.CapacityFactorKPrimePassed);

			suitabilityResult.Resolution = new SuitabilityParameterResult(suitabilityResultEntity.Resolution,
					(SuitabilityParameterFailureReason)suitabilityResultEntity.ResolutionFailureReason,
					suitabilityResultEntity.ResolutionPassed);

			suitabilityResult.UspResolution = new SuitabilityParameterResult(suitabilityResultEntity.UspResolution,
					(SuitabilityParameterFailureReason)suitabilityResultEntity.UspResolutionFailureReason, 
					suitabilityResultEntity.UspResolutionPassed);

			suitabilityResult.SignalToNoise = new SuitabilityParameterResult(suitabilityResultEntity.SignalToNoise,
					(SuitabilityParameterFailureReason)suitabilityResultEntity.SignalToNoiseFailureReason, 
					suitabilityResultEntity.SignalToNoisePassed);

			suitabilityResult.PeakWidthAtBase =
					new SuitabilityParameterResult(suitabilityResultEntity.PeakWidthAtBase,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.PeakWidthAtBaseFailureReason,
						suitabilityResultEntity.PeakWidthAtBasePassed);

			suitabilityResult.PeakWidthAt5Pct =
					new SuitabilityParameterResult(suitabilityResultEntity.PeakWidthAt5Pct,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.PeakWidthAt5PctFailureReason,
						suitabilityResultEntity.PeakWidthAt5PctPassed);

			suitabilityResult.PeakWidthAt10Pct =
					new SuitabilityParameterResult(suitabilityResultEntity.PeakWidthAt10Pct,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.PeakWidthAt10PctFailureReason,
						suitabilityResultEntity.PeakWidthAt10PctPassed);

			suitabilityResult.PeakWidthAt50Pct =
					new SuitabilityParameterResult(suitabilityResultEntity.PeakWidthAt50Pct,
						(SuitabilityParameterFailureReason)suitabilityResultEntity.PeakWidthAt50PctFailureReason,
						suitabilityResultEntity.PeakWidthAt50PctPassed);
        }

		internal static void PopulateCompoundSuitabilitySummaryResultEntity(Guid compoundGuid, ICompoundSuitabilitySummaryResults compoundSuitabilitySummaryResults,
			CompoundSuitabilitySummaryResults compoundSuitabilitySummaryResultsEntity)
        {
            compoundSuitabilitySummaryResultsEntity.CompoundGuid = compoundGuid; //compoundSuitabilitySummaryResults.CompoundGuid;
            compoundSuitabilitySummaryResultsEntity.SstFlag = compoundSuitabilitySummaryResults.SstFlag;
			compoundSuitabilitySummaryResultsEntity.AreaAverage = compoundSuitabilitySummaryResults.Area.Average;
			compoundSuitabilitySummaryResultsEntity.AreaStDev = compoundSuitabilitySummaryResults.Area.StDev;
			compoundSuitabilitySummaryResultsEntity.AreaRelativeStdDevPercent = compoundSuitabilitySummaryResults.Area.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.AreaRelativeStdDevPassed = compoundSuitabilitySummaryResults.Area.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.AreaFailureReason = (short)compoundSuitabilitySummaryResults.Area.FailureReason;

			compoundSuitabilitySummaryResultsEntity.HeightAverage = compoundSuitabilitySummaryResults.Height.Average;
			compoundSuitabilitySummaryResultsEntity.HeightStDev = compoundSuitabilitySummaryResults.Height.StDev;
			compoundSuitabilitySummaryResultsEntity.HeightRelativeStdDevPercent = compoundSuitabilitySummaryResults.Height.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.HeightRelativeStdDevPassed = compoundSuitabilitySummaryResults.Height.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.HeightFailureReason = (short)compoundSuitabilitySummaryResults.Height.FailureReason;

			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNAverage = compoundSuitabilitySummaryResults.TheoreticalPlatesN.Average;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNStDev = compoundSuitabilitySummaryResults.TheoreticalPlatesN.StDev;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNRelativeStdDevPercent = compoundSuitabilitySummaryResults.TheoreticalPlatesN.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNRelativeStdDevPassed = compoundSuitabilitySummaryResults.TheoreticalPlatesN.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNFailureReason = (short)compoundSuitabilitySummaryResults.TheoreticalPlatesN.FailureReason;

			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanAverage = compoundSuitabilitySummaryResults.TheoreticalPlatesNTan.Average;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanStDev = compoundSuitabilitySummaryResults.TheoreticalPlatesNTan.StDev;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanRelativeStdDevPercent = compoundSuitabilitySummaryResults.TheoreticalPlatesNTan.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanRelativeStdDevPassed = compoundSuitabilitySummaryResults.TheoreticalPlatesNTan.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanFailureReason = (short)compoundSuitabilitySummaryResults.TheoreticalPlatesNTan.FailureReason;

			compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryAverage = compoundSuitabilitySummaryResults.TailingFactorSymmetry.Average;
			compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryStDev = compoundSuitabilitySummaryResults.TailingFactorSymmetry.StDev;
			compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryRelativeStdDevPercent = compoundSuitabilitySummaryResults.TailingFactorSymmetry.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryRelativeStdDevPassed = compoundSuitabilitySummaryResults.TailingFactorSymmetry.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryFailureReason = (short)compoundSuitabilitySummaryResults.TailingFactorSymmetry.FailureReason;

			compoundSuitabilitySummaryResultsEntity.RelativeRetentionAverage = compoundSuitabilitySummaryResults.RelativeRetention.Average;
			compoundSuitabilitySummaryResultsEntity.RelativeRetentionStDev = compoundSuitabilitySummaryResults.RelativeRetention.StDev;
			compoundSuitabilitySummaryResultsEntity.RelativeRetentionRelativeStdDevPercent = compoundSuitabilitySummaryResults.RelativeRetention.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.RelativeRetentionRelativeStdDevPassed = compoundSuitabilitySummaryResults.RelativeRetention.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.RelativeRetentionFailureReason = (short)compoundSuitabilitySummaryResults.RelativeRetention.FailureReason;

            compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeAverage = compoundSuitabilitySummaryResults.RelativeRetentionTime.Average;
            compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeStDev = compoundSuitabilitySummaryResults.RelativeRetentionTime.StDev;
            compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeRelativeStdDevPercent = compoundSuitabilitySummaryResults.RelativeRetentionTime.RelativeStDevPercent;
            compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeRelativeStdDevPassed = compoundSuitabilitySummaryResults.RelativeRetentionTime.RelativeStDevPassed;
            compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeFailureReason = (short)compoundSuitabilitySummaryResults.RelativeRetentionTime.FailureReason;

            compoundSuitabilitySummaryResultsEntity.RetentionTimeAverage = compoundSuitabilitySummaryResults.RetentionTime.Average;
            compoundSuitabilitySummaryResultsEntity.RetentionTimeStDev = compoundSuitabilitySummaryResults.RetentionTime.StDev;
            compoundSuitabilitySummaryResultsEntity.RetentionTimeRelativeStdDevPercent = compoundSuitabilitySummaryResults.RetentionTime.RelativeStDevPercent;
            compoundSuitabilitySummaryResultsEntity.RetentionTimeRelativeStdDevPassed = compoundSuitabilitySummaryResults.RetentionTime.RelativeStDevPassed;
            compoundSuitabilitySummaryResultsEntity.RetentionTimeFailureReason = (short)compoundSuitabilitySummaryResults.RetentionTime.FailureReason;

            compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeAverage = compoundSuitabilitySummaryResults.CapacityFactorKPrime.Average;
			compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeStDev = compoundSuitabilitySummaryResults.CapacityFactorKPrime.StDev;
			compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeRelativeStdDevPercent = compoundSuitabilitySummaryResults.CapacityFactorKPrime.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeRelativeStdDevPassed = compoundSuitabilitySummaryResults.CapacityFactorKPrime.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeFailureReason = (short)compoundSuitabilitySummaryResults.CapacityFactorKPrime.FailureReason;

			compoundSuitabilitySummaryResultsEntity.ResolutionAverage = compoundSuitabilitySummaryResults.Resolution.Average;
			compoundSuitabilitySummaryResultsEntity.ResolutionStDev = compoundSuitabilitySummaryResults.Resolution.StDev;
			compoundSuitabilitySummaryResultsEntity.ResolutionRelativeStdDevPercent = compoundSuitabilitySummaryResults.Resolution.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.ResolutionRelativeStdDevPassed = compoundSuitabilitySummaryResults.Resolution.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.ResolutionFailureReason = (short)compoundSuitabilitySummaryResults.Resolution.FailureReason;

			compoundSuitabilitySummaryResultsEntity.UspResolutionAverage = compoundSuitabilitySummaryResults.UspResolution.Average;
			compoundSuitabilitySummaryResultsEntity.UspResolutionStDev = compoundSuitabilitySummaryResults.UspResolution.StDev;
			compoundSuitabilitySummaryResultsEntity.UspResolutionRelativeStdDevPercent = compoundSuitabilitySummaryResults.UspResolution.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.UspResolutionRelativeStdDevPassed = compoundSuitabilitySummaryResults.UspResolution.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.UspResolutionFailureReason = (short)compoundSuitabilitySummaryResults.UspResolution.FailureReason;

			compoundSuitabilitySummaryResultsEntity.SignalToNoiseAverage = compoundSuitabilitySummaryResults.SignalToNoise.Average;
			compoundSuitabilitySummaryResultsEntity.SignalToNoiseStDev = compoundSuitabilitySummaryResults.SignalToNoise.StDev;
			compoundSuitabilitySummaryResultsEntity.SignalToNoiseRelativeStdDevPercent = compoundSuitabilitySummaryResults.SignalToNoise.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.SignalToNoiseRelativeStdDevPassed = compoundSuitabilitySummaryResults.SignalToNoise.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.SignalToNoiseFailureReason = (short)compoundSuitabilitySummaryResults.SignalToNoise.FailureReason;

			compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseAverage = compoundSuitabilitySummaryResults.PeakWidthAtBase.Average;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseStDev = compoundSuitabilitySummaryResults.PeakWidthAtBase.StDev;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseRelativeStdDevPercent = compoundSuitabilitySummaryResults.PeakWidthAtBase.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseRelativeStdDevPassed = compoundSuitabilitySummaryResults.PeakWidthAtBase.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseFailureReason = (short)compoundSuitabilitySummaryResults.PeakWidthAtBase.FailureReason;

			compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctAverage = compoundSuitabilitySummaryResults.PeakWidthAt5Pct.Average;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctStDev = compoundSuitabilitySummaryResults.PeakWidthAt5Pct.StDev;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctRelativeStdDevPercent = compoundSuitabilitySummaryResults.PeakWidthAt5Pct.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctRelativeStdDevPassed = compoundSuitabilitySummaryResults.PeakWidthAt5Pct.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctFailureReason = (short)compoundSuitabilitySummaryResults.PeakWidthAt5Pct.FailureReason;

			compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctAverage = compoundSuitabilitySummaryResults.PeakWidthAt10Pct.Average;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctStDev = compoundSuitabilitySummaryResults.PeakWidthAt10Pct.StDev;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctRelativeStdDevPercent = compoundSuitabilitySummaryResults.PeakWidthAt10Pct.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctRelativeStdDevPassed = compoundSuitabilitySummaryResults.PeakWidthAt10Pct.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctFailureReason = (short)compoundSuitabilitySummaryResults.PeakWidthAt10Pct.FailureReason;

			compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctAverage = compoundSuitabilitySummaryResults.PeakWidthAt50Pct.Average;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctStDev = compoundSuitabilitySummaryResults.PeakWidthAt50Pct.StDev;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctRelativeStdDevPercent = compoundSuitabilitySummaryResults.PeakWidthAt50Pct.RelativeStDevPercent;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctRelativeStdDevPassed = compoundSuitabilitySummaryResults.PeakWidthAt50Pct.RelativeStDevPassed;
			compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctFailureReason = (short)compoundSuitabilitySummaryResults.PeakWidthAt50Pct.FailureReason;
		}

		internal static void PopulateCompoundSuitabilitySummaryResults(CompoundSuitabilitySummaryResults compoundSuitabilitySummaryResultsEntity,
			ICompoundSuitabilitySummaryResults compoundSuitabilitySummaryResults)
		{
			compoundSuitabilitySummaryResults.CompoundGuid = compoundSuitabilitySummaryResultsEntity.CompoundGuid;
			compoundSuitabilitySummaryResults.SstFlag = compoundSuitabilitySummaryResultsEntity.SstFlag;

			compoundSuitabilitySummaryResults.Area = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.AreaRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.AreaStDev,
				Average = compoundSuitabilitySummaryResultsEntity.AreaAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.AreaRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.AreaFailureReason
			};
			compoundSuitabilitySummaryResults.Height = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.HeightRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.HeightStDev,
				Average = compoundSuitabilitySummaryResultsEntity.HeightAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.HeightRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.HeightFailureReason
			};
			compoundSuitabilitySummaryResults.TheoreticalPlatesNTan = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanStDev,
				Average = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNTanFailureReason
			};
			compoundSuitabilitySummaryResults.TheoreticalPlatesN = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNStDev,
				Average = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.TheoreticalPlatesNFailureReason
			};
			compoundSuitabilitySummaryResults.TailingFactorSymmetry = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryStDev,
				Average = compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.TailingFactorSymmetryFailureReason
			};
			compoundSuitabilitySummaryResults.RelativeRetention = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.RelativeRetentionRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.RelativeRetentionStDev,
				Average = compoundSuitabilitySummaryResultsEntity.RelativeRetentionAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.RelativeRetentionRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.RelativeRetentionFailureReason
			};
            compoundSuitabilitySummaryResults.RelativeRetentionTime = new SummaryResult()
            {
                RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeRelativeStdDevPercent,
                StDev = compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeStDev,
                Average = compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeAverage,
                RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeRelativeStdDevPassed,
                FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.RelativeRetentionTimeFailureReason
            };
            compoundSuitabilitySummaryResults.RetentionTime = new SummaryResult()
            {
                RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.RetentionTimeRelativeStdDevPercent,
                StDev = compoundSuitabilitySummaryResultsEntity.RetentionTimeStDev,
                Average = compoundSuitabilitySummaryResultsEntity.RetentionTimeAverage,
                RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.RetentionTimeRelativeStdDevPassed,
                FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.RetentionTimeFailureReason
            };
            compoundSuitabilitySummaryResults.CapacityFactorKPrime = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeStDev,
				Average = compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.CapacityFactorKPrimeFailureReason
			};
			compoundSuitabilitySummaryResults.Resolution = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.ResolutionRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.ResolutionStDev,
				Average = compoundSuitabilitySummaryResultsEntity.ResolutionAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.ResolutionRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.ResolutionFailureReason
			};
			compoundSuitabilitySummaryResults.UspResolution = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.UspResolutionRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.UspResolutionStDev,
				Average = compoundSuitabilitySummaryResultsEntity.UspResolutionAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.UspResolutionRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.UspResolutionFailureReason
			};
			compoundSuitabilitySummaryResults.SignalToNoise = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.SignalToNoiseRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.SignalToNoiseStDev,
				Average = compoundSuitabilitySummaryResultsEntity.SignalToNoiseAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.SignalToNoiseRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.SignalToNoiseFailureReason
			};
			compoundSuitabilitySummaryResults.PeakWidthAtBase = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseStDev,
				Average = compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.PeakWidthAtBaseFailureReason
			};
			compoundSuitabilitySummaryResults.PeakWidthAt5Pct = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctStDev,
				Average = compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.PeakWidthAt5PctFailureReason
			};
			compoundSuitabilitySummaryResults.PeakWidthAt10Pct = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctStDev,
				Average = compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.PeakWidthAt10PctFailureReason
			};
			compoundSuitabilitySummaryResults.PeakWidthAt50Pct = new SummaryResult()
			{
				RelativeStDevPercent = compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctRelativeStdDevPercent,
				StDev = compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctStDev,
				Average = compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctAverage,
				RelativeStDevPassed = compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctRelativeStdDevPassed,
				FailureReason = (SuitabilityParameterFailureReason)compoundSuitabilitySummaryResultsEntity.PeakWidthAt50PctFailureReason
			};
        }

		internal static void PopulateChromatogramSettingInfo(IChromatogramSetting chromatogramSetting,
			ChromatogramSetting chromatogramSettingEntity)
		{
			if (chromatogramSetting.CreatedByUser == null)
				chromatogramSetting.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (chromatogramSetting.ModifiedByUser == null)
				chromatogramSetting.ModifiedByUser = DomainFactory.Create<IUserInfo>();
			try
			{
				if (chromatogramSettingEntity != null)
				{
					chromatogramSetting.ConfigurePeakLabels = chromatogramSettingEntity.ConfigurePeakLabels;
					chromatogramSetting.IsOrientationVertical = chromatogramSettingEntity.IsOrientationVertical;
					chromatogramSetting.IsRescalePlotSignalToCustom = chromatogramSettingEntity.IsRescalePlotSignalToCustom;
					chromatogramSetting.IsRescalePlotSignalToFull = chromatogramSettingEntity.IsRescalePlotSignalToFull;
					chromatogramSetting.IsRescalePlotSignalToMaxY = chromatogramSettingEntity.IsRescalePlotSignalToMaxY;
					chromatogramSetting.IsRescalePlottimeFull = chromatogramSettingEntity.IsRescalePlottimeFull;
					chromatogramSetting.IsSignalUnitInUv = chromatogramSettingEntity.IsSignalUnitInUv;
					chromatogramSetting.IsTimeUnitInMinute = chromatogramSettingEntity.IsTimeUnitInMinute;
					chromatogramSetting.RescalePlotSignalFrom = chromatogramSettingEntity.RescalePlotSignalFrom;
					chromatogramSetting.RescalePlotSignalTo = chromatogramSettingEntity.RescalePlotSignalTo;
					chromatogramSetting.RescalePlotTimeFrom = chromatogramSettingEntity.RescalePlotTimeFrom;
					chromatogramSetting.RescalePlotTimeTo = chromatogramSettingEntity.RescalePlotTimeTo;
					chromatogramSetting.CreatedByUser.UserId = chromatogramSettingEntity.CreatedUserId;
					chromatogramSetting.ModifiedByUser.UserId = chromatogramSettingEntity.ModifiedUserId;
					chromatogramSetting.CreatedDateUtc = chromatogramSettingEntity.CreatedDate;
					chromatogramSetting.ModifiedDateUtc = chromatogramSettingEntity.ModifiedDate;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateChromatogramSettingInfo() method", ex);
				throw;
			}
		}
		#region CalibrationMethodAndResults

		internal static void PopulateChromatogramSettingEntity(IChromatogramSetting chromatogramSetting,
			ChromatogramSetting chromatogramSettingEntity)
		{
			if (chromatogramSetting.CreatedByUser == null)
				chromatogramSetting.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (chromatogramSetting.ModifiedByUser == null)
				chromatogramSetting.ModifiedByUser = DomainFactory.Create<IUserInfo>();
			try
			{
				chromatogramSettingEntity.ConfigurePeakLabels = chromatogramSetting.ConfigurePeakLabels;
				chromatogramSettingEntity.IsOrientationVertical = chromatogramSetting.IsOrientationVertical;
				chromatogramSettingEntity.IsRescalePlotSignalToCustom = chromatogramSetting.IsRescalePlotSignalToCustom;
				chromatogramSettingEntity.IsRescalePlotSignalToFull = chromatogramSetting.IsRescalePlotSignalToFull;
				chromatogramSettingEntity.IsRescalePlotSignalToMaxY = chromatogramSetting.IsRescalePlotSignalToMaxY;
				chromatogramSettingEntity.IsRescalePlottimeFull = chromatogramSetting.IsRescalePlottimeFull;
				chromatogramSettingEntity.IsSignalUnitInUv = chromatogramSetting.IsSignalUnitInUv;
				chromatogramSettingEntity.IsTimeUnitInMinute = chromatogramSetting.IsTimeUnitInMinute;
				chromatogramSettingEntity.RescalePlotSignalFrom = chromatogramSetting.RescalePlotSignalFrom;
				chromatogramSettingEntity.RescalePlotSignalTo = chromatogramSetting.RescalePlotSignalTo;
				chromatogramSettingEntity.RescalePlotTimeFrom = chromatogramSetting.RescalePlotTimeFrom;
				chromatogramSettingEntity.RescalePlotTimeTo = chromatogramSetting.RescalePlotTimeTo;
				chromatogramSettingEntity.CreatedUserId = chromatogramSetting.CreatedByUser.UserId;
				chromatogramSettingEntity.ModifiedUserId = chromatogramSetting.ModifiedByUser.UserId;
				chromatogramSettingEntity.CreatedDate = chromatogramSetting.CreatedDateUtc;
				chromatogramSettingEntity.ModifiedDate = chromatogramSetting.ModifiedDateUtc;
			}

			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateChromatogramSettingEntity() method", ex);
				throw;
			}
		}

		internal static void PopulateReportTemplate(ReportTemplate entity, IReportTemplate template)
		{
			Log.Info($"PopulateReportTemplate() called");
            if (template.CreatedByUser == null)
                template.CreatedByUser = DomainFactory.Create<IUserInfo>();
            if (template.ModifiedByUser == null)
                template.ModifiedByUser = DomainFactory.Create<IUserInfo>();
            try
			{
				template.Id = entity.Id;
				template.Name = entity.Name;
				template.Category = entity.Category.ToReportTemplateType();
				template.ModifiedDate = entity.ModifiedDate;
                template.ModifiedByUser.UserId = entity.ModifiedUserId;
                template.ModifiedByUser.UserFullName = entity.ModifiedUserName;
                template.CreatedDate = entity.CreatedDate;
                template.CreatedByUser.UserId = entity.CreatedUserId;
                template.CreatedByUser.UserFullName = entity.CreatedUserName;
                template.IsDefault = entity.IsDefault;
                template.ReviewApproveState = (ReviewApproveState)entity.ReviewApproveState;

                if (entity.Content != null)
				{
					template.Content = new MemoryStream(entity.Content);
				}
                if(entity.Config != null)
                {
                    template.Config = new MemoryStream(entity.Config);
                }
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateReportTemplate() method", ex);
				throw;
			}
		}

		internal static void PopulateReportTemplateEntity(IReportTemplate template, ReportTemplate entity)
		{
			Log.Info($"PopulateReportTemplateEntity() called");
			try
			{
				entity.Id = template.Id;
				entity.Name = template.Name;
				entity.Category = template.Category.GetDescription();
				entity.ModifiedDate = template.ModifiedDate;
                entity.ModifiedUserId = template.ModifiedByUser.UserId;
                entity.ModifiedUserName = template.ModifiedByUser.UserFullName;
                entity.CreatedDate = template.CreatedDate;
                entity.CreatedUserId = template.CreatedByUser.UserId;
                entity.CreatedUserName = template.CreatedByUser.UserFullName;
                entity.ProjectId = template.ProjectId;
				entity.IsGlobal = template.IsGlobal;
				entity.IsDefault = template.IsDefault;
				entity.ReviewApproveState = (short)template.ReviewApproveState;
                if (template.Content == null)
				{
					entity.Content = null;
				}
				else
				{
					byte[] buffer = new byte[template.Content.Length];
					template.Content.Position = 0;
					template.Content.Read(buffer, 0, Convert.ToInt32(template.Content.Length)); //entity.Value;
					entity.Content = buffer;
				}
                if (template.Config == null)
                {
                    entity.Config = null;
                }
                else
                {
                    byte[] buffer = new byte[template.Config.Length];
                    template.Config.Position = 0;
                    template.Config.Read(buffer, 0, Convert.ToInt32(template.Config.Length)); //entity.Value;
                    entity.Config = buffer;
                }
            }
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateReportTemplateEntity() method", ex);
				throw;
			}
		}

		internal static void PopulateCompoundCalibrationResultsEntity(ICompoundCalibrationResults compoundCalibrationResults,
			CompoundCalibrationResults compoundCalibrationResultsEntity)
		{
			Log.Info($"PopulateCompoundCalibrationResultsEntity() called");
			try
			{
				compoundCalibrationResultsEntity.NotEnoughLevelsFoundError = compoundCalibrationResults.NotEnoughLevelsFoundError;
				compoundCalibrationResultsEntity.InvalidAmountError = compoundCalibrationResults.InvalidAmountError;
				compoundCalibrationResultsEntity.Guid = compoundCalibrationResults.CompoundGuid;
				//compoundCalibrationResultsEntity.Name = compoundCalibrationResults.Name; //ASK Scott to delete
				//compoundCalibrationResultsEntity.ChannelIndex = compoundCalibrationResults.ChannelIndex;
				compoundCalibrationResultsEntity.ConfLimitTestResult = compoundCalibrationResults.ConfLimitTestResult;

				if (compoundCalibrationResults.RegressionEquation != null)
				{
					compoundCalibrationResultsEntity.RegressionType = (int)compoundCalibrationResults.RegressionEquation.RegressionType;
					compoundCalibrationResultsEntity.Coefficients = compoundCalibrationResults.RegressionEquation.Coefficients;

					if (compoundCalibrationResults.RegressionEquation.Coefficients != null)
					{
						foreach (var coefficient in compoundCalibrationResults.RegressionEquation.Coefficients)
						{
							CompCalibResultCoefficient coefficientEntity = new CompCalibResultCoefficient();
							coefficientEntity.Coefficients = coefficient;
							compoundCalibrationResultsEntity.CompCalibResultCoefficients.Add(coefficientEntity);
						}
					}

					compoundCalibrationResultsEntity.RSquare = compoundCalibrationResults.RegressionEquation.RSquare;
					compoundCalibrationResultsEntity.RelativeStandardErrorValue = compoundCalibrationResults.RegressionEquation.RelativeStandardErrorValue;
					compoundCalibrationResultsEntity.RelativeStandardDeviationPercent = compoundCalibrationResults.RegressionEquation.RelativeStandardDeviationPercent;
					compoundCalibrationResultsEntity.CorrelationCoefficient = compoundCalibrationResults.RegressionEquation.CorrelationCoefficient;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateCompoundCalibrationResultsEntity()", ex);
				throw;
			}
		}
		private static void PopulateCalibrationPointResponseEntity(ICalibrationPointResponse calibrationPointResponse, CalibrationPointResponse calibrationPointResponseEntity)
		{
			Log.Info($"PopulateCalibrationPointResponseEntity() called");
			try
			{
				calibrationPointResponseEntity.QuantifyUsingArea = calibrationPointResponse.QuantifyUsingArea;
				calibrationPointResponseEntity.UseInternalStandard = calibrationPointResponse.UseInternalStandard;
				calibrationPointResponseEntity.Area = calibrationPointResponse.Area;
				calibrationPointResponseEntity.AreaRatio = calibrationPointResponse.AreaRatio;
				calibrationPointResponseEntity.Height = calibrationPointResponse.Height;
                calibrationPointResponseEntity.PeakNotFoundError = calibrationPointResponse.PeakNotFoundError;
                calibrationPointResponseEntity.InternalStandardPeakNotFoundError = calibrationPointResponse.InternalStandardPeakNotFoundError;
                calibrationPointResponseEntity.HeightRatio = calibrationPointResponse.HeightRatio;
				calibrationPointResponseEntity.Excluded = calibrationPointResponse.Excluded;
				calibrationPointResponseEntity.BatchRunGuid = calibrationPointResponse.BatchRunGuid;
				calibrationPointResponseEntity.External = calibrationPointResponse.External;
				calibrationPointResponseEntity.PeakAreaPercentage = calibrationPointResponse.PeakAreaPercentage;
				calibrationPointResponseEntity.PointCalibrationFactor = calibrationPointResponse.PointCalibrationFactor;
				calibrationPointResponseEntity.InvalidAmountError = calibrationPointResponse.InvalidAmountError;
				calibrationPointResponseEntity.OutlierTestFailed = calibrationPointResponse.OutlierTestFailed;
				calibrationPointResponseEntity.OutlierTestResult = calibrationPointResponse.OutlierTestResult;
				calibrationPointResponseEntity.StandardAmountAdjustmentCoeff = calibrationPointResponse.StandardAmountAdjustmentCoeff;
				calibrationPointResponseEntity.InternalStandardAmountAdjustmentCoeff = calibrationPointResponse.InternalStandardAmountAdjustmentCoeff;
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateCalibrationPointResponseEntity()", ex);
				throw;
			}
		}

		private static void PopulateCompound(Compound compoundEntity, ICompound compound)
		{
			Log.Info($"PopulateCompound() called");
			try
			{
				compound.Guid = compoundEntity.Guid;
				compound.Name = compoundEntity.Name;
				compound.CompoundNumber = compoundEntity.CompoundNumber;
				compound.ProcessingMethodChannelGuid = compoundEntity.ProcessingMethodChannelGuid;
				compound.CompoundType = (CompoundType)compoundEntity.CompoundType;
				compound.UsedForSuitability = compoundEntity.UsedForSuitability;

                if (compound is ICompoundGroup)
				{
					ICompoundGroup compoundGroup = (ICompoundGroup)compound;
					compoundGroup.StartTime = compoundEntity.StartTime.HasValue ? compoundEntity.StartTime.Value : 0.0;
					compoundGroup.EndTime = compoundEntity.EndTime.HasValue ? compoundEntity.EndTime.Value : 0.0;

					foreach (var compoundGuidEntity in compoundEntity.CompoundGuids)
					{
						compoundGroup.CompoundGuids.Add(compoundGuidEntity.CompoundGuid);
					}
				}

				// IdentificationParameters
				compound.IdentificationParameters = DomainFactory.Create<IIdentificationParameters>();
				compound.IdentificationParameters.ExpectedRetentionTime = compoundEntity.ExpectedRetentionTime;
				compound.IdentificationParameters.RetentionTimeWindowAbsolute = compoundEntity.RetentionTimeWindowAbsolute;
				compound.IdentificationParameters.RetentionTimeWindowInPercents = compoundEntity.RetentionTimeWindowInPercents;
				compound.IdentificationParameters.RetTimeWindowStart = compoundEntity.RetTimeWindowStart;
				compound.IdentificationParameters.RetTimeWindowEnd = compoundEntity.RetTimeWindowEnd;
				compound.IdentificationParameters.IsRetTimeReferencePeak = compoundEntity.IsRetTimeReferencePeak;
				compound.IdentificationParameters.RetTimeReferencePeakGuid = compoundEntity.RetTimeReferencePeakGuid;
				compound.IdentificationParameters.RetentionIndex = compoundEntity.RetentionIndex;
				compound.IdentificationParameters.UseClosestPeak = compoundEntity.UseClosestPeak;
				compound.IdentificationParameters.IsIntStdReferencePeak = compoundEntity.IsIntStdReferencePeak;
				compound.IdentificationParameters.IntStdReferenceGuid = compoundEntity.IntStdReferenceGuid;
				compound.IdentificationParameters.Index = compoundEntity.Index;
				compound.IdentificationParameters.IsRrtReferencePeak = compoundEntity.IsRrtReferencePeak;
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateCompound()", ex);
				throw;
			}
		}

		private static void PopulateLevelAmounts(LevelAmount levelAmountsEntity,
			IDictionary<int, double?> levelAmounts)
		{
			if (levelAmounts.ContainsKey(levelAmountsEntity.Level))
			{
				levelAmounts[levelAmountsEntity.Level] = levelAmountsEntity.Amount;
			}
			else
			{
				levelAmounts.Add(levelAmountsEntity.Level, levelAmountsEntity.Amount);
			}
		}

		private static void PopulateCalibrationParameters(Compound compoundEntity, ICalibrationParameters calibrationParameters)
		{
			calibrationParameters.CalibrationFactor = compoundEntity.CalibrationFactor;
			calibrationParameters.CalibrationType = (CompoundCalibrationType)compoundEntity.CalibrationType;
			calibrationParameters.InternalStandard = compoundEntity.InternalStandard;
			calibrationParameters.OriginTreatment = (OriginTreatment)compoundEntity.OriginTreatment;
			calibrationParameters.Purity = compoundEntity.Purity;
			calibrationParameters.QuantifyUsingArea = compoundEntity.QuantifyUsingArea;
			calibrationParameters.ReferenceCompoundGuid = compoundEntity.ReferenceCompoundGuid;
			calibrationParameters.ReferenceInternalStandardGuid = compoundEntity.ReferenceInternalStandardGuid;
			calibrationParameters.Scaling = (CalibrationScaling)compoundEntity.Scaling;
			calibrationParameters.WeightingType = (CalibrationWeightingType)compoundEntity.WeightingType;
			calibrationParameters.InternalStandardAmount = compoundEntity.InternalStandardAmount;
		}
		private static void PopulateCompoundCalibrationResults(CompoundCalibrationResults compoundCalibrationResultsEntity,
			ICompoundCalibrationResults compoundCalibrationResults)
		{
			Log.Info($"PopulateCompoundCalibrationResults() called");
			try
			{
				//compoundCalibrationResults.Name = compoundCalibrationResultsEntity.Name; //ASK Scott to delete
				compoundCalibrationResults.CompoundGuid = compoundCalibrationResultsEntity.Guid;
				//compoundCalibrationResults.ChannelIndex = compoundCalibrationResultsEntity.ChannelIndex;
				compoundCalibrationResults.NotEnoughLevelsFoundError = compoundCalibrationResultsEntity.NotEnoughLevelsFoundError;
				compoundCalibrationResults.InvalidAmountError = compoundCalibrationResultsEntity.InvalidAmountError;
				compoundCalibrationResults.RegressionEquation = DomainFactory.Create<ICalibrationRegressionEquation>();
				compoundCalibrationResults.RegressionEquation.RegressionType = (RegressionType)compoundCalibrationResultsEntity.RegressionType;
				compoundCalibrationResults.RegressionEquation.RSquare = compoundCalibrationResultsEntity.RSquare;
				compoundCalibrationResults.RegressionEquation.RelativeStandardErrorValue = compoundCalibrationResultsEntity.RelativeStandardErrorValue;
				compoundCalibrationResults.ConfLimitTestResult = compoundCalibrationResultsEntity.ConfLimitTestResult;
				compoundCalibrationResults.RegressionEquation.RelativeStandardDeviationPercent = compoundCalibrationResultsEntity.RelativeStandardDeviationPercent;
				compoundCalibrationResults.RegressionEquation.CorrelationCoefficient = compoundCalibrationResultsEntity.CorrelationCoefficient;
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateCompoundCalibrationResults()", ex);
				throw;
			}
		}
		#endregion //CalibrationMethodAndResults

		public static void PopulateCompoundEntity(ICompound compound, Compound compoundEntity)
		{
			Log.Info($"PopulateCompoundEntity() called");
			try
			{
				compoundEntity.Guid = compound.Guid;
				compoundEntity.Name = compound.Name;
				compoundEntity.CompoundNumber = compound.CompoundNumber;
				compoundEntity.ProcessingMethodChannelGuid = compound.ProcessingMethodChannelGuid;
				compoundEntity.CompoundType = (int)compound.CompoundType;
				compoundEntity.UsedForSuitability = compound.UsedForSuitability;

                if (compound is ICompoundGroup)
				{
					compoundEntity.IsCompoundGroup = true;
					ICompoundGroup compoundGroup = (ICompoundGroup)compound;
					compoundEntity.StartTime = compoundGroup.StartTime;
					compoundEntity.EndTime = compoundGroup.EndTime;

					foreach (var compoundGuid in compoundGroup.CompoundGuids)
					{
						CompoundGuids compoundGuidsEntity = new CompoundGuids()
						{
							CompoundGuid = compoundGuid
						};

						compoundEntity.CompoundGuids.Add(compoundGuidsEntity);
					}
				}
				else
				{
					compoundEntity.IsCompoundGroup = false;
				}

				if (compound.IdentificationParameters != null)
				{
					compoundEntity.ExpectedRetentionTime = compound.IdentificationParameters.ExpectedRetentionTime;
					compoundEntity.RetentionTimeWindowAbsolute = compound.IdentificationParameters.RetentionTimeWindowAbsolute;
					compoundEntity.RetentionTimeWindowInPercents = compound.IdentificationParameters.RetentionTimeWindowInPercents;
					compoundEntity.RetTimeWindowStart = compound.IdentificationParameters.RetTimeWindowStart;
					compoundEntity.RetTimeWindowEnd = compound.IdentificationParameters.RetTimeWindowEnd;
					compoundEntity.IsRetTimeReferencePeak = compound.IdentificationParameters.IsRetTimeReferencePeak;
					compoundEntity.RetTimeReferencePeakGuid = compound.IdentificationParameters.RetTimeReferencePeakGuid;
					compoundEntity.RetentionIndex = compound.IdentificationParameters.RetentionIndex;
					compoundEntity.UseClosestPeak = compound.IdentificationParameters.UseClosestPeak;
					compoundEntity.IsIntStdReferencePeak = compound.IdentificationParameters.IsIntStdReferencePeak;
					compoundEntity.IntStdReferenceGuid = compound.IdentificationParameters.IntStdReferenceGuid;
					compoundEntity.Index = compound.IdentificationParameters.Index;
					compoundEntity.IsRrtReferencePeak = compound.IdentificationParameters.IsRrtReferencePeak;
				}

				if (compound.CalibrationParameters != null)
				{
					compoundEntity.CalibrationFactor = compound.CalibrationParameters.CalibrationFactor;
					compoundEntity.Purity = compound.CalibrationParameters.Purity;
					compoundEntity.CalibrationType = (int)compound.CalibrationParameters.CalibrationType;
					compoundEntity.InternalStandard = compound.CalibrationParameters.InternalStandard;
					compoundEntity.OriginTreatment = (int)compound.CalibrationParameters.OriginTreatment;
					compoundEntity.InternalStandardAmount = compound.CalibrationParameters.InternalStandardAmount;
					compoundEntity.QuantifyUsingArea = compound.CalibrationParameters.QuantifyUsingArea;
					compoundEntity.ReferenceCompoundGuid = compound.CalibrationParameters.ReferenceCompoundGuid;
					compoundEntity.ReferenceInternalStandardGuid = compound.CalibrationParameters.ReferenceInternalStandardGuid;
					compoundEntity.Scaling = (int)compound.CalibrationParameters.Scaling;
					compoundEntity.WeightingType = (int)compound.CalibrationParameters.WeightingType;

					// LevelAmounts
					foreach (var levelAmount in compound.CalibrationParameters.LevelAmounts)
					{
						LevelAmount levelAmountEntity = new LevelAmount();
						levelAmountEntity.Amount = levelAmount.Value;
						levelAmountEntity.Level = levelAmount.Key;
						compoundEntity.LevelAmounts.Add(levelAmountEntity);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error("Error occured in PopulateCompoundEntity()", ex);
				throw;
			}

		}
		public static void PopulateCalibrationPointResponseInfo(CalibrationPointResponse calibrationPointResponseProjEntity, ICalibrationPointResponse calibrationPointResponse)
		{
			calibrationPointResponse.Area = calibrationPointResponseProjEntity.Area;
			calibrationPointResponse.AreaRatio = calibrationPointResponseProjEntity.AreaRatio;
            calibrationPointResponse.PeakNotFoundError = calibrationPointResponseProjEntity.PeakNotFoundError;
            calibrationPointResponse.InternalStandardPeakNotFoundError = calibrationPointResponseProjEntity.InternalStandardPeakNotFoundError;
            calibrationPointResponse.QuantifyUsingArea = calibrationPointResponseProjEntity.QuantifyUsingArea;
			calibrationPointResponse.UseInternalStandard = calibrationPointResponseProjEntity.UseInternalStandard;
			calibrationPointResponse.Height = calibrationPointResponseProjEntity.Height;
			calibrationPointResponse.HeightRatio = calibrationPointResponseProjEntity.HeightRatio;
			calibrationPointResponse.Excluded = calibrationPointResponseProjEntity.Excluded;
			calibrationPointResponse.BatchRunGuid = calibrationPointResponseProjEntity.BatchRunGuid;
			calibrationPointResponse.External = calibrationPointResponseProjEntity.External;
			calibrationPointResponse.PeakAreaPercentage = calibrationPointResponseProjEntity.PeakAreaPercentage;
			calibrationPointResponse.Level = calibrationPointResponseProjEntity.Level;
			calibrationPointResponse.PointCalibrationFactor = calibrationPointResponseProjEntity.PointCalibrationFactor;
			calibrationPointResponse.InvalidAmountError = calibrationPointResponseProjEntity.InvalidAmountError;
			calibrationPointResponse.OutlierTestFailed = calibrationPointResponseProjEntity.OutlierTestFailed;
			calibrationPointResponse.OutlierTestResult = calibrationPointResponseProjEntity.OutlierTestResult;
			calibrationPointResponse.StandardAmountAdjustmentCoeff = calibrationPointResponseProjEntity.StandardAmountAdjustmentCoeff;
			calibrationPointResponse.InternalStandardAmountAdjustmentCoeff = calibrationPointResponseProjEntity.InternalStandardAmountAdjustmentCoeff;
		}
		public static void PopulateProjectInfo(Project projectEntity, IProjectInfo projectInfo)
		{
			projectInfo.Guid = projectEntity.Guid;
			projectInfo.Name = projectEntity.Name;
			projectInfo.IsEnabled = projectEntity.IsEnabled;
			projectInfo.IsESignatureOn = projectEntity.IsESignatureOn;
			projectInfo.IsReviewApprovalOn = projectEntity.IsReviewApprovalOn;
			projectInfo.IsSecurityOn = projectEntity.IsSecurityOn;

			projectInfo.Description = projectEntity.Description;

			if (projectInfo.CreatedByUser == null)
				projectInfo.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (projectInfo.ModifiedByUser == null)
				projectInfo.ModifiedByUser = DomainFactory.Create<IUserInfo>();

			projectInfo.CreatedByUser.UserId = projectEntity.CreatedUserId;
            projectInfo.CreatedByUser.UserFullName = projectEntity.CreatedUserName;
            projectInfo.CreatedDateUtc = projectEntity.CreatedDate;
			projectInfo.ModifiedByUser.UserId = projectEntity.ModifiedUserId;
            projectInfo.ModifiedByUser.UserFullName = projectEntity.ModifiedUserName;
            projectInfo.ModifiedDateUtc = projectEntity.ModifiedDate;
			projectInfo.StartDateUtc = projectEntity.StartDate;
			projectInfo.EndDateUtc = projectEntity.EndDate;
			projectInfo.IsSecurityOn = projectEntity.IsSecurityOn;
			projectInfo.IsESignatureOn = projectEntity.IsESignatureOn;
			projectInfo.IsReviewApprovalOn = projectEntity.IsReviewApprovalOn;
		}
		public static void PopulateProjectEntity(IProjectInfo projectInfo, Project projectEntity)
		{
			projectEntity.Guid = projectInfo.Guid;
			projectEntity.Name = projectInfo.Name;
			projectEntity.IsEnabled = projectInfo.IsEnabled;
			projectEntity.IsESignatureOn = projectInfo.IsESignatureOn;
			projectEntity.IsReviewApprovalOn = projectInfo.IsReviewApprovalOn;
			projectEntity.IsSecurityOn = projectInfo.IsSecurityOn;
			projectEntity.Description = projectInfo.Description;
			projectEntity.CreatedUserId = projectInfo.CreatedByUser.UserId;
            projectEntity.CreatedUserName = projectInfo.CreatedByUser.UserFullName;
            projectEntity.CreatedDate = projectInfo.CreatedDateUtc;
			projectEntity.ModifiedUserId = projectInfo.ModifiedByUser.UserId;
            projectEntity.ModifiedUserName = projectInfo.ModifiedByUser.UserFullName;
            projectEntity.ModifiedDate = projectInfo.ModifiedDateUtc;
			projectEntity.IsSecurityOn = projectInfo.IsSecurityOn;
			projectEntity.IsESignatureOn = projectInfo.IsESignatureOn;
			projectEntity.IsReviewApprovalOn = projectInfo.IsReviewApprovalOn;
		}
        public static void PopulateBatchRunChannelMap(IChannelMappingItem channelMappingItem, BatchRunChannelMap batchRunChannelMapEntity)
		{
			channelMappingItem.ProcessingMethodChannelGuid = batchRunChannelMapEntity.ProcessingMethodChannelGuid;
			channelMappingItem.BatchRunChannelGuid = batchRunChannelMapEntity.BatchRunChannelGuid;
			channelMappingItem.ProcessingMethodChannelGuid = batchRunChannelMapEntity.ProcessingMethodChannelGuid;
			channelMappingItem.ProcessingMethodGuid = batchRunChannelMapEntity.ProcessingMethodGuid;
			channelMappingItem.OriginalBatchRunGuid = batchRunChannelMapEntity.OriginalBatchRunGuid;
			channelMappingItem.BatchRunGuid = batchRunChannelMapEntity.BatchRunGuid;

			if (batchRunChannelMapEntity.BatchRunChannelDescriptorType == ChromatographicChannelDescriptorType)
			{
				channelMappingItem.BatchRunChannelDescriptor = JsonConverter.JsonConverter.FromJson<IChromatographicChannelDescriptor>(batchRunChannelMapEntity.BatchRunChannelDescriptor);
			}

			if (batchRunChannelMapEntity.XData != null && batchRunChannelMapEntity.YData != null)
			{
				var internalChannelMappingItem = channelMappingItem as IInternalChannelMappingItem;
				internalChannelMappingItem.XyData = (batchRunChannelMapEntity.XData, batchRunChannelMapEntity.YData); //dkt copy references
			}
		}

		public static void PopulateBatchRunChannelMapEntity(IChannelMappingItem channelMappingItem, IAnalysisResultSetChromatogramData chromatographyData,
	        BatchRunChannelMap batchRunChannelMapEntity)
		{
			batchRunChannelMapEntity.ProcessingMethodChannelGuid = channelMappingItem.ProcessingMethodChannelGuid;
			batchRunChannelMapEntity.ProcessingMethodGuid = channelMappingItem.ProcessingMethodGuid;
			batchRunChannelMapEntity.BatchRunGuid = channelMappingItem.BatchRunGuid;
			batchRunChannelMapEntity.BatchRunChannelGuid = channelMappingItem.BatchRunChannelGuid;
			batchRunChannelMapEntity.OriginalBatchRunGuid = channelMappingItem.OriginalBatchRunGuid;

			var xyData = chromatographyData.GetXyData(channelMappingItem.BatchRunChannelGuid);
			batchRunChannelMapEntity.XData = xyData.TimeInSeconds.ToArray();
			batchRunChannelMapEntity.YData = xyData.Response.ToArray();

			if (channelMappingItem.BatchRunChannelDescriptor != null &&
				channelMappingItem.BatchRunChannelDescriptor is IChromatographicChannelDescriptor)
			{
				batchRunChannelMapEntity.BatchRunChannelDescriptor = JsonConverter.JsonConverter.ToJson(channelMappingItem.BatchRunChannelDescriptor);
				batchRunChannelMapEntity.BatchRunChannelDescriptorType = ChromatographicChannelDescriptorType;
			}
		}

		public static void PopulateManualOverrideMap(ManualOverrideMap manualOverrideMapEntity, IManualOverrideMappingItem manualOverrideMappingItem)
		{
			manualOverrideMappingItem.BatchRunGuid = manualOverrideMapEntity.BatchRunGuid;
			manualOverrideMappingItem.BatchRunChannelGuid = manualOverrideMapEntity.BatchRunChannelGuid;
		}
		public static void PopulateManualOverrideMapEntity(IManualOverrideMappingItem manualOverrideMappingItem, ManualOverrideMap manualOverrideMapEntity)
		{
			manualOverrideMapEntity.BatchRunGuid = manualOverrideMappingItem.BatchRunGuid;
			manualOverrideMapEntity.BatchRunChannelGuid = manualOverrideMappingItem.BatchRunChannelGuid;
		}
		public static void PopulateCompoundLibraryEntity(Guid libraryGuid, string libraryName, string description, ProjectCompoundLibrary compoundLibraryEntity, IUserInfo userInfo)
		{
			compoundLibraryEntity.CreatedDate = DateTime.UtcNow;
			compoundLibraryEntity.CreatedUserId = userInfo.UserId;
			compoundLibraryEntity.CreatedUserName = userInfo.UserFullName;
			compoundLibraryEntity.ModifiedDate = DateTime.UtcNow;
			compoundLibraryEntity.ModifiedUserId = userInfo.UserId;
			compoundLibraryEntity.ModifiedUserName = userInfo.UserFullName;
			compoundLibraryEntity.LibraryName = libraryName;
			compoundLibraryEntity.Description = description;
            compoundLibraryEntity.LibraryGuid = libraryGuid;
		}
		public static void PopulateCompoundLibraryItemEntity(List<ICompoundLibraryItem> compoundLibraryItems, List<CompoundLibraryItem> compoundLibraryItemEntities)
		{
			foreach (var compoundLibraryItem in compoundLibraryItems)
			{
                CompoundLibraryItem compoundLibraryItemEntity = new CompoundLibraryItem
                {
                    CompoundName = compoundLibraryItem.CompoundName,
                    CompoundGuid = compoundLibraryItem.CompoundGuid,
                    CreatedDate = compoundLibraryItem.CreatedDate,
                    IsBaselineCorrected = compoundLibraryItem.IsBaselineCorrected
                    
                };
                compoundLibraryItemEntities.Add(compoundLibraryItemEntity);
            }
		}

		public static void PopulateCompoundLibraryItemEntity(List<ICompoundLibraryItemContent> compoundLibraryItemContents,
			List<CompoundLibraryItem> compoundLibraryItemEntities)
        {
            for (var index = 0; index < compoundLibraryItemEntities.Count; index++)
            {
                var compoundLibraryItemEntity = compoundLibraryItemEntities[index];
                var compoundLibraryItemContent = compoundLibraryItemContents[index];
                if (compoundLibraryItemContent != null && compoundLibraryItemEntity != null)
                {
                    compoundLibraryItemEntity.EndWavelength = compoundLibraryItemContent.EndWavelength;
                    compoundLibraryItemEntity.RetentionTime = compoundLibraryItemContent.RetentionTime;
                    compoundLibraryItemEntity.BaselineAbsorbances = compoundLibraryItemContent.BaselineAbsorbances?.ToArray();
                    compoundLibraryItemEntity.SpectrumAbsorbances = compoundLibraryItemContent.SpectrumAbsorbances?.ToArray();
                    compoundLibraryItemEntity.StartWavelength = compoundLibraryItemContent.StartWavelength;
                    compoundLibraryItemEntity.Step = compoundLibraryItemContent.Step;
                    
                }
            }
        }
		public static void PopulateCompoundLibrary(ProjectCompoundLibrary compoundLibraryEntity, ICompoundLibrary compoundLibrary)
		{
			compoundLibrary.Name = compoundLibraryEntity.LibraryName;
            compoundLibrary.Guid = compoundLibraryEntity.LibraryGuid;
			compoundLibrary.Description = compoundLibraryEntity.Description;
			compoundLibrary.CreatedTime = compoundLibraryEntity.CreatedDate.ToString();
			compoundLibrary.CreatedDateUtc = compoundLibraryEntity.CreatedDate;
			compoundLibrary.ModifiedTime = compoundLibraryEntity.ModifiedDate.ToString();

			// ModifiedBy
			IUserInfo userInfoModifiedBy = DomainFactory.Create<IUserInfo>();
			userInfoModifiedBy.UserFullName = compoundLibraryEntity.ModifiedUserName;
			userInfoModifiedBy.UserId = compoundLibraryEntity.ModifiedUserId;
			compoundLibrary.ModifiedByUser = userInfoModifiedBy;

			// CreatedBy
			IUserInfo userInfoCreatedBy = DomainFactory.Create<IUserInfo>();
			userInfoCreatedBy.UserFullName = compoundLibraryEntity.CreatedUserName;
			userInfoCreatedBy.UserId = compoundLibraryEntity.CreatedUserId;
			compoundLibrary.CreatedByUser = userInfoCreatedBy;
		}
		public static void PopulateCompoundLibraryItem(CompoundLibraryItem compoundLibraryItemEntity, ICompoundLibraryItem compoundLibraryItem)
		{
			compoundLibraryItem.CompoundName = compoundLibraryItemEntity.CompoundName;
            compoundLibraryItem.CompoundGuid = compoundLibraryItemEntity.CompoundGuid;
			compoundLibraryItem.CreatedDate = compoundLibraryItemEntity.CreatedDate;
            compoundLibraryItem.IsBaselineCorrected = compoundLibraryItemEntity.IsBaselineCorrected;
		}
		public static void PopulateCompoundLibraryItemContent(CompoundLibraryItem compoundLibraryEntity, ICompoundLibraryItemContent compoundLibraryItemContent)
		{
			compoundLibraryItemContent.RetentionTime = compoundLibraryEntity.RetentionTime;
			//compoundLibraryItemContent.BaselineAbsorbances = compoundLibraryEntity.BaselineAbsorbances;
			compoundLibraryItemContent.EndWavelength = compoundLibraryEntity.EndWavelength;
			compoundLibraryItemContent.SpectrumAbsorbances = compoundLibraryEntity.SpectrumAbsorbances;
			compoundLibraryItemContent.StartWavelength = compoundLibraryEntity.StartWavelength;
			compoundLibraryItemContent.Step = compoundLibraryEntity.Step;
		}
		public static void PopulateESignaturePointInfo(ESignaturePoint eSignaturePointEntity, IESignaturePointInfo eSignaturePointContent)
		{
			if (eSignaturePointEntity == null) throw new ArgumentNullException(nameof(eSignaturePointEntity));
			if (eSignaturePointContent == null) throw new ArgumentNullException(nameof(eSignaturePointContent));

			eSignaturePointContent.Guid = eSignaturePointEntity.Guid;
			eSignaturePointContent.Name = eSignaturePointEntity.Name;
			eSignaturePointContent.ModuleName = eSignaturePointEntity.ModuleName;
			eSignaturePointContent.DisplayOrder = eSignaturePointEntity.DisplayOrder;
			eSignaturePointContent.IsUseAuth = eSignaturePointEntity.IsUseAuth;
			eSignaturePointContent.IsCustomReason = eSignaturePointEntity.IsCustomReason;
			eSignaturePointContent.IsPredefinedReason = eSignaturePointEntity.IsPredefinedReason;
			eSignaturePointContent.PredefineReasons = string.IsNullOrWhiteSpace(eSignaturePointEntity.Reasons) ?
				new List<string>() : JsonConvert.DeserializeObject<List<string>>(eSignaturePointEntity.Reasons);

			if (eSignaturePointContent.CreatedByUser == null)
				eSignaturePointContent.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (eSignaturePointContent.ModifiedByUser == null)
				eSignaturePointContent.ModifiedByUser = DomainFactory.Create<IUserInfo>();

			eSignaturePointContent.CreatedByUser.UserId = eSignaturePointEntity.CreatedUserId;
			eSignaturePointContent.CreatedDateUtc = eSignaturePointEntity.CreatedDate;
			eSignaturePointContent.ModifiedByUser.UserId = eSignaturePointEntity.ModifiedUserId;
			eSignaturePointContent.ModifiedDateUtc = eSignaturePointEntity.ModifiedDate;
		}
		public static void PopulateESignaturePointEntity(ESignaturePoint eSignaturePointEntity, IESignaturePointInfo eSignaturePointContent)
		{
			if (eSignaturePointEntity == null) throw new ArgumentNullException(nameof(eSignaturePointEntity));
			if (eSignaturePointContent == null) throw new ArgumentNullException(nameof(eSignaturePointContent));

			eSignaturePointEntity.Guid = eSignaturePointContent.Guid;
			eSignaturePointEntity.Name = eSignaturePointContent.Name;
			eSignaturePointEntity.ModuleName = eSignaturePointContent.ModuleName;
			eSignaturePointEntity.DisplayOrder = eSignaturePointContent.DisplayOrder;
			eSignaturePointEntity.IsUseAuth = eSignaturePointContent.IsUseAuth;
			eSignaturePointEntity.IsCustomReason = eSignaturePointContent.IsCustomReason;
			eSignaturePointEntity.IsPredefinedReason = eSignaturePointContent.IsPredefinedReason;
			if (eSignaturePointContent.PredefineReasons == null)
				eSignaturePointContent.PredefineReasons = new List<string>();
			eSignaturePointEntity.Reasons = JsonConvert.SerializeObject(eSignaturePointContent.PredefineReasons);

			eSignaturePointEntity.CreatedUserId = eSignaturePointContent.CreatedByUser.UserId;
			eSignaturePointEntity.CreatedDate = eSignaturePointContent.CreatedDateUtc;
			eSignaturePointEntity.ModifiedUserId = eSignaturePointContent.ModifiedByUser.UserId;
			eSignaturePointEntity.ModifiedDate = eSignaturePointContent.ModifiedDateUtc;

		}
		public static void PopulateApprovalReviewItemInfo(ApprovalReviewItem approvalReviewItemEntity, IApprovalReviewItemInfo approvalReviewContent)
		{
			if (approvalReviewItemEntity == null) throw new ArgumentNullException(nameof(approvalReviewItemEntity));
			if (approvalReviewContent == null) throw new ArgumentNullException(nameof(approvalReviewContent));

			approvalReviewContent.Guid = approvalReviewItemEntity.Guid;
			approvalReviewContent.Name = approvalReviewItemEntity.Name;
			approvalReviewContent.DisplayOrder = approvalReviewItemEntity.DisplayOrder;
			approvalReviewContent.IsApprovalReviewOn = approvalReviewItemEntity.IsApprovalReviewOn;
			approvalReviewContent.IsSubmitReviewApprove = approvalReviewItemEntity.IsSubmitReviewApprove;
			approvalReviewContent.IsSubmitApprove = approvalReviewItemEntity.IsSubmitApprove;

			if (approvalReviewContent.CreatedByUser == null)
				approvalReviewContent.CreatedByUser = DomainFactory.Create<IUserInfo>();
			if (approvalReviewContent.ModifiedByUser == null)
				approvalReviewContent.ModifiedByUser = DomainFactory.Create<IUserInfo>();

			approvalReviewContent.CreatedByUser.UserId = approvalReviewItemEntity.CreatedUserId;
			approvalReviewContent.CreatedDateUtc = approvalReviewItemEntity.CreatedDate;
			approvalReviewContent.ModifiedByUser.UserId = approvalReviewItemEntity.ModifiedUserId;
			approvalReviewContent.ModifiedDateUtc = approvalReviewItemEntity.ModifiedDate;
		}
		public static void PopulateApprovalReviewEntity(ApprovalReviewItem approvalReviewItemEntity, IApprovalReviewItemInfo approvalReviewContent)
		{
			if (approvalReviewItemEntity == null) throw new ArgumentNullException(nameof(approvalReviewItemEntity));
			if (approvalReviewContent == null) throw new ArgumentNullException(nameof(approvalReviewContent));

			approvalReviewItemEntity.Guid = approvalReviewContent.Guid;
			approvalReviewItemEntity.Name = approvalReviewContent.Name;
			approvalReviewItemEntity.DisplayOrder = approvalReviewContent.DisplayOrder;
			approvalReviewItemEntity.IsApprovalReviewOn = approvalReviewContent.IsApprovalReviewOn;
			approvalReviewItemEntity.IsSubmitReviewApprove = approvalReviewContent.IsSubmitReviewApprove;
			approvalReviewItemEntity.IsSubmitApprove = approvalReviewContent.IsSubmitApprove;

			approvalReviewItemEntity.CreatedUserId = approvalReviewContent.CreatedByUser.UserId;
			approvalReviewItemEntity.CreatedDate = approvalReviewContent.CreatedDateUtc;
			approvalReviewItemEntity.ModifiedUserId = approvalReviewContent.ModifiedByUser.UserId;
			approvalReviewItemEntity.ModifiedDate = approvalReviewContent.ModifiedDateUtc;
		}

		public static void PopulateReviewApproveDataEntity(ReviewApprovableDataEntity dataEntity, IReviewApprovableEntity entity)
		{
			dataEntity.Id = entity.Id;
			dataEntity.ProjectId = entity.ProjectId;
			dataEntity.ProjectName = entity.ProjectName;
			dataEntity.EntityId = entity.EntityId;
			dataEntity.EntityName = entity.EntityName;
			dataEntity.EntityType = entity.EntityType;
			dataEntity.EntityReviewApproveState = (short)entity.EntityReviewApproveState;
			dataEntity.InReviewBy = entity.InReviewBy;
			dataEntity.InReviewByUserId = entity.InReviewByUserId;
			dataEntity.InApproveBy = entity.InApproveBy;
			dataEntity.InApproveByUserId = entity.InApproveByUserId;
			if (entity.ReviewedBy == null || entity.ReviewedBy.Count == 0)
			{
				dataEntity.ReviewedBy = string.Empty;
				dataEntity.ReviewedByUserId = string.Empty;
			}
			else
			{
				dataEntity.ReviewedBy = string.Join(";", entity.ReviewedBy);
				dataEntity.ReviewedByUserId = string.Join(";", entity.ReviewedByUserId);
			}

			if (entity.ApprovedBy == null || entity.ApprovedBy.Count == 0)
			{
				dataEntity.ApprovedBy = string.Empty;
				dataEntity.ApprovedByUserId = string.Empty;
			}
			else
			{
				dataEntity.ApprovedBy = string.Join(";", entity.ApprovedBy);
				dataEntity.ApprovedByUserId = string.Join(";", entity.ApprovedByUserId);
			}
			dataEntity.RejectedBy = entity.RejectedBy;
			dataEntity.RejectedByUserId = entity.RejectedByUserId;
			dataEntity.RecalledBy = entity.RecalledBy;
			dataEntity.RecalledByUserId = entity.RecalledByUserId;
			dataEntity.PostponedBy = entity.PostponedBy;
			dataEntity.PostponedByUserId = entity.PostponedByUserId;
			dataEntity.SubmittedBy = entity.SubmittedBy;
			dataEntity.SubmittedByUserId = entity.SubmittedByUserId;
			dataEntity.LastModifiedBy = entity.LastModifiedBy;
			dataEntity.LastModifiedByUserId = entity.LastModifiedByUserId;
			dataEntity.ReviewedCount = entity.ReviewedCount;
			dataEntity.ApprovedCount = entity.ApprovedCount;
			dataEntity.SubmitTimestamp = entity.SubmitTimestamp.ToUniversalTime();
			dataEntity.ReviewedTimestamp = entity.ReviewedTimestamp?.ToUniversalTime();
			dataEntity.ApprovedTimestamp = entity.ApprovedTimestamp?.ToUniversalTime();
			dataEntity.LastActionTimestamp = entity.LastActionTimestamp.ToUniversalTime();
            dataEntity.DataName = entity.DataName;
            dataEntity.VersionNumber = entity.VersionNumber;
		}

		public static void PopulateReviewApproveEntity(IReviewApprovableEntity entity, ReviewApprovableDataEntity dataEntity)
		{
			entity.Id = dataEntity.Id;
			entity.ProjectId = dataEntity.ProjectId;
			entity.ProjectName = dataEntity.ProjectName;
			entity.EntityId = dataEntity.EntityId;
			entity.EntityName = dataEntity.EntityName;
			entity.EntityType = dataEntity.EntityType;
			entity.EntityReviewApproveState = (ReviewApproveState)dataEntity.EntityReviewApproveState;
			entity.InReviewBy = dataEntity.InReviewBy;
			entity.InReviewByUserId = dataEntity.InReviewByUserId;
			entity.InApproveBy = dataEntity.InApproveBy;
			entity.InApproveByUserId = dataEntity.InApproveByUserId;
			if (string.IsNullOrWhiteSpace(dataEntity.ReviewedBy))
			{
				entity.ReviewedBy = new List<string>();
				entity.ReviewedByUserId = new List<string>();
			}
			else
			{
				entity.ReviewedBy = dataEntity.ReviewedBy.Split(';').ToList();
				entity.ReviewedByUserId = dataEntity.ReviewedByUserId.Split(';').ToList();
			}
			if (string.IsNullOrWhiteSpace(dataEntity.ApprovedBy))
			{
				entity.ApprovedBy = new List<string>();
				entity.ApprovedByUserId = new List<string>();
			}
			else
			{
				entity.ApprovedBy = dataEntity.ApprovedBy.Split(';').ToList();
				entity.ApprovedByUserId = dataEntity.ApprovedByUserId.Split(';').ToList();
			}
			entity.RejectedBy = dataEntity.RejectedBy;
			entity.RejectedByUserId = dataEntity.RejectedByUserId;
			entity.RecalledBy = dataEntity.RecalledBy;
			entity.RecalledByUserId = dataEntity.RecalledByUserId;
			entity.PostponedBy = dataEntity.PostponedBy;
			entity.PostponedByUserId = dataEntity.PostponedByUserId;
			entity.SubmittedBy = dataEntity.SubmittedBy;
			entity.SubmittedByUserId = dataEntity.SubmittedByUserId;
			entity.LastModifiedBy = dataEntity.LastModifiedBy;
			entity.LastModifiedByUserId = dataEntity.LastModifiedByUserId;
			entity.ReviewedCount = dataEntity.ReviewedCount;
			entity.ApprovedCount = dataEntity.ApprovedCount;
			entity.LastActionTimestamp = dataEntity.LastActionTimestamp.ToLocalTime();
			entity.SubmitTimestamp = dataEntity.SubmitTimestamp.ToLocalTime();
			entity.ReviewedTimestamp = dataEntity.ReviewedTimestamp?.ToLocalTime();
			entity.ApprovedTimestamp = dataEntity.ApprovedTimestamp?.ToLocalTime();
			entity.DataName = dataEntity.DataName;
            entity.VersionNumber = dataEntity.VersionNumber;
		}

		public static void PopulateReviewApproveDataEntitySubItem(ReviewApprovableDataEntitySubItem dataEntitySubItem, IReviewApprovableEntitySubItem entitySubItem)
		{
			dataEntitySubItem.Id = entitySubItem.Id;
			dataEntitySubItem.ProjectId = entitySubItem.ProjectId;
			dataEntitySubItem.ProjectName = entitySubItem.ProjectName;
			dataEntitySubItem.EntityReviewApproveId = entitySubItem.EntityReviewApproveId;
			dataEntitySubItem.EntitySubItemId = entitySubItem.EntitySubItemId;
			dataEntitySubItem.EntitySubItemName = entitySubItem.EntitySubItemName;
			dataEntitySubItem.EntitySubItemType = entitySubItem.EntitySubItemType;
			dataEntitySubItem.EntitySubItemReviewApproveState = (short)entitySubItem.EntitySubItemReviewApproveState;
			dataEntitySubItem.EntitySubItemSampleReportTemplate = entitySubItem.EntitySubItemSampleReportTemplate;
			dataEntitySubItem.EntitySubItemSummaryReportGroup = entitySubItem.EntitySubItemSummaryReportGroup;
			dataEntitySubItem.ReviewApproveComment = entitySubItem.ReviewApproveComment;
		}

		public static void PopulateReviewApproveEntitySubItem(IReviewApprovableEntitySubItem entitySubItem, ReviewApprovableDataEntitySubItem dataEntitySubItem)
		{
			entitySubItem.Id = dataEntitySubItem.Id;
			entitySubItem.ProjectId = dataEntitySubItem.ProjectId;
			entitySubItem.ProjectName = dataEntitySubItem.ProjectName;
			entitySubItem.EntityReviewApproveId = dataEntitySubItem.EntityReviewApproveId;
			entitySubItem.EntitySubItemId = dataEntitySubItem.EntitySubItemId;
			entitySubItem.EntitySubItemName = dataEntitySubItem.EntitySubItemName;
			entitySubItem.EntitySubItemType = dataEntitySubItem.EntitySubItemType;
			entitySubItem.EntitySubItemReviewApproveState = (ReviewApproveState)dataEntitySubItem.EntitySubItemReviewApproveState;
			entitySubItem.EntitySubItemSampleReportTemplate = dataEntitySubItem.EntitySubItemSampleReportTemplate;
			entitySubItem.EntitySubItemSummaryReportGroup = dataEntitySubItem.EntitySubItemSummaryReportGroup;
			entitySubItem.ReviewApproveComment = dataEntitySubItem.ReviewApproveComment;
		}
		public static void PopulateBrChannelsWithExceededNumberOfPeakEntities(long analysisResultSetId,
			Guid batchRunChannelGuid, BrChannelsWithExceededNumberOfPeaks brChannelsWithExceededNumberOfPeaks)
		{
			brChannelsWithExceededNumberOfPeaks.AnalysisResultSetId = analysisResultSetId;
			brChannelsWithExceededNumberOfPeaks.BatchRunChannelGuid = batchRunChannelGuid;
		}
        public static void PopulateBrChannelsWithExceededNumberOfPeakEntities(Guid batchRunChannelGuid, BrChannelsWithExceededNumberOfPeaks brChannelsWithExceededNumberOfPeaks)
        {
            brChannelsWithExceededNumberOfPeaks.BatchRunChannelGuid = batchRunChannelGuid;
        }
    }
}