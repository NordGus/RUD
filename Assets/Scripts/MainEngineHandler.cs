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

    public void StartEngine()
    {
        if (!asComponent.isPlaying) asComponent.Play();
        if (!engineJetParticles.isPlaying) engineJetParticles.Play();
        plComponent.enabled = true;
    }

    public void StopEngine()
    {
        asComponent.Stop();
        engineJetParticles.Stop();
        plComponent.enabled = false;
    }
}
