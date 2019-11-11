using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//represents a node in the folder structure
namespace ControlPanel.Helpers
{
    public class TreeNode
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType Type { get; set; }
        public string Name { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode(FileSystemInfo fileInfo)
        {
            Name = fileInfo.Name;
            Children = new List<TreeNode>();

            if (fileInfo.Attributes == FileAttributes.Directory)
            {
                Type = NodeType.Folder;
                foreach (FileSystemInfo f in (fileInfo as DirectoryInfo).GetFileSystemInfos())
                {
                    Children.Add(new TreeNode(f));
                }
            }
            else
            {
                Type = NodeType.File;
            }
        }
    }
}
