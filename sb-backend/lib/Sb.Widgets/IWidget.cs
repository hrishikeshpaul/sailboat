namespace Sb.Widgets
{
    public interface IWidget
    {
        public string Id { get; set; }
    }

    public interface ICanComment
    {
        public IEnumerable<Comment> Comments { get; set; }
    }

    public interface ITimeSpanWidget : IWidget
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public interface ITransportationWidget: ITimeSpanWidget
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
    }


    public class AnchorWidget : IWidget
    {
        public string Id { get; set; } = string.Empty;
        public IEnumerable<IWidget> Children { get; set; } = Enumerable.Empty<IWidget>();
    }

    public class DateWidget : AnchorWidget
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

    public class BoardingWidget : ITimeSpanWidget
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Id { get; set; } = string.Empty;
    }
}