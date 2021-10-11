using Microsoft.AspNetCore.Mvc;
using TimeItUpData.Library.DataAccess;

namespace TimeItUpAPI.Controllers
{
    public class PausesController : Controller
    {
        private readonly EFDbContext _context;

        public PausesController(EFDbContext context)
        {
            _context = context;
        }

        //GET: GetAllPause
        //GET: GetPauseById
        //GET: GetAllPausesByTimerId

        //GET: GetAllActivePauses
        //GET: GetAllPastPauses
        //GET: GetAllActivePausesByTimerId
        //GET: GetAllPastPausesByTimerId

        //POST: AddNewPause

        //PUT: EditPause
        //PUT: FinishPause

        //DELETE: RemovePause
    }
}
