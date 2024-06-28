using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioListenerManager : MonoBehaviour
{

    [SerializeField] AudioListener audioListener;
    
    // Update is called once per frame
    void Update()
    {
       if(SceneManager.GetActiveScene().buildIndex == 1) {
            audioListener.enabled = true;
       } 
    }
}
