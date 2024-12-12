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
      Console.WriteLine(cardValue);
      Console.WriteLine(cardSuit);
      return RedirectToAction("Index");
    }
  }
}