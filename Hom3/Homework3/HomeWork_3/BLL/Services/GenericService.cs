using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BLL.Services
{
    public abstract class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        protected GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task Add(T obj)
        {
            try
            {
                var result = await _repository.AddAsync(obj);

                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to add {typeof(T).Name}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add {typeof(T).Name}. Exception: {ex.Message}");
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
        public virtual async Task Delete(Guid id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);

                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to delete {typeof(T).Name} with Id {id}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete {typeof(T).Name} with Id {id}. Exception: {ex.Message}");
            }
        }

        public virtual async Task<T> GetById(Guid id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);

                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to get {typeof(T).Name} by Id {id}.");
                }

                return result.Data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get {typeof(T).Name} by Id {id}. Exception: {ex.Message}");
            }
        }

        public virtual async Task<List<T>> GetAll()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                
                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to get all {typeof(T).Name}s.");
                }

                return result.Data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get all {typeof(T).Name}s. Exception: {ex.Message}");
            }
        }

        public async Task<T> GetByPredicate(Func<T, bool> predicate)
        {
            try
            {
                var result = await _repository.GetByPredicateAsync(predicate);
                
                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to get by predicate {typeof(T).Name}s.");
                }

                return result.Data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get by predicate {typeof(T).Name}s. Exception: {ex.Message}");
            }
        }

        public virtual async Task Update(Guid id, T obj)
        {
            try
            {
                var result = await _repository.UpdateAsync(id, obj);
                
                if (!result.IsSuccessful)
                {
                    throw new Exception($"Failed to update {typeof(T).Name} with Id {id}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update {typeof(T).Name} with Id {id}. Exception: {ex.Message}");
            }
        }
    }
}