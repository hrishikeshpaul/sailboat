using System;
using System.Collections.Generic;

namespace Sb.Data.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<int> Boats { get; set; }
    }
}
