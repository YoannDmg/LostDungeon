using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<Card> cardForThisEnemy;

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
        currentEnergy = stat.MaxEnergy;
        Draw();
        handPile[0].UseCard();

    }


    protected override void MoveToDiscardPile(Card card)
    {
        card.gameObject.SetActive(false);
        discardPile.Add(card);
        handPile.Remove(card);
    }

    public void setCards()
    {
        //Configure les cartes de l enemi
        SpriteRenderer sprender;
        Canvas canvas;
        foreach (Card card in cardForThisEnemy)
        {
            deck.Add(Instantiate(card));
        }
        //On rend les carte non visible
        foreach (Card card in deck)
        {
            sprender = card.gameObject.GetComponent<SpriteRenderer>();
            sprender.enabled = false;

            canvas = card.transform.Find("Canvas").gameObject.GetComponent<Canvas>();
            canvas.enabled = false;
        }
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
