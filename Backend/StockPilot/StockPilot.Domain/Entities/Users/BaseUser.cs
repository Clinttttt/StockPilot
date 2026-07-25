using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.Enums;

namespace StockPilot.Domain.Entities.Users
{
    public  class BaseUser : Contact
    {    
        public bool MustChangePassword { get; protected set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public int FailedAttempts { get; protected set; }
        public DateTime? LockedUntil { get; protected set; }
        public DateTime? LastLoginAt { get; protected set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; protected set; }
        public UserRole userRole { get; set; }
        public BaseUser() { }

       
        public static BaseUser CreateUser(string fullName, string? phoneNumber, string? email, string? address, string userName, string passwordHash, UserRole userRole)
        {
            return new BaseUser
            {

                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                UserName = userName,
                PasswordHash = passwordHash,
                IsActive = true,
                MustChangePassword = false,
                FailedAttempts = 0,
                userRole = userRole
            };
        }

          
        public void FieldAttempts()
        {
            FailedAttempts++;
            if (FailedAttempts >= 5)
            {
                LockedUntil = DateTime.UtcNow.AddMinutes(15);
            }
        }
    }


}
