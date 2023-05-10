using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
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
        
        public ActionResult Index(string sortOrder)
        {
            List<Tree> treeList = new();
            treeList = _dbContext.Trees.OrderBy(x => x.TreeId).ToList();
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                case "Id_desc":
                    treeList = treeList.OrderByDescending(x => x.TreeId).ToList();
                    break;
                case "name_desc":
                    treeList = treeList.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    treeList = treeList.OrderBy(x => x.Name).ToList();
                    break;
            }
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

        [HttpGet]
        [Route("Tree/{treeId}/Delete")]
        public ActionResult DeleteTreeNode(int treeId)
        {
            var tree = _dbContext.Trees.FirstOrDefault(t => t.TreeId == treeId);
            return View(tree);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tree/{treeId}/Delete")]
        public ActionResult DeleteTreeNode(Tree tree)
        {
            _dbContext.Trees.Remove(tree);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ChangeNodePanel(string sortOrder)
        {
            List<Tree> treeList = new();
            treeList = _dbContext.Trees.ToList();
            ViewBag.ParentSort = String.IsNullOrEmpty(sortOrder) ? "parentId_asc" : "";
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch(sortOrder)
            {
                case "parentId_asc":
                    treeList = treeList.OrderBy(x => x.ParentId).ToList();
                    break;
                case "name_desc":
                    treeList = treeList.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    treeList = treeList.OrderBy(x => x.Name).ToList();
                    break;
            }
            return View(treeList);
        }

        [HttpGet]
        [Route("Tree/{treeId}/Edit")]
        public IActionResult EditTreeNode(int treeId)
        {
            var tree = _dbContext.Trees.FirstOrDefault(t => t.TreeId == treeId);
            DropDownList();
            return View(tree);
        }

        [HttpPost]
        [Route("Tree/{treeId}/Edit")]
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
