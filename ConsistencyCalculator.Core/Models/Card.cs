using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Entities
{
    public class Card
    {
        public Card() { }
        public String Name { get; set; } = "Filler";
        public HashSet<String> Tags { get; set; } = new();
    }
}
