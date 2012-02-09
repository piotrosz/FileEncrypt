using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

namespace FileEncrypt
{
    public class SaltStore
    {
		private string saltFileName;
		
		public SaltStore(string saltFileName)
		{
			this.saltFileName = saltFileName;
		}
		
        public void Save(byte[] salt)
        {
            File.WriteAllBytes(saltFileName, salt);
        }

        public byte[] Get()
        {
            return File.ReadAllBytes(saltFileName);
        }

        public bool SaltCreated()
        {
            return File.Exists(saltFileName);
        }
    }
}
