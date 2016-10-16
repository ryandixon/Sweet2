using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Sweet.Models;
using System;
using System.Web.UI.WebControls;
using System.Web;
namespace Sweet.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public TicketsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = _dbContext.Tickets.ToList();

            return View(tickets);
        }
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Add(Ticket ticket)
        {
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.Id == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }

        [HttpPost]
        public ActionResult Update(Ticket ticket)
        {
            var ticketInDb = _dbContext.Tickets.SingleOrDefault(v => v.Id == ticket.Id);
            if (ticketInDb == null)
                return HttpNotFound();

            ticketInDb.Title = ticket.Title;
            ticketInDb.Description = ticket.Description;
            ticketInDb.Resolve = ticket.Resolve;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.Id == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }

        [HttpPost]
        public ActionResult DoDelete(string id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.Id == id);

            if (ticket == null)
                return HttpNotFound();

            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}