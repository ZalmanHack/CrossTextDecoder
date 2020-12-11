using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextDecoder.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CrossTextDecoderTests.Helpers;

namespace TextDecoder.Utils.Tests
{
    [TestClass()]
    public class EncryptorTextTests
    {
        [TestMethod()]
        public async Task DecryptTest_EmtyText_EmptyKey_Async()
        {
            // arange
            string actual;
            string key = "";

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Decrypt(string.Empty, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod()]
        public async Task EncryptTest_EmtyText_EmptyKey_Async()
        {
            // arange
            string actual;
            string key = "";

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Encrypt(string.Empty, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }





        [TestMethod()]
        public async Task DecryptTest_EmtyText_Async()
        {
            // arange
            string actual;
            string key = "скорпион";
            
            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Decrypt(string.Empty, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod()]
        public async Task EncryptTest_EmtyText_Async()
        {
            // arange
            string actual;
            string key = "скорпион";

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Encrypt(string.Empty, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod()]
        public async Task DecryptTest_KeyEmpty_Async()
        {
            // arange
            string actual;
            string key = "";
            string dataEncrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 encrypted.txt"));

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Decrypt(dataEncrypted, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod()]
        public async Task EncryptTest_KeyEmpty_Async()
        {
            // arange
            string actual;
            string key = "";
            string dataDecrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 decrypted.txt"));

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Encrypt(dataDecrypted, key);

            // assert
            Assert.AreEqual(string.Empty, actual);
        }


        [TestMethod()]
        public async Task DecryptTest_1000000symbols_Async()
        {
            // arange
            string actual;
            string key = "скорпион";
            string dataDecrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 decrypted.txt"));
            string dataEncrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 encrypted.txt"));
            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Decrypt(dataEncrypted, key);

            // assert
            Assert.AreEqual(dataDecrypted, actual);
        }

        [TestMethod()]
        public async Task EncryptTest_1000000symbols_Async()
        {
            // arange
            string actual;
            string key = "скорпион";
            string dataDecrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 decrypted.txt"));
            string dataEncrypted = await FileReader.getDataStringAsync(Path.GetFullPath(@"..\..\..\Resources\ANSI Length-1000000 encrypted.txt"));

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Encrypt(dataDecrypted, key);

            // assert
            Assert.AreEqual(dataEncrypted, actual);
        }

        [TestMethod()]
        public async Task DecryptTest_Key_bigger_Text_Async()
        {
            // arange
            string actual;
            string key = "йцукенгшщзхъфывапролджэячсмитьбю";
            string dataDecrypted = "один два three четыре";
            string dataEncrypted = "шъьш ипг three пюъркщ";

            // act
            EncryptorText encryptorText = new EncryptorText();
            actual = await encryptorText.Decrypt(dataEncrypted, key);

            // assert
            Assert.AreEqual(dataDecrypted, actual);
        }

    }
}