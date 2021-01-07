using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public void Take(int index=0)
    {
        cards.RemoveAt(index);
    }
}
