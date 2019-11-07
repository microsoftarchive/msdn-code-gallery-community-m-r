using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGPSnippet.KeyGeneration
{
    public class KeysForPGPEncryptionDecryption
    {
        public static void GenerateKey(string username, string password, string keyStoreUrl)
        {
            IAsymmetricCipherKeyPairGenerator kpg = new RsaKeyPairGenerator();
            kpg.Init(new RsaKeyGenerationParameters(BigInteger.ValueOf(0x13), new SecureRandom(), 1024, 8));
            AsymmetricCipherKeyPair kp = kpg.GenerateKeyPair();
            FileStream out1 = new FileInfo(string.Format("{0}PGPPrivateKey.asc", keyStoreUrl)).OpenWrite();
            FileStream out2 = new FileInfo(string.Format("{0}PGPPublicKey.asc", keyStoreUrl)).OpenWrite();
            ExportKeyPair(out1, out2, kp.Public, kp.Private, username, password.ToCharArray(), true);
            out1.Close();
            out2.Close();
        }

        private static void ExportKeyPair(Stream secretOut, Stream publicOut,AsymmetricKeyParameter publicKey,AsymmetricKeyParameter privateKey,string identity,char[] passPhrase, bool armor)
        {
            if (armor)
            {
                secretOut = new ArmoredOutputStream(secretOut);
            }

            PgpSecretKey secretKey = new PgpSecretKey(PgpSignature.DefaultCertification,PublicKeyAlgorithmTag.RsaGeneral,publicKey,privateKey,DateTime.Now,identity,SymmetricKeyAlgorithmTag.Cast5,passPhrase,null,null,new SecureRandom()
             //                ,"BC"
                );

            secretKey.Encode(secretOut);

            secretOut.Close();

            if (armor)
            {
                publicOut = new ArmoredOutputStream(publicOut);
            }

            PgpPublicKey key = secretKey.PublicKey;

            key.Encode(publicOut);

            publicOut.Close();
        }
    }
}
