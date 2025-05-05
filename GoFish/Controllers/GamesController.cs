using Microsoft.AspNetCore.Mvc;
using GoFish.Models;
using System;
using System.Collections.Generic;

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
      bool drawnCardResult = gameObj.AskCard(tempCard);
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
        return RedirectToAction("ComputerThinking", new {playerAskedCardValue = tempCard.Value, playerDrawnCardResult = drawnCardResult});
      }
    }

    [HttpGet("/games/computer-thinking")]
    public ActionResult ComputerThinking(string playerAskedCardValue, bool playerDrawnCardResult)
    {
      Game gameObj = Game.GetInstance();
      if (gameObj.ComputerObject.Hand.Count == 0)
      {
        List<Card> drawnCard = gameObj.NewDeck.DrawCards(1);
        gameObj.ComputerObject.Hand.Add(drawnCard[0]);
      }
      Random randomObj = new Random();
      int randomCardIndex = randomObj.Next(0, gameObj.ComputerObject.Hand.Count);
      Card randomCard = gameObj.ComputerObject.Hand[randomCardIndex];
      ViewBag.ComputerDrawnCardResult = gameObj.AskCard(randomCard);
      ViewBag.PlayerDrawnCardResult = playerDrawnCardResult;
      ViewBag.ComputerAskedCardValue = randomCard.Value;
      ViewBag.PlayerAskedCardValue = playerAskedCardValue;
      return View();
    }

    [HttpGet("/games/turn-summary")]
    public ActionResult TurnSummary(string computerCardValue, string playerCardValue, bool computerDrawnCard, bool playerDrawnCard)
    {
      ViewBag.ComputerDrawnCardResult = computerDrawnCard;
      ViewBag.PlayerDrawnCardResult = playerDrawnCard;
      ViewBag.ComputerAskedCardValue = computerCardValue;
      ViewBag.PlayerAskedCardValue = playerCardValue;
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