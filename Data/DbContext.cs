using System;
using Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MongoDB.Driver;
using Serilog;

namespace Data
{
    public class DbContext
    {
        private readonly IMongoDatabase _db;

        public DbContext(IMongoDatabase db)
        {
            _db = db;
            SetupIndexes();
        }

        public virtual IMongoCollection<Bug> Bugs => _db.GetCollection<Bug>("bug");

        public void SetupIndexes()
        {
            Log.Debug("Setting up indexes!");

            IndexKeysDefinitionBuilder<Bug> bugIndexKeysDefinition = Builders<Bug>.IndexKeys;

            CreateIndexModel<Bug>[] bugIndexModel = new[]
            {
                new CreateIndexModel<Bug>(bugIndexKeysDefinition.Ascending(_ => _.Id)),
                new CreateIndexModel<Bug>(bugIndexKeysDefinition.Ascending(_ => _.Title))
            };

            Bugs.Indexes.CreateMany(bugIndexModel);
            Log.Debug("Done setting up indexes!");
        }
    }
}