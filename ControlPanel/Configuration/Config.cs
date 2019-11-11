using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPanel.Configuration
{
    //facade class for all config classes
    public class Config
    {
        public EncryptionSettings Encryption { get; set; }
        public DMSSettings DMS { get; set; }
    }
}
