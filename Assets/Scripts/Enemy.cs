using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Draw()
    {
        if (deck.Count <= 0)
        {
            Shuffle();
        }
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            for (int i = 0; i < gm.cardSlots.Count; i++)
            {
                if (gm.cardSlots[i].available == true)
                {
                    randCard.gameObject.SetActive(true);

                    randCard.hasBeenPlayed = false;
                    randCard.handIndex = i;

                    handPile.Add(randCard);
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void PlayTurn()
    {
        Draw();
        handPile[0].PlayCard();
    }


    protected override void MoveToDiscardPile(Card card)
    {
        card.gameObject.SetActive(false);
        discardPile.Add(card);
        handPile.Remove(card);
    }


    protected override void UpdateCall()
    {
        if (deck.Count <= 0)
        {
            Shuffle();
        }
        //Deplace les carte joue dans la discardPile
        PlayedCard();
    }

}
