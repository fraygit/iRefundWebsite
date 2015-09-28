﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRefundUmbraco7.Custom.Data.Entities.Base
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
