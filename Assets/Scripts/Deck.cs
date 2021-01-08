using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private static Queue<Card> cards;

    public Deck()
    {
        cards = new Queue<Card>();
    }

    public Card Draw()
    {
        return cards.Dequeue();
    }

    public int Count()
    {
        return cards.Count;
    }
}