using System.Collections.Generic;
using asp.net_mvc_event_manager.Models;

namespace asp.net_mvc_event_manager.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}