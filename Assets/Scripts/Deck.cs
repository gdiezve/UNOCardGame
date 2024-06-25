using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private readonly string[] colors = {"RED", "BLU", "YEL", "GRE"};
    private readonly string[] specialCards = {"SKI", "REV", "PL2"};
    private readonly string[] wildCards = {"PL4", "COL"};
    private readonly System.Random random = new();
    public List<Card> cards;
    public List<Card> discardedCards; 
    GameManager gameManager;
    Player activePlayer;
    Card openCard;

    void Start() {
        BuildDeck();
        cards = Shuffle(cards);
    }

    void Update() {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
            activePlayer = gameManager.activePlayer.GetComponent<Player>();
            openCard = gameManager.openCardClone.GetComponent<Card>();
        }
    }

    public void DiscardCard(Card card) {
        discardedCards.Add(card);
    }

    public Card DrawFromDeck() {
        if (cards.Count == 0) {
            cards = Shuffle(discardedCards);
            discardedCards.Clear();
        }
        var drawnCard = cards.First();
        cards.Remove(drawnCard);
        return (Card)drawnCard;
    }

    private void BuildDeck() {
        foreach (string color in colors) {
            for (int i = 0; i<2; i++) {
                BuildColorCards(color);
                BuildSpecialCards(color);
            }
        }
        for (int i = 0; i<4; i++) {
            BuildWildCards();
        }
        cards = gameObject.GetComponents<Card>().ToList();
    }

    private List<Card> Shuffle(List<Card> cards) {
        cards = cards.OrderBy(_ => random.Next()).ToList();
        return cards;
    }

    private void BuildColorCards(string color) {
        foreach (int value in Enumerable.Range(0, 10)) {
            Card card = gameObject.AddComponent<Card>();
            card.SetValues(color, value.ToString());
        }
    }

    private void BuildSpecialCards(string color) {
        foreach (string value in specialCards) {
            Card card = gameObject.AddComponent<Card>();
            card.SetValues(color, value);
        }
    }

    private void BuildWildCards() {
        foreach (string value in wildCards) {
            Card card = gameObject.AddComponent<Card>();
            card.SetValues("WILD", value);
        }
    } 

    void OnMouseDown() {
        activePlayer.Draw(openCard, this, gameManager.card, gameManager.turn.turnNumber);
    }
}