using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{

    public bool hasBeenPlayed;
    public int handIndex;

    private GameManager gm;

    private void OnMouseDown()
    {
        //Debug.Log(handIndex);
        if (hasBeenPlayed == false)
        {
            StartCoroutine(PlayCard());
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

    IEnumerator PlayCard()
    {
        if (gm.target)
        {
            transform.position += Vector3.up * 1;
            //Play animation here


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
