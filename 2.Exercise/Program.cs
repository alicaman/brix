using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Exercise
{
    class Program
    {
        static readonly int inputNumberOfCharacters = 5;
        static readonly Random random = new Random();
        static readonly int oneMB = 1024 * 1024;
        static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        static readonly string filePath = "C:\\tmp2\\file.txt";

        static void Main(string[] args)
        {
            //CreateFile(inputNumberOfCharacters, 1, filePath);

            var input = GetValidInputString();

            // initial load: Create ALL Permutations for the input. Create lookup table for the Permutations

            var allInputPermutations = GenerateAllPermutations(input);

            var lookupTable = new HashSet<int>();

            foreach (var item in allInputPermutations)
            {
                lookupTable.Add(item.GetHashCode());
            }

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            Console.WriteLine($"\r\nFollowing result\\s for the \"{input}\" input:");

            // the search algorithm: get line from the file, search with O(1) complexity 
            Parallel.ForEach(File.ReadLines(filePath), parallelOptions, line =>
            {
                if (line == null) return;

                line = line.Trim();

                if (line.Length != 5) return;

                if (lookupTable.Contains(line.GetHashCode()))
                {
                    Console.WriteLine(line);
                }
            });

            Console.WriteLine("\r\nPress \"ENTER\" to exit.");
            Console.ReadLine();
        }

        private static string GenerateRandomString(int size)
        {
            var stringChars = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                stringChars.Append(chars[random.Next(chars.Length)]);
            }

            return stringChars.ToString();
        }

        private static void CreateFile(int nOfCharacters, int fileSizeInMb = 1, string path = "C:\\tmp2\\file.txt")
        {
            var sizeLimitInBytes = oneMB * fileSizeInMb;

            using (var stream = new FileStream(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            {
                while (stream.Position < sizeLimitInBytes)
                {
                    writer.WriteLine(GenerateRandomString(nOfCharacters)); // write data seperated by commas
                }
            }
        }

        private static bool HasInvalidCharacters(string input)
        {
            foreach (var ch in input.ToArray())
            {
                if (!chars.Contains(ch))
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetValidInputString()
        {
            var input = string.Empty;
            bool isValidString = false;

            while (!isValidString)
            {
                Console.WriteLine($"Please type {inputNumberOfCharacters} characters long alphanumerical string:");

                input = Console.ReadLine();

                if (input.Length > inputNumberOfCharacters || input.Length < inputNumberOfCharacters)
                {
                    Console.WriteLine("Invalid characters length!");
                    continue;
                }

                if (HasInvalidCharacters(input))
                {
                    Console.WriteLine("Invalid character\\s!");
                    continue;
                }

                isValidString = true;
            }

            return input;
        }
        
        private static string[] GenerateAllPermutations(string source)
        {
            if (source.Length == 1) return new string[] { source };

            var permutations = from c in source
                               from p in GenerateAllPermutations(new String(source.Where(x => x != c).ToArray()))
                               select c + p;

            return permutations.ToArray();
        }
    }
}
