using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector3 openCardPosition = new(0, 0, 0);
    Vector3 deckPosition = new(-7, 0, 0);
    readonly string[] specialAndWildCards = {"SKI", "REV", "PL2", "PL4", "COL"};
    public GameObject card;
    [SerializeField] GameObject deck;
    public GameObject deckClone, openCardClone;
    Player player1;
    public Player activePlayer;
    AI aiPlayer;
    public Turn turn;
    public bool winner = false;
    public string winnerName;

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
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            if (turn.turnNumber == 0) {
                SetOpenCard();
                InitializePlayersHand();
                turn.turnNumber = 1;
            } else if (turn.turnNumber >= 1) {
                turn.PlayTurn(activePlayer, aiPlayer, openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card);
            }
        }
    }

    private void SetOpenCard() {
        Card firstCard = deckClone.GetComponent<Deck>().DrawFromDeck();
        while (specialAndWildCards.Contains(firstCard.value)) {
            firstCard = deckClone.GetComponent<Deck>().DrawFromDeck(); 
        }
        openCardClone.GetComponent<Card>().SetValues(firstCard.color, firstCard.value);
        openCardClone.GetComponent<Card>().SetSprite(openCardClone);
        openCardClone.GetComponent<Card>().playedBy = "AI"; // TODO: change to not be hardcoded
        openCardClone.GetComponent<SpriteRenderer>().sortingOrder = 1;
        deckClone.GetComponent<Deck>().DiscardCard(firstCard);
    }

    private void InitializePlayersHand() {
        player1.Draw(openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card, 7);
        for(int i=0; i<7; i++) {
            aiPlayer.Draw(openCardClone.GetComponent<Card>(), deckClone.GetComponent<Deck>(), card, turn.turnNumber);
        }
    }
}
