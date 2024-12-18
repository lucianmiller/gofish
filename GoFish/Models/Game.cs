using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Game
  {
    private static Game _instance;
    public Deck NewDeck { get; set; } = new Deck();
    public Player PlayerObject { get; set; } = new Player();
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
      _instance.ComputerTurn = false;
      _instance.DrawInitialHand();
    }

    public void DrawInitialHand()
    {
      PlayerObject.PlayerHand = NewDeck.DrawCards(5);
      PlayerObject.ComputerHand = NewDeck.DrawCards(5);
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

    public void AskCard(Card selectedCard)
    {
      List<Card> currentUserCards = PlayerObject.PlayerHand;
      List<Card> askedUserHand = PlayerObject.ComputerHand;
      if (ComputerTurn == true)
      {
        currentUserCards = PlayerObject.ComputerHand;
        askedUserHand = PlayerObject.PlayerHand;
      }
      bool valueMatched = false;
      List<Card> newAskedUserHand = new List<Card> {};
      foreach (Card otherUserCard in askedUserHand)
      {
        if (selectedCard.Value == otherUserCard.Value)
        {
          valueMatched = true;
          currentUserCards.Add(otherUserCard);
        }
        else
        {
          newAskedUserHand.Add(otherUserCard);
        }
      }
      askedUserHand = newAskedUserHand;
      if (valueMatched == false)
      {
        List<Card> drawnCard = NewDeck.DrawCards(1);
        currentUserCards.Add(drawnCard[0]);
      }
      SwitchTurn();
    }
  }
}