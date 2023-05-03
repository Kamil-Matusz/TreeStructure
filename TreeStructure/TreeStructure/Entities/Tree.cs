using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeStructure.Entities
{
    public class Tree
    {
        public int TreeId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Tree()
        {
            
        }
        public Tree(int treeId,string name,int? parentId)
        {
            TreeId = treeId;
            Name = name;
            ParentId = parentId;
        }

        public Tree(string name,int? parentId) 
        {
            Name = name;
            ParentId = parentId;
        }

        
    }
}
