using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode_2020.Archive
{
    class Day4
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day4.txt";

            using (var stream = new StreamReader(path))
            {
                string line;
                string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var correctPasswords = 0;

                while ((line = stream.ReadLine()) != null)
                {
                    Dictionary<string, string> currentPassport = new Dictionary<string, string>();
                    do
                    {
                        foreach (var info in line.Split(" "))
                        {
                            currentPassport.Add(info.Split(":")[0], info.Split(":")[1]);
                        }

                        line = stream.ReadLine();
                    } while (line != string.Empty && line != null);

                    var foundElements = 0;
                    foreach (var element in requiredFields)
                    {
                        if (!currentPassport.ContainsKey(element) || currentPassport[element] == string.Empty)
                            continue;

                        if (CheckData(element, currentPassport[element]))
                            ++foundElements;
                    }

                    if (foundElements == requiredFields.Length)
                        ++correctPasswords;
                }

                Console.WriteLine("Correct Passwords: " + correctPasswords);
            }
        }

        private static bool CheckData(string key, string value)
        {
            switch (key)
            {
                case "byr":
                    var birthYear = int.Parse(value);
                    return (birthYear >= 1920 && birthYear <= 2002);

                case "iyr":
                    var issuedYear = int.Parse(value);
                    return (issuedYear >= 2010 && issuedYear <= 2020);

                case "eyr":
                    var expirationYear = int.Parse(value);
                    return (expirationYear >= 2020 && expirationYear <= 2030);

                case "hgt":
                    if (!Regex.IsMatch(value, "[0-9]{2,}(cm|in)"))
                        return false;

                    var unit = value.Substring(value.Length - 2, 2);
                    var length = int.Parse(value.Substring(0, value.Length - 2));
                    
                    if (unit == "cm")
                        return (length >= 150 && length <= 193);
                    else
                        return (length >= 59 && length <= 76);

                case "hcl":
                    var hairPattern = "#([0-9a-fA-F]{6})";
                    return Regex.IsMatch(value, hairPattern);

                case "ecl":
                    var colorPattern = "amb|blu|brn|gry|grn|hzl|oth";
                    return Regex.IsMatch(value, colorPattern);

                case "pid":
                    return (value.Length == 9);
            }

            return false;
        }
    }
}
