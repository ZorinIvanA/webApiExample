using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;
using FitnessRecords.Domain.Interfaces;

namespace FitnessRecords.Domain.Services
{
    public class ExercisesService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExercisesService(IExerciseRepository repository)
        {
            _exerciseRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Exercise[]> GetExercises()
        {
            return await _exerciseRepository.GetExercises();
        }

        public async Task AddExercise(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            _exerciseRepository.AddExercise(exercise);
        }
    }
}
