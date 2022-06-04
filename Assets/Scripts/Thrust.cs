using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    private AudioSource audioSourceComponent;
    private LandingSensor landingSensorComponent;

    [SerializeField] float thrustPower = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        audioSourceComponent = GetComponent<AudioSource>();
        landingSensorComponent = GetComponent<LandingSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbodyComponent.AddRelativeForce(CalculateThrustForce());
            if (landingSensorComponent.HasLanded()) landingSensorComponent.TakeOff();
            if (!audioSourceComponent.isPlaying) audioSourceComponent.Play();
        }
        else if (audioSourceComponent.isPlaying)
        {
            audioSourceComponent.Stop();
        } 
    }

    private Vector3 CalculateThrustForce()
    {
        float force = thrustPower * Time.deltaTime;
        
        return new Vector3(0, force, 0);
    }
}
