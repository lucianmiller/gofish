using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Game
  {
    private static Game _instance;
    public Deck NewDeck { get; set; } = new Deck();
    public Player PlayerObject { get; set; } = new Player();

    public static Game GetInstance()
    {
      if (_instance == null)
      {
        _instance = new Game();
        _instance.DrawInitialHand();
      }
      return _instance;
    }

    public void DrawInitialHand()
    {
      PlayerObject.PlayerHand = NewDeck.DrawCards(5);
      PlayerObject.ComputerHand = NewDeck.DrawCards(5);
    }

    public void AskCard(Card selectedCard)
    {
      bool valueMatched = false;
      List<Card> newComputerHand = new List<Card> {};
      foreach (Card computersCard in PlayerObject.ComputerHand)
      {
        if (selectedCard.Value == computersCard.Value)
        {
          valueMatched = true;
          PlayerObject.PlayerHand.Add(computersCard);
        }
        else
        {
          newComputerHand.Add(computersCard);
        }
      }
      PlayerObject.ComputerHand = newComputerHand;
      if (valueMatched == false)
      {
        List<Card> drawnCard = NewDeck.DrawCards(1);
        PlayerObject.PlayerHand.Add(drawnCard[0]);
      }
    }
  }
}