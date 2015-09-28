using iRefundUmbraco7.Custom.Data.Interface;
using iRefundUmbraco7.Custom.Data.Model;
using iRefundUmbraco7.Custom.Data.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRefundUmbraco7.Custom.Data.Repository
{
    public class RefundApplicationRepository : EntityService<RefundApplication>, IRefundApplicationRepository
    {
        public async Task<List<RefundApplication>> GetAll()
        {
            var builder = Builders<RefundApplication>.Filter;
            var filter = builder.Exists("IRDNumber");
            var refundApplications = await this.ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return refundApplications;
        }
    }
}
