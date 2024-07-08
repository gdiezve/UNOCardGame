using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string playerName;
    private readonly Vector3 initialCardPosition = new(0,0,0);
    public List<GameObject> hand = new();
    public List<GameObject> playableHand = new();
    public bool canPlay = true;
    public int drawnInTurnNumber = -1;
    public bool needToChooseColor = false;
    GameManager gameManager;
    int turnNumber;

    void Update() {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
            turnNumber = gameManager.turn.turnNumber;
        }
    }

    public void EvaluateOpenCard(Card openCard, Deck deck, GameObject cardObject, string previousPlayer) {
        if (openCard.playedBy == previousPlayer) {
            if (openCard.value == "PL4") {
                Debug.Log("Draw PL4");
                Draw(openCard, deck, cardObject, 4);
                canPlay = false;
            }
            if (openCard.value == "PL2") {
                Debug.Log("Draw PL2");
                Draw(openCard, deck, cardObject, 2);
                canPlay = false;
            }
            if (openCard.value == "SKI") {
                Debug.Log("SKIP");
                canPlay = false;
            }
        }
    }

    public void EvaluateHand(Card openCard) {
        playableHand.Clear();
        List<GameObject> plus4 = new();
        bool canPlayColor = false;
        foreach (GameObject card in hand) {
            if (card.GetComponent<Card>().value == "PL4") {
                plus4.Add(card);
            }
            if (card.GetComponent<Card>().EvaluateCard(openCard.color, openCard.value)) {
                if (card.GetComponent<Card>().color == openCard.color) {
                    canPlayColor = true;
                }
                playableHand.Add(card);
            }
        }
        // PL4 can only be played if no other card in hand has the same color as the openCard
        if (!canPlayColor) {
            foreach (GameObject card in plus4) {
                playableHand.Add(card);
                card.GetComponent<Card>().canBePlayed = true;
            }
        }
    }

    public void Draw(Card openCard, Deck deck, GameObject cardObject, int numberCards) {
        for (int i=0; i<numberCards;i++) {
            Card card = deck.DrawFromDeck();
            GameObject cardClone = Instantiate(cardObject, initialCardPosition, Quaternion.identity);
            cardClone.GetComponent<Card>().SetValues(card.color, card.value);
            cardClone.GetComponent<Card>().SetSprite(cardClone);
            hand.Add(cardClone);
            RealigneHand(-3);
            EvaluateHand(openCard);
        }
    }

    public void PlayCard(Card playedCard) {
        foreach (GameObject card in hand) {
            if (playedCard == card.GetComponent<Card>()) {
                Destroy(card);
                hand.Remove(card);
                break;
            }
        }
        RealigneHand(-3);
        EvaluatePlayedCard(playedCard);
        CheckWin();
        canPlay = false;
    }

    public void SetName(string name) {
        playerName = name;
    }

    public void RealigneHand(float desideredYOffset) {
        int cardIndex = 0;
        foreach (GameObject card in hand) {
            card.GetComponent<Card>().CardAlignment(cardIndex, hand.Count, card, desideredYOffset);
            card.GetComponent<SpriteRenderer>().sortingOrder = cardIndex+1;
            cardIndex += 1;
        }
    }

    private void EvaluatePlayedCard(Card playedCard) {
        if (playedCard.color == "WILD") {
            needToChooseColor = true;
        }
    }

    private void CheckWin() {
        if (hand.Count == 0) {
            gameManager.winnerName = playerName;
            gameManager.winner = true;
        }
    }
}