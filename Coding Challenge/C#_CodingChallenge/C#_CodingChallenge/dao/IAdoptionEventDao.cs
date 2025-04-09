using C__CodingChallenge.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__CodingChallenge.dao
{
    public interface IAdoptionEventDao
    {
        List<AdoptionEvent> GetUpcomingEvents();
        void RegisterParticipant(string name, string type, int eventId);
    }
}
