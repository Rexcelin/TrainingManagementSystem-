using System;
using System.IO;
using System.Text.Json;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class DataStorageService
    {
        private string filePath = "TrainingManagementSystem_Data.json";

        public void SaveData(TrainingData data)
        {
            try
            {
                string json = JsonSerializer.Serialize(
                    data,
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);

                Console.WriteLine("Application data saved successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("File error: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access error: " + ex.Message);
            }
        }

        public TrainingData? LoadData()
        {
            try
            {
                if (File.Exists(filePath) == false)
                {
                    return null;
                }

                string json = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<TrainingData>(json);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File error: " + ex.Message);
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Invalid saved data: " + ex.Message);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access error: " + ex.Message);
                return null;
            }
        }
    }
}