
namespace GoFish.Models
{
  public class Card
  {
    public string Suit { get; set; }
    public string Value { get; set; }

    public Card(string suit, string value)
    {
      Suit = suit;
      Value = value;
    }
  }
}