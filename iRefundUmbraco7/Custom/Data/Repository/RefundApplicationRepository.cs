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

        public async Task<List<RefundApplication>> FindIRD(string irdNumber)
        {
            var builder = Builders<RefundApplication>.Filter;
            var filter = builder.Eq("IRDNumber", irdNumber);
            var refundApplications = await this.ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return refundApplications;
        }

        public async Task<List<RefundApplication>> FindByEmail(string email)
        {
            var builder = Builders<RefundApplication>.Filter;
            var filter = builder.Eq("Email", email);
            var refundApplications = await this.ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return refundApplications;
        }

        public async Task<bool> Update(string irdNumber, string title, string firstName, string lastname, string email)
        {
            var builder = Builders<RefundApplication>.Filter;
            var filter = builder.Eq("IRDNumber", irdNumber);
            var update = Builders<RefundApplication>.Update
            .Set("Title", title)
            .Set("FirstName", firstName)
            .Set("LastName", lastname)
            .Set("Email", email)
            .CurrentDate("lastModified");
            await this.ConnectionHandler.MongoCollection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}
