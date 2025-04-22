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
    public ActionResult AskCard(string cardSuit, string cardValue)
    {
      Game gameObj = Game.GetInstance();
      Card tempCard = new Card(cardSuit, cardValue);
      gameObj.AskCard(tempCard);
      if (gameObj.CheckIfGameOver() == true)
      {
        return RedirectToAction("GameOver");
      }
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
      ViewBag.CardValue = randomCard.Value;
      Console.WriteLine("\x1b[37;46mCOMPUTERS TURN\x1b[0m");
      Console.WriteLine($"\x1b[37;40mComputer Asked Card: {randomCard.Value}\x1b[0m");
      gameObj.PlayerObject.Hand.ForEach(card => Console.WriteLine($"\x1b[95;40mPlayer Hand: {card.Value} of {card.Suit}\x1b[0m"));
      gameObj.ComputerObject.Hand.ForEach(card => Console.WriteLine($"\x1b[36;40mComputer Hand: {card.Value} of {card.Suit}\x1b[0m"));
      return View();
    }

    [HttpGet("/games/turn-summary")]
    public ActionResult TurnSummary(string cardValue)
    {
      ViewBag.CardValue = cardValue;
      return View();
    }

    [HttpPost("/games/reset")]
    public ActionResult Reset()
    {
      Game.Reset();
      return RedirectToAction("Index");
    }

    [HttpGet("/games/game-over")]
    public ActionResult GameOver()
    {
      Game gameObj = Game.GetInstance();
      if (gameObj.PlayerObject.BooksCount > gameObj.ComputerObject.BooksCount)
      {
        ViewBag.Winner = "You Won!";
      }
      else
      {
        ViewBag.Winner = "The Computer Won!";
      }
      return View();
    }
  }
}