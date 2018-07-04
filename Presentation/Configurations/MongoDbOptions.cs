namespace Presentation.Configurations
{
    public class MongoDbOptions
    {
        public MongoDbOptions(string connectionString, string databaseName, string collectionId)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionId = collectionId;
        }

        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
        public string CollectionId { get; private set; }
    }
}
