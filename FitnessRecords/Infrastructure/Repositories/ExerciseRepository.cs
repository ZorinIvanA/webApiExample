using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessRecords.Domain.Entities;
using FitnessRecords.Domain.Interfaces;
using FitnessRecords.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;

namespace FitnessRecords.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private const string CONNECTION_STRING_NAME = "FitnessRecords";

        private readonly IConfiguration _configuration;

        public ExerciseRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Exercise[]> GetExercises()
        {
            List<ExerciseDTO> exercises = new List<ExerciseDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM dbo.Excercises", connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        exercises.Add(new ExerciseDTO()
                        {
                            id = int.Parse(reader["id"].ToString()),
                            name = reader["name"].ToString(),
                            description = reader["description"].ToString()
                        });
                    }
                }
            }

            return exercises.Select(e => e.ToModel()).ToArray();
        }

        public async Task AddExercise(Exercise exercise)
        {
            if (exercise == null)
            {
                Console.WriteLine("adding excercise via repo");
                throw new ArgumentNullException(nameof(exercise));
            }

            using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                Console.WriteLine($"open con {connection.ConnectionString}");
                await connection.OpenAsync();
                Console.WriteLine("con opened");
                using (var cmd = new SqlCommand($"INSERT INTO dbo.Exercises (name, description) VALUES ('{exercise.Name}','{exercise.Description}')", connection))
                {
                    Console.WriteLine("exec command");
                    await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("exec command finished");
                }
            }
        }

        public async Task AddFile(string description, byte[] file)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                var b = Convert.ToBase64String(file);
                using (var cmd = new SqlCommand($"INSERT INTO dbo.Exercises (name, description, picture) VALUES ('{description}','{description}, {b}')", connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
