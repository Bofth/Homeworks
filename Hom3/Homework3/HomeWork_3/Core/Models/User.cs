using Core.Enums;
using System.Collections.Generic;

namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool IsLocked { get; set; }
    }
}