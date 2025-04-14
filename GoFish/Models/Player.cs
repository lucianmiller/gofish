using System.Collections.Generic;


namespace GoFish.Models
{
  public class Player
  {
    public List<Card> Hand { get; set; } = new List<Card> ();
    public int BooksCount { get; set; } = 0;
  }
}