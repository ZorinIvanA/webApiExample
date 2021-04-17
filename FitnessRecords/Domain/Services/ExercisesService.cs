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
            {
                Console.WriteLine("bad argument");
                throw new ArgumentNullException(nameof(exercise));
            }

            Console.WriteLine("repository");
            await _exerciseRepository.AddExercise(exercise);
        }

        public async Task AddFile(string description, byte[] file)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("bad argument");
                throw new ArgumentNullException(nameof(description));
            }

            if (file==null )
                throw new ArgumentNullException(nameof(file));

            Console.WriteLine("repository");
            await _exerciseRepository.AddFile(description, file);
        }
    }
}
