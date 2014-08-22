/// <copyright>
/// Copyright Unisys 2014.  All rights reserved.
/// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Serialization;
using Common.Persistent.ORMapping;
using Common.Extensions;
using Logging;

namespace Unitest.TestModel
{
    [Table]
    [Serializable]
    public partial class KeyRelationship : ModelElement
    {
        public KeyRelationship()
            : this(TableType.KeyRelationship)
        {
        }

        public KeyRelationship(TableType type)
            : base(type)
        {
        }

        #region ORMapping Columns

        private Guid? uniqueKeyId;

        private ReferentialRuleType deleteRule;
        private ReferentialRuleType updateRule;
        private DeferrabilityType deferrability;

        [Column(Type = DbType.Guid, AllowNull = true)]
        public Guid? UniqueKeyId
        {
            get { return uniqueKeyId; }
            set { uniqueKeyId = value;  }
        }

        [Column(Type = DbType.Int16)]
        public ReferentialRuleType DeleteRule
        {
            get { return deleteRule; }
            set { deleteRule = value;  }
        }

        [Column(Type = DbType.Int16)]
        public ReferentialRuleType UpdateRule
        {
            get { return updateRule; }
            set { updateRule = value;  }
        }

        [Column(Type = DbType.Int16)]
        public DeferrabilityType Deferrability
        {
            get { return deferrability; }
            set { deferrability = value;  }
        }

        #endregion

    }
}
