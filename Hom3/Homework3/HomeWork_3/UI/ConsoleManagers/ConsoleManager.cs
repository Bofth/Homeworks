using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace UI.ConsoleManagers
{
    public abstract class ConsoleManager<TService, TEntity>
    where TEntity : BaseEntity
    where TService : IGenericService<TEntity>
    {
        protected readonly TService Service;

        protected ConsoleManager(TService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public abstract Task PerformOperationsAsync();
        public SubscriptionType ChoiseSubscriptionType()
        {
            Console.WriteLine("Enter Type: (0 = Monthly, 1 = Quarterly, 2 = Annual)");
            int choise = int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 0:
                    return SubscriptionType.Monthly;

                case 1:
                    return SubscriptionType.Quarterly;

                case 2:
                    return SubscriptionType.Annual;

                default:
                    return SubscriptionType.Annual;
            }

        }
        public string HashPassword(string passwordtoHash)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordtoHash!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await Service.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                
                return Enumerable.Empty<TEntity>();
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                return await Service.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                
                return null;
            }
        }

        public virtual async Task<TEntity> GetByPredicateAsync(Func<TEntity, bool> predicate)
        {
            try
            {
                return await Service.GetByPredicate(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByPredicateAsync: {ex.Message}");
                
                return null;
            }
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await Service.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAsync: {ex.Message}");
            }
        }

        public virtual async Task UpdateAsync(Guid id, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await Service.Update(id, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
            }
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                await Service.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
            }
        }
    }
}