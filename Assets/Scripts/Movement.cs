using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainEngineThrustPower = 1000f;
    [SerializeField] float rotationThrustPower = 500f;

    private Rigidbody rbComponent;
    private AudioSource asComponent;
    private MainEngineHandler mehComponent;

    private bool hasLanded = false;

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
        asComponent = GetComponent<AudioSource>();
        mehComponent = GetComponentInChildren<MainEngineHandler>();
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
            
            if (hasLanded) hasLanded = false;
            
            if (!mehComponent.IsPlaying()) mehComponent.PlayThrustAudio();
        }
        else if (mehComponent.IsPlaying())
        {
            mehComponent.StopThrustSound();
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
        else if (rbComponent.angularVelocity.z > 0 && !hasLanded)
        {
            // Contraresting torque applyed to cancel excess rotation.
            rbComponent.AddRelativeTorque(-CalculateRotationThurstForce());
        }
        else if (rbComponent.angularVelocity.z < 0 && !hasLanded)
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

    // Use to handle excess rotation cancellation.
    private void OnCollisionEnter(Collision collision)
    {
        hasLanded = true;
    }
}

