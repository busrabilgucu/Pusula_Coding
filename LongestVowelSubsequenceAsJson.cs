using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pusula_Coding
{
    internal class LongestVowelSubsequenceAsJson
    {
        public static string LongestVowelSubsequenceAsJsonMethod(List<string> words)
        {
            // Liste boşsa boş JSON döndürmek için.
            if (words == null || words.Count == 0)
                return JsonSerializer.Serialize(new List<object>());

            
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            var resultList = new List<object>();

            foreach (var word in words)
            {
                string currentSequence = "";  
                string bestSequence = "";     

                foreach (var ch in word)
                {
                    if (vowels.Contains(ch))
                    {
                        currentSequence += ch; // Artış devam ediyorsa eklesin.
                    }
                    else
                    {
                        // kontrol 
                        if (currentSequence.Length > bestSequence.Length)
                        {
                            bestSequence = currentSequence;
                        }
                        currentSequence = ""; // Yeni alt dizi başlat
                    }
                }

                // kontrol 
                if (currentSequence.Length > bestSequence.Length)
                {
                    bestSequence = currentSequence;
                }

                // Sonucu listeye ekleme.
                resultList.Add(new
                {
                    word = word,
                    sequence = bestSequence,
                    length = bestSequence.Length
                });
            }

            // JSON olarak döndür.
            return JsonSerializer.Serialize(resultList);
        }
    }
}
