/// <copyright>
/// Copyright ? Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;

namespace Unitest.TestModel
{
	/// <summary>
	/// Represents an abstract mapping object for database types.
	/// </summary>
	public enum DataType : int
	{
		None,

		/// <summary>
		/// An Oracle BFILE data type that contains a reference to binary data with a maximum size of 4 gigabytes that is stored in an external file. 
		/// Use the OracleClient OracleBFile data type with the Value property. 
		/// </summary>
		BFile,
		
		/// <summary>
		/// Array of type Byte. A fixed-length stream of binary data ranging between 1 and 8,000 bytes. 
		/// </summary>
		Binary,
		
		/// <summary>
		/// Array of type Byte. A variable-length stream of binary data ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes. 
		/// </summary>
		Blob,

		/// <summary>
		/// Boolean. An unsigned numeric value that can be 0, 1, or a null reference
		/// </summary>
		Boolean,

		/// <summary>
		/// Byte. An 8-bit unsigned integer. 
		/// </summary>
		Byte,

		/// <summary>
		/// String. A fixed-length stream of non-Unicode characters ranging between 1 and 8,000 characters. 
		/// </summary>
		Char,

		/// <summary>
		/// Decimal. A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) 
		/// with an accuracy to a ten-thousandth of a currency unit. 
		/// </summary>
		Currency,

		/// <summary>
		/// An Oracle CLOB data type that contains character data, based on the default character set on the server, with a maximum size of 4 gigabytes. 
		/// Use the OracleClient OracleLob data type in Value. 
		/// </summary>
		Clob,

		/// <summary>
		/// An Oracle REF CURSOR. The OracleDataReader object is not available. 
		/// </summary>
		Cursor,

		/// <summary>
		/// DateTime. Date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds. 
		/// </summary>
		DateTime,

		/// <summary>
		/// Decimal. A fixed precision and scale numeric value between -10^38 -1 and 10^38 -1. 
		/// </summary>
		Decimal,

		/// <summary>
		/// A double-precision floating-point value. 
		/// Use the .NET Framework Double or OracleClient OracleNumber data type in Value. 
		/// </summary>
		Double,

		/// <summary>
		/// Double. A floating point number within the range of -1.79E +308 through 1.79E +308. 
		/// </summary>
		Float,

		/// <summary>
		/// Guid. A globally unique identifier (or GUID). 
		/// </summary>
		Guid,

		/// <summary>
		/// Int64. A 64-bit signed integer.
		/// </summary>
		Int64,

		/// <summary>
		/// Int32. A 32-bit signed integer. 
		/// </summary>
		Int32,

		/// <summary>
		/// Int16. A 16-bit signed integer. 
		/// </summary>
		Int16,

		/// <summary>
		/// An Oracle INTERVAL DAY TO SECOND data type that contains an interval of time in days, hours, minutes, and seconds, 
		/// and has a fixed size of 11 bytes. Use the .NET Framework TimeSpan or OracleClient OracleTimeSpan data type in Value. 
		/// </summary>
		IntervalDayToSecond,

		/// <summary>
		/// An Oracle INTERVAL YEAR TO MONTH data type that contains an interval of time in years and months, 
		/// and has a fixed size of 5 bytes. Use the .NET Framework Int32 or OracleClient OracleMonthSpan data type in Value. 
		/// </summary>
		IntervalYearToMonth,

		/// <summary>
		/// An Oracle LONGRAW data type that contains variable-length binary data with a maximum size of 2 gigabytes. 
		/// Use the .NET Framework Byte[] or OracleClient OracleBinary data type in Value. 
		/// </summary>
		LongRaw,

		/// <summary>
		/// An Oracle LONG data type that contains a variable-length character string with a maximum size of 2 gigabytes. 
		/// Use the .NET Framework String or OracleClient OracleString data type in Value. 
		/// </summary>
		LongVarChar,

		/// <summary>
		/// String. A fixed-length stream of Unicode characters ranging between 1 and 4,000 characters. 
		/// </summary>
		NChar,

		/// <summary>
		/// An Oracle NCLOB data type that contains character data to be stored in the national character set of the database, 
		/// with a maximum size of 4 gigabytes (not characters) when stored in the database.
		/// </summary>
		NClob,

		/// <summary>
		/// String. A variable-length stream of Unicode data with a maximum length of 2 30 - 1 (or 1,073,741,823) characters. 
		/// </summary>
		NText,

		/// <summary>
		/// An Oracle NUMBER data type that contains variable-length numeric data with a maximum precision and scale of 38. 
		/// This maps to Decimal. Use the .NET Framework Decimal or OracleClient OracleNumber data type in Value. 
		/// </summary>
		Number,

		/// <summary>
		/// String. A variable-length stream of Unicode characters ranging between 1 and 4,000 characters. 
		/// Implicit conversion fails if the string is greater than 4,000 characters. 
		/// Explicitly set the object when working with strings longer than 4,000 characters. 
		/// </summary>
		NVarChar,

		/// <summary>
		/// An Oracle RAW data type that contains variable-length binary data with a maximum size of 2,000 bytes. 
		/// Use the .NET Framework Byte[] or OracleClient OracleBinary data type in Value. 
		/// </summary>
		Raw,

		/// <summary>
		/// Single. A floating point number within the range of -3.40E +38 through 3.40E +38. 
		/// </summary>
		Real,

		/// <summary>
		/// DateTime. Date and time data ranging in value from January 1, 1900 to June 6, 2079 to an accuracy of one minute. 
		/// </summary>
		SmallDate,

		/// <summary>
		/// Decimal. A currency value ranging from -214,748.3648 to +214,748.3647 with an accuracy to a ten-thousandth of a currency unit. 
		/// </summary>
		SmallCurrency,

		/// <summary>
		/// String. A variable-length stream of non-Unicode data with a maximum length of 2 31 -1 (or 2,147,483,647) characters. 
		/// </summary>
		Text,

		/// <summary>
		/// Array of type Byte. Automatically generated binary numbers, which are guaranteed to be unique within a database.
		/// Timestamp is used typically as a mechanism for version-stamping table rows. The storage size is 8 bytes. 
		/// </summary>
		Timestamp,

		/// <summary>
		/// An Oracle TIMESTAMP WITH LOCAL TIMEZONE (Oracle 9i or later) that contains date, time, 
		/// and a reference to the original time zone, and ranges in size from 7 to 11 bytes. 
		/// Use the .NET Framework DateTime or OracleClient OracleDateTime data type in Value. 
		/// </summary>
		TimestampLocal,

		/// <summary>
		/// An Oracle TIMESTAMP WITH TIMEZONE (Oracle 9i or later) that contains date, time, and a specified time zone, and has a fixed size of 13 bytes. 
		/// Use the .NET Framework DateTime or OracleClient OracleDateTime data type in Value. 
		/// </summary>
		TimestampWithTz,

		/// <summary>
		/// Array of type Byte. A variable-length stream of binary data ranging between 1 and 8,000 bytes. 
		/// Implicit conversion fails if the byte array is greater than 8,000 bytes. 
		/// Explicitly set the object when working with byte arrays larger than 8,000 bytes. 
		/// </summary>
		VarBinary,

		/// <summary>
		/// String. A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters. 
		/// </summary>
		VarChar,

		/// <summary>
		/// Object. A special data type that can contain numeric, string, binary, or date data as well as the SQL Server values Empty and Null, 
		/// which is assumed if no other type is declared. 
		/// </summary>
		Variant,

		/// <summary>
		/// An XML value. Obtain the XML as a string using the GetValue method or Value property, 
		/// or as an XmlReader by calling the CreateReader method. 
		/// </summary>
		Xml,

		/// <summary>
		/// Date data ranging in value from January 1,1 AD through December 31, 9999 AD.
		/// </summary>
		Date,

		/// <summary>
		/// Time data based on a 24-hour clock. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. 
		/// </summary>
		Time,

		/// <summary>
		/// Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. 
		/// Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.
		/// </summary>
		DateTime2,

		/// <summary>
		/// Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. 
		/// Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. 
		/// Time zone value range is -14:00 through +14:00.
		/// </summary>
		DateTimeOffset,

		/// <summary>
		/// For SQL Server Geography data type.
		/// </summary>
		Geography,

		/// <summary>
		/// For SQL Server Geometry data type.
		/// </summary>
		Geometry,
		
		NotSupported
	}
}
