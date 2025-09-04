using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pusula_Coding
{
    internal class FilterEmployees
    {
        public static string FilterEmployeesMethod(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
        {
            // Eğer liste boşsa boş JSON döndür.
            if (employees == null || !employees.Any())
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0m,
                    AverageSalary = 0m,
                    MinSalary = 0m,
                    MaxSalary = 0m,
                    Count = 0
                });

            // Filtreleme için şartlar.
            var filtered = employees
                .Where(e => e.Age >= 25 && e.Age <= 40
                            && (e.Department == "IT" || e.Department == "Finance")
                            && e.Salary >= 5000 && e.Salary <= 9000
                            && e.HireDate.Year > 2017)
                .ToList();

            // İsimleri uzundan kısaya sırala, sonra da alfabetik olarak sırala.
            var sortedNames = filtered
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            decimal totalSalary = filtered.Sum(e => e.Salary);
            decimal averageSalary = filtered.Any() ? filtered.Average(e => e.Salary) : 0m;
            decimal minSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0m;
            decimal maxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0m;
            int count = filtered.Count;

            // JSON formatında döndür.
            return JsonSerializer.Serialize(new
            {
                Names = sortedNames,
                TotalSalary = Math.Round(totalSalary, 2),
                AverageSalary = Math.Round(averageSalary, 2),
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            });
        }
    }
}
