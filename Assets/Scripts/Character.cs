using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [HideInInspector]
    public List<Card> deck = new List<Card>();
    [HideInInspector]
    public List<Card> discardPile = new List<Card>();
    [HideInInspector]
    public List<Card> handPile = new List<Card>();

    public Stat stat;

    [HideInInspector]
    public int currentEnergy;

    //HUD
    public Text nameText;
    public Text currentCharacterHealthText;
    public Text maxCharacterHealthText;
    public Text strenghtText;
    public Text energyText;

    [HideInInspector]
    public bool isDead = false;

    protected GameManager gm;

    /************Virtual function ****************/
    protected abstract void StartCall();
    protected abstract void UpdateCall();
    protected abstract void MoveToDiscardPile(Card card);


    /************ Gestion des evenements ****************/
    private void OnMouseDown()
    {
        //set the target
        gm.target = this;
        Debug.Log("target = " + gm.target.gameObject.name);
    }



    /************ Gestion du deck ****************/
    public void PlayedCard()
    {
        if (handPile.Count >= 1)
        {
            foreach (Card card in handPile)
            {
                if (card.hasBeenPlayed == true)
                {
                    card.hasBeenPlayed = false;
                    MoveToDiscardPile(card);
                    return;
                }
            }
        }
    }
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
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }


    /************ Gestion des variables ****************/

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


    /************ Start / Update ****************/


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        currentEnergy = stat.MaxEnergy;
        StartCall();
    }


    // Update is called once per frame
    void Update()
    {
        //Met a jour l'affichage des statistiques
        UpdateHud();

        if (deck.Count <= 0)
        {
            Shuffle();
        }

        UpdateCall();
    }

    private void UpdateHud()
    {
        nameText.text = stat.Name;
        currentCharacterHealthText.text = stat.Health.ToString();
        maxCharacterHealthText.text = stat.MaxHealth.ToString();
        strenghtText.text = stat.Strength.ToString();
        energyText.text = currentEnergy.ToString();
    }
}
