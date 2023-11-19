using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collision with freindly object");
                break;
            case "Finish":
                LoadNextScene();
                break;
            default:
                ReloadScene();
                break;
        }
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextScene()
    {
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        bool isLastScene = currentIndex == totalScenes - 1;

        if (isLastScene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
