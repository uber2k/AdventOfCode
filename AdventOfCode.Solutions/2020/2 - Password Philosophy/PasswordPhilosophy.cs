using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions._2020
{
    /// <summary>
    /// Solution to Advent of Code 2020 - Day 2 - Part 1
    /// </summary>
    /// <see href="https://adventofcode.com/2020/day/2"></see>
    public class PasswordPhilosophy
    {
        public int GetNumberOfValidPasswords(IEnumerable<string> passwordsWithPolicy)
        {
            return passwordsWithPolicy.Count(p => IsPasswordValid(p) );
        }

        private bool IsPasswordValid(string passwordWithPolicy)
        {
            var (letter, password, minOccurrences, maxOccurrences) = ValidateInputAndExtractElements(passwordWithPolicy);
            //leaving this using Regex as it was my first thought; we could do a password.Count(...), loop on password...
            var letterCount = Regex.Matches(password, letter).Count;
      
            return letterCount >= minOccurrences && letterCount <= maxOccurrences;
        }

        /// <summary>
        /// Validates input and extracts the needed values from the input password-policy combination. 
        /// </summary>
        /// <param name="passwordWithPolicy">Policy/password combination to validate</param>
        /// <returns>Values extracted from the inpur password-poicy combination</returns>
        /// <exception cref="ArgumentException">Exception thrown when invalid data is found</exception>
        private (string letter, string password, int minOccurrences, int maxOccurrences) ValidateInputAndExtractElements(string passwordWithPolicy)
        {
            var inputElements = passwordWithPolicy.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (inputElements.Length != 2)
            {
                throw new ArgumentException($"Invalid policy-password element: {passwordWithPolicy}");
            }

            var policyElements = inputElements[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (policyElements?.Length != 2)
            {
                throw new ArgumentException($"Invalid policy element: {passwordWithPolicy}");
            }

            var password = inputElements[1];
            var range = policyElements[0];
            var letter = policyElements[1];

            var rangeValues = range.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (rangeValues?.Length != 2)
            {
                throw new ArgumentException($"Invalid range format in policy element: {passwordWithPolicy}");
            }

            if (!int.TryParse(rangeValues[0], out int minOccurrences) || !int.TryParse(rangeValues[1], out int maxOccurrences))
            {
                throw new ArgumentException($"Non-numeric value in policy range element at: {passwordWithPolicy}");
            }

            return new(letter, password, minOccurrences, maxOccurrences);
        }
    }
}
