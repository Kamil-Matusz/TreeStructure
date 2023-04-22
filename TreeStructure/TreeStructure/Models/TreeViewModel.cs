namespace TreeStructure.Models
{
    public class TreeViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public virtual List<TreeViewModel> Childrens { get; set; }
    }
}
