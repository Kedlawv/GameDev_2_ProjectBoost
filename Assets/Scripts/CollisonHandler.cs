using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crashAudioClip;
    [SerializeField] AudioClip successAudioClip;

    private bool isTransitioning = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collision with freindly object");
                break;
            case "Finish":
                StartSuccessSequance();
                break;
            default:
                StartCrashSquence();
                break;
        }
    }

    private void StartCrashSquence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crashAudioClip);
        this.GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }

    private void StartSuccessSequance()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(successAudioClip);
        Invoke("LoadNextScene", delay);
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
