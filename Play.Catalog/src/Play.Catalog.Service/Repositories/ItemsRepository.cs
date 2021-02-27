namespace Play.Catalog.Service.Repositories{
    public class ItemsRepository{
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database  = mongoClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        
        public async Task<IReadOnlyCollection<Item>> GetAsync(){
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(){
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id,id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task CreateAsync(Item entity){
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));
            }
            await dbCollection.InsertOneAsync(entity);
        }

         public async Task UpdateAsync(Item entity){
            if(entity == null){
                throw new ArgumentNullException(nameof(entity));

            }
            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity =>existingEntity.Id,entity.Id);
            await dbCollection.InsertOneAsync(entity);
        }

        public async Task RemoveAsync(Guid id){
            
           FilterDefinition<Item> filter = filterBuilder.Eq(entity =>entity.Id, id);
           await dbCollection.DeleteOneAsync(filter);
        }
         

    }
}