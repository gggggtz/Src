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
        #region String Consts

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

        #endregion

        IDbConnection NewConnection();

        IDbConnection GetDatabaseConnection(string databaseName);

        #region Execute

        int ExecuteCommand(string cmdText);
        object ExecuteCommandScalar(string cmdText);
        int ExecuteStoreProcedure(string spName, List<SPParameter> args);
        object ExecuteStoreProcedureScalar(string spName, List<SPParameter> args);
        List<T> ExecuteStoreProcedureReader<T>(string spName, List<SPParameter> args) where T : Entity, new();
        void ExecuteSqlResourceFile(Type typeInResourceAssembly, string fileQualifiedName, IDbConnection connection, bool splitCommands);

        #endregion

        void CreateDatabase(string databaseName);
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
        bool Exist<T>(string condition, params object[] parameterValues) where T : Entity;
        void Update<T>(string setSql, string condition, params object[] parameterValues) where T : Entity;
        void Delete<T>(string condition, params object[] parameterValues) where T : Entity;
        List<T> Search<T>(string condition, params object[] parameterValues) where T : Entity, new();
    }

	
}
