DROP INDEX IF EXISTS idx_batchrun_guid;

DROP INDEX IF EXISTS idx_streamdatabatchresult_batchRunIdAndStreamIndex;

--DROP INDEX IF EXISTS idx_streamdatabatchresult_id;

--DROP INDEX IF EXISTS idx_namedcontent_id;

--DROP INDEX IF EXISTS idx_sequencesampleinfomodifiable_id;

DROP INDEX IF EXISTS idx_namedcontent_batchrunid;

DROP INDEX IF EXISTS idx_devicemethod_acquisitionmethodid;

DROP INDEX IF EXISTS idx_devicemoduledetails_devicemethodid;

DROP INDEX IF EXISTS idx_calculatedchanneldata_batchrunanalysisresultid;

DROP INDEX IF EXISTS idx_suitabilityresult_calculatedchanneldataid;

DROP INDEX IF EXISTS idx_runpeakresult_calculatedchanneldataid;

DROP INDEX IF EXISTS idx_batchrunchannelmap_analysisresultsetid;

DROP INDEX IF EXISTS idx_compoundsuitabilitysummaryresult_analysisresultsetid;

CREATE INDEX idx_batchrun_guid ON batchrun(guid);

CREATE INDEX idx_streamdatabatchresult_batchRunIdAndStreamIndex ON streamdatabatchresult(batchrunid,streamindex);

--CREATE INDEX idx_streamdatabatchresult_id ON streamdatabatchresult(id);

--CREATE INDEX idx_namedcontent_id ON namedcontent(id);

--CREATE INDEX idx_sequencesampleinfomodifiable_id ON sequencesampleinfomodifiable(id);

CREATE INDEX idx_namedcontent_batchrunid on namedcontent using HASH(batchrunid);

CREATE INDEX idx_devicemethod_acquisitionmethodid on devicemethod using HASH(acquisitionmethodid);

CREATE INDEX idx_devicemoduledetails_devicemethodid on devicemoduledetails using HASH(devicemethodid);

CREATE INDEX idx_calculatedchanneldata_batchrunanalysisresultid on calculatedchanneldata using HASH(batchrunanalysisresultid);

CREATE INDEX idx_suitabilityresult_calculatedchanneldataid on suitabilityresult using HASH(calculatedchanneldataid);

CREATE INDEX idx_runpeakresult_calculatedchanneldataid on suitabilityresult using HASH(calculatedchanneldataid);

CREATE INDEX idx_batchrunchannelmap_analysisresultsetid on batchrunchannelmap using HASH(analysisresultsetid);

CREATE INDEX idx_compoundsuitabilitysummaryresult_analysisresultsetid on compoundsuitabilitysummaryresult using HASH(analysisresultsetid);