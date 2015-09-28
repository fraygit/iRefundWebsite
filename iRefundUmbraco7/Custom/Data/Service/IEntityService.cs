using iRefundUmbraco7.Custom.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRefundUmbraco7.Custom.Data.Service
{
    public interface IEntityService<T> where T : IMongoEntity
    {
        void Create(T entity);
    }
}
