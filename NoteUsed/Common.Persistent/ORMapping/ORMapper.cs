﻿using Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Persistent.ORMapping
{
    public abstract class ORMapper : IBatchORMapper
    {
        private static List<DbType> InvalidDbTypes = new List<DbType>() { DbType.Time };

        public bool IncludeSchema { get; private set; }

        public string SchemaName { get; private set; }

        public string ForeignKeySuffix { get; set; }

        public ORMapper(string foreignKeySuffix, string schemaName)
        {
            SchemaName = schemaName;
            IncludeSchema = !String.IsNullOrEmpty(SchemaName);
            ForeignKeySuffix = foreignKeySuffix;
        }

        #region Const Strings

        public abstract string Join { get; }
        public abstract string On { get; }
        public abstract string Equal { get; }
        public abstract string Dot { get; }

        public abstract string CountSql
        {
            get;
        }

        public abstract string FindSql
        {
            get;
        }

        public abstract string FindAttributeSql
        {
            get;
        }

        public abstract string WhereSql
        {
            get;
        }

        public abstract string OrderSql
        {
            get;
        }

        public abstract string ConditionSql
        {
            get;
        }

        public abstract string UpdateSql
        {
            get;
        }

        public abstract string DeleteSql
        {
            get;
        }

        public abstract string InsertSql
        {
            get;
        }

        public abstract string SetSql
        {
            get;
        }

        public abstract string Comma
        {
            get;
        }

        public abstract string SemiColon
        {
            get;
        }

        public abstract string And { get; }

        public abstract string UpdateSqlValueParameterName
        {
            get;
        }

        public abstract string UpdateSqlValueParameter
        {
            get;
        }

        public abstract string KeySqlValueParameterName
        {
            get;
        }

        public abstract string KeySqlValueParameter
        {
            get;
        }

        public abstract string LeftSquare { get; }

        public abstract string RightSquare { get; }

        public abstract string StoreProcedureExistSql { get; }

        public abstract string CreateDatabaseSql { get; }

        public abstract string DropDatabaseSql { get; }

        public abstract string CommandSpitterRegexString { get; }

        public abstract string DatabaseExistSql { get; }

        public abstract string MasterDatabaseName { get; }

        public abstract string BetweenSql { get; }

        public abstract string RowNumSql { get; }

        public abstract string MaxSql
        {
            get;
        }

        #endregion

        #region Implement IBatchORMapper

        private string GetUpdateSetItem(string columnName, int parameterIndex)
        {
            return string.Format(SetSql, columnName, GetParameterName(parameterIndex));
        }

        private string GetParameterName(int index)
        {
            return string.Format(UpdateSqlValueParameter, index);
        }

        public bool Exist<T>(string condition, params object[] parameterValues) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                var result = GetExistCommand<T>(conn, condition, parameterValues).ExecuteScalar();
                return result != null;
            }
        }

        public void Update<T>(string setSql, string condition, params object[] parameterValues) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                using (var command = GetUpdateCommand<T>(conn, setSql, condition, parameterValues))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete<T>(string condition, params object[] parameterValues) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                using (var command = GetDeleteCommand<T>(conn, condition, parameterValues))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<T> Search<T>(string condition, params object[] parameterValues) where T : Entity,new ()
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return ExecuteSearchCommand<T>(GetSearchCommand<T>(conn, condition, parameterValues));
            }
        }

        protected IDbCommand GetExistCommand<T>(IDbConnection connection, string condition, params object[] parameterValues)
        {
            var cmd = connection.CreateCommand();

            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T),ForeignKeySuffix,SchemaName);
            string sql = string.Format(FindSql, GetExistSelect(), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, IncludeSchema));
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);

                if (parameterValues != null && parameterValues.Length > 0)
                {
                    for (int index = 0; index < parameterValues.Length; index++)
                    {
                        AddParameter(GetParameterName(index), parameterValues[index], cmd);
                    }
                }
            }
            cmd.CommandText = sql;
            return cmd;
        }

        protected IDbCommand GetUpdateCommand<T>(IDbConnection conn, string setSql, string condition, params object[] parameterValues)
        {
            IDbCommand cmd = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);

            string sql = string.Format(UpdateSql, GetTableName(dataInfo.Name), setSql);
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);

                if (parameterValues != null && parameterValues.Length > 0)
                {
                    for (int index = 0; index < parameterValues.Length; index++)
                    {
                        AddParameter(GetParameterName(index), parameterValues[index], cmd);
                    }
                }
            }

            cmd.CommandText = sql;
            return cmd;
        }

        protected IDbCommand GetDeleteCommand<T>(IDbConnection conn, string condition, object[] parameterValues)
        {
            IDbCommand cmd = conn.CreateCommand();

            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
            string sql = string.Format(DeleteSql, GetTableName(dataInfo.Name));
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);

                if (parameterValues != null && parameterValues.Length > 0)
                {
                    for (int index = 0; index < parameterValues.Length; index++)
                    {
                        AddParameter(GetParameterName(index), parameterValues[index], cmd);
                    }
                }
            }

            cmd.CommandText = sql;
            return cmd;
        }

        public IDbCommand GetSearchCommand<T>(IDbConnection conn, string condition, params object[] parameterValues)
        {
            IDbCommand cmd = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
            string sql = string.Empty;
            sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, IncludeSchema));
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);
                if (parameterValues != null && parameterValues.Length > 0)
                {
                    for (int index = 0; index < parameterValues.Length; index++)
                    {
                        AddParameter(GetParameterName(index), parameterValues[index], cmd);
                    }
                }
            }
            cmd.CommandText = sql;
            return cmd;
        }

        

        #endregion

        #region Exist

        protected abstract string GetExistSelect();

        public IDbCommand GetExistCommand<T>(T obj, IDbConnection connection)
        {
            var cmd = connection.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName);
            string condition = GetKeyCondition(obj, dataInfo, cmd);
            string sql = string.Format(WhereSql, string.Format(FindSql, GetExistSelect(), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, IncludeSchema)), condition);
            cmd.CommandText = sql;
            return cmd;
        }

        public bool Exist<T>(T obj)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                var ExistCmd = GetExistCommand<T>(obj, conn);
                var result = ExistCmd.ExecuteScalar();
                return result != null;
            }
        }

        public abstract bool StoreProcedureExist(string databaseName, string storeProceudreName);

        public virtual bool DatabaseExist(string databaseName)
        {
            using (var connection = GetDatabaseConnection(MasterDatabaseName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(DatabaseExistSql, databaseName);
                    string name = command.ExecuteScalar() as string;
                    return string.Compare(name, databaseName, true) == 0;
                }
            }
        }

        #endregion

        #region Insert

        public void Insert<T>(T obj) where T: Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();

                var InsertCMD = GetInsertCommands(obj, conn);
                foreach (var command in InsertCMD)
                {
                    using (command)
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<IDbCommand> GetInsertCommands<T>(T obj, IDbConnection connection)
        {
            return GetInsertCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName), connection);
        }

        private List<IDbCommand> GetInsertCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
        {
            List<IDbCommand> result = new List<IDbCommand>();

            // add commands for base infos
            if (dataInfo.BaseInfo != null)
            {
                result.AddRange(GetInsertCommands(obj, dataInfo.BaseInfo, connection));
            }

            // add command for current table.
            if (!string.IsNullOrEmpty(dataInfo.Name))
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetInsertStatement(obj, dataInfo, command);
                result.Add(command);
            }

            // add commands for many to many relationships
            var manyToManyInfos = dataInfo.Collections.Values.Cast<CollectionInfo>().Where(t => t.CollectionType == CollectionType.ManyToMany);
            foreach (CollectionInfo info in manyToManyInfos)
            {
                result.AddRange(GetInsertCommands(obj, dataInfo, info, connection));
            }

            return result;
        }

        private List<IDbCommand> GetInsertCommands(Object obj, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, IDbConnection connection)
        {
            List<IDbCommand> result = new List<IDbCommand>();

            IEnumerable targets = collectionInfo.GetValue(obj);
            if (targets != null)
            {
                foreach (var target in targets)
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText = GetInsertStatement(obj, target, dataInfo, collectionInfo, command);
                    result.Add(command);
                }
            }

            return result;
        }

        private string GetInsertStatement(Object src, Object target, ColumnCollectionInfo srcDataInfo, CollectionInfo collectionInfo, IDbCommand command)
        {
            var targetDataInfo = ColumnCollectionInfo.GetInfo(target.GetType(), ForeignKeySuffix, SchemaName);

            int index = 0;
            List<string> values = new List<string>();
            foreach (ColumnInfo column in srcDataInfo.PrimaryKeys.Values)
            {
                string parameterName = string.Format(UpdateSqlValueParameter, index++);
                values.Add(parameterName);
                AddParameter(column, parameterName, column.GetValue(src), command);
            }
            foreach (ColumnInfo column in targetDataInfo.PrimaryKeys.Values)
            {
                string parameterName = string.Format(UpdateSqlValueParameter, index++);
                values.Add(parameterName);
                AddParameter(column, parameterName, column.GetValue(target), command);
            }

            return string.Format(
                InsertSql,
                GetTableName(collectionInfo.IntermediateTableName),
                collectionInfo.MapSourceKey + Comma + collectionInfo.MapTargetKey,
                string.Join(Comma, values));
        }

        private string GetInsertStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
        {
            int index = 0;
            List<string> values = new List<string>();
            foreach (ColumnInfo column in dataInfo.PrimaryKeys.Values)
            {
                string parameterName = string.Format(UpdateSqlValueParameter, index++);
                values.Add(parameterName);
                AddParameter(column, parameterName, column.GetValue(obj), command);
            }
            foreach (ColumnInfo column in dataInfo.Columns.Values)
            {
                if (!column.ReadOnly)
                {
                    string parameterName = string.Format(UpdateSqlValueParameter, index++);
                    values.Add(parameterName);
                    AddParameter(column, parameterName, column.GetValue(obj), command);
                }
            }

            return string.Format(InsertSql, GetTableName(dataInfo.Name), dataInfo.GetSelectOrInsertColumns(Comma, true, Dot, LeftSquare, RightSquare, true), string.Join(Comma, values));
        }

        #endregion

        #region Update

        public void Update<T>(T obj) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                var UpdateCMD = GetUpdateCommands(obj, conn);
                foreach (var command in UpdateCMD)
                {
                    using (command)
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<IDbCommand> GetUpdateCommands<T>(T obj, IDbConnection connection)
        {
            return GetUpdateCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName), connection);
        }

        private List<IDbCommand> GetUpdateCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
        {
            List<IDbCommand> result = new List<IDbCommand>();
            if (dataInfo.BaseInfo != null)
            {
                result.AddRange(GetUpdateCommands(obj, dataInfo.BaseInfo, connection));
            }

            if (dataInfo.Columns.Count > 0)
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetUpdateStatement(obj, dataInfo, command);
                result.Add(command);
            }

            var manyToManyInfos = dataInfo.Collections.Values.Cast<CollectionInfo>().Where(t => t.CollectionType == CollectionType.ManyToMany);
            foreach (CollectionInfo info in manyToManyInfos)
            {
                result.AddRange(GetUpdateCommands(obj, dataInfo, info, connection));
            }

            return result;
        }

        private List<IDbCommand> GetUpdateCommands(Object obj, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, IDbConnection conn)
        {
            List<IDbCommand> commands = new List<IDbCommand>();

            IEnumerable colllectionValue = collectionInfo.GetValue(obj);
            if (colllectionValue != null)
            {
                var newTargets = colllectionValue.Cast<Object>().ToList();
                if (newTargets.Count == 0)
                {
                    commands.Add(GetClearCollectionCommand(obj, dataInfo, collectionInfo, conn));
                }
                else
                {
                    var targetDataInfo = ColumnCollectionInfo.GetInfo(collectionInfo.TargetType, ForeignKeySuffix, SchemaName);
                    ColumnInfo targetPrimaryKeyInfo = targetDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();

                    var oldPrimaryKeys = GetCollectionTargets(obj, dataInfo, collectionInfo, targetPrimaryKeyInfo.GetValue(newTargets.First()), conn);
                    foreach (var newTarget in newTargets)
                    {
                        if (!oldPrimaryKeys.Contains(targetPrimaryKeyInfo.GetValue(newTarget)))
                        {
                            IDbCommand command = conn.CreateCommand();
                            command.CommandText = GetInsertStatement(obj, newTarget, dataInfo, collectionInfo, command);
                            commands.Add(command);
                        }
                    }

                    var newPrimaryKeys = newTargets.Select(t => targetPrimaryKeyInfo.GetValue(t));
                    foreach (var primaryKey in oldPrimaryKeys)
                    {
                        if (newPrimaryKeys.Where(t => t.Equals(primaryKey)).Count() == 0)
                        {
                            commands.Add(GetClearCollectionCommand(obj, dataInfo, collectionInfo, conn, targetPrimaryKeyInfo, primaryKey));
                        }
                    }
                }
            }

            return commands;
        }

        private List<T> GetCollectionTargets<T>(Object src, ColumnCollectionInfo dataInfo, CollectionInfo collectionInfo, T targetKey, IDbConnection conn)
            where T : class, new()
        {
            List<T> result = new List<T>();

            using (var command = conn.CreateCommand())
            {
                string sql = string.Format(FindSql, collectionInfo.MapTargetKey, collectionInfo.IntermediateTableName);

                ColumnInfo primaryKeyColumn = dataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
                string parameterName = string.Format(UpdateSqlValueParameter, 0);
                string whereSql = collectionInfo.MapSourceKey + Equal + parameterName;
                AddParameter(primaryKeyColumn, parameterName, primaryKeyColumn.GetValue(src), command);

                sql = string.Format(WhereSql, sql, whereSql);

                command.CommandText = sql;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T obj = reader.GetValue(0) as T;
                        result.Add(obj);
                    }
                }
            }

            return result;
        }

        private IDbCommand GetClearCollectionCommand(
            Object obj,
            ColumnCollectionInfo dataInfo,
            CollectionInfo collectionInfo,
            IDbConnection conn,
            ColumnInfo targetPrimaryKeyInfo = null,
            object targetPrimaryKey = null)
        {
            IDbCommand command = conn.CreateCommand();

            string sql = string.Format(DeleteSql, collectionInfo.IntermediateTableName);

            ColumnInfo primaryKeyColumn = dataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
            string parameterName = string.Format(UpdateSqlValueParameter, 0);
            string whereSql = collectionInfo.MapSourceKey + Equal + parameterName;
            AddParameter(primaryKeyColumn, parameterName, primaryKeyColumn.GetValue(obj), command);

            if (targetPrimaryKeyInfo != null && targetPrimaryKey != null)
            {
                parameterName = string.Format(UpdateSqlValueParameter, 1);
                whereSql = whereSql + And + collectionInfo.MapTargetKey + Equal + parameterName;
                AddParameter(targetPrimaryKeyInfo, parameterName, targetPrimaryKey, command);
            }

            sql = string.Format(WhereSql, sql, whereSql);
            command.CommandText = sql;

            return command;
        }

        private string GetUpdateStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
        {
            int index = 0;
            List<string> values = new List<string>();
            foreach (ColumnInfo column in dataInfo.Columns.Values)
            {
                if (!column.IsPrimaryKey && !column.ReadOnly)
                {
                    string parameterName = string.Format(UpdateSqlValueParameter, index++);
                    values.Add(string.Format(SetSql, column.Name, parameterName));
                    AddParameter(column, parameterName, column.GetValue(obj), command);
                }
            }
            string sql = string.Format(UpdateSql, GetTableName(dataInfo.Name), string.Join(Comma, values));
            string condition = GetKeyCondition(obj, dataInfo, command);
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);
            }
            return sql;
        }

        #endregion

        #region Delete

        public void Delete<T>(T obj) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                var DeleteCMD = GetDeleteCommands(obj, conn);
                foreach (var command in DeleteCMD)
                {
                    using (command)
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<IDbCommand> GetDeleteCommands<T>(T obj, IDbConnection connection)
        {
            return GetDeleteCommands(obj, ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName), connection);
        }

        private List<IDbCommand> GetDeleteCommands(Object obj, ColumnCollectionInfo dataInfo, IDbConnection connection)
        {
            List<IDbCommand> result = new List<IDbCommand>();

            // Many to Many are deleted by CASCADE foreign key
            //var manyToManyInfos = dataInfo.Collections.Values.Cast<CollectionInfo>().Where(t => t.CollectionType == CollectionType.ManyToMany);
            //foreach (CollectionInfo info in manyToManyInfos)
            //{
            //    result.Add(GetClearCollectionCommand(obj, dataInfo, info, connection));
            //}

            IDbCommand command = connection.CreateCommand();
            command.CommandText = GetDeleteStatement(obj, dataInfo, command);
            result.Add(command);

            //Base tables are deleted by triggers
            //if (dataInfo.BaseInfo != null)
            //{
            //    result.AddRange(GetDeleteCommands(obj, dataInfo.BaseInfo, connection));
            //}

            return result;
        }

        private string GetDeleteStatement(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
        {
            string sql = string.Format(DeleteSql, GetTableName(dataInfo.Name));
            string condition = GetKeyCondition(obj, dataInfo, command);
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);
            }
            return sql;
        }

        #endregion

        #region Load

        public void  Load<T>(T obj) where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                    conn.Open();

                using (IDbCommand command = GetSelectCommand(obj, conn))
                {
                    var dataInfo = ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName);
                    using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataInfo[reader.GetName(i)].SetValue(obj, reader.GetValue(i));
                            }
                            obj.LoadCompleted();
                        }
                    }
                }
            }
        }

        public IDbCommand GetSelectCommand<T>(T obj, IDbConnection conn)
        {
            IDbCommand command = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(obj.GetType(), ForeignKeySuffix, SchemaName);
            string sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(
                Comma,
                true,
                Dot, LeftSquare, RightSquare),
                dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, IncludeSchema));
            string condition = GetKeyCondition(obj, dataInfo, command);
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);
            }
            command.CommandText = sql;
            return command;
        }

        #endregion

        #region Search

        public virtual List<T> Search<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : Entity, new()
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return ExecuteSearchCommand<T>(GetSearchCommand<T>(conn, condition, order, start, count));
            }
        }

        public List<TEntity> Search<TOwner, TEntity>(TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "") where TEntity : Entity, new()
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return ExecuteSearchCommand<TEntity>(GetSearchCommand<TOwner, TEntity>(conn, owner, exp, order));
            }
        }

        public List<TEntity> Search<TOwner, TEntity>(TOwner owner, string collectionName, string order = "") where TEntity : Entity, new()
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                return ExecuteSearchCommand<TEntity>(GetSearchCommand<TOwner, TEntity>(conn, owner, collectionName, order));
            }
        }

        public IDbCommand GetSearchCommand<T>(IDbConnection conn, string condition = "", string order = "", int start = -1, int count = -1)
        {
            IDbCommand command = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
            string sql = string.Empty;
            bool useRowNumber = start > 0 || count > 0;
            string rowNumberOrder = string.IsNullOrEmpty(order) ? dataInfo.GetKeys(Comma, Dot, LeftSquare, RightSquare) : order;
            string rowNumberWhere = string.Empty;

            string rowNumberCondition = string.Empty;
            if (useRowNumber)
            {
                int startNumber = start > 0 ? start : 1;
                if (count > 0)
                {
                    rowNumberCondition += string.Format(BetweenSql, "RN", startNumber, startNumber + count - 1);
                }
                else
                {
                    rowNumberCondition += string.Format("(RN > {0})", startNumber);
                }

                rowNumberWhere = string.IsNullOrEmpty(condition) ? string.Empty : " WHERE " + condition;

                sql = string.Format(RowNumSql, rowNumberOrder,
                    dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare),
                    dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false), rowNumberWhere);

                if (!string.IsNullOrEmpty(rowNumberCondition))
                {
                    sql = string.Format(WhereSql, sql, rowNumberCondition);
                }
            }
            else
            {
                sql = string.Format(FindSql, dataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare), dataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, false));
                if (!string.IsNullOrEmpty(condition))
                {
                    sql = string.Format(WhereSql, sql, condition);
                }
                if (!string.IsNullOrEmpty(order))
                {
                    sql = string.Format(OrderSql, sql, order);
                }
            }
            command.CommandText = sql;
            return command;
        }

        public IDbCommand GetSearchCommand<TOwner, TEntity>(IDbConnection conn, TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "")
            where TEntity : Entity
        {
            MemberExpression body = exp.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException();
            }

            var collectionName = body.Member.Name;
            return GetSearchCommand<TOwner, TEntity>(conn, owner, collectionName, order);
        }

        public IDbCommand GetSearchCommand<TOwner, TEntity>(IDbConnection conn, TOwner owner, string collectionName, string order = "")
        {
            IDbCommand command = conn.CreateCommand();

            var entityDataInfo = ColumnCollectionInfo.GetInfo(typeof(TEntity), ForeignKeySuffix, SchemaName);
            var ownerDataInfo = ColumnCollectionInfo.GetInfo(owner.GetType(), ForeignKeySuffix, SchemaName);

            string sql = string.Format(
                FindSql,
                entityDataInfo.GetAllSelectColumns(Comma, true, Dot, LeftSquare, RightSquare),
                entityDataInfo.GetFromClause(Join, On, Equal, Dot, LeftSquare, RightSquare, And, IncludeSchema));

            CollectionInfo collectionInfo = ownerDataInfo.GetCollectionInfo(collectionName);
            if (collectionInfo != null)
            {
                // for one to many relationship
                if (collectionInfo.CollectionType == CollectionType.OneToMany)
                {
                    ColumnInfo keyColumn = ownerDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
                    string parameterName = string.Format(KeySqlValueParameter, 0);
                    string whereSql = LeftSquare + collectionInfo.MapSourceKey + RightSquare + Equal + parameterName;
                    sql = string.Format(WhereSql, sql, whereSql);
                    AddParameter(keyColumn, parameterName, keyColumn.GetValue(owner), command);
                }
                // for many to many relationship
                else
                {
                    string intermediateTableName = LeftSquare + collectionInfo.IntermediateTableName + RightSquare;
                    string sourceKey = LeftSquare + ownerDataInfo.Name + RightSquare + Dot + ownerDataInfo.PrimaryKeys.Keys.OfType<string>().Single();
                    string targetKey = LeftSquare + entityDataInfo.Name + RightSquare + Dot + entityDataInfo.PrimaryKeys.Keys.OfType<string>().Single();
                    string toSourceKey = intermediateTableName + Dot + collectionInfo.MapSourceKey;
                    string toTargetKey = intermediateTableName + Dot + collectionInfo.MapTargetKey;
                    string joinSql =
                        Join + intermediateTableName + On + targetKey + Equal + toTargetKey +
                        Join + LeftSquare + ownerDataInfo.Name + RightSquare + On + toSourceKey + Equal + sourceKey;

                    string parameterName = string.Format(KeySqlValueParameter, 0);
                    string whereSql = sourceKey + Equal + parameterName;
                    ColumnInfo keyColumnInfo = ownerDataInfo.PrimaryKeys.Values.OfType<ColumnInfo>().Single();
                    AddParameter(keyColumnInfo, parameterName, keyColumnInfo.GetValue(owner), command);
                    sql = string.Format(WhereSql, sql + joinSql, whereSql);
                }

                if (!string.IsNullOrEmpty(order))
                {
                    sql = string.Format(OrderSql, sql, order);
                }
            }
            command.CommandText = sql;
            return command;
        }

        private List<T> ExecuteSearchCommand<T>(IDbCommand command) where T : Entity, new()
        {
            List<T> result = new List<T>();
            using (command)
            {
                var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T obj = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            ColumnInfo column = dataInfo[name];
                            if (column != null)
                            {
                                column.SetValue(obj, reader.GetValue(i));
                            }
                        }
                        if (obj is Entity)
                        {
                            (obj as Entity).LoadCompleted();
                        }
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Protected Methods

        protected virtual string GetTableName(string tableName)
        {
            string result = LeftSquare + tableName + RightSquare;
            if (string.IsNullOrEmpty(SchemaName))
            {
                return result;
            }
            else
            {
                return LeftSquare + this.SchemaName + RightSquare + Dot + result;
            }
        }

        private string GetKeyCondition(Object obj, ColumnCollectionInfo dataInfo, IDbCommand command)
        {
            int index = 0;
            List<string> conditions = new List<string>();
            foreach (ColumnInfo primaryKey in dataInfo.PrimaryKeys.Values)
            {
                string parameterName = string.Format(KeySqlValueParameter, index++);
                string keyName = LeftSquare + dataInfo.Name + RightSquare + Dot + primaryKey.Name;
                conditions.Add(string.Format(ConditionSql, keyName, parameterName));
                AddParameter(primaryKey, parameterName, primaryKey.GetValue(obj), command);
            }

            return string.Join(And, conditions.ToArray());
        }

        private void AddParameter(string name, object value, IDbCommand command)
        {
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
        }

        private void AddParameter(ColumnInfo column,string name, object value, IDbCommand command)
        {
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            if (!InvalidDbTypes.Contains(column.Type))
            {
                parameter.DbType = column.Type;
            }
            command.Parameters.Add(parameter);
        }

        private void AddParameter(IDbCommand command, SPParameter spParam)
        {
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = spParam.Name;
            parameter.Value = spParam.Value;
            parameter.Direction = spParam.Direction;
            if (!InvalidDbTypes.Contains(spParam.Type))
            {
                parameter.DbType = spParam.Type;
            }
            command.Parameters.Add(parameter);
        }

        #endregion

        #region Execute 

        public int ExecuteCommand(string cmdText)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = CommandType.Text;
                return command.ExecuteNonQuery();
            }
        }
        public object ExecuteCommandScalar(string cmdText)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = CommandType.Text;
                return command.ExecuteScalar();
            }
        }

        public int ExecuteStoreProcedure(string procName, List<SPParameter> args)
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procName;
                if (args != null && args.Count() > 0)
                {
                    foreach (var arg in args)
                    {
                        AddParameter(command, arg);
                    }
                }

                // set command timeout to 2 minutes, as for some stored procedures like DeleteDatabase
                // it takes really long time if the database is big.
                command.CommandTimeout = 120;

                var r = command.ExecuteNonQuery();

                foreach (IDbDataParameter p in command.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                    {
                        SPParameter arg = args.FirstOrDefault(a => a.Name == p.ParameterName);
                        if (arg != null)
                        {
                            arg.Value = p.Value;
                        }
                    }
                }
                return r;
            }
        }

        public object ExecuteStoreProcedureScalar(string procName, List<SPParameter> args)
        {
            using (IDbConnection conn = NewConnection())
            {
                    conn.Open();

                using (IDbCommand command = conn.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (args != null && args.Count() > 0)
                    {
                        foreach (var arg in args)
                        {
                            AddParameter(command, arg);
                        }
                    }

                    // set command timeout to 2 minutes, as for some stored procedures like DeleteDatabase
                    // it takes really long time if the database is big.
                    command.CommandTimeout = 120;

                    var result = command.ExecuteScalar();

                    if (args != null && args.Count() > 0)
                    {
                        foreach (var arg in args)
                        {
                            if (arg.Direction != ParameterDirection.Input && command.Parameters.Contains(arg.Name))
                            {
                                arg.Value = ((IDbDataParameter)command.Parameters[arg.Name]).Value;
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public List<T> ExecuteStoreProcedureReader<T>(string procName, List<SPParameter> args) where T : Entity, new()
        {
            List<T> result = new List<T>();
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = GetStoreProcedureCommand(conn, procName, args);
                var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T obj = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            ColumnInfo column = dataInfo[name];
                            if (column != null)
                            {
                                column.SetValue(obj, reader.GetValue(i));
                            }
                        }
                        obj.LoadCompleted();
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        public virtual void ExecuteSqlResourceFile(Type typeInResourceAssembly, string fileQualifiedName, IDbConnection connection, bool splitCommands)
        {
            using (var command = connection.CreateCommand())
            {
                string script = AssemblyHelper.GetAssemblyEmbeddedResourceString(typeInResourceAssembly, fileQualifiedName);
                if (splitCommands)
                {
                    Regex regex = new Regex(CommandSpitterRegexString);
                    var commands = regex.Split(script);
                    foreach (var cmd in commands)
                    {
                        command.CommandText = cmd;
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                }
            }
        }

        private IDbCommand GetStoreProcedureCommand(IDbConnection conn, string spName, List<SPParameter> args)
        {
            IDbCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = spName;
            foreach (var arg in args)
            {
                AddParameter(command, arg);
            }
            return command;
        }

        #endregion

        #region Count

        public int GetCount<T>(string condition = "") where T : Entity
        {
            using (IDbConnection conn = NewConnection())
            {
                conn.Open();
                IDbCommand command = GetCountCommand<T>(conn, condition);
                object result = command.ExecuteScalar();
                return (int)result;
            }
        }

        private IDbCommand GetCountCommand<T>(IDbConnection conn, string condition = "") where T : Entity
        {
            IDbCommand command = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
            string sql = string.Empty;
            sql = string.Format(CountSql, dataInfo.Name);
            if (!string.IsNullOrEmpty(condition))
            {
                sql = string.Format(WhereSql, sql, condition);
            }
            command.CommandText = sql;
            return command;
        }

        #endregion

        #region Max

        public int GetMax<T>(string name) where T : Entity
        {
            if (GetCount<T>() == 0)
            {
                return 0;
            }
            else
            {
                using (IDbConnection conn = NewConnection())
                {
                    conn.Open();
                    IDbCommand command = GetMaxCommand<T>(conn,name);
                    object result = command.ExecuteScalar();
                    return (int)result;
                }
            }
        }

        private IDbCommand GetMaxCommand<T>(IDbConnection conn,string name) where T : Entity
        {
            IDbCommand command = conn.CreateCommand();
            var dataInfo = ColumnCollectionInfo.GetInfo(typeof(T), ForeignKeySuffix, SchemaName);
            command.CommandText = string.Format(MaxSql, name, dataInfo.Name);
            return command;
        }

        

        #endregion

        #region Misc

        public abstract IDbConnection NewConnection();

        public abstract IDbConnection GetDatabaseConnection(string databaseName);

        public abstract void WaitUntilConnectionBeUsed(IDbConnection connection, int retryTimes = 20, int retryInterval = 500);

        public virtual void CreateDatabase(string databaseName)
        {
            using (var connection = GetDatabaseConnection(MasterDatabaseName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(CreateDatabaseSql, databaseName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void DropDatabase(string databaseName)
        {
            if (!DatabaseExist(databaseName))
            {
                return;
            }
            using (var connection = GetDatabaseConnection(MasterDatabaseName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(DropDatabaseSql, databaseName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public abstract string GetDatabaseName();

        public abstract string GetDatabaseHost();

        public abstract bool IsSystemDatabase(string databaseName);
        
        #endregion

    }
}
