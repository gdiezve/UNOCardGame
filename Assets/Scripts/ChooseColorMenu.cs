using UnityEngine;

public class ChooseColorMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject chooseColorMenuUI;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.activePlayer.needToChooseColor) {
            ActivateChooseColorMenu();
        }
    }

    void ActivateChooseColorMenu () {
        Time.timeScale = 0f;
        chooseColorMenuUI.SetActive(true);
    }

    public void RedButton() {
        ColorButton("RED");
    }
    public void BlueButton() {
        ColorButton("BLU");
    }
    public void GreenButton() {
        ColorButton("GRE");
    }
    public void YellowButton() {
        ColorButton("YEL");
    }

    void ColorButton(string color) {
        gameManager.openCardClone.GetComponent<Card>().color = color;
        chooseColorMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
