using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;

namespace FitnessRecords.Domain.Interfaces
{
    /// <summary>
    /// Служба для работы с интерфейсами
    /// </summary>
    public interface IExerciseService
    {
        /// <summary>
        /// Возвращает список упражнений
        /// </summary>
        /// <returns></returns>
        Task<Exercise[]> GetExercises();

        Task AddExercise(Exercise exercise);

        Task AddFile(string description, byte[] file);
    }
}
