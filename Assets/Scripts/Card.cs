using System;
using System.Linq;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string color;
    public string value;
    private readonly string[] wildValues = { "PL4", "COL" };
    public bool canBePlayed = false;
    GameManager gameManager;
    Deck deck;
    Card openCard;
    Player activePlayer;

    void Update() {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
            deck = gameManager.deckClone.GetComponent<Deck>();
            openCard = gameManager.openCardClone.GetComponent<Card>(); 
            activePlayer = gameManager.activePlayer.GetComponent<Player>(); 
        }
    }

    public void SetValues(string color, string value) {
        this.color = color;
        this.value = value;
    }

    public bool EvaluateCard(string openColor, string openValue) {
        if (openColor == color || openValue == value || wildValues.Contains(value)) {
            canBePlayed = true;
            return true;
        }
        canBePlayed = false;
        return false;
    }

    public void CardAlignment(float cardIndex, int cardCount, GameObject card, float desideredYOffset)
    {
        float xOffset = 0; //The horizontal center of the card fan (in worldspace units)
        float xRange = cardCount * 0.2f; //The horizontal range of the card fan (in worldspace units)
        float yOffset = desideredYOffset; //The vertical center of the card fan (in worldspace units)
    
        float alignResult = 0.5f;
        if(cardCount >= 2) alignResult = cardIndex / (cardCount - 1.0f);
        float xPos = Mathf.Lerp(xOffset-xRange, xOffset+xRange, alignResult);
    
        card.transform.position = new Vector3(xPos, yOffset, 0);
    }

    public void SetSprite(GameObject cardClone) {
        SpriteRenderer spriteRenderer = cardClone.GetComponent<SpriteRenderer>();
        string cardName = string.Format("{0}{1}", color, value);
        spriteRenderer.sprite = Resources.Load<Sprite>(cardName);
        cardClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void PrintCard() {
        var message = string.Format("{0}, {1}", color, value);
        Debug.Log(message);
    }

    void OnMouseDown() {
        if (canBePlayed) {
            deck.DiscardCard(openCard);
            openCard.SetValues(color, value);
            openCard.SetSprite(gameManager.openCardClone);
            activePlayer.PlayCard(this);
        }
    }
}