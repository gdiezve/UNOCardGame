using UnityEngine;

public class Turn : MonoBehaviour
{
    //bool winner = false;
    public int turnNumber = 0;

    public void PlayTurn(Player player1, AI aiPlayer, Card openCard, Deck deck, GameObject card) {
        player1.EvaluateHand(openCard);
        if (!player1.canPlay) {
            aiPlayer.canPlay = true;
            aiPlayer.EvaluateHand(openCard);
            if (aiPlayer.playableHand.Count == 0) {
                aiPlayer.Draw(openCard, deck, card, turnNumber);
            }
            if (aiPlayer.canPlay) aiPlayer.PlayCard();
            player1.canPlay = true;
            turnNumber += 1;
        }
    }
}