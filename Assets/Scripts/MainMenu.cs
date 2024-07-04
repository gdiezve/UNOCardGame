using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioListener audioListener;
    
    public void PlayButton() {
        audioListener.enabled = false;
        SceneManager.LoadScene("GameScene");
    }
}
