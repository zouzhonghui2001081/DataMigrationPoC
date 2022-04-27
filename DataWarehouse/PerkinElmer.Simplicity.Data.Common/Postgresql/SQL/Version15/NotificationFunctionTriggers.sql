-- AcquisitionMethod Notification
CREATE OR REPLACE FUNCTION acquisitionmethod_update_notify() RETURNS trigger AS $$
DECLARE
  guid uuid;
BEGIN
  IF TG_OP = 'INSERT' OR TG_OP = 'UPDATE' THEN
    guid = NEW.Guid;
  ELSE
    guid = OLD.Guid;
  END IF;
  PERFORM pg_notify('acquisitionmethodnotification', TG_OP || ',' || guid);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- INSERT Trigger
DROP TRIGGER IF EXISTS acquisitionmethod_notify_insert ON acquisitionmethod;
CREATE TRIGGER acquisitionmethod_notify_insert AFTER INSERT ON acquisitionmethod FOR EACH ROW EXECUTE PROCEDURE acquisitionmethod_update_notify();

-- UPDATE Trigger
DROP TRIGGER IF EXISTS acquisitionmethod_notify_update ON acquisitionmethod;
CREATE TRIGGER acquisitionmethod_notify_update AFTER UPDATE ON acquisitionmethod FOR EACH ROW EXECUTE PROCEDURE acquisitionmethod_update_notify();

-- DELETE Trigger
DROP TRIGGER IF EXISTS acquisitionmethod_notify_delete ON acquisitionmethod;
CREATE TRIGGER acquisitionmethod_notify_delete AFTER DELETE ON acquisitionmethod FOR EACH ROW EXECUTE PROCEDURE acquisitionmethod_update_notify();

-- ProcessingMethod Notification
CREATE OR REPLACE FUNCTION processingmethod_update_notify() RETURNS trigger AS $$
DECLARE
  guid uuid;
  name varchar;
BEGIN
  IF TG_OP = 'INSERT' OR TG_OP = 'UPDATE' THEN
    guid = NEW.Guid;
	name = NEW.Name;
  ELSE
    guid = OLD.Guid;
	name = OLD.Name;
  END IF;
  PERFORM pg_notify('processingmethodnotification', TG_OP || ',' || guid || ',' || name);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- INSERT Trigger
DROP TRIGGER IF EXISTS processingmethod_notify_insert ON processingmethod;
CREATE TRIGGER processingmethod_notify_insert AFTER INSERT ON processingmethod FOR EACH ROW EXECUTE PROCEDURE processingmethod_update_notify();

-- UPDATE Trigger
DROP TRIGGER IF EXISTS processingmethod_notify_update ON processingmethod;
CREATE TRIGGER processingmethod_notify_update AFTER UPDATE ON processingmethod FOR EACH ROW EXECUTE PROCEDURE processingmethod_update_notify();

-- DELETE Trigger
DROP TRIGGER IF EXISTS processingmethod_notify_delete ON processingmethod;
CREATE TRIGGER processingmethod_notify_delete AFTER DELETE ON processingmethod FOR EACH ROW EXECUTE PROCEDURE processingmethod_update_notify(); 

-- BatchRunAnalysisResult Notification
CREATE OR REPLACE FUNCTION batchrunanalysisresult_update_notify() RETURNS trigger AS $$
DECLARE
  modifiablebatchruninfoguid uuid;
  analysisresultsetid bigint;
BEGIN
  IF TG_OP = 'INSERT' OR TG_OP = 'UPDATE' THEN
    modifiablebatchruninfoguid = NEW.ModifiableBatchRunInfoGuid;
	analysisresultsetid = NEW.AnalysisResultSetId;
  ELSE
    modifiablebatchruninfoguid = OLD.ModifiableBatchRunInfoGuid;
	analysisresultsetid = OLD.AnalysisResultSetId;
  END IF;
  PERFORM pg_notify('batchrunanalysisresultnotification', TG_OP || ',' || modifiablebatchruninfoguid || ',' || analysisresultsetid);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- INSERT Trigger
DROP TRIGGER IF EXISTS batchrunanalysisresult_notify_insert ON batchrunanalysisresult;
CREATE TRIGGER batchrunanalysisresult_notify_insert AFTER INSERT ON batchrunanalysisresult FOR EACH ROW EXECUTE PROCEDURE batchrunanalysisresult_update_notify();

-- UPDATE Trigger
DROP TRIGGER IF EXISTS batchrunanalysisresult_notify_update ON batchrunanalysisresult;
CREATE TRIGGER batchrunanalysisresult_notify_update AFTER UPDATE ON batchrunanalysisresult FOR EACH ROW EXECUTE PROCEDURE batchrunanalysisresult_update_notify();

-- DELETE Trigger
DROP TRIGGER IF EXISTS batchrunanalysisresult_notify_delete ON batchrunanalysisresult;
CREATE TRIGGER batchrunanalysisresult_notify_delete AFTER DELETE ON batchrunanalysisresult FOR EACH ROW EXECUTE PROCEDURE batchrunanalysisresult_update_notify();
