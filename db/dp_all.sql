USE [luxentcz]
GO
/****** Object:  User [luxentcz]    Script Date: 11/23/2008 17:16:16 ******/
CREATE USER [luxentcz] FOR LOGIN [luxentcz] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[EstateOfferType]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateOfferType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrencyType]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateProperties]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateProperties](
	[EstateId] [int] NOT NULL,
	[Created] [datetime] NULL,
	[Disposition] [nvarchar](255) NULL,
	[FlatSize] [nvarchar](255) NULL,
	[Floor] [nvarchar](255) NULL,
	[TotalFloors] [nvarchar](255) NULL,
	[FloorSize] [nvarchar](255) NULL,
	[FlatsTotal] [nvarchar](255) NULL,
	[FlatsTypes] [nvarchar](255) NULL,
	[TotalFlatsSize] [nvarchar](255) NULL,
	[ReconstructionYear] [nvarchar](255) NULL,
	[BuildYear] [nvarchar](255) NULL,
	[ColaudationYear] [nvarchar](255) NULL,
	[ForMovement] [nvarchar](255) NULL,
	[BuildedPlaceSize] [nvarchar](255) NULL,
	[UseablePlaceSize] [nvarchar](255) NULL,
	[PropertySize] [nvarchar](255) NULL,
	[TeracesSize] [nvarchar](255) NULL,
	[GardenSize] [nvarchar](255) NULL,
	[UndergroundSize] [nvarchar](255) NULL,
	[PlaceSize] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Culture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Culture](
	[Id] [int] NOT NULL,
	[Created] [datetime] NULL,
	[IsDefault] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateMessage]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[MessageType] [nvarchar](255) NULL,
	[EstateType] [nvarchar](255) NULL,
	[EstateOfferType] [nvarchar](255) NULL,
	[Ps] [nvarchar](255) NULL,
	[PriceFrom] [nvarchar](255) NULL,
	[PriceTo] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[FirmName] [nvarchar](255) NULL,
	[Street] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[Zip] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[www] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Telephone] [nvarchar](255) NULL,
	[Fax] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actuality]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actuality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Publish] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 11/23/2008 17:16:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateType]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageTemplate]    Script Date: 11/23/2008 17:16:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Title] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceType]    Script Date: 11/23/2008 17:16:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateManCard]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateManCard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Title] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Telephone] [nvarchar](255) NULL,
	[Mobil] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateOfferTypeCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateOfferTypeCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[EstateOfferTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estate]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estate](
	[EstateId] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[HotTip] [bit] NULL,
	[Publish] [bit] NULL,
	[Saled] [bit] NULL,
	[Rented] [bit] NULL,
	[Reserved] [bit] NULL,
	[Exclusivity] [bit] NULL,
	[EstateTypeId] [int] NULL,
	[DeveloperProjectId] [int] NULL,
	[EstateOfferTypeId] [int] NULL,
	[EstateManCardId] [int] NULL,
	[EstateAddressInfoStreet] [nvarchar](255) NULL,
	[EstateAddressInfoCity] [nvarchar](255) NULL,
	[EstateAddressInfoZip] [nvarchar](255) NULL,
	[EstateAddressInfoRegionId] [int] NULL,
	[EstatePriceInfoPriceValue] [int] NULL,
	[EstatePriceInfoBankruptcyEstateSale] [bit] NULL,
	[EstatePriceInfoCurrencyTypeId] [int] NULL,
	[EstatePriceInfoPriceTypeId] [int] NULL,
	[EstateExtendedInfoOrderNumber] [nvarchar](255) NULL,
	[EstateExtendedInfoVirtualTourUrl] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstatePropertiesCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstatePropertiesCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ObjectType] [nvarchar](255) NULL,
	[BuildingType] [nvarchar](255) NULL,
	[BuildingState] [nvarchar](255) NULL,
	[EstateType] [nvarchar](255) NULL,
	[NoLiveArea] [nvarchar](255) NULL,
	[Constructions] [nvarchar](255) NULL,
	[OwnerShip] [nvarchar](255) NULL,
	[Properties] [nvarchar](255) NULL,
	[LocalityInfo] [nvarchar](255) NULL,
	[Green] [nvarchar](255) NULL,
	[Sites] [nvarchar](255) NULL,
	[Comunication] [nvarchar](255) NULL,
	[Telecomunication] [nvarchar](255) NULL,
	[Electricity] [nvarchar](255) NULL,
	[Water] [nvarchar](255) NULL,
	[Channel] [nvarchar](255) NULL,
	[Heating] [nvarchar](255) NULL,
	[Gas] [nvarchar](255) NULL,
	[Trafic] [nvarchar](255) NULL,
	[Facilities] [nvarchar](255) NULL,
	[UseType] [nvarchar](255) NULL,
	[SocialMachine] [nvarchar](255) NULL,
	[Stairs] [nvarchar](255) NULL,
	[OtherInfo] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[EstateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateTypeCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateTypeCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[EstateTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActualityCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActualityCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Heading] [nvarchar](255) NULL,
	[Perex] [nvarchar](255) NULL,
	[Content] [nvarchar](max) NULL,
	[CultureId] [int] NULL,
	[ActualityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperProjectCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperProjectCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[LocalityDescription] [nvarchar](255) NULL,
	[BasicDescription] [nvarchar](255) NULL,
	[FullDescription] [nvarchar](255) NULL,
	[ProjectsEtapsDescription] [nvarchar](255) NULL,
	[Parking] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[DeveloperProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Section]    Script Date: 11/23/2008 17:16:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[OrderPriority] [int] NULL,
	[Created] [datetime] NULL,
	[Published] [bit] NULL,
	[ParentSectionId] [int] NULL,
	[CultureId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateCulture]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[BasicDescription] [nvarchar](max) NULL,
	[FullDescription] [nvarchar](max) NULL,
	[AdditionalDescription] [nvarchar](max) NULL,
	[PriceComment] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[EstateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceTypeCulture]    Script Date: 11/23/2008 17:16:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceTypeCulture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CultureId] [int] NULL,
	[PriceTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Keywords] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Contents] [nvarchar](max) NULL,
	[ConnectedPageId] [int] NULL,
	[Title] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[OrderPriority] [int] NULL,
	[Created] [datetime] NULL,
	[Published] [bit] NULL,
	[SectionId] [int] NULL,
	[PageTemplateId] [int] NULL,
	[CultureId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperProject]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperProject](
	[DeveloperProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[HotTip] [bit] NULL,
	[Publish] [bit] NULL,
	[EstateManCardId] [int] NULL,
	[DeveloperId] [int] NULL,
	[InvestorId] [int] NULL,
	[EstateAddressInfoStreet] [nvarchar](255) NULL,
	[EstateAddressInfoCity] [nvarchar](255) NULL,
	[EstateAddressInfoZip] [nvarchar](255) NULL,
	[EstateAddressInfoRegionId] [int] NULL,
	[ProjectNextInfoForMove] [nvarchar](255) NULL,
	[ProjectNextInfoBuildStartDate] [nvarchar](255) NULL,
	[ProjectNextInfoBuildEndDate] [nvarchar](255) NULL,
	[ProjectNextInfoSaleStartDate] [nvarchar](255) NULL,
	[ProjectNextInfoBaseInvestPercents] [int] NULL,
	[ProjectNextInfoBuildingSavingPercents] [int] NULL,
	[ProjectNextInfoHypoPercents] [int] NULL,
	[ProjectNextInfoHypoCompany] [nvarchar](255) NULL,
	[ProjectNextInfoPersonalRight] [bit] NULL,
	[ProjectNextInfoBuildingsInProject] [int] NULL,
	[AdditionalProjectObjectsInfoOtherAreas] [nvarchar](255) NULL,
	[AdditionalProjectObjectsInfoGarages] [bit] NULL,
	[AdditionalProjectObjectsInfoParkingCount] [int] NULL,
	[BuildingConstructionInfoPlatform] [nvarchar](255) NULL,
	[BuildingConstructionInfoRoofliner] [nvarchar](255) NULL,
	[BuildingConstructionInfoRoof] [nvarchar](255) NULL,
	[BuildingConstructionInfoCovering] [nvarchar](255) NULL,
	[BuildingConstructionInfoInnerPlaster] [nvarchar](255) NULL,
	[BuildingConstructionInfoOuterPlaster] [nvarchar](255) NULL,
	[BuildingConstructionInfoInnerCoat] [nvarchar](255) NULL,
	[BuildingConstructionInfoOuterCoat] [nvarchar](255) NULL,
	[BuildingConstructionInfoPlumbConstruction] [nvarchar](255) NULL,
	[BuildingConstructionInfoFloors] [nvarchar](255) NULL,
	[BuildingConstructionInfoStairs] [nvarchar](255) NULL,
	[BuildingConstructionInfoDoors] [nvarchar](255) NULL,
	[BuildingConstructionInfoWindows] [nvarchar](255) NULL,
	[BuildingConstructionInfoKitchen] [bit] NULL,
	[TelecomunicationInfoTelephone] [bit] NULL,
	[TelecomunicationInfoInternet] [bit] NULL,
	[TelecomunicationInfoSatelit] [bit] NULL,
	[TelecomunicationInfoCableTV] [bit] NULL,
	[TelecomunicationInfoCableWires] [bit] NULL,
	[TelecomunicationInfoOtherWires] [bit] NULL,
	[IngSitesInfoLocalGasHeating] [bit] NULL,
	[IngSitesInfoLocalSolidHeating] [bit] NULL,
	[IngSitesInfoLocalElHeating] [bit] NULL,
	[IngSitesInfoCentralGasHeating] [bit] NULL,
	[IngSitesInfoCentralSolidHeating] [bit] NULL,
	[IngSitesInfoCentralElHeating] [bit] NULL,
	[IngSitesInfoCentralRemoteHeating] [bit] NULL,
	[IngSitesInfoAnotherHeating] [bit] NULL,
	[IngSitesInfoTransportRail] [bit] NULL,
	[IngSitesInfoTransportHighWay] [bit] NULL,
	[IngSitesInfoTransportRoad] [bit] NULL,
	[IngSitesInfoTransportMHD] [bit] NULL,
	[IngSitesInfoTransportBus] [bit] NULL,
	[IngSitesInfoCentralWater] [bit] NULL,
	[IngSitesInfoRemoteWater] [bit] NULL,
	[IngSitesInfoHotCoolWater] [bit] NULL,
	[IngSitesInfoDrainage] [bit] NULL,
	[IngSitesInfoCentralWaterCleaner] [bit] NULL,
	[IngSitesInfoIndividualGas] [bit] NULL,
	[IngSitesInfoGasConduit] [bit] NULL,
	[ElectricityInfoEl120V] [bit] NULL,
	[ElectricityInfoEl230V] [bit] NULL,
	[ElectricityInfoEl380V] [bit] NULL,
	[ElectricityInfoEl400V] [bit] NULL,
	[FacilitiesInfoSchool] [bit] NULL,
	[FacilitiesInfoPlayGroup] [bit] NULL,
	[FacilitiesInfoHospital] [bit] NULL,
	[FacilitiesInfoPost] [bit] NULL,
	[FacilitiesInfoSupermarket] [bit] NULL,
	[FacilitiesInfoCompleteSite] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DeveloperProjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperProjectImage]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperProjectImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[FilePath] [nvarchar](255) NULL,
	[Description_cs] [nvarchar](255) NULL,
	[Description_en] [nvarchar](255) NULL,
	[IsMain] [bit] NULL,
	[DeveloperProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperProjectAttachment]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperProjectAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[FilePath] [nvarchar](255) NULL,
	[FileSize] [bigint] NULL,
	[Description_cs] [nvarchar](255) NULL,
	[Description_en] [nvarchar](255) NULL,
	[DeveloperProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateAttachment]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateAttachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[FilePath] [nvarchar](255) NULL,
	[FileSize] [bigint] NULL,
	[Description_cs] [nvarchar](255) NULL,
	[Description_en] [nvarchar](255) NULL,
	[EstateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstateImage]    Script Date: 11/23/2008 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstateImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[FilePath] [nvarchar](255) NULL,
	[Description_cs] [nvarchar](255) NULL,
	[Description_en] [nvarchar](255) NULL,
	[IsMain] [bit] NULL,
	[EstateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FKE42FE5857DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[ActualityCulture]  WITH CHECK ADD  CONSTRAINT [FKE42FE5857DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[ActualityCulture] CHECK CONSTRAINT [FKE42FE5857DEE8A4D]
GO
/****** Object:  ForeignKey [FKE42FE585F0A20965]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[ActualityCulture]  WITH CHECK ADD  CONSTRAINT [FKE42FE585F0A20965] FOREIGN KEY([ActualityId])
REFERENCES [dbo].[Actuality] ([Id])
GO
ALTER TABLE [dbo].[ActualityCulture] CHECK CONSTRAINT [FKE42FE585F0A20965]
GO
/****** Object:  ForeignKey [FKE98271B3318C735]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProject]  WITH CHECK ADD  CONSTRAINT [FKE98271B3318C735] FOREIGN KEY([InvestorId])
REFERENCES [dbo].[CompanyInfo] ([Id])
GO
ALTER TABLE [dbo].[DeveloperProject] CHECK CONSTRAINT [FKE98271B3318C735]
GO
/****** Object:  ForeignKey [FKE98271B33F27FB17]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProject]  WITH CHECK ADD  CONSTRAINT [FKE98271B33F27FB17] FOREIGN KEY([EstateManCardId])
REFERENCES [dbo].[EstateManCard] ([Id])
GO
ALTER TABLE [dbo].[DeveloperProject] CHECK CONSTRAINT [FKE98271B33F27FB17]
GO
/****** Object:  ForeignKey [FKE98271B3B7D65FD0]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProject]  WITH CHECK ADD  CONSTRAINT [FKE98271B3B7D65FD0] FOREIGN KEY([DeveloperId])
REFERENCES [dbo].[CompanyInfo] ([Id])
GO
ALTER TABLE [dbo].[DeveloperProject] CHECK CONSTRAINT [FKE98271B3B7D65FD0]
GO
/****** Object:  ForeignKey [FKE98271B3BFACF329]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProject]  WITH CHECK ADD  CONSTRAINT [FKE98271B3BFACF329] FOREIGN KEY([EstateAddressInfoRegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[DeveloperProject] CHECK CONSTRAINT [FKE98271B3BFACF329]
GO
/****** Object:  ForeignKey [FK45D436EDE24BD587]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProjectAttachment]  WITH CHECK ADD  CONSTRAINT [FK45D436EDE24BD587] FOREIGN KEY([DeveloperProjectId])
REFERENCES [dbo].[DeveloperProject] ([DeveloperProjectId])
GO
ALTER TABLE [dbo].[DeveloperProjectAttachment] CHECK CONSTRAINT [FK45D436EDE24BD587]
GO
/****** Object:  ForeignKey [FKFB06A9E67DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProjectCulture]  WITH CHECK ADD  CONSTRAINT [FKFB06A9E67DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[DeveloperProjectCulture] CHECK CONSTRAINT [FKFB06A9E67DEE8A4D]
GO
/****** Object:  ForeignKey [FKFB06A9E6E24BD587]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProjectCulture]  WITH CHECK ADD  CONSTRAINT [FKFB06A9E6E24BD587] FOREIGN KEY([DeveloperProjectId])
REFERENCES [dbo].[DeveloperProject] ([DeveloperProjectId])
GO
ALTER TABLE [dbo].[DeveloperProjectCulture] CHECK CONSTRAINT [FKFB06A9E6E24BD587]
GO
/****** Object:  ForeignKey [FKD1D4CC9FE24BD587]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[DeveloperProjectImage]  WITH CHECK ADD  CONSTRAINT [FKD1D4CC9FE24BD587] FOREIGN KEY([DeveloperProjectId])
REFERENCES [dbo].[DeveloperProject] ([DeveloperProjectId])
GO
ALTER TABLE [dbo].[DeveloperProjectImage] CHECK CONSTRAINT [FKD1D4CC9FE24BD587]
GO
/****** Object:  ForeignKey [FK33B5BFAB1D0837FB]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFAB1D0837FB] FOREIGN KEY([EstatePriceInfoPriceTypeId])
REFERENCES [dbo].[PriceType] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFAB1D0837FB]
GO
/****** Object:  ForeignKey [FK33B5BFAB3F27FB17]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFAB3F27FB17] FOREIGN KEY([EstateManCardId])
REFERENCES [dbo].[EstateManCard] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFAB3F27FB17]
GO
/****** Object:  ForeignKey [FK33B5BFAB62582799]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFAB62582799] FOREIGN KEY([EstateTypeId])
REFERENCES [dbo].[EstateType] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFAB62582799]
GO
/****** Object:  ForeignKey [FK33B5BFAB71F09A70]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFAB71F09A70] FOREIGN KEY([EstatePriceInfoCurrencyTypeId])
REFERENCES [dbo].[CurrencyType] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFAB71F09A70]
GO
/****** Object:  ForeignKey [FK33B5BFAB92C1E1B7]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFAB92C1E1B7] FOREIGN KEY([EstateOfferTypeId])
REFERENCES [dbo].[EstateOfferType] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFAB92C1E1B7]
GO
/****** Object:  ForeignKey [FK33B5BFABBFACF329]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFABBFACF329] FOREIGN KEY([EstateAddressInfoRegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFABBFACF329]
GO
/****** Object:  ForeignKey [FK33B5BFABE24BD587]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Estate]  WITH CHECK ADD  CONSTRAINT [FK33B5BFABE24BD587] FOREIGN KEY([DeveloperProjectId])
REFERENCES [dbo].[DeveloperProject] ([DeveloperProjectId])
GO
ALTER TABLE [dbo].[Estate] CHECK CONSTRAINT [FK33B5BFABE24BD587]
GO
/****** Object:  ForeignKey [FK6BD14D06297BE84C]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateAttachment]  WITH CHECK ADD  CONSTRAINT [FK6BD14D06297BE84C] FOREIGN KEY([EstateId])
REFERENCES [dbo].[Estate] ([EstateId])
GO
ALTER TABLE [dbo].[EstateAttachment] CHECK CONSTRAINT [FK6BD14D06297BE84C]
GO
/****** Object:  ForeignKey [FK16FEA58B297BE84C]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateCulture]  WITH CHECK ADD  CONSTRAINT [FK16FEA58B297BE84C] FOREIGN KEY([EstateId])
REFERENCES [dbo].[Estate] ([EstateId])
GO
ALTER TABLE [dbo].[EstateCulture] CHECK CONSTRAINT [FK16FEA58B297BE84C]
GO
/****** Object:  ForeignKey [FK16FEA58B7DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateCulture]  WITH CHECK ADD  CONSTRAINT [FK16FEA58B7DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[EstateCulture] CHECK CONSTRAINT [FK16FEA58B7DEE8A4D]
GO
/****** Object:  ForeignKey [FK8A3B06C3297BE84C]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateImage]  WITH CHECK ADD  CONSTRAINT [FK8A3B06C3297BE84C] FOREIGN KEY([EstateId])
REFERENCES [dbo].[Estate] ([EstateId])
GO
ALTER TABLE [dbo].[EstateImage] CHECK CONSTRAINT [FK8A3B06C3297BE84C]
GO
/****** Object:  ForeignKey [FKB4C6627F7DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateOfferTypeCulture]  WITH CHECK ADD  CONSTRAINT [FKB4C6627F7DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[EstateOfferTypeCulture] CHECK CONSTRAINT [FKB4C6627F7DEE8A4D]
GO
/****** Object:  ForeignKey [FKB4C6627F92C1E1B7]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateOfferTypeCulture]  WITH CHECK ADD  CONSTRAINT [FKB4C6627F92C1E1B7] FOREIGN KEY([EstateOfferTypeId])
REFERENCES [dbo].[EstateOfferType] ([Id])
GO
ALTER TABLE [dbo].[EstateOfferTypeCulture] CHECK CONSTRAINT [FKB4C6627F92C1E1B7]
GO
/****** Object:  ForeignKey [FK46E2C970297BE84C]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstatePropertiesCulture]  WITH CHECK ADD  CONSTRAINT [FK46E2C970297BE84C] FOREIGN KEY([EstateId])
REFERENCES [dbo].[EstateProperties] ([EstateId])
GO
ALTER TABLE [dbo].[EstatePropertiesCulture] CHECK CONSTRAINT [FK46E2C970297BE84C]
GO
/****** Object:  ForeignKey [FK46E2C9707DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstatePropertiesCulture]  WITH CHECK ADD  CONSTRAINT [FK46E2C9707DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[EstatePropertiesCulture] CHECK CONSTRAINT [FK46E2C9707DEE8A4D]
GO
/****** Object:  ForeignKey [FK9B61306F62582799]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateTypeCulture]  WITH CHECK ADD  CONSTRAINT [FK9B61306F62582799] FOREIGN KEY([EstateTypeId])
REFERENCES [dbo].[EstateType] ([Id])
GO
ALTER TABLE [dbo].[EstateTypeCulture] CHECK CONSTRAINT [FK9B61306F62582799]
GO
/****** Object:  ForeignKey [FK9B61306F7DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[EstateTypeCulture]  WITH CHECK ADD  CONSTRAINT [FK9B61306F7DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[EstateTypeCulture] CHECK CONSTRAINT [FK9B61306F7DEE8A4D]
GO
/****** Object:  ForeignKey [FK5E47AAB736D0D566]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK5E47AAB736D0D566] FOREIGN KEY([PageTemplateId])
REFERENCES [dbo].[PageTemplate] ([Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK5E47AAB736D0D566]
GO
/****** Object:  ForeignKey [FK5E47AAB77DEE8A4D]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK5E47AAB77DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK5E47AAB77DEE8A4D]
GO
/****** Object:  ForeignKey [FK5E47AAB7BC671738]    Script Date: 11/23/2008 17:16:16 ******/
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK5E47AAB7BC671738] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK5E47AAB7BC671738]
GO
/****** Object:  ForeignKey [FKC6FD20837BBC0019]    Script Date: 11/23/2008 17:16:17 ******/
ALTER TABLE [dbo].[PriceTypeCulture]  WITH CHECK ADD  CONSTRAINT [FKC6FD20837BBC0019] FOREIGN KEY([PriceTypeId])
REFERENCES [dbo].[PriceType] ([Id])
GO
ALTER TABLE [dbo].[PriceTypeCulture] CHECK CONSTRAINT [FKC6FD20837BBC0019]
GO
/****** Object:  ForeignKey [FKC6FD20837DEE8A4D]    Script Date: 11/23/2008 17:16:17 ******/
ALTER TABLE [dbo].[PriceTypeCulture]  WITH CHECK ADD  CONSTRAINT [FKC6FD20837DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[PriceTypeCulture] CHECK CONSTRAINT [FKC6FD20837DEE8A4D]
GO
/****** Object:  ForeignKey [FK969B97274BFD9585]    Script Date: 11/23/2008 17:16:17 ******/
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK969B97274BFD9585] FOREIGN KEY([ParentSectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK969B97274BFD9585]
GO
/****** Object:  ForeignKey [FK969B97277DEE8A4D]    Script Date: 11/23/2008 17:16:17 ******/
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK969B97277DEE8A4D] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Culture] ([Id])
GO
ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK969B97277DEE8A4D]
GO
