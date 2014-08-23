/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
namespace UnitTest.TestModel
{
    public enum ClassifierType : short
    {
        Default,
        FixedDataSet,
        RemappedDataSet
    }

    public enum AuthenticationMode
    {
        UserNameAndPassword,
        Windows
    }

    public enum DatabaseProvider
    {
        SQLServer,
        Unkown
    }

	/// <summary> 
	/// Operation type 
	/// </summary> 
	public enum DBOperation
	{
		TranCommit,
		TranAbort,
		Insert,
		Delete,
		Update,
		SyncPoint,
		EmbeddedOwner,
		InitialLoad
	}

	[Flags]
	public enum ValidationState
	{
		Valid = 1,
		NeedValidation = 2,
		Invalid = 4
	}

	[Flags]
	public enum ElementState
	{
		Unknown=0,
		Normal = 1,
		Purged = 2,
	}

	public enum Events
	{
		None = 0,
		Added = 1,
		Deleted = 2,
		Changed = 4,
		MemberAdded = 8,
		MemberDeleted = 16,
		SourceCollectionChanged = 32,
		TargetCollectionChanged = 64,
	}

	public enum DeferrabilityType
	{
		Unknown = 0,
		InitiallyDeferred,
		InitiallyImmediate,
		NotDeferrable
	}

	public enum VisibilityKind
	{
		Unknown = 0,
		Public = 1,
		Protected = 2,
		Private = 4,
		Package = 8,
		NotApplicable = 16
	}

	public enum OrderKind : short
	{
		Unknown = 0,
		Unordered = 1,
		Ordered = 2
	}

	public enum ScopeKind : short
	{
		Unknown = 0,
		Instance = 1,
		Classifier = 2
	}

	public enum ChangableKind : short
	{
		Unknown = 0,
		Changeable = 1,
		Frozen = 2,
		AddOnly = 4
	}

	public enum Direction
	{
		Unknown = 0,
		In = 1,
		Out = 2,
		InOut = 4,
		Return = 8
	}

	public enum AggregationKind
	{
		Unknown = 0,
		None,
		Aggregate,
		Composite
	}

	public enum ProcedureType
	{
		Unknown = 0,
		Procedure,
		Function
	}

	public enum ReferentialRuleType
	{
		Unknown = 0,
		NoAction,
		Cascade,
		SetNull,
		KeyRestrict,
		SetDefault
	}

	[Flags]
	public enum ItemFlags
	{
		Unknown = 0,
		IsRequired = 1,
		IsKey = 2,
		IsReadOnly = 4,
		IsKeyData = 8,
		IsRowID = 16,
		IsVirtual = 32,
	}

	public enum ItemNullType : byte
	{
		No = 0,
		Default = 1,
		LowValue = 2,
		Link = 3,
		Alpha = 4,
		Numeric = 5,
		Blank = 6,
		Word = 7,
	}

	public enum StructureFlags
	{
		Unknown = 0,
		IsRequiredAll = 1,
		IsReadonlyAll = 2,
		HasLinkItems  = 4,
	}

	public enum TableSubType : byte
	{
		Standard = 0,
		Random = 1,
		Ordered = 2,
		UnOrdered = 3,
		Global = 4,
		Direct = 5,
		Compact = 6,
		SequentialFile = 7,
		IndexedFile = 8,
		RelativeFile = 9,

		ExStandard = 16,
		ExRandom = 17,
		ExOrdered = 18,
		ExUnOrdered = 19,
		ExGlobal = 20,
		ExDirect = 21,
		ExCompact = 22,

		SecStandard = 48,
		SecRandom = 49,
		SecOrdered = 50,
		SecUnOrdered = 51,
		SecGlobal = 52,
		SecDirect = 53,
		SecCompact = 54,

		Mask = 15,
	};

	public enum XmlImportOption : int
	{
		Replace,
		Ignore,
		UnKnown
	}

	public enum ConnectToMetaStoreMode : int
	{
		CreateNew,
		ConnectExisting,
	}

	public enum ViewMode
	{
		Unknown = 0,
		FromSource,
		ToTarget,
		SourceToTarget
	}

	public enum OccursMappingOption
	{
		Whole = 0,
		Value,
		Index
	}

	public enum DatabaseState : short
	{
		Normal,
		ChangedButNotDeployed,
	}

	public enum RepositoryPreCheckState : short
	{
		UnKnown,
		NewRepository,
		NotADataExchangeRepository,
		RepositoryCorrupted,
		RepositoryNotFound,
		UnderMigration,
		NeedDowngrade,
		NeedUpgrade,
		OkToConnect,
	}

	public enum AliasCcsVersion
	{
		NONE = 0,
		JAPANEBCDICJBIS8 = 100,
		JAPANV24JBIS8 = 114,
		EBCDICKSC5601 = 105,
		EBCDICUTL = 108,
		EBCDICGB2312 = 111,
	}

	public enum DBCcsVersion
	{
		NONE = 0,
		ASUTL = 82,
		ASKSC = 902,
		GB2312 = 935,
		JBIS8 = 80,
	}

	public enum CcsVersion
	{
		NONE = 0,
		EBCDIC = 4,
		ASCII = 5,
		ISOVISIBLESTRING = 9,
		IA5STRING = 10,
		LATIN1EBCDIC = 12,
		LATIN1ISO = 13,
		LATIN5EBCDIC = 14,
		LATIN5ISO = 15,
		CANSUPPLEBCDIC = 16,
		CANSUPPLISO = 17,
		CODEPAGE850 = 18,
		LATINGREEKEBCDIC = 19,
		LATINGREEKISO = 20,
		CODEPAGE851 = 21,
		FRENCHARABICE = 22,
		FRENCHARABICISO = 23,
		NORWAYBTOS = 24,
		LATINGREEKBTOS = 25,
		LATIN2EBCDIC = 26,
		LATIN2ISO = 27,
		CODEPAGE852 = 28,
		LATINCYRILLICEBC = 29,
		LATINCYRILLICISO = 30,
		CODEPAGE866 = 31,
		CODEPAGE1251 = 32,
		CODEPAGE1250 = 33,
		ARABIC20EBCDIC = 34,
		ARABIC20ISO = 35,
		CODEPAGE437 = 36,
		CODEPAGE1252 = 37,
		MACROMAN = 38,
		IBM297 = 39,
		JAPANEBCDIC = 41,
		JISASCII = 42,
		CODEPAGE1254 = 43,
		CODEPAGE857 = 44,
		CODEPAGE1253 = 45,
		HUNGARIANBTOS = 46,
		LATIN9EBCDIC = 47,
		LATIN9ISO = 48,
		JAPANV24 = 49,
		LOCALEBCDIC = 50,
		IBMSWEDENEBCDIC = 51,
		ARABICV201B = 52,
		ARABICV202D = 53,
		CODEPAGE864 = 54,
		CODEPAGE1256 = 55,
		JBIS8 = 80,
		ASUTL = 82,
		UCS2NT = 84,
		UCS2 = 85,
		JAPANEBCDICJBIS8 = 100,
		CODEPAGE932 = 102,
		EUCJP = 103,
		LETSJ = 104,
		EBCDICKSC5601 = 105,
		CODEPAGE949 = 107,
		EBCDICUTL = 108,
		CODEPAGE950 = 110,
		EBCDICGB2312 = 111,
		CODEPAGE936 = 113,
		JAPANV24JBIS8 = 114,
		V24EBCDICJBIS8 = 301,
		NXEBCDIC = 302,
		ASKSC = 902,
		LETSJISX16 = 930,
		GB2312 = 935,
		MSEBCDICUTL = 939,
		MSUTL = 961
	}

    [Flags]
    public enum PersistentState
    {
        None = 0,
        Loaded = 1,
        Added = 2,
        Changed = 4,
    }
}