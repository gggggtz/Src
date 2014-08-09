using Common.Persistent.ORMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistent
{

    public interface IEntity
    {
        void LoadCompleted();
    }
    public interface IEntityAccesser
    {
        void Load<T>(T obj) where T : IEntity, new();
        void Insert<T>(T obj) where T : IEntity, new();
        void Update<T>(T obj) where T : IEntity, new();
        void Delete<T>(T obj) where T : IEntity, new();
        List<T> Search<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : IEntity, new();
        List<TEntity> Search<TOwner, TEntity>(TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "") where TEntity : IEntity, new();
        void Initialize(bool reset);
        int GetCount<T>(string condition = "") where T : IEntity, new();
        int GetMax<T>() where T : IEntity, new();
        int ExecuteSPNonQuery(string spName, List<SPParameter> args);
        object ExecuteSPScalar(string spName, List<SPParameter> args);
        int ExecuteSPReturn(string spName, List<SPParameter> args);
        List<T> ExecuteSPReader<T>(string spName, List<SPParameter> args) where T : IEntity, new();
        List<string> ExecuteCommandReader(string cmdText);
        int ExecuteCommand(string cmdText);
        object ExecuteCommandScalar(string cmdText);
    }

    
}
