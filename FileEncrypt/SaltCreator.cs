using System.Security.Cryptography;

namespace FileEncrypt
{
	public class SaltCreator
	{	
		private const int SaltStrength = 128;
		
		public static byte[] Create()
        {
            var temp = new byte[SaltStrength];
            RandomNumberGenerator.Create().GetBytes(temp);
            return temp;
        }
	}
}

