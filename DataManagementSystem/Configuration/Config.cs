using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementSystem.Configuration
{
    public class Config
    {
        public AuthenticationSettings Authentication { get; set; }
        public EncryptionSettings Encryption { get; set; }
    }
}
