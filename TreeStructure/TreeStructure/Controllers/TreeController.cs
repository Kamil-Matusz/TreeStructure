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
            List<Tree> treeList = new();
            treeList = _dbContext.Trees.OrderBy(x => x.TreeId).ToList();
            return View(treeList);
        }
    }
}
