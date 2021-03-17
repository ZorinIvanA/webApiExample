using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;

namespace FitnessRecords.Presentation.Models
{
    public class ExerciseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ExerciseModel()
        {
            
        }
        
        public ExerciseModel(Exercise exercise)
        {
            Name = exercise?.Name ?? throw new ArgumentNullException(nameof(exercise.Name));
            Description = exercise?.Description ?? throw new ArgumentNullException(nameof(exercise.Description));
        }

        public Exercise ToEntity()
        {
            return new Exercise()
            {
                Description = this.Description,
                Name = this.Name
            };
        }
    }
}
