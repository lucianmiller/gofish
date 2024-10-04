using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Deck
  {
    public List<Card> DeckOfCards { get; set; } = new List<Card> ();

    public void CreateDeck()
    {
      List<string> suits = new List<string> {"Hearts", "Clubs", "Diamonds", "Spades"};
      List<string> values = new List<string> {"Ace", "2", "3", "4"};
      foreach (string suit in suits)
      {
        foreach (string value in values)
        {
          Card newCard = new Card(suit, value);
          DeckOfCards.Add(newCard);
        }
      }
      Console.WriteLine(DeckOfCards.Count);
    }
  }
}