
CREATE TABLE schemaversion
(
	majorversion INT,
	minorversion INT
) WITHOUT Oids;
COMMIT;

CREATE TABLE properties
(
	key VARCHAR(256) UNIQUE,
	value VARCHAR(1024),
	PRIMARY KEY(key)
) WITHOUT Oids;
COMMIT;

CREATE TABLE accounts
(
	sid VARCHAR(256),
	name VARCHAR(256),
	isgroup BOOLEAN,
	isenabled BOOLEAN,
	isdefault BOOLEAN,
	PRIMARY KEY(sid)
) WITHOUT Oids;
COMMIT;

CREATE TABLE roles
(
	id VARCHAR(256),
	name VARCHAR(256),
	description VARCHAR(1024),
	isdefault BOOLEAN,
	isdisabled BOOLEAN,
	isglobal BOOLEAN,
	PRIMARY KEY(id)
) WITHOUT Oids;
COMMIT;

CREATE TABLE accesscontrolgroups
(
	id VARCHAR(256),
	name VARCHAR(256),
	description VARCHAR(1024),
	PRIMARY KEY(id)
) WITHOUT Oids;
COMMIT;

CREATE TABLE accesscontrolitems
(
	id VARCHAR(256),
	name VARCHAR(256),
	description VARCHAR(1024),
	accesscontrolgroupid VARCHAR(256) REFERENCES accesscontrolgroups(id) ON DELETE CASCADE,
	subgroup VARCHAR(256),
	PRIMARY KEY(id)
) WITHOUT Oids;
COMMIT;

CREATE TABLE roleaccesscontrolgroup
(
	roleid VARCHAR(256) REFERENCES roles(id) ON DELETE CASCADE,
	accesscontrolgroupid VARCHAR(256) REFERENCES accesscontrolgroups(id) ON DELETE CASCADE,
	PRIMARY KEY(roleid, accesscontrolgroupid)
) WITHOUT Oids;
COMMIT;

CREATE TABLE roleaccesscontrolitem
(
	roleid VARCHAR(256) REFERENCES roles(id) ON DELETE CASCADE,
	accesscontrolitemid VARCHAR(256) REFERENCES accesscontrolitems(id) ON DELETE CASCADE,
	PRIMARY KEY(roleid,accesscontrolitemid)
) WITHOUT Oids;
COMMIT;

CREATE TABLE accountrole
(
	roleid VARCHAR(256) REFERENCES roles(id) ON DELETE CASCADE,
	accountsid VARCHAR(256) REFERENCES accounts(sid) ON DELETE CASCADE,
	projectid VARCHAR(256)
) WITHOUT Oids;
COMMIT;

CREATE TABLE projectsystem
(
	projectid VARCHAR(256),
	systemid VARCHAR(256),
	systemmasterid VARCHAR(256)
) WITHOUT Oids;
COMMIT;

CREATE TABLE loginfailedusers
(
	id SERIAL,
	username VARCHAR(256),
	PRIMARY KEY(id)
) WITHOUT Oids;
COMMIT;
