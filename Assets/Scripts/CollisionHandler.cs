using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private Movement mvComponent;
    private Rigidbody rbComponent;

    [SerializeField] float crashDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;

    void Start()
    {
        mvComponent = GetComponent<Movement>();
        rbComponent = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                StartFinishedSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        // TODO: add crash SFX
        // TODO: add crash particle effect
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Invoke("ReloadScene", crashDelay);
    }

    private void StartFinishedSequence()
    {
        // TODO: add crash SFX
        // TODO: add crash particle effect
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
