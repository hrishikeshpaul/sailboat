using System;

namespace Sb.Data.Models
{
    public class ConnectedAccount
    {
        public string Id { get; set; }
        public string Provider { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public DateTime? DateConnected { get; set; }
    }
}
