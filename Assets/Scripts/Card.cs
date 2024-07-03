using System.Collections.Generic;
using UnityEngine;

public enum CardEffect {
    NO_EFFECT,
    LOSE_TURN,
    CHANGE_COLOR,
    CHANGE_DIR,
    DRAW2,
    DRAW4
}

public class Card : MonoBehaviour
{
    public string color;
    public string value;
    public List<CardEffect> effects = new();
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

    public void SetValues(string color, string value, List<CardEffect> effects)
    {
        this.color = color;
        this.value = value;
        this.effects = effects;
    }

    public bool EvaluateCard(string openColor, string openValue) {
        if (openColor == color || openValue == value || openValue == "COL") {
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
            openCard.SetValues(color, value, effects);
            openCard.SetSprite(gameManager.openCardClone);
            activePlayer.PlayCard(this);
        }
    }
}
