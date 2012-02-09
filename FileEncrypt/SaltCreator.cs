using System;
using System.Security.Cryptography;

namespace FileEncrypt
{
	public class SaltCreator
	{	
		private const int saltStrength = 128;
		
		public static byte[] Create()
        {
            byte[] temp = new byte[saltStrength];
            RNGCryptoServiceProvider.Create().GetBytes(temp);
            return temp;
        }
	}
}

