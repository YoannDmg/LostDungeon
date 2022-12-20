using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Tooltip("Arbitary text message")]
public class GameManager : MonoBehaviour
{
    //Variable public:
    [Header("Le joueur")]
    public Character player = new Character();

    [Header("Liste de tout les ennemis disponible")]
    public List<Character> enemyList = new List<Character>();
    
    [Header("Liste de toutes les cartes de faible rarite")]
    public List<Card> lowRarityDeck = new List<Card>();

    [Header("Liste des positions des slots de cartes du joueur")]
    public List<PositionSlot> cardSlots = new List<PositionSlot>();

    [Header("Liste des positions des slots des enemies")]
    public List<PositionSlot> enemySlots = new List<PositionSlot>();

    [Header("Ennemi en cours")]
    public Character currentEnemy = new Character();

    //Variable private:
    [Header("Liste des ennemis dans le combat")]
    private List<Character> enemyInBattle = new List<Character>();

    [Header("Character cible")]
    public Character target = new Character();
    //private bool isPlayerTurn = true;

    public void ChangeTurn()
    {
        player.isPlayerTurn = !player.isPlayerTurn;

        if (player.isPlayerTurn)
        {
            target = currentEnemy;
            for (int i = 0; i < player.stat.StartCardInHand; i++)
            {
                player.Draw();
            }
        }
        else
        { 
            
            player.MoveHandToDiscardPile();
            target = player;

            ChangeTurn(); 
        }

    }

    private void SetEnemy()
    {
        //Set les enemis pour le combat
        int nombreEnemy = Random.Range(1, enemySlots.Count);
        while (nombreEnemy >= 0)
        {
            enemyInBattle.Add(Instantiate(enemyList[Random.Range(0, enemyList.Count)]));
            nombreEnemy--;
        }

        //Set les enemi sur une position
        if (enemyInBattle.Count >= 1)
        {
            foreach (Character enemy in enemyInBattle)
            {
                for (int i = 0; i < enemySlots.Count; i++)
                {
                    if (enemySlots[i].available == true)
                    {
                        enemy.gameObject.SetActive(true);
                        enemy.transform.position = enemySlots[i].transform.position;
                        enemySlots[i].available = false;
                        break;
                    }   
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        //Configure un enemi a combattre
        currentEnemy = enemyList[Random.Range(0, enemyList.Count)];

        //Set l'enemi cible
        //target = currentEnemy;

        //Configure les enemy pour le combat
        SetEnemy();


        //Add card to player
        foreach (Card card in lowRarityDeck)
        {
            player.deck.Add(Instantiate(card));
        }

        //Add card to Enemy -> les cartes seront set dans le prefab
        foreach (Card card in lowRarityDeck)
        {
            currentEnemy.deck.Add(Instantiate(card));
        }

        // On debute le tour avec le joueur -> gameObject non instancie a cet instant
        //ChangeTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
