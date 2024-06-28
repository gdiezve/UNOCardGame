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
        gameManager.openCardClone.GetComponent<Card>().SetSprite(gameManager.openCardClone);
        chooseColorMenuUI.SetActive(false);
        gameManager.activePlayer.canPlay = false;
        gameManager.activePlayer.needToChooseColor = false;
    }
}
