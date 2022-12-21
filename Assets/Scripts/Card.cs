using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    
    //Indique si la carte a ete joue
    public bool hasBeenPlayed;

    //Position de la carte dans la main
    public int handIndex;

    public int test;

    public CardEffect effect;

    private GameManager gm;

    private void OnMouseDown()
    {
        //Debug.Log(handIndex);
        if (hasBeenPlayed == false && gm.isPlayerTurn == true)
        {
            PlayCard();
        }
    }

    public void PlayCard()
    {
        //Debug.Log(handIndex);
        if (hasBeenPlayed == false)
        {
            StartCoroutine(PlayAnimCard());
        }
    }

    public void UseCard()
    {
        gm.target.TakeDamage(10);
    }

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("-UpdateCard");
    }

    IEnumerator PlayAnimCard()
    {
        if (gm.target)
        {
            transform.position += Vector3.up * 1;
            //Play animation here
            effect.test.transform.position = gm.target.transform.position;
            //randCard.transform.position = gm.cardSlots[i].transform.position;

            yield return new WaitForSeconds(1f);
            UseCard();
            hasBeenPlayed = true;
        }
        else 
        {
            print("Pas d'enemi cible");
        }
    }

}
