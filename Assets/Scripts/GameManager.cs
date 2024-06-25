using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 openCardPosition = new(0, 0, 0);
    Vector3 deckPosition = new(-7, 0, 0);
    readonly string[] specialAndWildCards = {"SKI", "REV", "PL2", "PL4", "COL"};
    public GameObject card;
    [SerializeField] GameObject deck;
    public GameObject deckClone, openCardClone;
    Card firstCard;
    Player player1;
    public Player activePlayer;
    AI aiPlayer;
    public Turn turn;
    //bool winner = false;

    // Start is called before the first frame update
    void Start()
    {
        deckClone = Instantiate(deck, deckPosition, Quaternion.identity);
        openCardClone = Instantiate(card, openCardPosition, Quaternion.identity);
        player1 = gameObject.AddComponent<Player>();
        player1.SetName("Player1");
        aiPlayer = gameObject.AddComponent<AI>();
        aiPlayer.SetName("AI");
        activePlayer = player1;
        turn = gameObject.AddComponent<Turn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstCard == null) {
            SetOpenCard();
            InitializePlayersHand();
            turn.turnNumber = 1;
        }
        turn.PlayTurn(activePlayer, aiPlayer, openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card);
    }

    private void SetOpenCard() {
        firstCard = deckClone.GetComponent<Deck>().DrawFromDeck();
        while (specialAndWildCards.Contains(firstCard.value)) {
            firstCard = deckClone.GetComponent<Deck>().DrawFromDeck(); 
        }
        openCardClone.GetComponent<Card>().SetValues(firstCard.color, firstCard.value);
        openCardClone.GetComponent<Card>().SetSprite(openCardClone);
        openCardClone.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void InitializePlayersHand() {
        for(int i=0; i<7; i++) {
            player1.Draw(openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card, turn.turnNumber);
            aiPlayer.Draw(openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card, turn.turnNumber);
        }
    }
}
