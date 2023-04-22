using System.ComponentModel.DataAnnotations;

namespace TreeStructure.Entities
{
    public class TreeNode
    {
        [Required]
        public string NodeType { get; set; }
        [Required]
        public string NodeName { get; set; }
        [Required]
        public int? ParentId { get; set; }
    }
}
