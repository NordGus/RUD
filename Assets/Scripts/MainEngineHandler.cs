using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngineHandler : MonoBehaviour
{
    private AudioSource asComponent;
    private Light plComponent;
    [SerializeField] ParticleSystem engineJetParticles;

    // Start is called before the first frame update
    void Start()
    {
        asComponent = GetComponent<AudioSource>();
        plComponent = GetComponentInChildren<Light>();
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

    public void PlayParticles()
    {
        engineJetParticles.Play();
    }

    public void StopParticles()
    {
        engineJetParticles.Stop();
        
    }

    public bool IsPlayingParticles()
    {
        return engineJetParticles.isPlaying;
    }

    public void TurnLightOn()
    {
        plComponent.enabled = true;
    }

    public void TurnLightOff()
    {
        plComponent.enabled = false;
    }
}
