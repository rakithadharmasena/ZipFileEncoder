using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ControlPanel.Helpers
{
    public enum NodeType
    {
        [EnumMember(Value = "folder")]
        Folder,
        [EnumMember(Value = "file")]
        File
    }
}
