using System.Collections.Generic;

namespace GoFish.Models
{
  public class Game
  {
    public Deck newDeck { get; set; } = new Deck();
    public Player playerObject { get; set; } = new Player();

    public Game()
    {
      playerObject.PlayerHand = newDeck.DrawCards(5);
      playerObject.ComputerHand = newDeck.DrawCards(5);
    }

    public void AskCard(Card selectedCard)
    {
      bool valueMatched = false;
      foreach (Card currentCard in playerObject.ComputerHand)
      {
        if (selectedCard.Value == currentCard.Value)
        {
          valueMatched = true;
          playerObject.PlayerHand.Add(currentCard);
          playerObject.ComputerHand.Remove(currentCard);
        }
      }
      if (valueMatched == false)
      {
        List<Card> drawnCard = newDeck.DrawCards(1);
        playerObject.PlayerHand.Add(drawnCard[0]);
      }
    }
  }
}