using System.Collections.Generic;
using System;

namespace GoFish.Models
{
  public class Computer
  {
    public List<Card> Hand { get; set; } = new List<Card> ();
    public int Ranks { get; set; } = 0;
  }
}