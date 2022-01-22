using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
