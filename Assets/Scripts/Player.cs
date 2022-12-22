using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public void StartTurn()
    {
        currentEnergy = stat.MaxEnergy;
    }

    public void Draw()
    {
        if (gm.isPlayerTurn == true)
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
                        randCard.transform.position = gm.cardSlots[i].transform.position;

                        randCard.hasBeenPlayed = false;
                        randCard.handIndex = i;

                        gm.cardSlots[i].available = false;
                        handPile.Add(randCard);
                        deck.Remove(randCard);
                        return;
                    }
                }
            }
        }

    }


    protected override void MoveToDiscardPile(Card card)
    {
        //Debug.Log("MoveToDiscardPile");
        gm.cardSlots[card.handIndex].available = true;
        card.gameObject.SetActive(false);
        discardPile.Add(card);
        handPile.Remove(card);
    }

    protected override void StartCall()
    {

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
