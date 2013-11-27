using System.IO;

namespace FileEncrypt
{
    public class SaltStore
    {
		private readonly string _saltFileName;
		
		public SaltStore(string saltFileName)
		{
			_saltFileName = saltFileName;
		}
		
        public void Save(byte[] salt)
        {
            File.WriteAllBytes(_saltFileName, salt);
        }

        public byte[] Get()
        {
            return File.ReadAllBytes(_saltFileName);
        }

        public bool SaltCreated()
        {
            return File.Exists(_saltFileName);
        }

        public byte[] CreateAndGet()
        {
            if (!SaltCreated())
            {
                Save(SaltCreator.Create());
            }

            return Get();
        }
    }
}
