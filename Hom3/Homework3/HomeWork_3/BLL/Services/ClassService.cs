using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class ClassService : GenericService<FitnessClass>, IClassService
    {
        private readonly ITrainerService _trainerService;
        private readonly IMemberService _memberService;
        public ClassService(IRepository<FitnessClass> repository, ITrainerService trainerService, IMemberService memberService)
            : base(repository)
        {
            _trainerService = trainerService;
            this._memberService = memberService;
        }

        public async Task<FitnessClass> ScheduleClass(FitnessClass fitnessClass)
        {
            FitnessClass newFitnesClass = new FitnessClass(fitnessClass);
            await Add(newFitnesClass);
            return newFitnesClass;
        }

        public async Task<List<FitnessClass>> GetClassesByDate(DateTime date)
        {
            var classes = await GetAll();
            var classesByDate = classes.Where(i => i.Date == date).ToList();
            if (classesByDate == null)
            {
                throw new Exception("No classes found for this date");
            }
            return classesByDate;
        }

        public async Task<List<FitnessClass>> GetClassesByType(string classType)
        {
            var classes = await GetAll();
            var classesByType = classes.Where(i => i.Type == classType).ToList();
            if (classesByType == null)
            {
                throw new Exception("No classes found for this date");
            }
            return classesByType;
        }

        public async Task<List<FitnessClass>> GetClassesByTrainer(Guid trainerId)
        {
            var classes = await GetAll();
            var treiner = await _trainerService.GetById(trainerId);
            var classesByTrainer = classes.Where(i => i.Trainer == treiner).ToList();
            if (classesByTrainer == null)
            {
                throw new Exception("No classes found for this date");
            }
            return classesByTrainer;
        }

        public async Task AddAttendeeToClass(Guid classId, Guid memberId)
        {
            var accurateClass = await GetById(classId);
            var member = await _memberService.GetById(memberId);
            try
            {
                accurateClass.Attendees.Add(member);
            }
            catch
            {
                throw new Exception("Sorry, something went wrong");
            }
        }
    }
}