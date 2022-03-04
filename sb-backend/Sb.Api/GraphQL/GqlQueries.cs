using HotChocolate.Data;

using MongoDB.Driver;

using Sb.Data;
using Sb.Data.Models.Mongo;
using Sb.Data.Mongo;
using Sb.Widgets;

namespace Sb.Api.GraphQL
{
    public class GqlQueries
    {
        [UseFiltering]
        public IQueryable<User> GetUsers([Service] MongoRepository<User> userRepo)
        {
            return userRepo.Collection.AsQueryable();
        }

        [UseFiltering]
        public async Task<IAnchorWidget> GetBoatAnchor(string boatId, [Service] IRepository<IAnchorWidget> widgetRepo)
            => await widgetRepo.FirstOrDefaultAsync(widget => widget.BoatId == boatId);

    }
}
