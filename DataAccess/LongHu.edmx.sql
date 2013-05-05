-- --------------------------------------------------
-- Entity Designer DDL Script for Oracle database
-- --------------------------------------------------
-- Date Created: 2013/5/5 13:35:18
-- Generated from EDMX file: D:\Projects\MVC4\LongHu20130505\DataAccess\LongHu.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- ALTER TABLE "LONGUSER"."Employee" DROP CONSTRAINT "FK_R_DEPARTMENTS_EMPLOYEE" CASCADE;

-- ALTER TABLE "LONGUSER"."Department" DROP CONSTRAINT "FK_OrgChartDepartment" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectStructure" DROP CONSTRAINT "FK_OrgChartProjectStructure" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlan" DROP CONSTRAINT "FK_ProjectStructureProjectPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlan" DROP CONSTRAINT "FK_OrgChartProjectPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_ProjectPlanProjectPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_DepartmentProjectPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_ContractProjectPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_EmployeeProjectPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_EmployeeProjectPlanCollection1" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_EmployeeProjectPlanCollection2" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlanCollection" DROP CONSTRAINT "FK_EmployeeProjectPlanCollection3" CASCADE;

-- ALTER TABLE "LONGUSER"."DistricPlanSet" DROP CONSTRAINT "FK_OrgChartDistricPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."ProjectPlan" DROP CONSTRAINT "FK_EmployeeProjectPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."DistricPlanSet" DROP CONSTRAINT "FK_EmployeeDistricPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictPlanCollectionSet" DROP CONSTRAINT "FK_DistricPlanDistrictPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictPlanCollectionSet" DROP CONSTRAINT "FK_ContractDistrictPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictPlanDetailsSet" DROP CONSTRAINT "FK_DistrictPlanCollectionDistrictPlanDetails" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictPlanDetailsSet" DROP CONSTRAINT "FK_ProjectPlanCollectionDistrictPlanDetails" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanSet" DROP CONSTRAINT "FK_OrgChartGroupPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanSet" DROP CONSTRAINT "FK_EmployeeGroupPlan" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanCollectionSet" DROP CONSTRAINT "FK_GroupPlanGroupPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanCollectionSet" DROP CONSTRAINT "FK_ContractGroupPlanCollection" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanDetailSet" DROP CONSTRAINT "FK_GroupPlanCollectionGroupPlanDetail" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupPlanDetailSet" DROP CONSTRAINT "FK_DistrictPlanCollectionGroupPlanDetail" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictProjectRelationSet" DROP CONSTRAINT "FK_ProjectPlanCollectionDistrictProjectRelation" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupDistrictRelationSet" DROP CONSTRAINT "FK_GroupPlanGroupDistrictRelation" CASCADE;

-- ALTER TABLE "LONGUSER"."GroupDistrictRelationSet" DROP CONSTRAINT "FK_DistrictPlanCollectionGroupDistrictRelation" CASCADE;

-- ALTER TABLE "LONGUSER"."DistrictProjectRelationSet" DROP CONSTRAINT "FK_DistricPlanDistrictProjectRelation" CASCADE;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- DROP TABLE "LONGUSER"."OrgChart";

-- DROP TABLE "LONGUSER"."Contract";

-- DROP TABLE "LONGUSER"."DataDictionary";

-- DROP TABLE "LONGUSER"."Department";

-- DROP TABLE "LONGUSER"."Employee";

-- DROP TABLE "LONGUSER"."ProjectPlan";

-- DROP TABLE "LONGUSER"."ProjectPlanCollection";

-- DROP TABLE "LONGUSER"."ProjectStructure";

-- DROP TABLE "LONGUSER"."DistricPlanSet";

-- DROP TABLE "LONGUSER"."DistrictPlanCollectionSet";

-- DROP TABLE "LONGUSER"."DistrictPlanDetailsSet";

-- DROP TABLE "LONGUSER"."GroupPlanSet";

-- DROP TABLE "LONGUSER"."GroupPlanCollectionSet";

-- DROP TABLE "LONGUSER"."GroupPlanDetailSet";

-- DROP TABLE "LONGUSER"."DistrictProjectRelationSet";

-- DROP TABLE "LONGUSER"."GroupDistrictRelationSet";

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OrgChart'
CREATE TABLE "LONGUSER"."OrgChart" (
   "Id" NUMBER(38,0) NOT NULL,
   "Name" NVARCHAR2(200) NOT NULL,
   "ParentId" NUMBER(38,0) NULL,
   "Path" NVARCHAR2(100) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NCLOB NOT NULL
);

-- Creating table 'Contract'
CREATE TABLE "LONGUSER"."Contract" (
   "Id" NUMBER(38,0) NOT NULL,
   "Name" NVARCHAR2(200) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NCLOB NOT NULL,
   "Unit" NCLOB NOT NULL
);

-- Creating table 'DataDictionary'
CREATE TABLE "LONGUSER"."DataDictionary" (
   "Id" NUMBER(38,0) NOT NULL,
   "Name" NVARCHAR2(200) NOT NULL,
   "AttributeName" NVARCHAR2(100) NOT NULL,
   "Value" NVARCHAR2(200) NOT NULL,
   "DisplayOrder" NUMBER(38,0) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NCLOB NOT NULL
);

-- Creating table 'Department'
CREATE TABLE "LONGUSER"."Department" (
   "Id" NUMBER(38,0) NOT NULL,
   "Name" NVARCHAR2(200) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NCLOB NOT NULL,
   "OrgChartId" NUMBER(38,0) NULL
);

-- Creating table 'Employee'
CREATE TABLE "LONGUSER"."Employee" (
   "Id" NUMBER(38,0) NOT NULL,
   "UserName" NVARCHAR2(200) NOT NULL,
   "Email" NVARCHAR2(300) NULL,
   "ContactPhone1" VARCHAR2(16) NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "LoginName" NVARCHAR2(200) NOT NULL,
   "PassWord" NVARCHAR2(100) NOT NULL,
   "IsActive" NCLOB NOT NULL,
   "DepartmentId" NUMBER(38,0) NULL
);

-- Creating table 'ProjectPlan'
CREATE TABLE "LONGUSER"."ProjectPlan" (
   "Id" NUMBER(38,0) NOT NULL,
   "ProjectName" NVARCHAR2(200) NOT NULL,
   "BidPlanNumber" NVARCHAR2(200) NOT NULL,
   "Description" NCLOB NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "VersionName" NVARCHAR2(100) NULL,
   "Version" NUMBER(38,0) NULL,
   "IsForYear" NUMBER(5,0) NOT NULL,
   "IsActive" NCLOB NOT NULL,
   "OperatedOn" DATE NULL,
   "ProjectPlanStatusId" NUMBER(38,0) NOT NULL,
   "ProjectStructureId" NUMBER(38,0) NOT NULL,
   "OrgChartId" NUMBER(38,0) NOT NULL,
   "OperatedBEmployeeId" NUMBER(38,0) NOT NULL
);

-- Creating table 'ProjectPlanCollection'
CREATE TABLE "LONGUSER"."ProjectPlanCollection" (
   "Id" NUMBER(38,0) NOT NULL,
   "Year" CHAR(4) NOT NULL,
   "AgreementNumber" NVARCHAR2(200) NOT NULL,
   "Unit" NVARCHAR2(100) NOT NULL,
   "UnitPrice" NUMBER(11,2) NOT NULL,
   "EstimateValue" NUMBER(11,2) NOT NULL,
   "QualityRequirement" VARCHAR2(800) NOT NULL,
   "ReviewTime" DATE NULL,
   "DrawingProvidedTime" DATE NULL,
   "ContractTime" DATE NULL,
   "BidStartedTime" DATE NULL,
   "BidSeningTime" DATE NULL,
   "BidReturnTime" DATE NULL,
   "BidCheckTime" DATE NULL,
   "ContractSignTime" DATE NULL,
   "ComingIntoTime" DATE NULL,
   "Comments" NCLOB NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "BidTypeId" NUMBER(38,0) NULL,
   "IsActive" NCLOB NOT NULL,
   "ProjectPlanId" NUMBER(38,0) NOT NULL,
   "DepartmentId" NUMBER(38,0) NULL,
   "ContractId" NUMBER(38,0) NOT NULL,
   "OrganizerEmployeeId" NUMBER(38,0) NULL,
   "ResearchOwnerEmployeeId" NUMBER(38,0) NULL,
   "EngeerEmployeeId" NUMBER(38,0) NULL,
   "CostOwnerEmployeeId" NUMBER(38,0) NULL,
   "ContractCategoryId" NUMBER(38,0) NOT NULL,
   "ContractTypeId" NUMBER(38,0) NOT NULL,
   "ContractRelationId" NUMBER(38,0) NOT NULL
);

-- Creating table 'ProjectStructure'
CREATE TABLE "LONGUSER"."ProjectStructure" (
   "Id" NUMBER(38,0) NOT NULL,
   "Name" NVARCHAR2(200) NOT NULL,
   "PARENTID" NUMBER(38,0) NOT NULL,
   "Path" NVARCHAR2(200) NOT NULL,
   "IsProject" NUMBER(5,0) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "IsActive" NCLOB NOT NULL,
   "OrgChartId" NUMBER(38,0) NOT NULL
);

-- Creating table 'DistrictPlan'
CREATE TABLE "LONGUSER"."DistrictPlan" (
   "Id" NUMBER(38,0) NOT NULL,
   "DistricPlanNumber" NVARCHAR2(200) NOT NULL,
   "Description" NCLOB NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "VersionName" NVARCHAR2(100) NULL,
   "Version" NUMBER(38,0) NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "OperatedOn" DATE NULL,
   "DistricPlanStatusId" NUMBER(38,0) NOT NULL,
   "OrgChartId" NUMBER(38,0) NOT NULL,
   "OperatedBEmployeeId" NUMBER(38,0) NOT NULL
);

-- Creating table 'DistrictPlanCollection'
CREATE TABLE "LONGUSER"."DistrictPlanCollection" (
   "Id" NUMBER(38,0) NOT NULL,
   "Year" CHAR(4) NOT NULL,
   "AgreementNumber" NVARCHAR2(200) NOT NULL,
   "Unit" NVARCHAR2(100) NOT NULL,
   "QualityRequirement" VARCHAR2(800) NOT NULL,
   "ReviewTime" DATE NULL,
   "DrawingProvidedTime" DATE NULL,
   "ContractTime" DATE NULL,
   "BidStartedTime" DATE NULL,
   "BidSeningTime" DATE NULL,
   "BidReturnTime" DATE NULL,
   "BidCheckTime" DATE NULL,
   "ContractSignTime" DATE NULL,
   "ComingIntoTime" DATE NULL,
   "Comments" NCLOB NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "BidTypeId" NUMBER(38,0) NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "ContractCategoryId" NUMBER(38,0) NOT NULL,
   "BidModeId" NUMBER(38,0) NOT NULL,
   "DistricPlanId" NUMBER(38,0) NOT NULL,
   "ContractId" NUMBER(38,0) NOT NULL,
   "IsVoilate" NUMBER(5,0) NOT NULL,
   "Reason" NCLOB NOT NULL,
   "TotalNumber" NCLOB NOT NULL,
   "EstimatedTotalValue" NCLOB NOT NULL,
   "SubmittionNmuber" NCLOB NOT NULL,
   "SubmittionValue" NCLOB NOT NULL
);

-- Creating table 'DistrictPlanDetails'
CREATE TABLE "LONGUSER"."DistrictPlanDetails" (
   "Id" NUMBER(38,0) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "DistrictPlanCollectionId" NUMBER(38,0) NOT NULL,
   "ProjectPlanCollectionId" NUMBER(38,0) NOT NULL
);

-- Creating table 'GroupPlan'
CREATE TABLE "LONGUSER"."GroupPlan" (
   "Id" NUMBER(38,0) NOT NULL,
   "GroupPlanNumber" NVARCHAR2(200) NOT NULL,
   "Description" NCLOB NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "VersionName" NVARCHAR2(100) NULL,
   "Version" NUMBER(38,0) NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "OperatedOn" DATE NULL,
   "GroupPlanStatusId" NUMBER(38,0) NOT NULL,
   "OrgChartId" NUMBER(38,0) NOT NULL,
   "OperatedEmployeeId" NUMBER(38,0) NOT NULL
);

-- Creating table 'GroupPlanCollection'
CREATE TABLE "LONGUSER"."GroupPlanCollection" (
   "Id" NUMBER(38,0) NOT NULL,
   "Year" CHAR(4) NOT NULL,
   "AgreementNumber" NVARCHAR2(200) NOT NULL,
   "Unit" NVARCHAR2(100) NOT NULL,
   "QualityRequirement" VARCHAR2(800) NOT NULL,
   "ReviewTime" DATE NULL,
   "DrawingProvidedTime" DATE NULL,
   "ContractTime" DATE NULL,
   "BidStartedTime" DATE NULL,
   "BidSeningTime" DATE NULL,
   "BidReturnTime" DATE NULL,
   "BidCheckTime" DATE NULL,
   "ContractSignTime" DATE NULL,
   "ComingIntoTime" DATE NULL,
   "Comments" NCLOB NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "BidTypeId" NUMBER(38,0) NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "ContractCategoryId" NUMBER(38,0) NOT NULL,
   "BidModeId" NUMBER(38,0) NOT NULL,
   "IsVoilate" NUMBER(5,0) NOT NULL,
   "Reason" NCLOB NOT NULL,
   "TotalNumber" NCLOB NOT NULL,
   "EstimatedTotalValue" NCLOB NOT NULL,
   "SubmittionNmuber" NCLOB NOT NULL,
   "SubmittionValue" NCLOB NOT NULL,
   "GroupPlanId" NUMBER(38,0) NOT NULL,
   "ContractId" NUMBER(38,0) NOT NULL
);

-- Creating table 'GroupPlanDetail'
CREATE TABLE "LONGUSER"."GroupPlanDetail" (
   "Id" NUMBER(38,0) NOT NULL,
   "CreatedOn" DATE NOT NULL,
   "CreatedByEmployeeId" NUMBER(38,0) NOT NULL,
   "ModifiedOn" DATE NOT NULL,
   "ModifiedByEmployeeId" NUMBER(38,0) NOT NULL,
   "IsActive" NUMBER(5,0) NOT NULL,
   "GroupPlanCollectionId" NUMBER(38,0) NOT NULL,
   "DistrictPlanCollectionId" NUMBER(38,0) NOT NULL
);

-- Creating table 'DistrictProjectRelation'
CREATE TABLE "LONGUSER"."DistrictProjectRelation" (
   "Id" NUMBER(38,0) NOT NULL,
   "DistrictPlanId" NUMBER(38,0) NOT NULL,
   "ProjectPlanCollectionId" NUMBER(38,0) NOT NULL,
   "CreateOn" DATE NOT NULL,
   "ModifiedOn" DATE NOT NULL
);

-- Creating table 'GroupDistrictRelation'
CREATE TABLE "LONGUSER"."GroupDistrictRelation" (
   "Id" NUMBER(38,0) NOT NULL,
   "GroupPlanId" NUMBER(38,0) NOT NULL,
   "DistrictPlanCollectionId" NUMBER(38,0) NOT NULL,
   "CreateOn" DATE NOT NULL,
   "ModifiedOn" DATE NOT NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on "Id"in table 'OrgChart'
ALTER TABLE "LONGUSER"."OrgChart"
ADD CONSTRAINT "PK_OrgChart"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'Contract'
ALTER TABLE "LONGUSER"."Contract"
ADD CONSTRAINT "PK_Contract"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'DataDictionary'
ALTER TABLE "LONGUSER"."DataDictionary"
ADD CONSTRAINT "PK_DataDictionary"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'Department'
ALTER TABLE "LONGUSER"."Department"
ADD CONSTRAINT "PK_Department"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'Employee'
ALTER TABLE "LONGUSER"."Employee"
ADD CONSTRAINT "PK_Employee"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'ProjectPlan'
ALTER TABLE "LONGUSER"."ProjectPlan"
ADD CONSTRAINT "PK_ProjectPlan"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "PK_ProjectPlanCollection"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'ProjectStructure'
ALTER TABLE "LONGUSER"."ProjectStructure"
ADD CONSTRAINT "PK_ProjectStructure"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'DistrictPlan'
ALTER TABLE "LONGUSER"."DistrictPlan"
ADD CONSTRAINT "PK_DistrictPlan"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'DistrictPlanCollection'
ALTER TABLE "LONGUSER"."DistrictPlanCollection"
ADD CONSTRAINT "PK_DistrictPlanCollection"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'DistrictPlanDetails'
ALTER TABLE "LONGUSER"."DistrictPlanDetails"
ADD CONSTRAINT "PK_DistrictPlanDetails"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'GroupPlan'
ALTER TABLE "LONGUSER"."GroupPlan"
ADD CONSTRAINT "PK_GroupPlan"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'GroupPlanCollection'
ALTER TABLE "LONGUSER"."GroupPlanCollection"
ADD CONSTRAINT "PK_GroupPlanCollection"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'GroupPlanDetail'
ALTER TABLE "LONGUSER"."GroupPlanDetail"
ADD CONSTRAINT "PK_GroupPlanDetail"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'DistrictProjectRelation'
ALTER TABLE "LONGUSER"."DistrictProjectRelation"
ADD CONSTRAINT "PK_DistrictProjectRelation"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- Creating primary key on "Id"in table 'GroupDistrictRelation'
ALTER TABLE "LONGUSER"."GroupDistrictRelation"
ADD CONSTRAINT "PK_GroupDistrictRelation"
   PRIMARY KEY ("Id" )
   ENABLE
   VALIDATE;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on "DepartmentId" in table 'Employee'
ALTER TABLE "LONGUSER"."Employee"
ADD CONSTRAINT "FK_R_DEPARTMENTS_EMPLOYEE"
   FOREIGN KEY ("DepartmentId")
   REFERENCES "LONGUSER"."Department"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_R_DEPARTMENTS_EMPLOYEE'
CREATE INDEX "IX_FK_R_DEPARTMENTS_EMPLOYEE"
ON "LONGUSER"."Employee"
   ("DepartmentId");

-- Creating foreign key on "OrgChartId" in table 'Department'
ALTER TABLE "LONGUSER"."Department"
ADD CONSTRAINT "FK_OrgChartDepartment"
   FOREIGN KEY ("OrgChartId")
   REFERENCES "LONGUSER"."OrgChart"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OrgChartDepartment'
CREATE INDEX "IX_FK_OrgChartDepartment"
ON "LONGUSER"."Department"
   ("OrgChartId");

-- Creating foreign key on "OrgChartId" in table 'ProjectStructure'
ALTER TABLE "LONGUSER"."ProjectStructure"
ADD CONSTRAINT "FK_OrgChartProjectStructure"
   FOREIGN KEY ("OrgChartId")
   REFERENCES "LONGUSER"."OrgChart"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OrgChartProjectStructure'
CREATE INDEX "IX_FK_OrgChartProjectStructure"
ON "LONGUSER"."ProjectStructure"
   ("OrgChartId");

-- Creating foreign key on "ProjectStructureId" in table 'ProjectPlan'
ALTER TABLE "LONGUSER"."ProjectPlan"
ADD CONSTRAINT "FK_ProjectStructureProjectPlan"
   FOREIGN KEY ("ProjectStructureId")
   REFERENCES "LONGUSER"."ProjectStructure"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ProjectStructureProjectPlan'
CREATE INDEX "IX_FK_ProjectStructureProjectPlan"
ON "LONGUSER"."ProjectPlan"
   ("ProjectStructureId");

-- Creating foreign key on "OrgChartId" in table 'ProjectPlan'
ALTER TABLE "LONGUSER"."ProjectPlan"
ADD CONSTRAINT "FK_OrgChartProjectPlan"
   FOREIGN KEY ("OrgChartId")
   REFERENCES "LONGUSER"."OrgChart"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OrgChartProjectPlan'
CREATE INDEX "IX_FK_OrgChartProjectPlan"
ON "LONGUSER"."ProjectPlan"
   ("OrgChartId");

-- Creating foreign key on "ProjectPlanId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_ProjectPlanProjectPlanCollection"
   FOREIGN KEY ("ProjectPlanId")
   REFERENCES "LONGUSER"."ProjectPlan"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ProjectPlanProjectPlanCollection'
CREATE INDEX "IX_FK_ProjectPlanProjectPlanCollection"
ON "LONGUSER"."ProjectPlanCollection"
   ("ProjectPlanId");

-- Creating foreign key on "DepartmentId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_DepartmentProjectPlanCollection"
   FOREIGN KEY ("DepartmentId")
   REFERENCES "LONGUSER"."Department"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DepartmentProjectPlanCollection'
CREATE INDEX "IX_FK_DepartmentProjectPlanCollection"
ON "LONGUSER"."ProjectPlanCollection"
   ("DepartmentId");

-- Creating foreign key on "ContractId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_ContractProjectPlanCollection"
   FOREIGN KEY ("ContractId")
   REFERENCES "LONGUSER"."Contract"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ContractProjectPlanCollection'
CREATE INDEX "IX_FK_ContractProjectPlanCollection"
ON "LONGUSER"."ProjectPlanCollection"
   ("ContractId");

-- Creating foreign key on "OrganizerEmployeeId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_EmployeeProjectPlanCollection"
   FOREIGN KEY ("OrganizerEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeProjectPlanCollection'
CREATE INDEX "IX_FK_EmployeeProjectPlanCollection"
ON "LONGUSER"."ProjectPlanCollection"
   ("OrganizerEmployeeId");

-- Creating foreign key on "ResearchOwnerEmployeeId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_EmployeeProjectPlanCollection1"
   FOREIGN KEY ("ResearchOwnerEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeProjectPlanCollection1'
CREATE INDEX "IX_FK_EmployeeProjectPlanCollection1"
ON "LONGUSER"."ProjectPlanCollection"
   ("ResearchOwnerEmployeeId");

-- Creating foreign key on "EngeerEmployeeId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_EmployeeProjectPlanCollection2"
   FOREIGN KEY ("EngeerEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeProjectPlanCollection2'
CREATE INDEX "IX_FK_EmployeeProjectPlanCollection2"
ON "LONGUSER"."ProjectPlanCollection"
   ("EngeerEmployeeId");

-- Creating foreign key on "CostOwnerEmployeeId" in table 'ProjectPlanCollection'
ALTER TABLE "LONGUSER"."ProjectPlanCollection"
ADD CONSTRAINT "FK_EmployeeProjectPlanCollection3"
   FOREIGN KEY ("CostOwnerEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeProjectPlanCollection3'
CREATE INDEX "IX_FK_EmployeeProjectPlanCollection3"
ON "LONGUSER"."ProjectPlanCollection"
   ("CostOwnerEmployeeId");

-- Creating foreign key on "OrgChartId" in table 'DistrictPlan'
ALTER TABLE "LONGUSER"."DistrictPlan"
ADD CONSTRAINT "FK_OrgChartDistricPlan"
   FOREIGN KEY ("OrgChartId")
   REFERENCES "LONGUSER"."OrgChart"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OrgChartDistricPlan'
CREATE INDEX "IX_FK_OrgChartDistricPlan"
ON "LONGUSER"."DistrictPlan"
   ("OrgChartId");

-- Creating foreign key on "OperatedBEmployeeId" in table 'ProjectPlan'
ALTER TABLE "LONGUSER"."ProjectPlan"
ADD CONSTRAINT "FK_EmployeeProjectPlan"
   FOREIGN KEY ("OperatedBEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeProjectPlan'
CREATE INDEX "IX_FK_EmployeeProjectPlan"
ON "LONGUSER"."ProjectPlan"
   ("OperatedBEmployeeId");

-- Creating foreign key on "OperatedBEmployeeId" in table 'DistrictPlan'
ALTER TABLE "LONGUSER"."DistrictPlan"
ADD CONSTRAINT "FK_EmployeeDistricPlan"
   FOREIGN KEY ("OperatedBEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeDistricPlan'
CREATE INDEX "IX_FK_EmployeeDistricPlan"
ON "LONGUSER"."DistrictPlan"
   ("OperatedBEmployeeId");

-- Creating foreign key on "DistricPlanId" in table 'DistrictPlanCollection'
ALTER TABLE "LONGUSER"."DistrictPlanCollection"
ADD CONSTRAINT "FK_DistricPlanDistrictPlanCollection"
   FOREIGN KEY ("DistricPlanId")
   REFERENCES "LONGUSER"."DistrictPlan"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DistricPlanDistrictPlanCollection'
CREATE INDEX "IX_FK_DistricPlanDistrictPlanCollection"
ON "LONGUSER"."DistrictPlanCollection"
   ("DistricPlanId");

-- Creating foreign key on "ContractId" in table 'DistrictPlanCollection'
ALTER TABLE "LONGUSER"."DistrictPlanCollection"
ADD CONSTRAINT "FK_ContractDistrictPlanCollection"
   FOREIGN KEY ("ContractId")
   REFERENCES "LONGUSER"."Contract"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ContractDistrictPlanCollection'
CREATE INDEX "IX_FK_ContractDistrictPlanCollection"
ON "LONGUSER"."DistrictPlanCollection"
   ("ContractId");

-- Creating foreign key on "DistrictPlanCollectionId" in table 'DistrictPlanDetails'
ALTER TABLE "LONGUSER"."DistrictPlanDetails"
ADD CONSTRAINT "FK_DistrictPlanCollectionDistrictPlanDetails"
   FOREIGN KEY ("DistrictPlanCollectionId")
   REFERENCES "LONGUSER"."DistrictPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DistrictPlanCollectionDistrictPlanDetails'
CREATE INDEX "IX_FK_DistrictPlanCollectionDistrictPlanDetails"
ON "LONGUSER"."DistrictPlanDetails"
   ("DistrictPlanCollectionId");

-- Creating foreign key on "ProjectPlanCollectionId" in table 'DistrictPlanDetails'
ALTER TABLE "LONGUSER"."DistrictPlanDetails"
ADD CONSTRAINT "FK_ProjectPlanCollectionDistrictPlanDetails"
   FOREIGN KEY ("ProjectPlanCollectionId")
   REFERENCES "LONGUSER"."ProjectPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ProjectPlanCollectionDistrictPlanDetails'
CREATE INDEX "IX_FK_ProjectPlanCollectionDistrictPlanDetails"
ON "LONGUSER"."DistrictPlanDetails"
   ("ProjectPlanCollectionId");

-- Creating foreign key on "OrgChartId" in table 'GroupPlan'
ALTER TABLE "LONGUSER"."GroupPlan"
ADD CONSTRAINT "FK_OrgChartGroupPlan"
   FOREIGN KEY ("OrgChartId")
   REFERENCES "LONGUSER"."OrgChart"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_OrgChartGroupPlan'
CREATE INDEX "IX_FK_OrgChartGroupPlan"
ON "LONGUSER"."GroupPlan"
   ("OrgChartId");

-- Creating foreign key on "OperatedEmployeeId" in table 'GroupPlan'
ALTER TABLE "LONGUSER"."GroupPlan"
ADD CONSTRAINT "FK_EmployeeGroupPlan"
   FOREIGN KEY ("OperatedEmployeeId")
   REFERENCES "LONGUSER"."Employee"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_EmployeeGroupPlan'
CREATE INDEX "IX_FK_EmployeeGroupPlan"
ON "LONGUSER"."GroupPlan"
   ("OperatedEmployeeId");

-- Creating foreign key on "GroupPlanId" in table 'GroupPlanCollection'
ALTER TABLE "LONGUSER"."GroupPlanCollection"
ADD CONSTRAINT "FK_GroupPlanGroupPlanCollection"
   FOREIGN KEY ("GroupPlanId")
   REFERENCES "LONGUSER"."GroupPlan"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_GroupPlanGroupPlanCollection'
CREATE INDEX "IX_FK_GroupPlanGroupPlanCollection"
ON "LONGUSER"."GroupPlanCollection"
   ("GroupPlanId");

-- Creating foreign key on "ContractId" in table 'GroupPlanCollection'
ALTER TABLE "LONGUSER"."GroupPlanCollection"
ADD CONSTRAINT "FK_ContractGroupPlanCollection"
   FOREIGN KEY ("ContractId")
   REFERENCES "LONGUSER"."Contract"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ContractGroupPlanCollection'
CREATE INDEX "IX_FK_ContractGroupPlanCollection"
ON "LONGUSER"."GroupPlanCollection"
   ("ContractId");

-- Creating foreign key on "GroupPlanCollectionId" in table 'GroupPlanDetail'
ALTER TABLE "LONGUSER"."GroupPlanDetail"
ADD CONSTRAINT "FK_GroupPlanCollectionGroupPlanDetail"
   FOREIGN KEY ("GroupPlanCollectionId")
   REFERENCES "LONGUSER"."GroupPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_GroupPlanCollectionGroupPlanDetail'
CREATE INDEX "IX_FK_GroupPlanCollectionGroupPlanDetail"
ON "LONGUSER"."GroupPlanDetail"
   ("GroupPlanCollectionId");

-- Creating foreign key on "DistrictPlanCollectionId" in table 'GroupPlanDetail'
ALTER TABLE "LONGUSER"."GroupPlanDetail"
ADD CONSTRAINT "FK_DistrictPlanCollectionGroupPlanDetail"
   FOREIGN KEY ("DistrictPlanCollectionId")
   REFERENCES "LONGUSER"."DistrictPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DistrictPlanCollectionGroupPlanDetail'
CREATE INDEX "IX_FK_DistrictPlanCollectionGroupPlanDetail"
ON "LONGUSER"."GroupPlanDetail"
   ("DistrictPlanCollectionId");

-- Creating foreign key on "ProjectPlanCollectionId" in table 'DistrictProjectRelation'
ALTER TABLE "LONGUSER"."DistrictProjectRelation"
ADD CONSTRAINT "FK_ProjectPlanCollectionDistrictProjectRelation"
   FOREIGN KEY ("ProjectPlanCollectionId")
   REFERENCES "LONGUSER"."ProjectPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_ProjectPlanCollectionDistrictProjectRelation'
CREATE INDEX "IX_FK_ProjectPlanCollectionDistrictProjectRelation"
ON "LONGUSER"."DistrictProjectRelation"
   ("ProjectPlanCollectionId");

-- Creating foreign key on "GroupPlanId" in table 'GroupDistrictRelation'
ALTER TABLE "LONGUSER"."GroupDistrictRelation"
ADD CONSTRAINT "FK_GroupPlanGroupDistrictRelation"
   FOREIGN KEY ("GroupPlanId")
   REFERENCES "LONGUSER"."GroupPlan"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_GroupPlanGroupDistrictRelation'
CREATE INDEX "IX_FK_GroupPlanGroupDistrictRelation"
ON "LONGUSER"."GroupDistrictRelation"
   ("GroupPlanId");

-- Creating foreign key on "DistrictPlanCollectionId" in table 'GroupDistrictRelation'
ALTER TABLE "LONGUSER"."GroupDistrictRelation"
ADD CONSTRAINT "FK_DistrictPlanCollectionGroupDistrictRelation"
   FOREIGN KEY ("DistrictPlanCollectionId")
   REFERENCES "LONGUSER"."DistrictPlanCollection"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DistrictPlanCollectionGroupDistrictRelation'
CREATE INDEX "IX_FK_DistrictPlanCollectionGroupDistrictRelation"
ON "LONGUSER"."GroupDistrictRelation"
   ("DistrictPlanCollectionId");

-- Creating foreign key on "DistrictPlanId" in table 'DistrictProjectRelation'
ALTER TABLE "LONGUSER"."DistrictProjectRelation"
ADD CONSTRAINT "FK_DistricPlanDistrictProjectRelation"
   FOREIGN KEY ("DistrictPlanId")
   REFERENCES "LONGUSER"."DistrictPlan"
       ("Id")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_DistricPlanDistrictProjectRelation'
CREATE INDEX "IX_FK_DistricPlanDistrictProjectRelation"
ON "LONGUSER"."DistrictProjectRelation"
   ("DistrictPlanId");

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
