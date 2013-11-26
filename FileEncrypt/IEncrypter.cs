using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileEncrypt
{
    interface IEncrypter
    {
        void Encrypt();
        void Decrypt();
    }
}
