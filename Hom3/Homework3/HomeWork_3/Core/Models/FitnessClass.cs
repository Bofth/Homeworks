using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class FitnessClass : BaseEntity
    {
        public FitnessClass() { }
        public FitnessClass(FitnessClass fitnessClass) 
        {
            Name = fitnessClass.Name;
            Attendees = fitnessClass.Attendees;
            StartTime = fitnessClass.StartTime;
            Trainer = fitnessClass.Trainer;
            EndTime = fitnessClass.EndTime;
            Type = fitnessClass.Type;
            Date = fitnessClass.Date;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public Trainer Trainer { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Member> Attendees { get; set; }
    }
}