using PGPSnippet.Keys;
using PGPSnippet.PGPDecryption;
using PGPSnippet.PGPEncryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGPSnippet
{
    class Program
    {

        public void KeyGeneration()
        {
            #region PublicKey and Private Key Generation

            PGPSnippet.KeyGeneration.KeysForPGPEncryptionDecryption.GenerateKey("maruthi", "P@ll@m@lli", @"D:\Keys\");
            Console.WriteLine("Keys Generated Successfully");

            #endregion
        }

        public void Encryption()
        {
            #region PGP Encryption

            PgpEncryptionKeys encryptionKeys = new PgpEncryptionKeys(@"D:\Keys\PGPPublicKey.asc", @"D:\Keys\PGPPrivateKey.asc", "P@ll@m@lli");
            PgpEncrypt encrypter = new PgpEncrypt(encryptionKeys);
            using (Stream outputStream = File.Create("D:\\Keys\\EncryptData.txt"))
            {
                encrypter.EncryptAndSign(outputStream, new FileInfo(@"D:\Keys\PlainText.txt"));
            }
            Console.WriteLine("Encryption Done !");

            #endregion
        }

        public void Decryption()
        {

            #region PGP Decryption

            PGPDecrypt.Decrypt("D:\\Keys\\EncryptData.txt", @"D:\Keys\PGPPrivateKey.asc", "P@ll@m@lli", "D:\\Keys\\OriginalText.txt");

            Console.WriteLine("Decryption Done");

            #endregion
        }


        static void Main(string[] args)
        {

            Program objPGP = new Program();

            try
            {

                objPGP.KeyGeneration();
                objPGP.Encryption();
                objPGP.Decryption();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Some thing went wrong");
                Console.Read();
            }
        }
    }
}
