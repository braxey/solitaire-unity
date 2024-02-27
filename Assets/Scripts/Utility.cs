using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
        public static readonly Dictionary<string, string> SuitMap = new Dictionary<string, string>
        {
            { "h", "heart" }, { "d", "diamond" }, { "c", "club" }, { "s", "spade" }
        };
        public static readonly string CardBackSpriteName = "card_back";
        public static readonly int[] Ranks = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

        public static string GetSpriteNameFromCardId(string cardId)
        {
                string suit = cardId[0..1];
                string rank = cardId[1..];

                return rank + "_" + SuitMap[suit];
        }
}