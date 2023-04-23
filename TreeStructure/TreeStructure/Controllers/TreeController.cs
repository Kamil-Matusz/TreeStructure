using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using TreeStructure.DAL;
using TreeStructure.Entities;
using TreeStructure.Models;

namespace TreeStructure.Controllers
{
    public class TreeController : Controller
    {
        private readonly TreeDbContext _dbContext;
        public TreeController(TreeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult Index()
        {
            var TreeList = _dbContext.Trees.Where(x => x.ParentId == null)
                .Select(x => new
                {
                    x.TreeId,
                    x.Name
                }).ToList();
                ViewBag.TreeList = TreeList;
            TreeHierarchy();
            return View();
        }

        public JsonResult TreeHierarchy()
        {
            List<Tree> Tree = new();
            List<TreeViewModel> TreeRecords = new();

            Tree = _dbContext.Trees.ToList();

            TreeRecords = Tree.Where(x => x.ParentId is null)
                .Select(t => new TreeViewModel
                {
                    Id = t.TreeId,
                    Text = t.Name,
                    ParentId = t.ParentId,
                    Childrens = GetTreeChildrens(Tree, t.TreeId)
                }).ToList();

            return Json(TreeRecords);
        }

        private List<TreeViewModel> GetTreeChildrens(List<Tree> TreeList, int parentId)
        {
            return TreeList.Where(x => x.ParentId == parentId)
                .Select(t => new TreeViewModel
                {
                    Id = t.TreeId,
                    Text = t.Name,
                    ParentId = parentId,
                    Childrens = GetTreeChildrens(TreeList,t.TreeId)
                }).ToList();
        }

        [HttpPost]
        public JsonResult DeleteTreeNode(string values)
        {
            var id = values.Split(',');
            foreach(var item in id)
            {
                int TreeId = int.Parse(item);
                _dbContext.Trees.RemoveRange(_dbContext.Trees.Where(x => x.TreeId == TreeId).ToList());
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ChangeNode(int nodeId,int parentId)
        {
            var Node = _dbContext.Trees.First(x => x.TreeId==nodeId);
            Node.ParentId = parentId;
            _dbContext.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNode(TreeNode node)
        {
            if(ModelState.IsValid)
            {
                Tree tree = new Tree()
                {
                    Name = node.NodeName,
                    ParentId = node.ParentId,
                };

                _dbContext.Trees.Add(tree);
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}
