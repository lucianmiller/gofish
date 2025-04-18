using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Game
  {
    private static Game _instance;
    public Deck NewDeck { get; set; } = new Deck();
    public Player PlayerObject { get; set; } = new Player();
    public Computer ComputerObject { get; set; } = new Computer();
    public bool ComputerTurn { get; set; } = false;

    public static Game GetInstance()
    {
      if (_instance == null)
      {
        _instance = new Game();
        _instance.DrawInitialHand();
      }
      return _instance;
    }

    public static void Reset()
    {
      _instance.NewDeck = new Deck();
      _instance.PlayerObject = new Player();
      _instance.ComputerObject = new Computer();
      _instance.ComputerTurn = false;
      _instance.DrawInitialHand();
    }

    public void DrawInitialHand()
    {
      PlayerObject.Hand = NewDeck.DrawCards(5);
      ComputerObject.Hand = NewDeck.DrawCards(5);
    }

    public void SwitchTurn()
    {
      if (ComputerTurn == false)
      {
        ComputerTurn = true;
      }
      else
      {
        ComputerTurn = false;
      }
    }

    //Based on the current player, checks if selected card is in an opposing players hand
    public void AskCard(Card selectedCard)
    {
      //determine current player and opposing player hand
      List<Card> currentUserCards = PlayerObject.Hand;
      List<Card> askedUserHand = ComputerObject.Hand;
      if (ComputerTurn == true)
      {
        currentUserCards = ComputerObject.Hand;
        askedUserHand = PlayerObject.Hand;
      }
      //draw card if hand is empty
      if (currentUserCards.Count == 0)
      {
        List<Card> drawnCard = NewDeck.DrawCards(1);
        currentUserCards.Add(drawnCard[0]);
      }

      //searching through other hand and adding it to the current users hand if it is found
      bool valueMatched = false;
      List<Card> removeAskedUserCards = new List<Card> {};
      foreach (Card otherUserCard in askedUserHand)
      {
        if (selectedCard.Value == otherUserCard.Value)
        {
          valueMatched = true;
          currentUserCards.Add(otherUserCard);
          removeAskedUserCards.Add(otherUserCard);
        }
        // else
        // {
        //   newAskedUserHand.Add(otherUserCard);
        // }
      }
      //If card is not found, draw from deck

      // askedUserHand = newAskedUserHand;
      if (valueMatched == false)
      {
        List<Card> drawnCard = NewDeck.DrawCards(1);
        currentUserCards.Add(drawnCard[0]);
        CardValueMatch(drawnCard[0]);
      }
      else
      {
        CardValueMatch(selectedCard);
      }
      //reassign user hand to filtered hand
      askedUserHand.RemoveAll(card => removeAskedUserCards.Contains(card));
      removeAskedUserCards.Clear();
      SwitchTurn();
    }

    public void CardValueMatch(Card cardToCheck)
    {
      Console.WriteLine($"\x1b[37;44mChecking Card {cardToCheck.Value}\x1b[0m");
      List<Card> matchedCards = new List<Card> {};
      if (ComputerTurn == false)
      {
        foreach (Card cardInPlayerHand in PlayerObject.Hand)
        {
          Console.WriteLine($"\x1b[37;42mChecking card in Player hand: {cardInPlayerHand.Value}\x1b[0m");
          if (cardToCheck.Value == cardInPlayerHand.Value)
          {
            Console.WriteLine("\x1b[37;41mMatch Detected\x1b[0m");
            matchedCards.Add(cardInPlayerHand);
          }
          if (matchedCards.Count == 4)
          {
            break;
          }
        }
        Console.WriteLine($"\x1b[37;40mPlayer Matched Cards: {matchedCards.Count}\x1b[0m");
        if (matchedCards.Count == 4)
        {
          PlayerObject.Hand.RemoveAll(card => matchedCards.Contains(card));
          matchedCards.Clear();
          // ++ is increment by 1. Same as += 1
          PlayerObject.BooksCount++;
        }
        else
        {
          matchedCards.Clear();
        }
      }
      else
      {
        foreach (Card cardInComputerHand in ComputerObject.Hand)
        {
          if (cardToCheck.Value == cardInComputerHand.Value)
          {
            matchedCards.Add(cardInComputerHand);
          }
          if (matchedCards.Count == 4)
          {
            break;
          }
        }
        if (matchedCards.Count == 4)
        {
          ComputerObject.Hand.RemoveAll(card => matchedCards.Contains(card));
          matchedCards.Clear();
          ComputerObject.BooksCount++;
        }
        else
        {
          matchedCards.Clear();
        }
      }
    }
  }
}