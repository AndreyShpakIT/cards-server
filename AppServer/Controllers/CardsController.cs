using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AppServer.Models;
using System.Threading.Tasks;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        // GET: api/Cards
        [HttpGet]
        public IEnumerable<Card> GetCards()
        {
            return CardsStorage.Cards;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public ActionResult<Card> GetCard(long id)
        {
            Card card = FindCard(id);
            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // POST: api/Cards
        [HttpPost]
        public ActionResult<Card> PostCard(Card card)
        {
            if (CardExists(card.Id))
            {
                return Conflict();
            }
            CardsStorage.Cards.Add(card);
            CardsStorage.AddCardAsync(card);
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public ActionResult<Card> DeleteCard(long id)
        {
            Card card = FindCard(id);
            if (card == null)
            {
                return NotFound();
            }

            CardsStorage.Cards.Remove(card);
            CardsStorage.Save();

            return card;
        }

        private bool CardExists(long id)
        {
            return CardsStorage.Cards.Any(e => e.Id == id);
        }
        private Card FindCard(long id)
        {
            return CardsStorage.Cards.Find(card => card.Id == id);
        }
    }
}
