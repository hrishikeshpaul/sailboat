namespace Sb.Widgets
{
    public interface IWidget
    {
        public string Id { get; set; }
        public string BoatId { get; set; }
    }

    public interface ICanComment
    {
        public IEnumerable<Comment> Comments { get; set; }
    }

    public interface ITimeSpanWidget
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public interface ITransportationWidget: ITimeSpanWidget
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
    }

    public interface IDateWidget
    {
        public DateOnly Date { get; set; }
    }

    public class Comment
    {
        public string UserId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    public class Address
    {
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public string CountryCode { get; set; } = string.Empty;
    }

    public class BoardingWidget : IWidget, ITimeSpanWidget, ICanComment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Id { get; set; } = string.Empty;
        public string BoatId  { get; set; } = string.Empty;
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();
    }
}