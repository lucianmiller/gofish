using System.Collections.Generic;

namespace GoFish.Models
{
  public class Game
  {
    public Deck newDeck { get; set; } = new Deck();
    public Player playerObject { get; set; } = new Player();

    public Game()
    {
      playerObject.Player1Hand = newDeck.DrawCards(5);
      playerObject.ComputerHand = newDeck.DrawCards(5);
    }
  }
}