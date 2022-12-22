using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Tooltip("Arbitary text message")]
public class GameManager : MonoBehaviour
{
    //Variable public:
    [Header("Le joueur")]
    public Player player;

    [Header("Position du joueur")]
    public PositionSlot playerPosition;

    [Header("Liste de tout les ennemis disponible")]
    public List<Enemy> enemyList;

    [Header("Liste des positions des slots des enemies")]
    public List<PositionSlot> enemySlots;

    [Header("Liste de toutes les cartes de faible rarite")]
    public List<Card> lowRarityDeck;

    [Header("Liste des positions des slots de cartes du joueur")]
    public List<PositionSlot> cardSlots;

    [Header("Tour en cours")]
    [HideInInspector]
    public Character currentTurn;

    [Header("Character cible")]
    [HideInInspector]
    public Character target;
    
    [HideInInspector]
    public bool isPlayerTurn;

    //Variable prive:
    [Header("Liste des ennemis dans le combat")]
    private List<Enemy> enemyInBattle = new List<Enemy>();

    private bool firstTurn = true; 

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if (isPlayerTurn)
        {
            currentTurn = player;
            target = enemyInBattle[0];

            player.StartTurn();

            //Set le premier enemy vivant en target
            foreach (Enemy enemy in enemyInBattle)
            {
                if (enemy.isDead == false)
                {
                    target = enemy;
                    break;
                }
            }
            //Distribution des carte au joeur en debut de tour
            for (int i = 0; i < player.stat.StartCardInHand; i++)
            {
                player.Draw();
            }
        }
        else
        {
            player.MoveHandToDiscardPile();
            target = player;
            StartCoroutine(PlayEnemyTurn());
        }
    }

    IEnumerator PlayEnemyTurn()
    {
        if (target)
        {
            // les enemis joue leurs cartes
            foreach (Enemy enemy in enemyInBattle)
            {
                if (enemy.isDead == false)
                {
                    currentTurn = enemy;
                    enemy.PlayTurn();
                    yield return new WaitForSeconds(1f);
                }

            }
            ChangeTurn();
        }
    }


    private void SetCharacter()
    {
        player.transform.position = playerPosition.transform.position;

        //Set les enemis pour le combat
        int nombreEnemy = Random.Range(1, enemySlots.Count);
        //nombreEnemy = 4;
        while (nombreEnemy > 0)
        {
            enemyInBattle.Add(Instantiate(enemyList[Random.Range(0, enemyList.Count)]));
            nombreEnemy--;
        }

        //Set les enemis sur une position
        if (enemyInBattle.Count >= 1)
        {
            foreach (Enemy enemy in enemyInBattle)
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

        //Configure les cartes du joueur
        foreach (Card card in lowRarityDeck)
        {
            player.deck.Add(Instantiate(card));
        }

        //Configure les cartes ennemi
        foreach (Enemy enemy in enemyInBattle)
        {
            enemy.setCards();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //Configure les entite du combat
        SetCharacter(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTurn == true && player)
        {
            firstTurn = false;
            isPlayerTurn = false;
            ChangeTurn();
        }

    }
}
