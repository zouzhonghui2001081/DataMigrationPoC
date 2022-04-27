
--
-- Data for Name: accesscontrolgroups; Type: TABLE DATA; Schema: public; Owner: postgres
--
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('0', 'General', '');
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('1', 'Data Review', 'Data Review Main Menu');
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('2', 'Acquisition', 'Acquisition Main Menu');
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('3', 'Lab Management', 'Lab Management Main Menu');
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('4', 'Review and Approve Workflow', 'Review/Approve Main Menu');
INSERT INTO public.accesscontrolgroups (id, name, description) VALUES ('5', 'Reports Template', 'Reports Template Main Menu');


--
-- Data for Name: accesscontrolitems; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('0', 'System Selector', 'Permission for System Selector', '0', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('1', 'Data Browser', 'Permission for Data Browser', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('2', 'Review Parameters(Except Manual Overriding) '||E'\r\n'||' 1. Chromatogram '||E'\r\n'||' 2. Processing Method '||E'\r\n'||' 3. Calibration Curve', 'Permission for Review Parameters', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('3', 'Results Table', 'Permission for Results Table', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('4', 'DataReview Acquisition Method', 'Permission for Acquisition Method', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('5', 'Result Set Review List', 'Permission for Result Set Review list', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('6', 'Spectral Environment '||E'\r\n'||' 1. Library '||E'\r\n'||' 2. Spectrum '||E'\r\n'||' 3. PDA Contour Map', 'Permission for Spectral Environment', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('7', 'Review Parameters (Manual Overriding)', 'Permission for Review Parameters (manual overriding)', '1', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('8', 'Acquisition Method', 'Permission for Acquisition Method', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('9', 'Sequence', 'Permission for Sequence', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('10', 'Run Queue', 'Permission for Run Queue', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('11', 'Direct Control', 'Permission for Direct Control', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('12', 'Maintenance', 'Permission for Maintenance', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('13', 'Live Chromatogram', 'Permission for Live Chromatogram', '2', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('14', 'Audit Trail Log Viewer', 'Permission for Audit Trail Log Viewer', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('15', 'Application Security', 'Permission for Application Security', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('16', 'Instrument Systems', 'Permission for Instrument Systems', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('17', 'Roles', 'Permission for Roles', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('18', 'Users/Groups', 'Permission for Users/Groups', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('19', 'Projects', 'Permission for Projects', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('20', 'Data Management', 'Permission for Data Management', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('21', 'E-Signature (Global Level)', 'Permission for E-Signature (Global Level)', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('22', 'Review/Approve Workflow (Global Level)', 'Permission for Review/Approve Workflow (Global Level)', '3', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('23', 'Report Templates', 'Permission for Report Template Wizard', '5', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('24', 'Submit entities for Review/Approve '||E'\r\n'||' 1. Analysis Result Set '||E'\r\n'||' 2. Processing Method '||E'\r\n'||' 3. Acquisition Method '||E'\r\n'||' 4. Report Template', 'Submit entities for Review/Approve', '4', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('25', 'Review entities '||E'\r\n'||' 1. Analysis Result Set '||E'\r\n'||' 2. Processing Method '||E'\r\n'||' 3. Acquisition Method '||E'\r\n'||' 4. Report Template', 'Review entities', '4', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('26', 'Approve entities '||E'\r\n'||' 1. Analysis Result Set '||E'\r\n'||' 2. Processing Method '||E'\r\n'||' 3. Acquisition Method '||E'\r\n'||' 4. Report Template', 'Approve entities', '4', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('27', 'Print Action List', 'Print Review/Approve action list', '4', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('28', 'Ability to submit unapproved Acquisition Method', 'Ability to select unapproved Acquisition Method while creating Sequence and run it.', '4', NULL);
INSERT INTO public.accesscontrolitems (id, name, description, accesscontrolgroupid, subgroup) VALUES ('29', 'Ability to submit unapproved Processing Method', 'Ability to select unapproved Processing Method while creating Sequence and run it.', '4', NULL);

--
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('1', 'Administrator', 'Manage overall system and configure security, control all functionality. To have control over what users can do.', true, false, true);
INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('2', 'Operator', 'Run Set (pre-define) of analysis. Kick-Off operations,  Instrument Operation', true, false, false);
INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('3', 'Reviewer', 'Review the changes of Processing Method, Acquisition Method, Analysis Result Set and Report Template ', true, false, false);
INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('4', 'Approver', 'Approve the changes of Processing Method, Acquisition Method, Analysis Result Set and Report Template ', true, false, false);
INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('5', 'Method Developer', 'Develop, manage and validate methods', true, false, false);
INSERT INTO public.roles (id, name, description, isdefault, isdisabled, isglobal) VALUES ('6', 'Analyst', 'Run Set (pre-define) of analysis, Kick-Off operation, Instrument Operation, Optimize and Review Data', true, false, false);

--
-- Data for Name: properties; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.properties (key, value) VALUES ('security_enabled', 'false');
INSERT INTO public.properties (key, value) VALUES ('screen_lock_enabled', 'false');
INSERT INTO public.properties (key, value) VALUES ('screen_lock_timeout', '00:15:00');
INSERT INTO public.properties (key, value) VALUES ('data_initialized', 'true');

--
-- Data for Name: roleaccesscontrolgroup; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '0');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('2', '0');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '0');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '0');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '1');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('2', '1');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '1');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '1');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '2');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '2');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('2', '2');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '2');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('2', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('3', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('4', '3');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '5');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '5');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '5');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('5', '4');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('6', '4');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('1', '4');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('3', '4');
INSERT INTO public.roleaccesscontrolgroup (roleid, accesscontrolgroupid) VALUES ('4', '4');


--
-- Data for Name: roleaccesscontrolitem; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '0');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '0');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '0');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '0');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '1');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '1');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '1');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '1');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '2');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '2');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '2');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '3');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '3');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '3');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '3');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '4');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '4');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '4');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '4');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '5');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '5');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '5');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '5');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '6');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '6');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '6');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '6');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '7');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '7');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '7');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '7');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '8');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '8');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '9');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '9');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '9');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '9');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '10');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '10');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '10');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '10');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '11');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '11');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '11');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '11');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '12');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '12');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '12');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '12');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '13');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '13');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '13');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '13');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('2', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('3', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('4', '14');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '15');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '16');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '16');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '16');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '17');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '17');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '18');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '18');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '19');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '19');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '19');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '20');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '20');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '20');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '21');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '22');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '23');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '23');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '23');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '24');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '24');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '24');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('3', '25');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '25');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '25');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('4', '26');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '26');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('3', '27');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('4', '27');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '27');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '27');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '28');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '28');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('6', '29');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('5', '29');
INSERT INTO public.roleaccesscontrolitem (roleid, accesscontrolitemid) VALUES ('1', '29');
