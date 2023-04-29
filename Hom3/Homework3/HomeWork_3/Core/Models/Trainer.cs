using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Trainer : BaseEntity
    {
        public Trainer() { }
        public Trainer(Trainer trainer) 
        {
            FirstName = trainer.FirstName;
            LastName = trainer.LastName;
            Specialization = trainer.Specialization;
            AvailableDates = trainer.AvailableDates;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public ICollection<DateTime> AvailableDates { get; set; }
    }
}