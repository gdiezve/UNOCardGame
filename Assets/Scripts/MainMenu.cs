using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioListener audioListener;
    
    public void PlayButton() {
        audioListener.enabled = false;
        SceneManager.LoadScene("GameScene");
    }
}
