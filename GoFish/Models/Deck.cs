using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Deck
  {
    public List<Card> DeckOfCards { get; set; } = new List<Card> ();

    public Deck()
    {
      CreateDeck();
      ShuffleDeck();
    }

    private void CreateDeck()
    {
      List<string> suits = new List<string> {"Hearts", "Clubs", "Diamonds", "Spades"};
      List<string> values = new List<string> {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
      foreach (string suit in suits)
      {
        foreach (string value in values)
        {
          Card newCard = new Card(suit, value);
          DeckOfCards.Add(newCard);
        }
      }
    }

    // Fisher-Yates Shuffle, https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/
    public void ShuffleDeck()
    {
      Random randomObj = new Random();
      for (int index = DeckOfCards.Count - 1; index > 0; --index)
      {
        int randomIndex = randomObj.Next(index + 1);
        Card temp = DeckOfCards[index];
        DeckOfCards[index] = DeckOfCards[randomIndex];
        DeckOfCards[randomIndex] = temp;
      }
      // foreach (Card card in DeckOfCards)
      // {
      //   Console.WriteLine(card.Suit + card.Value);
      // }
    }
  }
}