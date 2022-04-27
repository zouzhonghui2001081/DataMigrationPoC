DROP TABLE IF EXISTS AuditTrailLogs;
DROP TABLE IF EXISTS AuditTrailConfiguration;
DROP TABLE IF EXISTS SchemaVersion;
DROP TABLE IF EXISTS EntityVersionLog;

CREATE TABLE SchemaVersion
(
	MajorVersion int,
	MinorVersion int
) WITHOUT Oids;
COMMIT;

CREATE TABLE AuditTrailLogs
(
	Id BIGSERIAL,
	UniqueId UUID NOT NULL,
	LogTime TIMESTAMP DEFAULT timezone('utc'::text, now()),
	ScopeType VARCHAR(256),
	RecordType VARCHAR(256),
	EntityId VARCHAR(256),
	EntityType VARCHAR(256),
	ActionTypeId VARCHAR(256),
	ActionType VARCHAR(256),
	ActionDescription TEXT,
	ItemId VARCHAR(256),
	ItemName VARCHAR(256),
	ItemType VARCHAR(256),
	ItemVersionId VARCHAR(256) DEFAULT '-1',
	UserId VARCHAR(256),
	UserLogin VARCHAR(256),
	UserFullName VARCHAR(256),
	UserRoleId VARCHAR(256),
	UserRole VARCHAR(256),
	ProjectId VARCHAR(256),
	ProjectName VARCHAR(256),
	WorkstationId VARCHAR(256),
	WorkstationName VARCHAR(256),
	InstrumentId VARCHAR(256),
	InstrumentName VARCHAR(256),
	Justification TEXT,
	JustificationTimestamp TIMESTAMP,
	Comment TEXT,
    PRIMARY KEY(Id)
) WITHOUT Oids;
COMMIT;

CREATE TABLE AuditTrailConfiguration
(
	Key VARCHAR(256),
	Value VARCHAR(256),
	UNIQUE (Key)
) WITHOUT Oids;
COMMIT;

CREATE TABLE EntityVersionLog
(
	Id BIGSERIAL,
	UniqueId UUID NOT NULL,
	EntityId VARCHAR(256),
	EntityType VARCHAR(256),
	AfterChangeVersionNumber BIGINT,
	VersionData Text NOT NULL,
	BeforeChangeEntityId VARCHAR(256),
	BeforeChangeVersionNumber BIGINT DEFAULT -1,
	CreationTimestamp TIMESTAMP,
	Description Text,
	PRIMARY KEY(Id),
	UNIQUE (EntityId,EntityType, AfterChangeVersionNumber)
) WITHOUT Oids;
COMMIT;
