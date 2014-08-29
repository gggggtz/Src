/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. MsTools CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistent
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Name;
        public DbType Type = DbType.Object;
        public int Size;
        public bool IsPrimaryKey = false;
        public bool AllowNull = true;
        public bool ReadOnly = false;
    }

    public enum CollectionType
    {
        OneToMany,
        ManyToMany,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class CollectionAttribute : Attribute
    {
        public CollectionType CollectionType = CollectionType.OneToMany;
        public string Name;
        public Type TargetType;
        public string IntermediateTableName;
        public string MapSourceKey;
        public string MapTargetKey;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Name;
        public bool IsRoot;
        public string SchemaName;
    }
}
