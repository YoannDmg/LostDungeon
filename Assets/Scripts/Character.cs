using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> handPile = new List<Card>();

    public Stat stat;

    public bool isPlayerTurn = false;
    protected bool isDead = false;
    private GameManager gm;


    public void StartTurn()
    {

    }


    /************ Gestion du deck ****************/

    public void MoveHandToDiscardPile()
    {
        if (handPile.Count >= 1)
        {
            foreach (PositionSlot slot in gm.cardSlots)
            {
                slot.available = true;
            }

            foreach (Card card in handPile)
            {
                card.gameObject.SetActive(false);
                discardPile.Add(card);
            }
            handPile.Clear();
        }
    }

    public void Shuffle()
    {
        if (discardPile.Count >= 1)
        {
            foreach(Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }

    public void Draw()
    {
        if (deck.Count <= 0)
        {
            Shuffle();
        }
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            for (int i = 0; i < gm.cardSlots.Count ; i++)
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

    public void Die()
    {
        isDead = true;
        print(gameObject.name + " est mort");
    }

    private void SetHealthTo(int hp)
    {
        stat.Health = hp;
        if (stat.Health == 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = stat.Health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = stat.Health + heal;
        SetHealthTo(healthAfterHeal);
    }


    public void MoveToDiscardPile(Card card)
    {
        //Debug.Log("MoveToDiscardPile");
        gm.cardSlots[card.handIndex].available = true;
        card.gameObject.SetActive(false);
        discardPile.Add(card);
        handPile.Remove(card);
    }

    public void PlayCard()
    {
        if (handPile.Count >= 1)
        {
            foreach (Card card in handPile)
            {
                if (card.hasBeenPlayed)
                {



                    card.UseCard();
                    card.hasBeenPlayed = false;
                    MoveToDiscardPile(card);

                    return;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.Count <= 0)
        {
            Shuffle();
        }
        //Joue une carte si elle a ete marque
        //PlayCard();

    }

    private void OnMouseDown()
    {
        //set the target
        gm.target = this;
        Debug.Log("target = " + gm.target.gameObject.name);
    }


}
