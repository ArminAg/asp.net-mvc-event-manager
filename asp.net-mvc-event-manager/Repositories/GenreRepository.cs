using asp.net_mvc_event_manager.Models;
using System.Collections.Generic;
using System.Linq;

namespace asp.net_mvc_event_manager.Repositories
{
    public class GenreRepository
    {
        private ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}