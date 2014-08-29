using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistent
{
    public enum EntityAccesserType
    {
        Xml,
        FlatFile,
        SqlServer
    }

    public interface IEntityAccesser
    {
        void Load<T>(T obj) where T : Entity;
        void Insert<T>(T obj) where T : Entity;
        void Update<T>(T obj) where T : Entity;
        void Delete<T>(T obj) where T : Entity;
        List<T> Search<T>(string condition = "", string order = "", int start = -1, int count = -1) where T : Entity, new();
        List<TEntity> Search<TOwner, TEntity>(TOwner owner, Expression<Func<TOwner, IEnumerable>> exp, string order = "") where TEntity : Entity, new();
        int GetCount<T>(string condition = "") where T : Entity;
        int GetMax<T>(string name) where T : Entity;
    }
}
