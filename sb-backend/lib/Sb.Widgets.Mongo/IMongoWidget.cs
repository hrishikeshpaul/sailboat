using Sb.Data.Models.Mongo;
using Sb.Widgets;
namespace Sb.Widgets.Mongo
{
    [MongoCollection("Widgets")]
    public interface IMongoWidget : IWidget
    {
    }
}