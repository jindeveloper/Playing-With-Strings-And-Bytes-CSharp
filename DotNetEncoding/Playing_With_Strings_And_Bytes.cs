using System;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetEncoding
{
    [TestClass]
    public class Playing_With_Strings_And_Bytes
    {

        #region encoding-class-tests
        [TestMethod]
        public void Test_If_Encodings_Are_Same_Type()
        {
            Assert.IsInstanceOfType(Encoding.ASCII, typeof(ASCIIEncoding)); //true

            Assert.IsInstanceOfType(Encoding.UTF7, typeof(UTF7Encoding)); //true

            Assert.IsInstanceOfType(Encoding.UTF8, typeof(UTF8Encoding)); //true

            Assert.IsInstanceOfType(Encoding.Unicode, typeof(UnicodeEncoding)); //true

            Assert.IsInstanceOfType(Encoding.UTF32, typeof(UTF32Encoding)); //true
        }

        [TestMethod]
        public void Test_Types_Of_Derived_Encoding_Classes()
        {
            var type = typeof(Encoding);
            var assembly = Assembly.GetAssembly(type);
            var types = assembly.GetTypes();

            var derivedClasses = types.Where(t => t.IsSubclassOf(type) && t.IsPublic == true).ToList();

            foreach (var @class in derivedClasses)
            {
                Console.WriteLine(@class.Name);
            }
        }
        #endregion

        #region member-variables

        private string strRandomWords = "I Love C#";

        private string strRandomNonEnglishStrings = "プログラミングが大好き"; //use non Latin alphabet  character to test UTF-8

        #endregion

        #region Get-String

        [TestMethod]
        public void Test_ASCII_Using_Get_String()
        {
            var byteResults = Encoding.ASCII.GetBytes(this.strRandomWords); //converts a string into byte array

            Assert.IsTrue(byteResults.Length > 0); //true

            string humanReadableString = Encoding.ASCII.GetString(byteResults, 0, byteResults.Length);

            Assert.AreEqual(humanReadableString, strRandomWords);
        }

        #endregion


        #region bit-converter
        [TestMethod]
        public void Test_Convert_String_To_Bytes_Formatted()
        {
            var bytes = Encoding.UTF8.GetBytes(strRandomWords);

            Assert.IsNotNull(bytes);

            var seriesOfByteStrings = BitConverter.ToString(bytes); //converts a byte array into a series of byte-strings 

            Assert.IsTrue(!string.IsNullOrWhiteSpace(seriesOfByteStrings));

            Console.WriteLine(seriesOfByteStrings); //49-20-4C-6F-76-65-20-43-23
        }

        [TestMethod]
        public void Test_Convert_To_Bytes_Formatted_Using_Other_Value_Types()
        {
            int bday = 03291982;

            var result = BitConverter.GetBytes(bday);

            Assert.IsNotNull(result);

            var seriesOfBytesStrings = BitConverter.ToString(result);

            Assert.IsTrue(!string.IsNullOrWhiteSpace(seriesOfBytesStrings));

            Console.WriteLine(seriesOfBytesStrings); //4E-3B-32-00
        }
        #endregion

        #region get-byte-count

        [TestMethod]
        public void Test_UTF8_Encoding_Using_GetByteCount()
        {
            var byteResults = Encoding.UTF8.GetBytes(this.strRandomNonEnglishStrings); //converts a string into byte array

            int byteCount = Encoding.UTF8.GetByteCount(this.strRandomNonEnglishStrings); //get the byte count

            int totalBits = 0;

            for (int counter = 0; counter < byteResults.Length; counter++)
            {
                string bits = Convert.ToString(byteResults[counter], 2);

                totalBits = bits.Length + totalBits;
            }

            Assert.AreEqual(byteCount, Math.Ceiling((totalBits / 8.00))); //lets check if they are equal. 8 is used because UTF8 uses 8 bits
        }

        [TestMethod]
        public void Test_ASCII_Using_GetByteCount()
        {
            var byteResults = Encoding.ASCII.GetBytes(this.strRandomWords); //converts a string into byte array

            int byteCount = Encoding.ASCII.GetByteCount(this.strRandomWords); //get the byte count

            int totalBits = 0;

            for (int counter = 0; counter < byteResults.Length; counter++)
            {
                string bits = Convert.ToString(byteResults[counter], 2);

                totalBits = bits.Length + totalBits;
            }

            Assert.AreEqual(byteCount, Math.Ceiling((totalBits / 7.00))); //lets check if they are equal. 7 is used because ASCII uses 7 bits
        }

        #endregion

        #region get-bytes
        [TestMethod]
        public void Test_ASCII_Using_GetBytes()
        {
            var byteResults = Encoding.ASCII.GetBytes(this.strRandomWords); //converts a string into byte array

            Assert.IsTrue(byteResults.Length > 0); //true

            #region iterate 

            foreach (var @byte in byteResults)
            {
                string fullResultInString = string.Format("Character: {0} in ASCII {1}", (char)@byte, @byte);

                Console.WriteLine(fullResultInString);
            }

            #endregion 
        }
        #endregion

    }
}
