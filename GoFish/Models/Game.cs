using System.Collections.Generic;

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
      foreach (Card currentCard in PlayerObject.ComputerHand)
      {
        if (selectedCard.Value == currentCard.Value)
        {
          valueMatched = true;
          PlayerObject.PlayerHand.Add(currentCard);
          PlayerObject.ComputerHand.Remove(currentCard);
        }
      }
      if (valueMatched == false)
      {
        List<Card> drawnCard = NewDeck.DrawCards(1);
        PlayerObject.PlayerHand.Add(drawnCard[0]);
      }
    }
  }
}