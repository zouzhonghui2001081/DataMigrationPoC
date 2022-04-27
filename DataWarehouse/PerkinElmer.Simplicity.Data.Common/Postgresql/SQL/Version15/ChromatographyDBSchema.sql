DROP TABLE IF EXISTS SchemaVersion;
DROP TABLE IF EXISTS SuitabilityResult;
DROP TABLE IF EXISTS RunPeakResult;
DROP TABLE IF EXISTS CalculatedChannelData;
-- ProcesingMethod
DROP TABLE IF EXISTS InvalidAmounts;
DROP TABLE IF EXISTS CompCalibResultCoefficient;
DROP TABLE IF EXISTS CalibrationPointResponse;
DROP TABLE IF EXISTS CompoundCalibrationResults;
DROP TABLE IF EXISTS CompoundGuids;
DROP TABLE IF EXISTS LevelAmount;
DROP TABLE IF EXISTS Compound;
DROP TABLE IF EXISTS IntegrationEvent;
DROP TABLE IF EXISTS PdaPeakPurityParameters;
DROP TABLE IF EXISTS PdaWavelengthMaxParameters;
DROP TABLE IF EXISTS PdaAbsorbanceRatioParameters;
DROP TABLE IF EXISTS PdaBaselineCorrectionParameters;
DROP TABLE IF EXISTS PdaStandardConfirmationParameters;
DROP TABLE IF EXISTS PdaLibrarySearchSelectedLibraries;
DROP TABLE IF EXISTS PdaLibrarySearchParameters;
DROP TABLE IF EXISTS PdaLibraryConfirmationSelectedLibraries;
DROP TABLE IF EXISTS PdaLibraryConfirmationParameters;
DROP TABLE IF EXISTS PdaApexOptimizedParameters;
DROP TABLE IF EXISTS SuitabilityParameters;
DROP TABLE IF EXISTS ChannelMethod;
DROP TABLE IF EXISTS ProjectToProcessingMethodMap;
DROP TABLE IF EXISTS BatchResultSetToProcessingMethodMap;
DROP TABLE IF EXISTS AnalysisResultSetToProcessingMethodMap;
DROP TABLE IF EXISTS ManualOverrideIntegrationEvent;
DROP TABLE IF EXISTS ManualOverrideMap;
DROP TABLE IF EXISTS BatchRunChannelMap;
DROP TABLE IF EXISTS ProjectCompoundLibraryToLibraryItemMap;
DROP TABLE IF EXISTS SnapshotCompoundLibraryToLibraryItemMap;
DROP TABLE IF EXISTS CompoundLibraryItem;
DROP TABLE IF EXISTS SnapshotCompoundLibrary;
DROP TABLE IF EXISTS ProjectCompoundLibrary;
DROP TABLE IF EXISTS SpectrumMethod;
DROP TABLE IF EXISTS ProcessingDeviceMethod;
DROP TABLE IF EXISTS CalibrationBatchRunInfo;
DROP TABLE IF EXISTS SuitabilityMethod;
DROP TABLE IF EXISTS ProcessingMethod;

DROP TABLE IF EXISTS SequenceSampleInfoModifiable;
DROP TABLE IF EXISTS CompoundSuitabilityResult;
DROP TABLE IF EXISTS BrChannelsWithExceededNumberOfPeaks;
DROP TABLE IF EXISTS BatchRunAnalysisResult;
DROP TABLE IF EXISTS AnalysisResultSet;
DROP TABLE IF EXISTS ChannelData;
DROP TABLE IF EXISTS StreamDataBatchResult;
DROP TABLE IF EXISTS NamedContent;
DROP TABLE IF EXISTS BatchRun;
DROP TABLE IF EXISTS SequenceSampleInfoBatchResult;
DROP TABLE IF EXISTS BatchResultDeviceModuleDetails;
DROP TABLE IF EXISTS DeviceDriverItemDetails;
-- DROP TABLE IF EXISTS SequenceBatchResult;
DROP TABLE IF EXISTS BatchResultSetToAcquisitionMethodMap;
DROP TABLE IF EXISTS BatchResultSet;
DROP TABLE IF EXISTS SequenceSampleInfo;
DROP TABLE IF EXISTS Sequence;
DROP TABLE IF EXISTS SequenceGroupSetting;
DROP TABLE IF EXISTS ChromatogramSetting;
DROP TABLE IF EXISTS ProjectToAcquisitionMethodMap;
DROP TABLE IF EXISTS DeviceModuleDetails;
DROP TABLE IF EXISTS ExpectedDeviceChannelDescriptor;
DROP TABLE IF EXISTS DeviceMethod;
DROP TABLE IF EXISTS AcquisitionMethod;
DROP TABLE IF EXISTS Project;
DROP TABLE IF EXISTS ReportTemplates;

DROP TABLE IF EXISTS MiscParam;
DROP TABLE IF EXISTS ESignaturePoints;
DROP TABLE IF EXISTS ApprovalReviewItems;

DROP TABLE IF EXISTS EntitySubItemReviewApprove;
DROP TABLE IF EXISTS EntityReviewApprove;
DROP TABLE IF EXISTS ReviewApproveSettings;
DROP TABLE IF EXISTS EntityReviewApproveAssociatedDataMap;

CREATE TABLE SchemaVersion
(
	MajorVersion int,
	MinorVersion int
) WITHOUT Oids;


CREATE TABLE Project
(
	Id BIGSERIAL,
	Guid UUID,
	Name VARCHAR(256),
	Description VARCHAR(500),
	IsEnabled BOOLEAN,
	IsSecurityOn BOOLEAN,
	IsESignatureOn BOOLEAN,
	IsReviewApprovalOn BOOLEAN,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),	
	ModifiedUserName VARCHAR(256),
	StartDate TIMESTAMP DEFAULT timezone('utc'::text, null),
	EndDate TIMESTAMP DEFAULT timezone('utc'::text, null),
	PRIMARY KEY(Id)
--	UNIQUE (Name)
) WITHOUT Oids;

CREATE TABLE SequenceGroupSetting
(	
	Id BIGSERIAL,
	ExportPath VARCHAR(256),
	ReportGroups bytea,
	IsDefault BOOLEAN,
	ProjectId BIGINT,
	IsGlobal BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE Sequence
(	
	Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id),
	Name VARCHAR(256),
    Guid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedUserName VARCHAR(256),
	PRIMARY KEY(Id)
	--UNIQUE (Name, ProjectId)
) WITHOUT Oids;


CREATE TABLE SequenceSampleInfo
(
	Id BIGSERIAL,
	SequenceId BIGINT REFERENCES Sequence(Id),
	Guid UUID,
	SampleName VARCHAR(256),
	Selected BOOLEAN,
	SampleId VARCHAR(256),
	UserComments text,
	SampleType SMALLINT,
	NumberOfRepeats INTEGER,
	Level INTEGER,
	Multiplier DOUBLE PRECISION,
	Divisor DOUBLE PRECISION,
	UnknownAmountAdjustment DOUBLE PRECISION,
	InternalStandardAmountAdjustment DOUBLE PRECISION,
	BaselineCorrection SMALLINT,
	-- For internal correction it should be SequenceSample guid,
	-- for external correction it should be BatchRun guid,
	-- if no correction used it should be empty guid
	BaselineRunId BIGINT,
    BaselineRunGuid UUID,
	RackCode VARCHAR(256),
	RackPosition INTEGER,
	PlateCode VARCHAR(256),
	PlateCodeAsInteger INTEGER,
	PlateCodeAsIntegerDeviceName VARCHAR(256),
	PlatePosition VARCHAR(256),
	PlatePositionAsInteger INTEGER,
	PlatePositionAsIntegerDeviceName VARCHAR(256),
	VialPosition VARCHAR(256),
	VialPositionAsInteger INTEGER,
	VialPositionAsIntegerDeviceName VARCHAR(256),
	DestinationVial VARCHAR(256),
	DestinationVialAsInteger INTEGER,
	DestinationVialAsIntegerDeviceName VARCHAR(256),
	InjectionVolume DOUBLE PRECISION,
	InjectionVolumeDeviceName VARCHAR(256),
	InjectionType VARCHAR(256),
	AcquisitionMethodName VARCHAR(256),
	AcquisitionMethodVersionNumber INTEGER,
	ProcessingMethodName VARCHAR(256),
	ProcessingMethodVersionNumber INTEGER,
	CalibrationCurveName VARCHAR(256),
    InjectionPortAsInteger INTEGER,
    InjectionPortAsIntegerDeviceName VARCHAR(256),
    InjectionPort VARCHAR(256),
    InjectionTypeAsInteger INTEGER,
    InjectionTypeAsIntegerDeviceName VARCHAR(256),
	SampleAmount DOUBLE PRECISION,
	DilutionFactor DOUBLE PRECISION,
	Addend DOUBLE PRECISION,
	NormalizationFactor DOUBLE PRECISION,
	SampleReportTemplate VARCHAR(256),
	SummaryReportGroup VARCHAR(256),
	StandardAmountAdjustment DOUBLE PRECISION,
	SuitabilitySampleType SMALLINT,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE BatchResultSet
(	
	Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id) ON DELETE CASCADE,
	Name VARCHAR(256),
    Guid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
    IsCompleted BOOLEAN,
	DataSourceType SMALLINT,
    InstrumentMasterId VARCHAR(256),
    InstrumentId VARCHAR(256),
    InstrumentName VARCHAR(256),
	Regulated BOOLEAN,
	UNIQUE(ProjectId,Guid),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE BatchResultDeviceModuleDetails
(
	Id BIGSERIAL,
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id) ON DELETE CASCADE,
    Name VARCHAR(256),
    DeviceType SMALLINT,
    IsDisplayDriver BOOLEAN,
    DeviceModuleId VARCHAR(256),
    InstrumentMasterId VARCHAR(256),
    InstrumentId VARCHAR(256),
    DeviceDriverItemId VARCHAR(256),
    SettingsUserInterfaceSupported BOOLEAN,
    Simulation BOOLEAN,
    CommunicationTestedSuccessfully BOOLEAN,
    FirmwareVersion VARCHAR(256),
    SerialNumber VARCHAR(256),
    ModelName VARCHAR(256),
    UniqueIdentifier VARCHAR(256),
    InterfaceAddress  VARCHAR(256),
    PRIMARY KEY (Id)
) WITHOUT Oids;

CREATE TABLE DeviceDriverItemDetails
(
	Id BIGSERIAL,
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id) ON DELETE CASCADE,
    Configuration TEXT,
    DeviceType SMALLINT,
    Name VARCHAR(256),
    IsDisplayDriver BOOLEAN,
    InstrumentMasterId VARCHAR(256),
    InstrumentId VARCHAR(256),
    DeviceDriverItemId VARCHAR(256),
    PRIMARY KEY (Id)
) WITHOUT Oids;

CREATE TABLE SequenceSampleInfoBatchResult
(
	Id BIGSERIAL,
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id),
	Guid UUID,
	SampleName VARCHAR(256),
	Selected BOOLEAN,
	SampleId VARCHAR(256),
	UserComments text,
	SampleType SMALLINT,
	NumberOfRepeats INTEGER,
	Level INTEGER,
	Multiplier DOUBLE PRECISION,
	Divisor DOUBLE PRECISION,
	UnknownAmountAdjustment DOUBLE PRECISION,
	InternalStandardAmountAdjustment DOUBLE PRECISION,
	BaselineCorrection SMALLINT,
	BaselineRunId BIGINT,
    BaselineRunGuid UUID,
	RackCode VARCHAR(256),
	RackPosition INTEGER,
	PlateCode VARCHAR(256),
	PlateCodeAsInteger INTEGER,
	PlateCodeAsIntegerDeviceName VARCHAR(256),
	PlatePosition VARCHAR(256),
	PlatePositionAsInteger INTEGER,
	PlatePositionAsIntegerDeviceName VARCHAR(256),
	VialPosition VARCHAR(256),
	VialPositionAsInteger INTEGER,
	VialPositionAsIntegerDeviceName VARCHAR(256),
	DestinationVial VARCHAR(256),
	DestinationVialAsInteger INTEGER,
	DestinationVialAsIntegerDeviceName VARCHAR(256),
	InjectionVolume DOUBLE PRECISION,
	InjectionVolumeDeviceName VARCHAR(256),
	InjectionType VARCHAR(256),
	AcquisitionMethodName VARCHAR(256),
	AcquisitionMethodVersionNumber INTEGER,
	ProcessingMethodName VARCHAR(256),
	ProcessingMethodVersionNumber INTEGER,
	CalibrationCurveName VARCHAR(256),
    InjectionPortAsInteger INTEGER,
    InjectionPortAsIntegerDeviceName VARCHAR(256),
    InjectionPort VARCHAR(256),
    InjectionTypeAsInteger INTEGER,
    InjectionTypeAsIntegerDeviceName VARCHAR(256),
	SampleAmount DOUBLE PRECISION,
	DilutionFactor DOUBLE PRECISION,
	Addend DOUBLE PRECISION,
	NormalizationFactor DOUBLE PRECISION,
	SampleReportTemplate VARCHAR(256),
	SummaryReportGroup VARCHAR(256),
	StandardAmountAdjustment DOUBLE PRECISION,
	SuitabilitySampleType SMALLINT,
	PRIMARY KEY(Id)
) WITHOUT Oids;



CREATE TABLE BatchRun
(
	Id BIGSERIAL,
	Name VARCHAR(256),
    Guid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	IsBaselineRun BOOLEAN,
	AcquisitionTime TIMESTAMP,
	AcquisitionCompletionState SMALLINT,
	RepeatIndex INTEGER,
	SequenceSampleInfoBatchResultId BIGINT, -- REFERENCES SequenceSampleInfoBatchResult(Id),
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id) ON DELETE CASCADE,
	ProcessingMethodBatchResultId BIGINT, -- REFERENCES ProcessingMethodBatchResult(Id),
	CalibrationMethodBatchResultId BIGINT,
	AcquisitionMethodBatchResultId BIGINT, -- REFERENCES AcquisitionMethodBatchResult(Id),
	DataSourceType SMALLINT,
	AcquisitionCompletionStateDetails  VARCHAR(256),
	IsModifiedAfterSubmission BOOLEAN,
	UNIQUE(BatchResultSetId,Guid),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE NamedContent
(
    Id BIGSERIAL,
    BatchRunId BIGINT REFERENCES BatchRun(Id) ON DELETE CASCADE,
    Key TEXT,
    Value TEXT,
    PRIMARY KEY (Id)
)
WITHOUT OIDS;

CREATE TABLE ChannelData
(
	Id BIGSERIAL,
	BatchRunId BIGINT REFERENCES BatchRun(Id) ON DELETE CASCADE,
	--IsRaw BOOLEAN,
	XData DOUBLE PRECISION[],
	YData DOUBLE PRECISION[],
	ChannelType SMALLINT NOT NULL,
	ChannelDataType SMALLINT NOT NULL,
	ChannelIndex SMALLINT NOT NULL,
    ChannelMetaData VARCHAR(256),
    RawChannelType INT,
    BlankSubtractionApplied BOOLEAN,
    SmoothApplied BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE StreamDataBatchResult
(
	Id BIGSERIAL,
	BatchRunId BIGINT REFERENCES BatchRun(Id) ON DELETE CASCADE,
	YData BYTEA,
	StreamIndex SMALLINT NOT NULL,
    MetaData VARCHAR(65536),
    MetaDataType VARCHAR(256),
    DeviceId VARCHAR(256),
    LargeObjectOid BIGINT,
    UseLargeObjectStream BOOLEAN,
    FirmwareVersion VARCHAR(256),
    SerialNumber VARCHAR(256),
    ModelName VARCHAR(256),
    UniqueIdentifier VARCHAR(256),
    InterfaceAddress VARCHAR(256),
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE AnalysisResultSet
(	
	Id BIGSERIAL,
	Guid UUID,
	ProjectId BIGINT REFERENCES Project(Id),
	Name VARCHAR(256),
	Type SMALLINT,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	ModifiedUserName VARCHAR(256),
	ReviewApproveState SMALLINT DEFAULT 0,
	BatchResultSetGuid UUID,
	BatchResultSetName VARCHAR(256) NULL,
	BatchResultSetCreatedDate TIMESTAMP,
	BatchResultSetCreatedUserId VARCHAR(256),
	BatchResultSetModifiedDate TIMESTAMP,
	BatchResultSetModifiedUserId VARCHAR(256),
    Imported BOOLEAN,
    AutoProcessed BOOLEAN,
    Partial BOOLEAN,
    OnlyOriginalExists BOOLEAN,
    OriginalAnalysisResultSetGuid UUID,
    IsCopy BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;



CREATE TABLE SequenceSampleInfoModifiable
(
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT ,
	Guid UUID,
	SampleName VARCHAR(256),
	Selected BOOLEAN,
	SampleId VARCHAR(256),
	UserComments text,
	SampleType SMALLINT,
	Level INTEGER,
	Multiplier DOUBLE PRECISION,
	Divisor DOUBLE PRECISION,
	UnknownAmountAdjustment DOUBLE PRECISION,
	InternalStandardAmountAdjustment DOUBLE PRECISION,
	BaselineCorrection SMALLINT,
	BaselineRunId BIGINT,
    BaselineRunGuid UUID,
	RackCode VARCHAR(256),
	RackPosition INTEGER,
	PlateCode VARCHAR(256),
	PlateCodeAsInteger INTEGER,
	PlateCodeAsIntegerDeviceName VARCHAR(256),
	PlatePosition VARCHAR(256),
	PlatePositionAsInteger INTEGER,
	PlatePositionAsIntegerDeviceName VARCHAR(256),
	VialPosition VARCHAR(256),
	VialPositionAsInteger INTEGER,
	VialPositionAsIntegerDeviceName VARCHAR(256),
	DestinationVial VARCHAR(256),
	DestinationVialAsInteger INTEGER,
	DestinationVialAsIntegerDeviceName VARCHAR(256),
	InjectionVolume DOUBLE PRECISION,
	InjectionVolumeDeviceName VARCHAR(256),
	InjectionType VARCHAR(256),
	AcquisitionMethodName VARCHAR(256),
	AcquisitionMethodVersionNumber INTEGER,
	ProcessingMethodName VARCHAR(256),
	ProcessingMethodVersionNumber INTEGER,
	CalibrationCurveName VARCHAR(256),
    InjectionPort VARCHAR(256),
    InjectionPortAsInteger INTEGER,
    InjectionPortAsIntegerDeviceName VARCHAR(256),
    InjectionTypeAsInteger INTEGER,
    InjectionTypeAsIntegerDeviceName VARCHAR(256),
	SampleAmount DOUBLE PRECISION,
	DilutionFactor DOUBLE PRECISION,
	Addend DOUBLE PRECISION,
	NormalizationFactor DOUBLE PRECISION,
	SampleReportTemplate VARCHAR(256),
	SummaryReportGroup VARCHAR(256),
	StandardAmountAdjustment DOUBLE PRECISION,
	SuitabilitySampleType SMALLINT,
	PRIMARY KEY(Id)
) WITHOUT Oids;



CREATE TABLE BatchRunAnalysisResult
(
	Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id),
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	OriginalBatchResultSetGuid UUID,
	OriginalBatchRunInfoGuid UUID,
	ModifiableBatchRunInfoGuid UUID,
	BatchRunName VARCHAR(256) NULL,	
	BatchRunCreatedDate TIMESTAMP ,
	BatchRunCreatedUserId VARCHAR(256),
	BatchRunModifiedDate TIMESTAMP,
	BatchRunModifiedUserId VARCHAR(256),	
	SequenceSampleInfoModifiableId BIGINT,
	ProcessingMethodModifiableId BIGINT,
	CalibrationMethodModifiableId BIGINT,
	IsBlankSubtractor BOOLEAN,
	DataSourceType SMALLINT,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CompoundSuitabilitySummaryResult
(
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	CompoundGuid UUID,
	AreaAverage DOUBLE PRECISION,
	AreaStDev DOUBLE PRECISION,
	AreaRelativeStdDevPercent DOUBLE PRECISION,
	AreaRelativeStdDevPassed BOOLEAN,
	AreaFailureReason SMALLINT,
	HeightAverage DOUBLE PRECISION,
	HeightStDev DOUBLE PRECISION,
	HeightRelativeStdDevPercent DOUBLE PRECISION,
	HeightRelativeStdDevPassed BOOLEAN,
	HeightFailureReason SMALLINT,
	TheoreticalPlatesNAverage DOUBLE PRECISION,
	TheoreticalPlatesNStDev DOUBLE PRECISION,
	TheoreticalPlatesNRelativeStdDevPercent DOUBLE PRECISION,
	TheoreticalPlatesNRelativeStdDevPassed BOOLEAN,
	TheoreticalPlatesNFailureReason SMALLINT,
	TheoreticalPlatesNTanAverage DOUBLE PRECISION,
	TheoreticalPlatesNTanStDev DOUBLE PRECISION,
	TheoreticalPlatesNTanRelativeStdDevPercent DOUBLE PRECISION,
	TheoreticalPlatesNTanRelativeStdDevPassed BOOLEAN,
	TheoreticalPlatesNTanFailureReason SMALLINT,
	TheoreticalPlatesNFoleyDorseyAverage DOUBLE PRECISION,
	TheoreticalPlatesNFoleyDorseyStDev DOUBLE PRECISION,
	TheoreticalPlatesNFoleyDorseyRelativeStdDevPercent DOUBLE PRECISION,
	TheoreticalPlatesNFoleyDorseyRelativeStdDevPassed BOOLEAN,
	TheoreticalPlatesNFoleyDorseyFailureReason SMALLINT,
	TailingFactorSymmetryAverage DOUBLE PRECISION,
	TailingFactorSymmetryStDev DOUBLE PRECISION,
	TailingFactorSymmetryRelativeStdDevPercent DOUBLE PRECISION,
	TailingFactorSymmetryRelativeStdDevPassed BOOLEAN,
	TailingFactorSymmetryFailureReason SMALLINT,
	RelativeRetentionAverage DOUBLE PRECISION,
	RelativeRetentionStDev DOUBLE PRECISION,
	RelativeRetentionRelativeStdDevPercent DOUBLE PRECISION,
	RelativeRetentionRelativeStdDevPassed BOOLEAN,
	RelativeRetentionFailureReason SMALLINT,
	RelativeRetentionTimeAverage DOUBLE PRECISION,
	RelativeRetentionTimeStDev DOUBLE PRECISION,
	RelativeRetentionTimeRelativeStdDevPercent DOUBLE PRECISION,
	RelativeRetentionTimeRelativeStdDevPassed BOOLEAN,
	RelativeRetentionTimeFailureReason SMALLINT,

	RetentionTimeAverage DOUBLE PRECISION,
	RetentionTimeStDev DOUBLE PRECISION,
	RetentionTimeRelativeStdDevPercent DOUBLE PRECISION,
	RetentionTimeRelativeStdDevPassed BOOLEAN,
	RetentionTimeFailureReason SMALLINT,

	CapacityFactorKPrimeAverage DOUBLE PRECISION,
	CapacityFactorKPrimeStDev DOUBLE PRECISION,
	CapacityFactorKPrimeRelativeStdDevPercent DOUBLE PRECISION,
	CapacityFactorKPrimeRelativeStdDevPassed BOOLEAN,
	CapacityFactorKPrimeFailureReason SMALLINT,
	ResolutionAverage DOUBLE PRECISION,
	ResolutionStDev DOUBLE PRECISION,
	ResolutionRelativeStdDevPercent DOUBLE PRECISION,
	ResolutionRelativeStdDevPassed BOOLEAN,
	ResolutionFailureReason SMALLINT,
	UspResolutionAverage DOUBLE PRECISION,
	UspResolutionStDev DOUBLE PRECISION,
	UspResolutionRelativeStdDevPercent DOUBLE PRECISION,
	UspResolutionRelativeStdDevPassed BOOLEAN,
	UspResolutionFailureReason SMALLINT,
	SignalToNoiseAverage DOUBLE PRECISION,
	SignalToNoiseStDev DOUBLE PRECISION,
	SignalToNoiseRelativeStdDevPercent DOUBLE PRECISION,
	SignalToNoiseRelativeStdDevPassed BOOLEAN,
	SignalToNoiseFailureReason SMALLINT,
	PeakWidthAtBaseAverage DOUBLE PRECISION,
	PeakWidthAtBaseStDev DOUBLE PRECISION,
	PeakWidthAtBaseRelativeStdDevPercent DOUBLE PRECISION,
	PeakWidthAtBaseRelativeStdDevPassed BOOLEAN,
	PeakWidthAtBaseFailureReason SMALLINT,
	PeakWidthAt5PctAverage DOUBLE PRECISION,
	PeakWidthAt5PctStDev DOUBLE PRECISION,
	PeakWidthAt5PctRelativeStdDevPercent DOUBLE PRECISION,
	PeakWidthAt5PctRelativeStdDevPassed BOOLEAN,
	PeakWidthAt5PctFailureReason SMALLINT,
	PeakWidthAt10PctAverage DOUBLE PRECISION,
	PeakWidthAt10PctStDev DOUBLE PRECISION,
	PeakWidthAt10PctRelativeStdDevPercent DOUBLE PRECISION,
	PeakWidthAt10PctRelativeStdDevPassed BOOLEAN,
	PeakWidthAt10PctFailureReason SMALLINT,
	PeakWidthAt50PctAverage DOUBLE PRECISION,
	PeakWidthAt50PctStDev DOUBLE PRECISION,
	PeakWidthAt50PctRelativeStdDevPercent DOUBLE PRECISION,
	PeakWidthAt50PctRelativeStdDevPassed BOOLEAN,
	PeakWidthAt50PctFailureReason SMALLINT,
	SstFlag BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE BrChannelsWithExceededNumberOfPeaks
(
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	BatchRunChannelGuid UUID,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE CalculatedChannelData
(
	Id BIGSERIAL,
	BatchRunChannelGuid UUID,
	BatchRunAnalysisResultId BIGINT REFERENCES BatchRunAnalysisResult(Id) ON DELETE CASCADE,
	XData DOUBLE PRECISION[],
	YData DOUBLE PRECISION[],
	ChannelType SMALLINT  NULL,
	ChannelDataType SMALLINT  NULL,
	ChannelIndex SMALLINT  NULL,
    ChannelMetaData VARCHAR(256) NULL,
    RawChannelType INT NULL,
    BlankSubtractionApplied BOOLEAN,
    SmoothApplied BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE RunPeakResult
(
	Id BIGSERIAL,
	CalculatedChannelDataId BIGINT REFERENCES CalculatedChannelData(Id) ON DELETE CASCADE,
	--CompoundId BIGINT REFERENCES CompoundModifiable(Id),
	BatchRunChannelGuid UUID ,
	CompoundGuid UUID,
	ChannelGuid UUID,
	PeakNumber INTEGER,
	Area DOUBLE PRECISION,
	Height DOUBLE PRECISION,
	InternalStandardAreaRatio DOUBLE PRECISION,
	InternalStandardHeightRatio DOUBLE PRECISION,
	AreaPercent DOUBLE PRECISION,
	RetentionTime DOUBLE PRECISION,
	StartPeakTime DOUBLE PRECISION,
	EndPeakTime DOUBLE PRECISION,
	BaselineSlope DOUBLE PRECISION,
	BaselineIntercept DOUBLE PRECISION,
	SignalToNoiseRatio DOUBLE PRECISION,
	Amount DOUBLE PRECISION,
	InternalStandardAmountRatio DOUBLE PRECISION,
	AreaToHeightRatio DOUBLE PRECISION,
	AreaToAmountRatio DOUBLE PRECISION,
	BaselineCode INTEGER,
	CalibrationInRange INTEGER,
	KPrime DOUBLE PRECISION,
	NormalizedAmount DOUBLE PRECISION,
	RelativeRetentionTime DOUBLE PRECISION,
	RawAmount DOUBLE PRECISION,
	TailingFactor DOUBLE PRECISION,
	Resolution DOUBLE PRECISION,
	PeakWidth DOUBLE PRECISION,
	PeakWidthAtHalfHeight DOUBLE PRECISION,
	PeakGroupGuid UUID,
	PeakName VARCHAR(256),
	PeakGroup VARCHAR(256),
	Overlapped BOOLEAN,
	IsBaselineExpo BOOLEAN,
	ExpoA DOUBLE PRECISION,
	ExpoB DOUBLE PRECISION,
	ExpoCorrection DOUBLE PRECISION,
	ExpoDecay DOUBLE PRECISION,
	RetTimeReferenceGuid UUID,
	RrtReferenceGuid UUID,
	InternalStandardGuid UUID,
	PlatesDorseyFoley DOUBLE PRECISION,
	PlatesTangential DOUBLE PRECISION,
	PeakWidthAt5PercentHeight DOUBLE PRECISION,
	PeakWidthAt10PercentHeight DOUBLE PRECISION,
	RelativeRetTimeSuit DOUBLE PRECISION,
	Signal DOUBLE PRECISION,
	ExpoHeight DOUBLE PRECISION,
	PeakGuid UUID,	
	InternalStandardAmount DOUBLE PRECISION,
    ReferenceInternalStandardCompoundGuid UUID,
	AmountError INTEGER,
    CompoundType SMALLINT,
	AbsorbanceRatio DOUBLE PRECISION,
	StandardConfirmationIndex DOUBLE PRECISION,
	StandardConfirmationPassed BOOLEAN,
    StandardConfirmationError SMALLINT,
	WavelengthMax DOUBLE PRECISION,
	AbsorbanceAtWavelengthMax DOUBLE PRECISION,
	WavelengthMaxError SMALLINT,
	PeakPurity DOUBLE PRECISION,
	PeakPurityPassed BOOLEAN,
	PeakPurityError	 SMALLINT,
	DataSourceType VARCHAR(256),
	ManuallyOverriden BOOLEAN,
	AbsorbanceRatioError INTEGER,
	MidIndex INTEGER,
	StartIndex INTEGER,
	StopIndex INTEGER,
	LibraryCompound VARCHAR(256),
	SearchLibraryCompound VARCHAR(256),
	LibraryName VARCHAR(256),
	SearchLibrary VARCHAR(256),
	LibraryGuid UUID,
	HitQualityValue VARCHAR(256),
	SearchMatch VARCHAR(256),
	LibraryConfirmation BOOLEAN,
	CompoundAssignmentType SMALLINT,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE index ON RunPeakResult(CalculatedChannelDataId);

CREATE TABLE SuitabilityResult
(
	Id BIGSERIAL,
	CalculatedChannelDataId BIGINT REFERENCES CalculatedChannelData(Id) ON DELETE CASCADE,
	PeakGuid UUID,
	CompoundGuid UUID,
	PeakName VARCHAR(256),
	PeakRetentionTime DOUBLE PRECISION,
	Area DOUBLE PRECISION,
	AreaPassed BOOLEAN,
	AreaFailureReason SMALLINT,
	Height DOUBLE PRECISION,
	HeightPassed BOOLEAN,
	HeightFailureReason SMALLINT,
	TheoreticalPlatesN DOUBLE PRECISION,
	TheoreticalPlatesNPassed BOOLEAN,
	TheoreticalPlatesNFailureReason SMALLINT,
	TheoreticalPlatesNTan DOUBLE PRECISION,
	TheoreticalPlatesNTanPassed BOOLEAN,
	TheoreticalPlatesNTanFailureReason SMALLINT,
	TheoreticalPlatesNFoleyDorsey DOUBLE PRECISION,
	TheoreticalPlatesNFoleyDorseyPassed BOOLEAN,
	TheoreticalPlatesNFoleyDorseyFailureReason SMALLINT,
	TailingFactorSymmetry DOUBLE PRECISION,
	TailingFactorSymmetryPassed BOOLEAN,
	TailingFactorSymmetryFailureReason SMALLINT,
	RelativeRetention DOUBLE PRECISION,
	RelativeRetentionPassed BOOLEAN,
	RelativeRetentionFailureReason SMALLINT,
	RelativeRetentionTime DOUBLE PRECISION,
	RelativeRetentionTimePassed BOOLEAN,
	RelativeRetentionTimeFailureReason SMALLINT,
	RetentionTime DOUBLE PRECISION,
	RetentionTimePassed BOOLEAN,
	RetentionTimeFailureReason SMALLINT,
	CapacityFactorKPrime DOUBLE PRECISION,
	CapacityFactorKPrimePassed BOOLEAN,
	CapacityFactorKPrimeFailureReason SMALLINT,
	Resolution DOUBLE PRECISION,
	ResolutionPassed BOOLEAN,
	ResolutionFailureReason SMALLINT,
	UspResolution DOUBLE PRECISION,
	UspResolutionPassed BOOLEAN,
	UspResolutionFailureReason SMALLINT,
	SignalToNoise DOUBLE PRECISION,
	SignalToNoisePassed BOOLEAN,
	SignalToNoiseFailureReason SMALLINT,
	PeakWidthAtBase DOUBLE PRECISION,
	PeakWidthAtBasePassed BOOLEAN,
	PeakWidthAtBaseFailureReason SMALLINT,
	PeakWidthAt5Pct DOUBLE PRECISION,
	PeakWidthAt5PctPassed BOOLEAN,
	PeakWidthAt5PctFailureReason SMALLINT,
	PeakWidthAt10Pct DOUBLE PRECISION,
	PeakWidthAt10PctPassed BOOLEAN,
	PeakWidthAt10PctFailureReason SMALLINT,
	PeakWidthAt50Pct DOUBLE PRECISION,
	PeakWidthAt50PctPassed BOOLEAN,
	PeakWidthAt50PctFailureReason SMALLINT,
	Noise DOUBLE PRECISION,
	NoiseStart DOUBLE PRECISION,
	NoiseGapStart DOUBLE PRECISION,
	NoiseGapEnd DOUBLE PRECISION,
	NoiseEnd DOUBLE PRECISION,
	SstFlag BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ChromatogramSetting
(	
	Id BIGSERIAL,
	ConfigurePeakLabels VARCHAR(4000),
	IsOrientationVertical  BOOLEAN,
	IsSignalUnitInuV BOOLEAN,
	IsTimeUnitInMinute BOOLEAN,
	IsRescalePlotSignalToFull BOOLEAN,
	IsRescalePlotSignalToMaxY BOOLEAN,
	IsRescalePlotSignalToCustom BOOLEAN,
	IsRescalePlotTimeFull BOOLEAN,
	RescalePlotSignalFrom BIGINT ,
	RescalePlotSignalTo BIGINT ,
	RescalePlotTimeFrom DOUBLE PRECISION ,
	RescalePlotTimeTo DOUBLE PRECISION ,
  	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	PRIMARY KEY(Id)
) WITHOUT Oids;



CREATE TABLE ReportTemplates
(
	Id UUID,
	Name VARCHAR(256), -- NOT NULL,
	Category  VARCHAR(256), -- NOT NULL,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	Content bytea,
	Config bytea,
	IsDefault BOOLEAN,
	ProjectId BIGINT,
	IsGlobal BOOLEAN,
	CreatedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedUserId VARCHAR(256),
	ModifiedUserName VARCHAR(256),
	ReviewApproveState SMALLINT DEFAULT 0,
	PRIMARY KEY(Id)
)WITHOUT Oids;


CREATE TABLE AcquisitionMethod
(
    Id BIGSERIAL,
    MethodName VARCHAR(256),
	ReconciledRunTime BOOLEAN,	
    CreateDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
    ModifyDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreateUserId VARCHAR(256),
	ModifyUserId VARCHAR(256),
	CreateUserName VARCHAR(256),
	ModifyUserName VARCHAR(256),
	Guid UUID,
	ReviewApproveState SMALLINT DEFAULT 0,
	VersionNumber INTEGER DEFAULT 1,
    PRIMARY KEY (Id)
)
WITHOUT OIDS;


CREATE TABLE DeviceMethod
(
    Id BIGSERIAL,
    AcquisitionMethodId BIGINT REFERENCES AcquisitionMethod(Id) ON DELETE CASCADE,
    Name VARCHAR(256),
    Content BYTEA,
    Configuration BYTEA,
    DeviceType SMALLINT,
    InstrumentMasterId VARCHAR(256),
    InstrumentId VARCHAR(256),
    DeviceDriverItemId VARCHAR(256),
    PRIMARY KEY (Id)
)
WITHOUT OIDS;


CREATE TABLE DeviceModuleDetails
(
    Id BIGSERIAL,
    DeviceMethodId BIGINT REFERENCES DeviceMethod(Id) ON DELETE CASCADE,
    Name VARCHAR(256),
    DeviceType SMALLINT,
    IsDisplayDriver BOOLEAN,
    DeviceModuleId VARCHAR(256),
    InstrumentMasterId VARCHAR(256),
    InstrumentId VARCHAR(256),
    DeviceDriverItemId VARCHAR(256),
    SettingsUserInterfaceSupported BOOLEAN,
    Simulation BOOLEAN,
    CommunicationTestedSuccessfully BOOLEAN,
    FirmwareVersion VARCHAR(256),
    SerialNumber VARCHAR(256),
    ModelName VARCHAR(256),
    UniqueIdentifier VARCHAR(256),
    InterfaceAddress  VARCHAR(256),
    PRIMARY KEY (Id)
)
WITHOUT OIDS;

CREATE TABLE ExpectedDeviceChannelDescriptor
(
    Id BIGSERIAL,
    DeviceMethodId BIGINT REFERENCES DeviceMethod(Id) ON DELETE CASCADE,
    DeviceChannelDescriptor TEXT,
    PRIMARY KEY (Id)
)
WITHOUT OIDS;


CREATE TABLE ProjectToAcquisitionMethodMap
(
    Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id),
	AcquisitionMethodId BIGINT REFERENCES AcquisitionMethod(Id),
	UNIQUE (AcquisitionMethodId)
)
WITHOUT OIDS;


CREATE TABLE BatchResultSetToAcquisitionMethodMap
(
    Id BIGSERIAL,
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id),
	AcquisitionMethodId BIGINT REFERENCES AcquisitionMethod(Id),
	UNIQUE (AcquisitionMethodId)
)
WITHOUT OIDS;


---- ==================================== Processing/Calibration Method Merge ===========================================
CREATE TABLE ProcessingMethod
(
	Id BIGSERIAL,
	IsDefault BOOLEAN,
	Name VARCHAR(256), -- NOT NULL,
	Guid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedUserName VARCHAR(256),
    NumberOfLevels INTEGER,
    AmountUnits VARCHAR (256),
    UnidentifiedPeakCalibrationType INTEGER,
    UnidentifiedPeakCalibrationFactor DOUBLE PRECISION,
    UnidentifiedPeakReferenceCompoundGuid UUID,
	ModifiedFromOriginal BOOLEAN,
	OriginalReadOnlyMethodGuid UUID,
	Description VARCHAR(100),
	ReviewApproveState SMALLINT DEFAULT 0,
	VersionNumber INTEGER DEFAULT 1,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE CalibrationBatchRunInfo
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	Key UUID,
	BatchRunGuid UUID,
	BatchResultSetGuid UUID,
    BatchRunName VARCHAR (256),
    ResultSetName VARCHAR (256),
	BatchRunAcquisitionTime TIMESTAMP DEFAULT timezone('utc'::text, now()),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE SuitabilityMethod
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	Enabled BOOLEAN,
	SelectedPharmacopeiaType SMALLINT,
	IsEfficiencyInPlates BOOLEAN,
	ColumnLength DOUBLE PRECISION,
	SignalToNoiseWindowStart DOUBLE PRECISION,
	SignalToNoiseWindowEnd DOUBLE PRECISION,
	SignalToNoiseEnabled BOOLEAN,
	AnalyzeAdjacentPeaks BOOLEAN,
	VoidTimeType SMALLINT,
	VoidTimeCustomValueInSeconds DOUBLE PRECISION,
	CompoundPharmacopeiaDefinitions TEXT,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE PdaApexOptimizedParameters
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
    WavelengthBandwidth DOUBLE PRECISION,
	UseReference BOOLEAN,
    ReferenceWavelength DOUBLE PRECISION,
	ReferenceWavelengthBandwidth DOUBLE PRECISION,
    ApplyBaselineCorrection BOOLEAN,
    UseAutoAbsorbanceThreshold BOOLEAN,
	ManualAbsorbanceThreshold DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ProcessingDeviceMethod
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	DeviceClass  VARCHAR(256),
	DeviceIndex INTEGER,
	MetaData TEXT,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE SpectrumMethod
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	Guid UUID,
	StartRetentionTime DOUBLE PRECISION,
	EndRetentionTime DOUBLE PRECISION,
	BaselineCorrectionType SMALLINT,
	BaselineStartRetentionTime DOUBLE PRECISION,
	BaselineEndRetentionTime DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ChannelMethod
(
	Id BIGSERIAL,
	ChannelIndex SMALLINT NOT NULL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	ChannelGuid UUID,
	ParentChannelGuid UUID,
    DataType SMALLINT,
	ChannelType SMALLINT,
	RrtReferenceCompound UUID,
	SmoothFunction INT,
	SmoothWidth INT,
	SmoothPasses INT,
	SmoothOrder INT,
	SmoothCycles INT,
    BunchingFactor INT,
    NoiseThreshold DOUBLE PRECISION,
    AreaThreshold DOUBLE PRECISION,
    WidthRatio DOUBLE PRECISION,
    ValleyToPeakRatio DOUBLE PRECISION,
    TimeAdjustment DOUBLE PRECISION,
    TangentSkimWidth DOUBLE PRECISION,
    PeakHeightRatio DOUBLE PRECISION,
    AdjustedHeightRatio DOUBLE PRECISION,
    ValleyHeightRatio DOUBLE PRECISION,
    VoidTime DOUBLE PRECISION,
    VoidTimeType SMALLINT,
    RrtReferenceType SMALLINT,
	SubtractedBlankOrigBatchRunGuid UUID,
	ChromatographicChannelDescriptor TEXT,
	ProcessingMethodChannelIdentifier TEXT,
	AutoGeneratedFromData BOOLEAN,
	IsPdaMethod BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE SuitabilityParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	ComplianceStandard SMALLINT,
	EfficiencyReporting SMALLINT,
	ColumnLength DOUBLE PRECISION,
	SignalToNoiseStartTime DOUBLE PRECISION,
	SignalToNoiseEndTime DOUBLE PRECISION,
    NumberOfSigmas INTEGER,
	AnalyzeMode SMALLINT,
	TailingFactorCalculation SMALLINT,
	AreaLimitIsUsed BOOLEAN,
	AreaLimitLowerLimit DOUBLE PRECISION,
	AreaLimitUpperLimit DOUBLE PRECISION,
	AreaLimitRelativeStDevPercent DOUBLE PRECISION,
	HeightLimitIsUsed BOOLEAN,
	HeightLimitLowerLimit DOUBLE PRECISION,
	HeightLimitUpperLimit DOUBLE PRECISION,
	HeightLimitRelativeStDevPercent DOUBLE PRECISION,
	NTanLimitIsUsed BOOLEAN,
	NTanLimitLowerLimit DOUBLE PRECISION,
	NTanLimitUpperLimit DOUBLE PRECISION,
	NTanLimitRelativeStDevPercent DOUBLE PRECISION,
	NFoleyDorseyLimitIsUsed BOOLEAN,
	NFoleyDorseyLimitLowerLimit DOUBLE PRECISION,
	NFoleyDorseyLimitUpperLimit DOUBLE PRECISION,
	NFoleyDorseyLimitRelativeStDevPercent DOUBLE PRECISION,
	NFoleyLimitIsUsed BOOLEAN,
	NFoleyLimitLowerLimit DOUBLE PRECISION,
	NFoleyLimitUpperLimit DOUBLE PRECISION,
	NFoleyLimitRelativeStDevPercent DOUBLE PRECISION,
	TailingFactorSymmetryLimitIsUsed BOOLEAN,
	TailingFactorSymmetryLimitLowerLimit DOUBLE PRECISION,
	TailingFactorSymmetryLimitUpperLimit DOUBLE PRECISION,
	TailingFactorSymmetryLimitRelativeStDevPercent DOUBLE PRECISION,
	UspResolutionLimitIsUsed BOOLEAN,
	UspResolutionLimitLowerLimit DOUBLE PRECISION,
	UspResolutionLimitUpperLimit DOUBLE PRECISION,
	UspResolutionLimitRelativeStDevPercent DOUBLE PRECISION,
	KPrimeLimitIsUsed BOOLEAN,
	KPrimeLimitLowerLimit DOUBLE PRECISION,
	KPrimeLimitUpperLimit DOUBLE PRECISION,
	KPrimeLimitRelativeStDevPercent DOUBLE PRECISION,
	ResolutionLimitIsUsed BOOLEAN,
	ResolutionLimitLowerLimit DOUBLE PRECISION,
	ResolutionLimitUpperLimit DOUBLE PRECISION,
	ResolutionLimitRelativeStDevPercent DOUBLE PRECISION,
	AlphaLimitIsUsed BOOLEAN,
	AlphaLimitLowerLimit DOUBLE PRECISION,
	AlphaLimitUpperLimit DOUBLE PRECISION,
	AlphaLimitRelativeStDevPercent DOUBLE PRECISION,
	SignalToNoiseLimitIsUsed BOOLEAN,
	SignalToNoiseLimitLowerLimit DOUBLE PRECISION,
	SignalToNoiseLimitUpperLimit DOUBLE PRECISION,
	SignalToNoiseLimitRelativeStDevPercent DOUBLE PRECISION,
	PeakWidthLimitIsUsed BOOLEAN,
	PeakWidthLimitLowerLimit DOUBLE PRECISION,
	PeakWidthLimitUpperLimit DOUBLE PRECISION,
	PeakWidthLimitRelativeStDevPercent DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaPeakPurityParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
    MinimumDataPoints INT,
    ApplyBaselineCorrection BOOLEAN,
	PurityLimit DOUBLE PRECISION,
	PercentOfPeakHeightForSpectra DOUBLE PRECISION,
    UseAutoAbsorbanceThreshold BOOLEAN,
	ManualAbsorbanceThreshold DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaWavelengthMaxParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
    ApplyBaselineCorrection BOOLEAN,
	UseAutoAbsorbanceThreshold BOOLEAN,
    ManualAbsorbanceThreshold DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaAbsorbanceRatioParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	WavelengthA DOUBLE PRECISION,
	WavelengthB DOUBLE PRECISION,
    ApplyBaselineCorrection BOOLEAN,
    UseAutoAbsorbanceThreshold BOOLEAN,
	ManualAbsorbanceThreshold DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaBaselineCorrectionParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	CorrectionType SMALLINT,
	SelectedSpectrumTime DOUBLE PRECISION,
	RangeStart DOUBLE PRECISION,
	RangeEnd DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaStandardConfirmationParameters
(
	Id BIGSERIAL,
	PdaStandardConfirmationGuid UUID,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
    MinimumDataPoints INTEGER,
    StandardType SMALLINT,
	PassThreshold DOUBLE PRECISION,
    ApplyBaselineCorrection BOOLEAN,
    UseAutoAbsorbanceThresholdForSample BOOLEAN,
	ManualAbsorbanceThresholdForSample DOUBLE PRECISION,
    UseAutoAbsorbanceThresholdForStandard BOOLEAN,
	ManualAbsorbanceThresholdForStandard DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaLibrarySearchParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
    MatchRetentionTimeWindow DOUBLE PRECISION,
	MatchRetentionTimeWindowEnabled BOOLEAN,
    BaselineCorrectionEnabled BOOLEAN,
    HitDistanceThreshold DOUBLE PRECISION,
    PeakLibrarySearch BOOLEAN,
	UseWavelengthLimits BOOLEAN,
    MaxNumberOfResults INTEGER,	
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaLibrarySearchSelectedLibraries
(
	Id BIGSERIAL,
	PdaLibrarySearchParameterId BIGINT REFERENCES PdaLibrarySearchParameters(Id) ON DELETE CASCADE,
	SelectedLibraries VARCHAR(256),
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaLibraryConfirmationParameters
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	MinWavelength DOUBLE PRECISION,
	MaxWavelength DOUBLE PRECISION,
	 BaselineCorrectionEnabled BOOLEAN,
    HitDistanceThreshold DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE PdaLibraryConfirmationSelectedLibraries
(
	Id BIGSERIAL,
	PdaLibraryConfirmationParameterId BIGINT REFERENCES PdaLibraryConfirmationParameters(Id) ON DELETE CASCADE,
	SelectedLibraries VARCHAR(256),
	PRIMARY KEY(Id)
) WITHOUT Oids;



CREATE TABLE IntegrationEvent
(
	Id BIGSERIAL,
	ChannelMethodId BIGINT REFERENCES ChannelMethod(Id) ON DELETE CASCADE,
	EventType SMALLINT,
	StartTime DOUBLE PRECISION NULL,
	EndTime DOUBLE PRECISION NULL,
    EventId INT,
	Value DOUBLE PRECISION NULL,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE Compound
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
    -- ICompoundOrGroupId
	Guid UUID,
	CompoundNumber SMALLINT,
	Name VARCHAR (256),
	ProcessingMethodChannelGuid UUID,
	CompoundType INTEGER,
	-- IIdentificationParameters
	ExpectedRetentionTime DOUBLE PRECISION,
	RetentionTimeWindowAbsolute DOUBLE PRECISION,
	RetentionTimeWindowInPercents DOUBLE PRECISION,
	RetTimeWindowStart DOUBLE PRECISION,
	RetTimeWindowEnd DOUBLE PRECISION,
	IsRetTimeReferencePeak BOOLEAN,
	RetTimeReferencePeakGuid UUID,
	RetentionIndex INTEGER,
	UseClosestPeak BOOLEAN,
	Index INTEGER,
	IsIntStdReferencePeak BOOLEAN,
	IntStdReferenceGuid UUID,
	IsRrtReferencePeak BOOLEAN,
    -- ICalibrationParameters
	InternalStandard BOOLEAN,
	ReferenceInternalStandardGuid UUID,
	Purity DOUBLE PRECISION,
	QuantifyUsingArea BOOLEAN,
	CalibrationType INTEGER,
	WeightingType INTEGER,
	Scaling INTEGER,
	OriginTreatment INTEGER,
	CalibrationFactor DOUBLE PRECISION,
	ReferenceCompoundGuid UUID,
	InternalStandardAmount DOUBLE PRECISION,
	IsCompoundGroup BOOLEAN,
	StartTime DOUBLE PRECISION,
	EndTime DOUBLE PRECISION,
	UsedForSuitability BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE LevelAmount
(
	Id BIGSERIAL,
	CompoundId BIGINT REFERENCES Compound(Id) ON DELETE CASCADE,
	Level Integer,
	Amount DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CompoundGuids
(
	Id BIGSERIAL,
	CompoundId BIGINT REFERENCES Compound(Id) ON DELETE CASCADE,
    CompoundGuid UUID,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CompoundCalibrationResults
(
	Id BIGSERIAL,
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id) ON DELETE CASCADE,
	NotEnoughLevelsFoundError BOOLEAN,
	InvalidAmountError BOOLEAN,
    -- ICalibrationRegressionEquation
    RegressionType INTEGER,
    RSquare DOUBLE PRECISION,
    RelativeStandardErrorValue DOUBLE PRECISION,
    RelativeStandardDeviationPercent DOUBLE PRECISION,
    CorrelationCoefficient DOUBLE PRECISION,
  -- ICompoundId
    Guid UUID,
	Name VARCHAR (256),
    ChannelIndex INTEGER,
    ConfLimitTestResult INTEGER,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CalibrationPointResponse
(
	Id BIGSERIAL,
	CompoundCalibrationResultsId BIGINT REFERENCES CompoundCalibrationResults(Id) ON DELETE CASCADE,
	Level Integer,
	QuantifyUsingArea BOOLEAN,
	UseInternalStandard BOOLEAN,
    Area DOUBLE PRECISION,
    AreaRatio DOUBLE PRECISION,
	PeakNotFoundError BOOLEAN,
	InternalStandardPeakNotFoundError BOOLEAN,
    Height DOUBLE PRECISION,
    HeightRatio DOUBLE PRECISION,
	Excluded BOOLEAN,
    BatchRunGuid UUID,
	External BOOLEAN,
    PeakAreaPercentage DOUBLE PRECISION,
    PointCalibrationFactor DOUBLE PRECISION,
    InvalidAmountError BOOLEAN,
    OutlierTestFailed BOOLEAN,
    OutlierTestResult DOUBLE PRECISION,
    StandardAmountAdjustmentCoeff DOUBLE PRECISION,
    InternalStandardAmountAdjustmentCoeff DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CompCalibResultCoefficient
(
	Id BIGSERIAL,
	CompoundCalibrationResultsId BIGINT REFERENCES CompoundCalibrationResults(Id) ON DELETE CASCADE,
    Coefficients DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE InvalidAmounts
(
	Id BIGSERIAL,
	CompoundCalibrationResultsId BIGINT REFERENCES CompoundCalibrationResults(Id) ON DELETE CASCADE,
    Amount DOUBLE PRECISION,
	PRIMARY KEY(Id)
) WITHOUT Oids;
 

CREATE TABLE ProjectToProcessingMethodMap
(
    Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id),
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id),
	UNIQUE (ProcessingMethodId)
)
WITHOUT OIDS;


CREATE TABLE BatchResultSetToProcessingMethodMap
(
    Id BIGSERIAL,
	BatchResultSetId BIGINT REFERENCES BatchResultSet(Id),
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id),
	UNIQUE (ProcessingMethodId)
)
WITHOUT OIDS;


CREATE TABLE AnalysisResultSetToProcessingMethodMap
(
    Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id),
	ProcessingMethodId BIGINT REFERENCES ProcessingMethod(Id),
	UNIQUE (ProcessingMethodId)
)
WITHOUT OIDS;


CREATE TABLE BatchRunChannelMap
(
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	BatchRunChannelGuid UUID,
	BatchRunGuid UUID,
	OriginalBatchRunGuid UUID,
    BatchRunChannelDescriptorType VARCHAR(256),
	BatchRunChannelDescriptor TEXT,
	ProcessingMethodGuid UUID,
	ProcessingMethodChannelGuid UUID,
	XData DOUBLE PRECISION[],
	YData DOUBLE PRECISION[],
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ManualOverrideMap
(
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	BatchRunChannelGuid UUID,
	BatchRunGuid UUID,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ManualOverrideIntegrationEvent
(
	Id BIGSERIAL,
	ManualOverrideMapId BIGINT REFERENCES ManualOverrideMap(Id) ON DELETE CASCADE,
	EventType SMALLINT,
	StartTime DOUBLE PRECISION NULL,
	EndTime DOUBLE PRECISION NULL,
    EventId INT,
	Value DOUBLE PRECISION NULL,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ProjectCompoundLibrary
(	
	Id BIGSERIAL,
	ProjectId BIGINT REFERENCES Project(Id) ON DELETE CASCADE,
	LibraryName VARCHAR(256),
	LibraryGuid UUID,
	Description VARCHAR(256),
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	CreatedUserName VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	ModifiedUserName VARCHAR(256),
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE CompoundLibraryItem
(	
	Id BIGSERIAL,
	CompoundName VARCHAR(256),
	CompoundGuid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	RetentionTime DOUBLE PRECISION NULL,
	SpectrumAbsorbances DOUBLE PRECISION[],
	BaselineAbsorbances DOUBLE PRECISION[],
	StartWavelength DOUBLE PRECISION NULL,
	EndWavelength DOUBLE PRECISION NULL,
    	Step DOUBLE PRECISION,
	IsBaselineCorrected BOOLEAN,
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE ProjectCompoundLibraryToLibraryItemMap
(
	Id BIGSERIAL,
	ProjectCompoundLibraryId BIGINT REFERENCES ProjectCompoundLibrary(Id),
	CompoundLibraryItemId BIGINT REFERENCES CompoundLibraryItem(Id),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE SnapshotCompoundLibrary
(	
	Id BIGSERIAL,
	AnalysisResultSetId BIGINT REFERENCES AnalysisResultSet(Id) ON DELETE CASCADE,
	LibraryName VARCHAR(256),
	LibraryGuid UUID,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
    ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE SnapshotCompoundLibraryToLibraryItemMap
(
	Id BIGSERIAL,
	SnapshotCompoundLibraryId BIGINT REFERENCES SnapshotCompoundLibrary(Id),
	CompoundLibraryItemId BIGINT REFERENCES CompoundLibraryItem(Id),
	PRIMARY KEY(Id)
) WITHOUT Oids;

CREATE TABLE MiscParam
(	
	KeyName VARCHAR(256),
	Value VARCHAR(256),
	PRIMARY KEY(KeyName)
) WITHOUT Oids;


CREATE TABLE ESignaturePoints
(
	Id BIGSERIAL,
	Guid UUID,
	Name VARCHAR(256)  NOT NULL,
	ModuleName VARCHAR(256)  NOT NULL,
	DisplayOrder INT NOT NULL,
	IsUseAuth BOOLEAN,
	IsCustomReason BOOLEAN,
	IsPredefinedReason BOOLEAN,
	Reasons TEXT,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	PRIMARY KEY(Id),
	UNIQUE(Name, ModuleName) 
) WITHOUT Oids;


CREATE TABLE ApprovalReviewItems
(
	Id BIGSERIAL,
	Guid UUID,
	Name VARCHAR(256) NOT NULL,
	DisplayOrder INT NOT NULL,
	IsApprovalReviewOn BOOLEAN,
	IsSubmitReviewApprove BOOLEAN,
	IsSubmitApprove BOOLEAN,
	CreatedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	CreatedUserId VARCHAR(256),
	ModifiedDate TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ModifiedUserId VARCHAR(256),
	PRIMARY KEY(Id),
	UNIQUE(Name) 
) WITHOUT Oids;


CREATE TABLE EntityReviewApprove
(
	Id BIGSERIAL,
	ProjectName VARCHAR(256) NOT NULL,
	ProjectId VARCHAR(256) NOT NULL,
	EntityId VARCHAR(256) NOT NULL,
	EntityName VARCHAR(256) NOT NULL,
	EntityType VARCHAR(256) NOT NULL,
	EntityReviewApproveState SMALLINT,
	LastActionTimestamp TIMESTAMP DEFAULT timezone('utc'::text, now()),
	InReviewBy VARCHAR(1024),
	InReviewByUserId VARCHAR(256),
	InApproveBy VARCHAR(1024),
	InApproveByUserId VARCHAR(256),
	ReviewedBy VARCHAR(1024),
	ReviewedByUserId VARCHAR(256),
	ApprovedBy VARCHAR(1024),
	ApprovedByUserId VARCHAR(256),
	RejectedBy VARCHAR(1024),
	RejectedByUserId VARCHAR(256),
	RecalledBy VARCHAR(1024),
	RecalledByUserId VARCHAR(256),
	PostponedBy VARCHAR(1024),
	PostponedByUserId VARCHAR(256),
	SubmittedBy VARCHAR(1024),
	SubmittedByUserId VARCHAR(256),
	LastModifiedBy VARCHAR(1024),
	LastModifiedByUserId VARCHAR(256),
	SubmitTimestamp TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ReviewedTimestamp TIMESTAMP,
	ApprovedTimestamp TIMESTAMP,
	VersionNumber INT,
	ReviewedCount INT,
	ApprovedCount INT,
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE EntitySubItemReviewApprove
(
	Id BIGSERIAL,
	ProjectName VARCHAR(256),
	ProjectId VARCHAR(256),
	EntityReviewApproveId BIGINT REFERENCES EntityReviewApprove(Id) ON DELETE CASCADE,
	EntitySubItemId VARCHAR(256) NOT NULL,
	EntitySubItemName VARCHAR(256) NOT NULL,
	EntitySubItemType VARCHAR(256) NOT NULL,
	EntitySubItemSampleReportTemplate VARCHAR(256),
	EntitySubItemSummaryReportGroup VARCHAR(256),
	EntitySubItemReviewApproveState SMALLINT,
	ReviewApproveComment VARCHAR(1024),
	PRIMARY KEY(Id)
) WITHOUT Oids;


CREATE TABLE ReviewApproveSettings
(	
	KeyName VARCHAR(256),
	Value VARCHAR(256),
	PRIMARY KEY(KeyName)
) WITHOUT Oids;

CREATE TABLE EntityReviewApproveAssociatedDataMap
(	
	reviewapproveid VARCHAR(256),
	dataid BIGINT
) WITHOUT Oids;
