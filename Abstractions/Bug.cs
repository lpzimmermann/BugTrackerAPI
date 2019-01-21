using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Abstractions
{
    public class Bug
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public BugState State { get; set; }

        public enum BugState
        {
            Created,
            Open,
            InProgress,
            InReview,
            Done,
            Closed
        }
        
        public interface IBugRepository
        {
            Task<Bug> GetBug(ObjectId aBugId);
            Task<IEnumerable<Bug>> GetBugs();
            void AddBug(Bug aBug);
        }
    }
}