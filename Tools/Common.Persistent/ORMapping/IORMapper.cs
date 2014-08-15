/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. MsTools CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Data;
using Common;

namespace Common.Persistent.ORMapping
{

    public interface IORMapper : IEntityAccesser
    {
        string And { get; }
        string Comma { get; }
        string ConditionSql { get; }
        string CountSql { get; }
        string DeleteSql { get; }
        string Dot { get; }
        string Equal { get; }
        string FindAttributeSql { get; }
        string FindSql { get; }
        string InsertSql { get; }
        string Join { get; }
        string KeySqlValueParameter { get; }
        string KeySqlValueParameterName { get; }
        string On { get; }
        string OrderSql { get; }
        string SemiColon { get; }
        string SetSql { get; }
        string UpdateSql { get; }
        string UpdateSqlValueParameter { get; }
        string UpdateSqlValueParameterName { get; }
        string WhereSql { get; }
        string LeftSquare { get; }
        string RightSquare { get; }
        string StoreProcedureExistSql { get; }
        string CreateDatabaseSql { get; }
        string DropDatabaseSql { get; }
        string CommandSpitterRegexString { get; }
        string DatabaseExistSql { get; }
        string MasterDatabaseName { get; }

        IDbConnection NewConnection();

        IDbConnection GetDatabaseConnection(string databaseName);

        void CreateDatabase(string databaseName);
        void ExecuteSqlResourceFile(Type typeInResourceAssembly, string fileQualifiedName, IDbConnection connection, bool splitCommands);
        void DropDatabase(string databaseName);
        bool DatabaseExist(string databaseName);
        bool StoreProcedureExist(string databaseName, string storeProceudreName);
        string GetDatabaseName();
        string GetDatabaseHost();
        bool IsSystemDatabase(string databaseName);
        void WaitUntilConnectionBeUsed(IDbConnection connection, int retryTimes = 20, int retryInterval = 500);
    }

    public interface IBatchORMapper : IORMapper
    {
        string GetUpdateSetItem(string columnName, int parameterIndex);
        string GetParameterName(int index);

        bool Exist<T>(string condition, params object[] parameterValues);
        void Update<T>(string setSql, string condition, params object[] parameterValues);
        void Delete<T>(string condition, params object[] parameterValues);
        List<T> Search<T>(string condition, params object[] parameterValues) where T : new();
    }

	
}
