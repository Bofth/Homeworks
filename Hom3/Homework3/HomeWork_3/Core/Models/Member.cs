using System;
using Core.Enums;

namespace Core.Models
{
    public class Member : BaseEntity
    {
        public Member() { }
        public Member(Member member) 
        {
            FirstName = member.FirstName;
            LastName = member.LastName;
            DateOfBirth = member.DateOfBirth;
            Email = member.Email;
            PhoneNumber = member.PhoneNumber;
            SubscriptionType = member.SubscriptionType;
            SubscriptionStartDate = member.SubscriptionStartDate;
            SubscriptionEndDate = member.SubscriptionEndDate;
            IsActive = member.IsActive;
            NumberVisits = member.NumberVisits;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public bool IsActive { get; set; }
        public int NumberVisits { get; set; }
    }
}