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
      if (gameObj.ComputerTurn == false)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return RedirectToAction("ComputerThinking");
      }
    }

    [HttpGet("/games/computer-thinking")]
    public ActionResult ComputerThinking()
    {
      Game gameObj = Game.GetInstance();
      Random randomObj = new Random();
      int randomCardIndex = randomObj.Next(0, gameObj.ComputerObject.Hand.Count);
      Card randomCard = gameObj.ComputerObject.Hand[randomCardIndex];
      gameObj.AskCard(randomCard);
      ViewBag.CardSuit = randomCard.Suit;
      Console.WriteLine("\x1b[37;46mCOMPUTERS TURN\x1b[0m");
      Console.WriteLine($"\x1b[37;40mCard: {randomCard.Suit}\x1b[0m");
      return View();
    }

    [HttpGet("/games/turn-summary")]
    public ActionResult TurnSummary(string cardSuit)
    {
      ViewBag.CardSuit = cardSuit;
      return View();
    }

    [HttpPost("/games/reset")]
    public ActionResult Reset()
    {
      Game.Reset();
      return RedirectToAction("Index");
    }
  }
}