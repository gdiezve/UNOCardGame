using UnityEngine;

public class Turn : MonoBehaviour
{
    //bool winner = false;
    public int turnNumber = 0;
    public string previousPlayer = "AI"; // TODO: change to not be hardcoded

    public void PlayTurn(Player player1, AI aiPlayer, Card openCard, Deck deck, GameObject card) {
        player1.EvaluateOpenCard(openCard, deck, card, previousPlayer);
        player1.EvaluateHand(openCard);
        if (!player1.canPlay) {
            previousPlayer = "Player1"; // TODO: change to not be hardcoded
            aiPlayer.canPlay = true;
            aiPlayer.EvaluateOpenCard(openCard, deck, card, previousPlayer);
            if (aiPlayer.canPlay) {
                aiPlayer.EvaluateHand(openCard);
                if (aiPlayer.playableHand.Count == 0) {
                    if (aiPlayer.drawnInTurnNumber != turnNumber) {
                        aiPlayer.Draw(openCard, deck, card, 1);
                        aiPlayer.drawnInTurnNumber = turnNumber;
                    } else {
                        Debug.Log("You've already drawn this turn!");
                        aiPlayer.canPlay = false;
                    }
                }
                if (aiPlayer.canPlay) aiPlayer.PlayCard();
            }
            player1.canPlay = true;
            turnNumber += 1;
            previousPlayer = "AI";
        }
    }
}