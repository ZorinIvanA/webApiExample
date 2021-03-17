using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;

namespace FitnessRecords.Domain.Interfaces
{
    public interface IExerciseRepository
    {
        Task<Exercise[]> GetExercises();

        Task AddExercise(Exercise exercise);
    }
}
