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
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteTreeNode()
        {
            DropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTreeNode(int treeId)
        {
            var tree = _dbContext.Trees.Find(treeId);
            
            if(tree != null)
            {
                _dbContext.Trees.Remove(tree);
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ChangeNodePanel()
        {
            List<Tree> treeList = new();
            treeList = _dbContext.Trees.ToList();
            return View(treeList);
        }

        [HttpGet]
        [Route("Tree/{treeName}/Edit")]
        public IActionResult EditTreeNode(string name)
        {
            var tree = _dbContext.Trees.FirstOrDefault(t => t.Name == name);
            return View(tree);
        }

        [HttpPost]
        [Route("Tree/{treeName}/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTreeNode(Tree tree)
        {
            if(ModelState.IsValid)
            {
                var entry = _dbContext.Entry(tree);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tree);
        }

        private void DropDownList()
        {
            List<SelectListItem> nodes = new SelectList(_dbContext.Trees,"TreeId","Name").ToList();
            ViewBag.Nodes = nodes;
        }
    }
}
