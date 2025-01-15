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

      //searching through other hand and adding it to the current users hand if it is found
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
      //reassign user hand to filtered hand

      //If card is not found, draw from deck
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