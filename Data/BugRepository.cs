using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data
{
    public class BugRepository : Bug.IBugRepository
    {
        private readonly IMongoCollection<Bug> _bugCollection;

        public BugRepository(IMongoCollection<Bug> bugCollection)
        {
            _bugCollection = bugCollection;
        }

        public async Task<Bug> GetBug(ObjectId aBugId)
        {
            var aBug = await _bugCollection.AsQueryable().Where(bug => bug.Id == aBugId).FirstOrDefaultAsync();
            return aBug;
        }

        public async Task<IEnumerable<Bug>> GetBugs()
        {
            var aBugList = await _bugCollection.AsQueryable().ToListAsync();
            return aBugList;
        }

        public async void AddBug(Bug aBug)
        {
            await _bugCollection.InsertOneAsync(aBug);
        }
    }
}