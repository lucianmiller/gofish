using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Computer
  {
    public List<Card> Hand { get; set; } = new List<Card> ();
    public int BooksCount { get; set; } = 0;
  }
}