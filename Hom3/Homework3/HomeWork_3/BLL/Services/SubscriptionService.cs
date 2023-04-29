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
    public class SubscriptionService : GenericService<Subscription>, ISubscriptionService
    {
        IMemberService memberService;
        public SubscriptionService(IRepository<Subscription> repository, IMemberService memberService) : base(repository)
        {
            this.memberService = memberService;
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            Subscription newSubscription = new Subscription(subscription);
            await Add(newSubscription);
            return newSubscription;
        }

        public async Task<List<Subscription>> GetSubscriptionsByMember(Guid memberId)
        {
            var subscriptionMember = await memberService.GetById(memberId);
            var subscriptions = await GetAll();
            var subscriptionsByMember = subscriptions.Where(i => i.Member == subscriptionMember).ToList();
            if (subscriptionMember == null)
            {
                throw new Exception("Mem not found");
            }
            return subscriptionsByMember;
        }

        public async Task<List<Subscription>> GetSubscriptionsByType(string subscriptionType)
        {
            var subscriptions = await GetAll();
            var subscriptionsByType = subscriptions.Where(u => u.Type.ToString() == subscriptionType).ToList();
            if (subscriptionsByType == null)
            {
                throw new Exception("Subscription not found");
            }
            return subscriptionsByType;
        }

        public async Task RenewSubscription(Guid subscriptionId)
        {
            var typeOfSumscription = await GetById(subscriptionId);
            switch (typeOfSumscription.Type.ToString()) 
            {
                case "Monthly":
                    typeOfSumscription.EndDate = typeOfSumscription.EndDate.AddDays(30);
                    break;
                case "Quarterly":
                    typeOfSumscription.EndDate = typeOfSumscription.EndDate.AddDays(90);
                    break;
                case "Annual":
                    typeOfSumscription.EndDate = typeOfSumscription.EndDate.AddDays(365);
                    break;
            }

        }

        public async Task CancelSubscription(Guid subscriptionId)
        {
            var subscription = await GetById(subscriptionId);
            subscription.IsActive = false;
        }
    }
}