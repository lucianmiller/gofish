using System.Collections.Generic;


namespace GoFish.Models
{
  public class Player
  {
    public List<Card> PlayerHand { get; set; } = new List<Card> ();
    public List<Card> ComputerHand { get; set; } = new List<Card> ();
  }
}