using AdventOfCode.Solutions._2020;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.UnitTests._2020
{
    [TestClass]
    public class PasswordPhilosophyTests
    {
        [TestMethod]
        public void GetNumberOfValidPasswords_ValidateBasicInput()
        {
            var input = new List<string>() {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"            
            };

            var sut = new PasswordPhilosophy();
            var result = sut.GetNumberOfValidPasswords(input);

            //same input with extra empty spaces
            input = new List<string>() {
                "1-3  a:   abcde",
                "  1-3  b:  cdefg   ",
                "2-9  c  :  ccccccccc      "
            };
            result = sut.GetNumberOfValidPasswords(input);

            Assert.AreEqual(2, result);
        }

        [TestMethod]       
        public void GetNumberOfValidPasswords_ValidateSampleInput()
        {
            var inputElements = File.ReadAllLines("2020\\2 - Password Philosophy\\SampleInput.txt");
           
            var sut = new PasswordPhilosophy();
            var result = sut.GetNumberOfValidPasswords(inputElements);

            Assert.AreEqual(625, result);
        }      

        [TestMethod]
        public void GetNumberOfValidPasswords_InvalidPolicySeparator_ThrowsException()
        {
            var input = new List<string>() {
                "1-3 a/ abcde"               
            };
            var sut = new PasswordPhilosophy();      
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));
        }

        [TestMethod]
        public void GetNumberOfValidPasswords_EmptyPassword_ThrowsException()
        {
            var input = new List<string>() {
                "1-3 a: "
            };
            var sut = new PasswordPhilosophy();
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "1-3 a:";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));
        }

        [TestMethod]
        public void GetNumberOfValidPasswords_InvalidPolicyElementSeparator_ThrowsException()
        {
            var input = new List<string>() {
                "1-3-a: abcde"
            };
            var sut = new PasswordPhilosophy();
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "1-3a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));            
        }

        [TestMethod]
        public void GetNumberOfValidPasswords_InvalidPolicy_ThrowsException()
        {
            var input = new List<string>() {
                "- : abcde"
            };
            var sut = new PasswordPhilosophy();
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "1/3 a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = ": abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));
        }

        [TestMethod]
        public void GetNumberOfValidPasswords_InvalidRangeValues_ThrowsException()
        {
            var input = new List<string>() {
                "v-3 a: abcde"
            };
            var sut = new PasswordPhilosophy();
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "1-r a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "13 a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "1/3 a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));

            input[0] = "13a: abcde";
            Assert.ThrowsException<ArgumentException>(() => sut.GetNumberOfValidPasswords(input));
        }
    }
}
