using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Utility
{
        public static readonly Dictionary<string, string> SuitMap = new Dictionary<string, string>
        {
            { "h", "heart" }, { "d", "diamond" }, { "c", "club" }, { "s", "spade" }
        };
        public static readonly string CardBackSpriteName = "card_back";
        public static readonly int[] Ranks = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

        public static void Shuffle<T>(this IList<T> list)
        {
                System.Random rng = new System.Random();
                int n = list.Count;
                while (n > 1) {
                        n--;
                        int k = rng.Next(n + 1);
                        T value = list[k];
                        list[k] = list[n];
                        list[n] = value;
                }
        }

        public static string GetSpriteNameFromCardId(string cardId)
        {
                string suit = cardId[0..1];
                string rank = cardId[1..];

                return rank + "_" + SuitMap[suit];
        }

        // card container getters

        public static List<CardContainer> GetSortedBoardContainers()
        {
                List<CardContainer> result = new List<CardContainer>();
                result.Add(GameObject.Find("FirstBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("SecondBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("ThirdBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("FourthBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("FifthBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("SixthBoardHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("SeventhBoardHolder").GetComponent<CardContainer>());
                return result;
        }

        public static List<CardContainer> GetSortedCollectionContainers()
        {
                List<CardContainer> result = new List<CardContainer>();
                result.Add(GameObject.Find("FirstCollectedHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("SecondCollectedHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("ThirdCollectedHolder").GetComponent<CardContainer>());
                result.Add(GameObject.Find("FourthCollectedHolder").GetComponent<CardContainer>());
                return result;
        }

        public static CardContainer GetDeckContainer()
        {
                return GameObject.Find("DeckHolder").GetComponent<CardContainer>();
        }

        public static CardContainer GetShownDeckContainer()
        {
                return GameObject.Find("ShownDeckHolder").GetComponent<CardContainer>();
        }

        public static List<CardContainer> GetAllCardContainers()
        {
                List<CardContainer> result = Utility.GetSortedBoardContainers();
                result.Add(Utility.GetDeckContainer());
                result.AddRange(Utility.GetSortedCollectionContainers());
                result.Add(Utility.GetShownDeckContainer());
                return result;
        }

        public static Card GetCardFromId(GameObject prefab)
        {
                string id = prefab.name;
                List<CardContainer> containers = Utility.GetAllCardContainers();
                foreach (CardContainer container in containers) {
                        if (container.Contains(id)) {
                                return container.GetCardById(id);
                        }
                }
                return null;
        }
}