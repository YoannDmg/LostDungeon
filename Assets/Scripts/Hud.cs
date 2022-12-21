using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private GameManager gm;


    /**** HUD ***/
    public Text deckSizeText;
    public Text discardPileText;
    public Text currentPlayerHealthText;
    public Text maxPlayerHealthText;
    public Text currentEnemyHealthText;
    public Text maxEnemyHealthText;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("HUD Update");

        /*** Deck HUD ***/
        deckSizeText.text = gm.player.deck.Count.ToString();
        discardPileText.text = gm.player.discardPile.Count.ToString();

        /*** Player HUD ***/
        currentPlayerHealthText.text = gm.player.stat.Health.ToString();
        maxPlayerHealthText.text = gm.player.stat.MaxHealth.ToString();

        /*** Enemy HUD ***/
        //currentEnemyHealthText.text = gm.currentEnemy.stat.Health.ToString();
        //maxEnemyHealthText.text = gm.currentEnemy.stat.MaxHealth.ToString();

    }
}
