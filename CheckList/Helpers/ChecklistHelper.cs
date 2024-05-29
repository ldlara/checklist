using CheckList.Data;
using System.Linq;

namespace CheckList.Helpers
{
    public class ChecklistHelper
    {
        private readonly ApplicationDbContext _context;

        public ChecklistHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ChecklistExists(int id)
        {
            return _context.Checklists.Any(e => e.Id == id);
        }
    }
}
