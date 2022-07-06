using System;
using System.Collections.Generic;
using System.Linq;

namespace Sb.Data.Models
{
    [PersistenceModel("Users")]
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? EmailConfirmationToken { get; set; }
        public DateTime? DateEmailConfirmed { get; set; }
        public IEnumerable<ConnectedAccount> ConnectedAccounts { get; set; } = Enumerable.Empty<ConnectedAccount>();
    }
}
