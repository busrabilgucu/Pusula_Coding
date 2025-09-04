using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pusula_Coding
{
    internal class FilterPeopleFromXml
    {
        public static string FilterPeopleFromXmlMethod(string xmlData)
        {
            // Eğer xmlData boşsa boş JSON döndür.
            if (string.IsNullOrWhiteSpace(xmlData))
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                });

            XDocument xdoc = XDocument.Parse(xmlData);

            // Tüm Person elemanlarını al.
            var people = xdoc.Descendants("Person")
                .Select(p => new
                {
                    Name = p.Element("Name")?.Value,
                    Age = int.Parse(p.Element("Age")?.Value ?? "0"),
                    Department = p.Element("Department")?.Value,
                    Salary = decimal.Parse(p.Element("Salary")?.Value ?? "0"),
                    HireDate = DateTime.Parse(p.Element("HireDate")?.Value ?? "1900-01-01")
                });

            // Filtreleme için şartlar
            var filtered = people
                .Where(p => p.Age > 30 && p.Department == "IT" && p.Salary > 5000 && p.HireDate.Year < 2019)
                .OrderBy(p => p.Name)  
                .ToList();

            var names = filtered.Select(p => p.Name).ToList();
            var totalSalary = filtered.Sum(p => p.Salary);
            var averageSalary = filtered.Any() ? filtered.Average(p => p.Salary) : 0;
            var maxSalary = filtered.Any() ? filtered.Max(p => p.Salary) : 0;
            var count = filtered.Count;

            // JSON formatında döndür.
            return JsonSerializer.Serialize(new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                MaxSalary = maxSalary,
                Count = count
            });
        }
    }
}
