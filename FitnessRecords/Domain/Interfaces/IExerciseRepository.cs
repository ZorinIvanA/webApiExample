using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FitnessRecords.Domain.Interfaces
{
    public interface IExerciseRepository
    {
        Task<Exercise[]> GetExercises();

        Task AddExercise(Exercise exercise);

        Task AddFile(string description, byte[] file);
    }
}
