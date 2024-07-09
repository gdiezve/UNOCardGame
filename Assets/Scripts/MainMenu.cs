using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioListener audioListener;
    [SerializeField] GameObject gameModeMenuUI;
    [SerializeField] GameObject playButton;
    
    public void PlayButton() {
       gameModeMenuUI.SetActive(true);
       playButton.SetActive(false);
    }

    public void AIButton() {
        audioListener.enabled = false;
        SceneManager.LoadScene("AIGameMode");
    }

    public void MultiplayerButton() {

    }
}
