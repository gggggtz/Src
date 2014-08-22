SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

/****** Object:  Table [dbo].[Dependency]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Dependency](
	[Id] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[SupplierId] [uniqueidentifier] NOT NULL,
	[Kind] [nvarchar](32) NULL,
 CONSTRAINT [PK_dbo.Dependency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Classifier]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Classifier](
	[Id] [uniqueidentifier] NOT NULL,
	[IsAbstract] [bit] NOT NULL,
	[ClassifierType] [smallint] NULL,
 CONSTRAINT [PK_dbo.Classifier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Column]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Column](
	[Id] [uniqueidentifier] NOT NULL,
	[IsNullable] [bit] NOT NULL,
	[CollationName] [nvarchar](256) NULL,
	[CharacterSetName] [nvarchar](256) NULL,
 CONSTRAINT [PK_dbo.Column] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Database]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Database](
	[Id] [uniqueidentifier] NOT NULL,
	[Host] [nvarchar](max) NULL,
	[Provider] [int] NOT NULL,
	[AuthenticationMode] [int] NOT NULL,
	[LoginUser] [nvarchar](max) NULL,
	[EncryptedPassword] [nvarchar](max) NULL,
	[RememberPassword] [bit] NOT NULL,
	[Port] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[DefaultCharacterSetName] [nvarchar](max) NULL,
	[DefaultCollationName] [nvarchar](max) NULL,
	[CcsVersion] [int] NULL,
	[DBCcsVersion] [int] NOT NULL,
	[AliasCcsVersion] [int] NOT NULL,
	[InitValues] [varbinary](max) NULL,
	[NullValues] [varbinary](max) NULL,
	[MinorVersion] [varchar](100) NULL,
    [ExtractedUTC] [datetime] NULL,
    [AppGroup] [varchar](max) NULL,
	[HostId] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.Database] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[Element]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Element](
	[Id] [uniqueidentifier] NOT NULL,
	[ReservedBy] [uniqueidentifier] NULL,
	[State] [int] NOT NULL,
	[Discriminator] int NOT NULL,
	[Revision] bigint NOT NULL,
	[LastModified] DateTime NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Element] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Expression]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Expression](
	[Id] [uniqueidentifier] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Language] [nvarchar](max) NOT NULL,	
 CONSTRAINT [PK_dbo.Expression] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Feature]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Feature](
	[Id] [uniqueidentifier] NOT NULL,
	[ClassifierId] [uniqueidentifier] NULL,
	[InitialValueId] [uniqueidentifier] NULL,
	[Length] [int] NOT NULL,
	[Precision] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Ordinal] [int] NOT NULL,
	[DataType] [smallint] NOT NULL,
	[ChangeKind] [smallint] NOT NULL,
	[OrderKind] [smallint] NOT NULL,
	[ScopeKind] [smallint] NOT NULL,
 CONSTRAINT [PK_dbo.Feature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



/****** Object:  Table [dbo].[Index]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Index](
	[Id] [uniqueidentifier] NOT NULL,
	[IsPartitioning] [bit] NOT NULL,
	[IsSorted] [bit] NOT NULL,
	[IsUnique] [bit] NOT NULL,
	[FilterCondition] [varchar](256) NOT NULL,
	[IsNullable] [bit] NOT NULL,
	[AutoUpdate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Index] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[IndexedFeature]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[IndexedFeature](
	[Id] [uniqueidentifier] NOT NULL,
	[IndexId] [uniqueidentifier] NOT NULL,
	[StructuralFeatureId] [uniqueidentifier] NOT NULL,
	[IsAscending] [bit] NULL,
	[IndexOrdinal] [int] NULL,
 CONSTRAINT [PK_dbo.IndexedFeature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[KeyRelationship]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[KeyRelationship](
	[Id] [uniqueidentifier] NOT NULL,
	[UniqueKeyId] [uniqueidentifier] NULL,
	[DeleteRule] [int] NOT NULL,
	[UpdateRule] [int] NOT NULL,
	[Deferrability] [int] NOT NULL,

 CONSTRAINT [PK_dbo.KeyRelationship] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[KeyRelationshipFeature]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[KeyRelationshipFeature](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyRelationshipId] [uniqueidentifier] NOT NULL,
	[StructuralFeatureId] [uniqueidentifier] NOT NULL,
	[FeatureOrdinal] [int] NULL,
 CONSTRAINT [PK_dbo.KeyRelationshipFeature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[ModelElement]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[ModelElement](
	[Id] [uniqueidentifier] NOT NULL,
	[OwnerId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Visibility] [int] NOT NULL,
	[SchemaVersion] [nvarchar](max) NULL,
	[MemberRevision] [bigint] NOT NULL,
	[Alias] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ModelElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[NameSpace]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[NameSpace](
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.NameSpace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Schema](
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Schema] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Table]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Table](
	[Id] [uniqueidentifier] NOT NULL,
	[IsTemporary] [bit] NOT NULL,
	[TemporaryScope] [varchar](256) NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[UniqueKey]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[UniqueKey](
	[Id] [uniqueidentifier] NOT NULL,
	[Deferrability] [int] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UniqueKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[UniqueKeyFeature]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[UniqueKeyFeature](
	[Id] [uniqueidentifier] NOT NULL,
	[UniqueKeyId] [uniqueidentifier] NOT NULL,
	[StructuralFeatureId] [uniqueidentifier] NOT NULL,
	[FeatureOrdinal] [int] NULL,
    [IsAscending] [bit] NULL,
    [Alias] [varchar](max) NULL,
 CONSTRAINT [PK_dbo.UniqueKeyFeature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Classifier]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Column]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Database]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


/****** Object:  Index [IX_ClassifierId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_ClassifierId] ON [dbo].[Feature]
(
	[ClassifierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Feature]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_InitialValueId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_InitialValueId] ON [dbo].[Feature]
(
	[InitialValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Index]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[IndexedFeature]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_IndexId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_IndexId] ON [dbo].[IndexedFeature]
(
	[IndexId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_StructuralFeatureId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_StructuralFeatureId] ON [dbo].[IndexedFeature]
(
	[StructuralFeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[KeyRelationship]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_UniqueKeyId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_UniqueKeyId] ON [dbo].[KeyRelationship]
(
	[UniqueKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_KeyRelationshipId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_KeyRelationshipId] ON [dbo].[KeyRelationshipFeature]
(
	[KeyRelationshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_StructuralFeatureId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_StructuralFeatureId] ON [dbo].[KeyRelationshipFeature]
(
	[StructuralFeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[ModelElement]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_OwnerId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_OwnerId] ON [dbo].[ModelElement]
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[NameSpace]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[Table]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IXId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IXId] ON [dbo].[UniqueKey]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_StructuralFeatureId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_StructuralFeatureId] ON [dbo].[UniqueKeyFeature]
(
	[StructuralFeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_UniqueKeyId]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_UniqueKeyId] ON [dbo].[UniqueKeyFeature]
(
	[UniqueKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
