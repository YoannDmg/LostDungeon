using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    
    //Indique si la carte a ete joue
    public bool hasBeenPlayed;

    //Position de la carte dans la main
    public int handIndex;

    public int test;

    public CardEffect effect;

    public Text energyCostText;


    private GameManager gm;

    private void OnMouseDown()
    {
        //Debug.Log(handIndex);
        if (hasBeenPlayed == false && gm.isPlayerTurn == true)
        {
            UseCard();
        }
    }

    public void UseCard()
    {
        //Debug.Log(handIndex);
        if (hasBeenPlayed == false)
        {
            StartCoroutine(PlayCard());
        }
    }

    private void PlayEffect()
    {
        //Deal Damage
        gm.target.TakeDamage(effect.Damage);
        //GiveHeal
        gm.currentTurn.Heal(effect.Heal);
        //Use Energy
        gm.currentTurn.stat.Energy -= effect.EnergyCost;
        //Give Energy
        gm.currentTurn.stat.Energy += effect.EnergyGain;

    }

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gameObject.SetActive(false);
    }


    private void PlayAnimationCard()
    {
        transform.position += Vector3.up * 1;


        effect.Effect.transform.position = (gm.target.transform.position + Vector3.back * 1);
    }

    IEnumerator PlayCard()
    {
        if (gm.target)
        {

            PlayAnimationCard();
            PlayEffect();
            yield return new WaitForSeconds(1f);
            hasBeenPlayed = true;
        }
        else 
        {
            print("Pas d'enemi cible");
        }
    }


    // Update is called once per frame
    private void Update()
    {
        //Update HUD card
        energyCostText.text = effect.EnergyCost.ToString();
    }
}
