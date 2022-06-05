using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 2f;
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] AudioClip crashAudioClip;

    private Rigidbody rbComponent;
    private Movement mvComponent;
    private AudioSource asComponent;
    private MainEngineHandler mehComponent;

    private bool hasCrashed = false;
    private bool hasFinished = false;

    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
        mvComponent = GetComponent<Movement>();
        asComponent = GetComponent<AudioSource>();
        mehComponent = GetComponentInChildren<MainEngineHandler>();
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
        if (hasCrashed) return;

        mehComponent.StopThrustSound();
        asComponent.PlayOneShot(crashAudioClip);
        // TODO: add crash particle effect
        hasCrashed = true;
        mvComponent.enabled = false;
        rbComponent.constraints = RigidbodyConstraints.None;
        if (!hasFinished) Invoke("ReloadScene", crashDelay);
    }

    private void StartFinishedSequence()
    {
        if (hasCrashed || hasFinished) return;

        mehComponent.StopThrustSound();
        asComponent.PlayOneShot(successAudioClip);
        // TODO: add crash particle effect
        hasFinished = true;
        mvComponent.enabled = false;
        rbComponent.constraints = RigidbodyConstraints.None;
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
