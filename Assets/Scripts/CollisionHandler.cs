using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    [SerializeField] AudioClip succesAudio;
    [SerializeField] AudioClip crashAudio;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("O arkadaþ sana zarar vermiyecek");
                break;
            case "Finish":
                StartSuccesSequence();
                break;           
            default:
                StartCrashSequence();
                break;

        }
    }
    void StartSuccesSequence()
    {
        //SFX düzelt ve Particle ekle.
        isTransitioning = true;
        audioSource.PlayOneShot(succesAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadLevelDelay);
    }
    void StartCrashSequence()
    {
        //SFX düzelt ve Particle ekle.
        isTransitioning= true;
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadLevelDelay);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=(currentSceneIndex+1);
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
