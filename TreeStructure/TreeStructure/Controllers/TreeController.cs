using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<Tree> treeList = new();
            treeList = _dbContext.Trees.OrderBy(x => x.TreeId).ToList();
            return View(treeList);
        }

        [HttpGet]
        public ActionResult CreateTreeNode()
        {
            DropDownList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateTreeNode(Tree tree) 
        {
            _dbContext.Trees.Add(tree);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private void DropDownList()
        {
            List<SelectListItem> nodes = new SelectList(_dbContext.Trees,"TreeId","Name").ToList();
            ViewBag.Nodes = nodes;


        }
    }
}
