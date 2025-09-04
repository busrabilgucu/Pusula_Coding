using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Pusula_Coding
{
    internal class MaxIncreasingSubArrayAsJson
    {
        public static string MaxIncreasingSubArrayAsJsonMethod(List<int> numbers)
        {
            // Liste boşsa boş JSON döndür.
            if (numbers == null || numbers.Count == 0)
                return JsonSerializer.Serialize(new List<int>());

            List<int> currentSubArray = new List<int>();
            List<int> bestSubArray = new List<int>();   // Toplamı en yüksek olan alt dizi

            int currentSum = 0;
            int bestSum = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                // Eğer alt dizi boşsa ya da artış hala devam ediyosa;
                if (currentSubArray.Count == 0 || numbers[i] > currentSubArray[currentSubArray.Count - 1])
                {
                    currentSubArray.Add(numbers[i]);
                    currentSum += numbers[i];
                }
                else
                {
                    // Artış bozulduğunda toplam kontrolü
                    if (currentSum > bestSum)
                    {
                        bestSubArray = new List<int>(currentSubArray);
                        bestSum = currentSum;
                    }

                    // Yeni alt dizi başlat
                    currentSubArray.Clear();
                    currentSubArray.Add(numbers[i]);
                    currentSum = numbers[i];
                }
            }

            // Son alt dizi kontrolü
            if (currentSum > bestSum)
            {
                bestSubArray = new List<int>(currentSubArray);
            }

            // JSON formatında döndürme
            return JsonSerializer.Serialize(bestSubArray);
        }
    }
}
