using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Configurations;

namespace Presentation.Repositories

{
    public class MongoDbRepository<T> where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(MongoDbOptions options)
        {
            var mongoUrl = new MongoUrl(options.ConnectionString);
            var settings = MongoClientSettings.FromUrl(mongoUrl);
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase(options.DatabaseName);

            _collection = database.GetCollection<T>(options.CollectionId);
        }

        public async Task<T> GetDocument(string id)
        {
            var document =
                await _collection
                    .FindAsync(e => e.Id == id)
                    .ConfigureAwait(false);

            return await document
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public List<T> GetAllDocuments()
        {
            var documents = _collection
                    .AsQueryable();

            return documents.ToList();
        }

        public async Task<IEnumerable<T>> GetDocument(Expression<Func<T, bool>> predicate)
        {
            var results = await _collection
                             .FindAsync(predicate)
                             .ConfigureAwait(false);

            return await results.ToListAsync().ConfigureAwait(false);
        }

        public async Task CreateDocument(T item)
        {
            await _collection.InsertOneAsync(item).ConfigureAwait(false);
        }

        public async Task CreateOrUpdate(T item)
        {
            var result = await _collection.ReplaceOneAsync(
                            filter: new BsonDocument("_id", item.Id),
                            options: new UpdateOptions { IsUpsert = true },
                            replacement: item);
        }

        public async Task<BsonDocument> UpdateDocument(T item)
        {
            var result = await _collection.ReplaceOneAsync(e => e.Id == item.Id, item).ConfigureAwait(false);

            return result.ModifiedCount > 0
                ? result.ToBsonDocument()
                : null;
        }

        public async Task<BsonDocument> UpdateDocument(Expression<Func<T, bool>> query, T item)
        {
            var result = await _collection.ReplaceOneAsync(query, item);
            return result.ModifiedCount > 0
                ? result.ToBsonDocument()
                : null;
        }

        public async Task<BsonDocument> UpdateDocument(string id, Dictionary<string, object> properties)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            var updates = properties.Select(property => Builders<T>.Update.Set(property.Key, property.Value)).ToList();
            var updatePartial = Builders<T>.Update.Combine(updates);

            var result = await _collection.UpdateOneAsync(filter, updatePartial);

            return result.ModifiedCount > 0
                ? result.ToBsonDocument()
                : null;
        }

        public async Task<bool> DeleteDocument(string id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }
    }
}
