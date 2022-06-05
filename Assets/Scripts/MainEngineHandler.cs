using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngineHandler : MonoBehaviour
{
    private AudioSource asComponent;

    // Start is called before the first frame update
    void Start()
    {
        asComponent = GetComponent<AudioSource>();
    }

    public void PlayThrustAudio()
    {
        asComponent.Play();
    }

    public void StopThrustSound()
    {
        asComponent.Stop();
    }

    public bool IsPlaying()
    {
        return asComponent.isPlaying;
    }
}
