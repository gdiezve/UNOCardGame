using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject winnerMenuUI;
    [SerializeField] TextMeshProUGUI winnerText;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.winner) {
            ActivateWinnerMenu();
        }
    }

    void ActivateWinnerMenu() {
        string message = string.Format("{0} wins the game!", gameManager.winnerName);
        winnerText.SetText(message);
        winnerMenuUI.SetActive(true);
    }

    public void PlayAgainButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}