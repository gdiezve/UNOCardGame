using UnityEngine;

public class AI: Player
{
    private readonly Vector3 initialCardPosition = new(0,0,0);
    private readonly string[] colors = {"RED", "BLU", "YEL", "GRE"};
    GameManager gameManager;
    Deck deck;
    Card openCard;

    void Update() {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
            deck = gameManager.deckClone.GetComponent<Deck>();
            openCard = gameManager.openCardClone.GetComponent<Card>(); 
        }
    }

    public new void EvaluateOpenCard(Card openCard, Deck deck, GameObject cardObject, string previousPlayer) {
        if (openCard.playedBy == previousPlayer && !openCard.alreadyEvaluated) {
            if (openCard.value == "PL4") {
                Draw(openCard, deck, cardObject, 4);
                canPlay = false;
                openCard.alreadyEvaluated = true;
            }
            if (openCard.value == "PL2") {
                Draw(openCard, deck, cardObject, 2);
                canPlay = false;
                openCard.alreadyEvaluated = true;
            }
            if (openCard.value == "SKI") {
                canPlay = false;
                openCard.alreadyEvaluated = true;
            }
        }
    }

    public new void Draw(Card openCard, Deck deck, GameObject cardObject, int numberCards) {
        for (int i=0; i<numberCards; i++) {
            Card card = deck.DrawFromDeck();
            GameObject cardClone = Instantiate(cardObject, initialCardPosition, Quaternion.identity);
            cardClone.GetComponent<Card>().SetValues(card.color, card.value);
            hand.Add(cardClone);
            SetSprite(cardClone);
            EvaluateHand(openCard);
            RealigneHand(3);
        }
    }

    public void PlayCard() {   
        if (playableHand.Count > 0) {
            int cardIndex = new System.Random().Next(playableHand.Count-1);
            Card playedCard = playableHand[cardIndex].GetComponent<Card>();
            if (playedCard.value == "PL4") {
                Debug.Log("AI has played PL4");
            } else if (playedCard.value == "PL2") {
                Debug.Log("AI has played PL2");
            }
            deck.DiscardCard(gameManager.openCardClone.GetComponent<Card>());
            openCard.SetValues(playedCard.color, playedCard.value);
            openCard.SetSprite(gameManager.openCardClone);
            openCard.playedBy = "AI";
            openCard.alreadyEvaluated = false;
            foreach (GameObject card in hand) {
                if (playedCard == card.GetComponent<Card>()) {
                    Destroy(card);
                    hand.Remove(card);
                    break;
                }
            }
            EvaluatePlayedCard(playedCard); 
            RealigneHand(3);
            CheckWin();
        }
        canPlay = false;
    }

    private void SetSprite(GameObject cardClone) {
        SpriteRenderer spriteRenderer = cardClone.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("CARDBACK");
        cardClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void EvaluatePlayedCard(Card playedCard) {
        if (playedCard.color == "WILD" && gameManager.winner == false) {
            int colorIndex = new System.Random().Next(colors.Length);
            openCard.SetValues(colors[colorIndex], playedCard.value);
            openCard.SetSprite(gameManager.openCardClone);
        }
    }

    private void CheckWin() {
        if (hand.Count == 0) {
            gameManager.winnerName = "AI";
            gameManager.winner = true;
        }
    }
}