using Microsoft.AspNetCore.Mvc;
using GoFish.Models;
using System;

namespace GoFish.Controllers
{
  public class GamesController : Controller
  {
    [HttpGet("/games")]
    public ActionResult Index()
    {
      Game gameObj = Game.GetInstance();
      return View(gameObj);
    }

    [HttpPost("/games/ask-card")]
    public ActionResult AskCard(string cardValue, string cardSuit)
    {
      Game gameObj = Game.GetInstance();
      Card tempCard = new Card(cardSuit, cardValue);
      gameObj.AskCard(tempCard);
      return RedirectToAction("Index");
    }

    [HttpPost("/games/reset")]
    public ActionResult Reset()
    {
      Game.Reset();
      return RedirectToAction("Index");
    }
  }
}