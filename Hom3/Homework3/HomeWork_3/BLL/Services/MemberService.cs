using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class MemberService : GenericService<Member>, IMemberService
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IClassService _classService;

        public MemberService(IRepository<Member> repository, ISubscriptionService subscriptionService, IClassService _classService) : base(repository)
        {
            _subscriptionService = subscriptionService;
            this._classService = _classService;        
        }
        

        public async Task<Member> RegisterMember(Member member)
        {
            Member newMember = new Member(member);
            await Add(member);
            return member;
        }

        public async Task<List<Member>> GetActiveMembers()
        {
            var members = await GetAll();
            var activMembers = members.Where(u => u.IsActive == true).ToList();
            if (activMembers == null)
            {
                throw new Exception("Member not found");
            }
            return activMembers;
        }

        public async Task<List<Member>> GetMembersBySubscriptionType(string subscriptionType)
        {
            var members = await GetAll();
            var membersBySybscription = members.Where(u => u.SubscriptionType.ToString() == subscriptionType).ToList();
            if (membersBySybscription == null)
            {
                throw new Exception("Member not found");
            }
            return membersBySybscription;
        }

        public async Task<List<Member>> GetMembersWithUpcomingRenewal(DateTime startDate, DateTime endDate)
        {
            var members = await GetAll();
            var membersBySybscription = members.Where(i => i.SubscriptionStartDate < startDate && i.SubscriptionEndDate > endDate).ToList();
            if (membersBySybscription == null)
            {
                throw new Exception("Member not found");
            }
            return membersBySybscription;
        }

        public async Task<bool> CheckMemberAttendance(Guid memberId, DateTime date)
        {
            var member = await GetById(memberId);
            var bookingClass = await _classService.GetClassesByDate(date);
            var attendClass = bookingClass.Where(i => i.Attendees.Contains(member));
            if (bookingClass == null)
            {
                throw new Exception("No classes found on this date");
            }
            if (attendClass == null) 
                    return false;
            return true;
        }

        public async Task RecordMemberAttendance(Guid memberId, DateTime date)
        {
            var member = await GetById(memberId);
            if(await CheckMemberAttendance(memberId,date) == true)
            {
                member.NumberVisits++;
            }
        }
    }
}