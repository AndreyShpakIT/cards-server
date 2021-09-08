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
        // GET: api/cards
        [HttpGet]
        public IEnumerable<SCard> GetCards()
        {
            return CardsStorage.Cards;
        }

        // GET: api/cards/5
        [HttpGet("{id}")]
        public ActionResult<SCard> GetCard(long id)
        {
            CardsStorage.Load();
            SCard card = FindCard(id);
            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // POST: api/cards
        [HttpPost]
        public ActionResult<SCard> PostCard(SCard card)
        {
            if (card.Id == -1)
            {
                card.Id = CardsStorage.Cards.Count > 0 ? CardsStorage.Cards.OrderBy(c => c.Id).Last().Id + 1 : 0;
            }
            //if (CardExists(card.Id))
            //{
            //    return Conflict();
            //}

            CardsStorage.Cards.Add(card);
            CardsStorage.Save();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // DELETE: api/cards/delete
        [HttpPost("delete")]
        public ActionResult<List<SCard>> DeleteCards(long[] delete)
        {
            List<SCard> deleted = new List<SCard>();
            foreach (int id in delete)
            {
                SCard card = FindCard(id);
                if (card == null)
                {
                    return NotFound();
                }

                CardsStorage.Cards.Remove(card);
                deleted.Add(card);
            }
            CardsStorage.Save();
            return CreatedAtAction(nameof(DeleteCards), deleted);
        }

        // Put: api/cards
        [HttpPut]
        public ActionResult<SCard> PutCard(SCard card)
        {
            if (!CardExists(card.Id))
            {
                return Conflict();
            }

            int i = CardsStorage.Cards.FindIndex(c => c.Id == card.Id);
            SCard oldCard = CardsStorage.Cards.ElementAt(i);

            oldCard.ImageBytes = card.ImageBytes;
            oldCard.Title = card.Title;
            
            //CardsStorage.Cards.Add(card);
            CardsStorage.Save();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // DELETE: api/cards/5
        [HttpDelete("{id}")]
        public ActionResult<SCard> DeleteCard(long id)  
        {
            SCard card = FindCard(id);
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
        private SCard FindCard(long id)
        {
            return CardsStorage.Cards.Find(card => card.Id == id);
        }
    }
}
