using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class TrainerService : GenericService<Trainer>, ITrainerService
    {
        protected List<Trainer> trenierList = new List<Trainer>();
        protected IClassService classService;
        public TrainerService(IRepository<Trainer> repository, IClassService classService)
            : base(repository)
        {
            this.classService = classService;
        }

        public async Task<Trainer> AddTrainer(Trainer trainer)
        {
            Trainer newTrainer = new Trainer(trainer);
            await Add(trainer);
            trenierList.Add(newTrainer);
            return newTrainer;
        }

        public async Task<List<Trainer>> GetTrainersBySpecialization(string specialization)
        {
            var trainers = await GetAll();
            var treinersBySpacialization = trainers.Where(u => u.Specialization.ToString() == specialization).ToList();
            if (treinersBySpacialization == null)
            {
                throw new Exception("User not found");
            }
            return treinersBySpacialization;
        }

        public async Task<List<Trainer>> GetAvailableTrainers(DateTime date)
        {
            var trainers = await GetAll();
            var availableTrainers = trainers.Where(i => i.AvailableDates.ToString() == date.ToString()).ToList();
            if (availableTrainers == null)
            {
                throw new Exception("Trainers not found");
            }
            return availableTrainers;
        }
        public async Task<bool> CheckTrainerAvailability(Guid trainerId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var trainer = await GetById(trainerId);
            if (trainer == null)
            {
                throw new Exception("Trainer not found!");
            }
            if (trainer.AvailableDates.ToString() == date.ToString() && endTime - startTime > new TimeSpan(0))
            {
                return true;
            }
            else { return false;};
        }

        public async Task AssignTrainerToClass(Guid trainerId, Guid classId)
        {
            var trainer = await GetById(trainerId);
            var fitnessClass = await classService.GetById(classId);
            if(trainer == null || fitnessClass == null) 
            {
                throw new Exception("Class or member not found");
            }
            fitnessClass.Trainer = trainer;
        }
        public async Task RemovinTrainerFromList()
        {
            Console.WriteLine("Enter Id trainer:");
            var trainer = await GetById(Guid.Parse(Console.ReadLine()));
            trenierList.Remove(trainer);
            await Delete(trainer.Id);
        }
    }
}