
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace OnlineBanking.DAL
{
    public sealed class Crypto : IDisposable
    {

        #region "Supporting Properties and Enums, Constructors"
        //[Guid("A26B0311-A9A7-47F7-9C90-B599CCF68E4D")]
        private string mstrInitVector = "z6B4545c3Fe686702"; // Randomly generated
        private string mstrPrivKey    = "A26b0q599pCs68E4D";

        private SymmetricAlgorithm mobjCryptoService;
        //Supported .Net intrinsic SymmetricAlgorithm classes.
        public enum Providers
        {
            DES,
            //   DESCryptoServiceProvider
            DES3,
            //   TripleDESCryptoServiceProvider (Actual 3DES)
            RC2,
            //   RC2CryptoServiceProvider
            Rijndael
            //   RijnDaelManaged
        }

        //Constructor for using default .Net SymmetricAlgorithm class.
        public Crypto()
            : this(Providers.DES)
        {
        }

        //Constructor for using an intrinsic .Net SymmetricAlgorithm class.
        public Crypto(Providers NetSelected)
        {
            switch (NetSelected)
            {
                case Providers.DES3:
                    mobjCryptoService = SymmetricAlgorithm.Create("3DES");
                    break;
                case Providers.DES:
                    mobjCryptoService = SymmetricAlgorithm.Create("DES");
                    break;
                case Providers.RC2:
                    mobjCryptoService = SymmetricAlgorithm.Create("RC2");
                    break;
                case Providers.Rijndael:
                    mobjCryptoService = SymmetricAlgorithm.Create("Rijndael");
                    break;
            }

            mobjCryptoService.KeySize = mobjCryptoService.LegalKeySizes[0].MaxSize;
            mobjCryptoService.BlockSize = mobjCryptoService.LegalBlockSizes[0].MaxSize;

        }

        public void Dispose()
        {
            mobjCryptoService = null;
            GC.SuppressFinalize(this);
        }


        public int BlockSize
        {
            get { return mobjCryptoService.BlockSize; }
        }

        public int KeySize
        {
            get { return mobjCryptoService.KeySize; }
        }

        private string InitVector
        {
            get { return mstrInitVector; }
        }

        private string PrivateKey
        {
            get { return mstrPrivKey; }
        }

        private byte[] GetIV
        {
            get
            {
                //Convert Bits to Bytes
                int thisSize = (this.BlockSize / 8) - 1;
                byte[] thisIV = new byte[thisSize + 1];
                if (this.InitVector.Length < 1)
                {
                    return thisIV;
                }

                int temp = 0;
                int lastBound = this.InitVector.Length;
                if (lastBound > thisSize)
                    lastBound = thisSize;
                for (temp = 0; temp <= lastBound - 1; temp++)
                {
                    thisIV[temp] = Convert.ToByte(InitVector[temp]);
                }
                return thisIV;
            }
        }

        private byte[] GetKey
        {
            get
            {
                int thisSize = (this.KeySize / 8) - 1;
                int temp = 0;
                byte[] thisKey = new byte[thisSize + 1];
                if (this.PrivateKey.Length < 1)
                {
                    return thisKey;
                }
                int lastBound = this.PrivateKey.Length;
                if (lastBound > thisSize)
                    lastBound = thisSize;
                for (temp = 0; temp <= lastBound - 1; temp++)
                {
                    thisKey[temp] = Convert.ToByte(PrivateKey[temp]);
                }
                return thisKey;
            }
        }

        #endregion

        #region "Main Methods of the class"

        public string Encrypt(string vstrSource)
        {
            ICryptoTransform encrypto = null;
            byte[] lbytIn = null;
            byte[] lbytOut = null;
            CryptoStream cs = null;
            MemoryStream ms = new MemoryStream();

            try
            {
                lbytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(vstrSource.ToCharArray());
                //create an Encryptor from the Provider Service instance
                encrypto = mobjCryptoService.CreateEncryptor(this.GetKey, this.GetIV);

                //create Crypto Stream that transforms a stream using the encryption
                cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                //write out encrypted content into MemoryStream
                cs.Write(lbytIn, 0, lbytIn.Length);
                cs.FlushFinalBlock();
                cs.Close();
                lbytOut = ms.ToArray();
                ms.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                encrypto.Dispose();
                encrypto = null;
                cs = null;
            }

            return Convert.ToBase64String(lbytOut);
            //convert into Base64 so that the result can be used in xml
        }

        public string Decrypt(string vstrSource)
        {
            string lstrOriginal = null;
            byte[] bytIn = null;
            MemoryStream ms = null;
            byte[] bytTemp = null;
            ICryptoTransform encrypto = null;
            CryptoStream cs = null;
            StreamReader mStrRead = null;

            try
            {
                //convert from Base64 to binary
                bytIn = System.Convert.FromBase64String(vstrSource);
                // resize array to hold values
                bytTemp = new byte[bytIn.Length + 1];
                // create memory stream
                ms = new MemoryStream(bytIn);

                //create a Decryptor from the Provider Service instance
                encrypto = mobjCryptoService.CreateDecryptor(this.GetKey, this.GetIV);

                //create Crypto Stream that transforms a stream using the decryption
                cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                mStrRead = new StreamReader(cs);

                lstrOriginal = mStrRead.ReadToEnd();

                cs.Close();
                mStrRead.Close();
                ms.Close();
                mobjCryptoService.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                encrypto.Dispose();
                mStrRead = null;
                encrypto = null;
                cs = null;
            }

            return lstrOriginal;
        }

        #endregion

    }

}