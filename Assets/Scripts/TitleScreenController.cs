using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    
    public void StartGame()
    {
        
        SceneManager.LoadScene("yoshiSurfers");
    }

    public void ExitGame()
    {
        // For editor play mode, stop the play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // For builds, quit the game
        Application.Quit();
        #endif
    }
}
