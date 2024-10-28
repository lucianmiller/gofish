using System.Collections.Generic;


namespace GoFish.Models
{
  public class Player
  {
    public string Player1Name { get; set; }
    public List<Card> Player1Hand { get; set; } = new List<Card> ();
    public List<Card> ComputerHand { get; set; } = new List<Card> ();
  }

  
}