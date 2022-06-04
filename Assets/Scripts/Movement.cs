using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rbComponent;
    private AudioSource asComponent;
    private LandingHandler lhComponent;

    [SerializeField] float mainEngineThrustPower = 1000f;
    [SerializeField] float rotationThrustPower = 500f;

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
        asComponent = GetComponent<AudioSource>();
        lhComponent = GetComponent<LandingHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMainEngineThrust();
        HandleRotationThrust();
    }

    private void HandleMainEngineThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rbComponent.AddRelativeForce(CalculateMainEngineThrustForce());
            
            if (lhComponent.HasLanded()) lhComponent.TakeOff();
            
            if (!asComponent.isPlaying) asComponent.Play();
        }
        else if (asComponent.isPlaying)
        {
            asComponent.Stop();
        }
    }

    private Vector3 CalculateMainEngineThrustForce()
    {
        float force = mainEngineThrustPower * Time.deltaTime;

        return new Vector3(0, force, 0);
    }

    private void HandleRotationThrust()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rbComponent.AddRelativeTorque(CalculateRotationThurstForce());
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rbComponent.AddRelativeTorque(-CalculateRotationThurstForce());
        }
        else if (rbComponent.angularVelocity.z > 0 && !lhComponent.HasLanded())
        {
            // Contraresting torque applyed to cancel excess rotation.
            rbComponent.AddRelativeTorque(-CalculateRotationThurstForce());
        }
        else if (rbComponent.angularVelocity.z < 0 && !lhComponent.HasLanded())
        {
            // Contraresting torque applyed to cancel excess rotation.
            rbComponent.AddRelativeTorque(CalculateRotationThurstForce());
        }
    }

    private Vector3 CalculateRotationThurstForce()
    {
        float force = rotationThrustPower * Time.deltaTime;

        return new Vector3(0, 0, force);
    }
}

