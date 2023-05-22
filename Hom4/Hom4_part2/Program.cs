using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hom4_part2
{
    public static class ValidatorExtensions
    {
        public static void Validate<T>(this T obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);

            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                throw new ValidationException($"Validation failed for {typeof(T)}");
            }
            foreach (var prop in obj.GetType().GetProperties())
            {
                foreach (var attr in prop.GetCustomAttributes(true))
                {
                    if (attr is IValidator validator)
                    {
                        validator.Validate(prop.GetValue(obj), obj);
                    }
                }
            }
        }
    }
    public class NotNullValidatorAttribute : Attribute, IValidator
    {
        public void Validate(object value, object container)
        {
            if (value == null)
            {
                throw new ValidationException($"{container.GetType().Name}.{nameof(value)} cannot be null");
            }
        }
    }
    public class PositiveNumberValidatorAttribute : Attribute, IValidator
    {
        public void Validate(object value, object container)
        {
            if (value is int intValue && intValue <= 0)
            {
                throw new ValidationException($"{container.GetType().Name}.{nameof(value)} must be a positive number");
            }
            else if (value is double doubleValue && doubleValue <= 0.0)
            {
                throw new ValidationException($"{container.GetType().Name}.{nameof(value)} must be a positive number");
            }
        }
    }
    public class Person
    {
        [NotNullValidator]
        public string Name { get; set; }

        [PositiveNumberValidator]
        public int Age { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var person = new Person { Name = "John", Age = -5 };
            try
            {
                person.Validate();
                Console.WriteLine("Validation succeeded!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Validation failed:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
