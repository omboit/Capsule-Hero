using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGameOnly : MonoBehaviour
{
    // Call this method from the Quit Button OnClick event
    public void QuitGame()
    {
        Debug.Log("Game is quitting...");

#if UNITY_EDITOR
        // Stops Play Mode when testing inside Unity Editor
        EditorApplication.isPlaying = false;
#else
            // Closes application in a built executable (.exe)
            Application.Quit();
#endif
    }
}