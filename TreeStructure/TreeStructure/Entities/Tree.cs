using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeStructure.Entities
{
    public class Tree
    {
        public int TreeId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
