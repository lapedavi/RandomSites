USE RandomSites_dev
GO

--****************************************************************************--
--------------------------------------------------------------------------------
---------------------Table Create-----------------------------------------------
--------------------------------------------------------------------------------
--****************************************************************************--

--
-- Table Structure for Table Suveys
--

CREATE TABLE Surveys (
	SurveyID int IDENTITY(1,1) PRIMARY KEY
	,[Name] nvarchar(191)
	,Description nvarchar(500)
	,NumOfOptions int NOT NULL
	,MultipleSelections bit NOT NULL
	)
GO

--
-- Table Structure for Table Options
--

CREATE TABLE Options (
	OptionID int IDENTITY(1,1) PRIMARY KEY
	,[Name] nvarchar(191)
	,SurveyID int NOT NULL CONSTRAINT Survey_Option FOREIGN KEY REFERENCES Surveys(SurveyID)
	)