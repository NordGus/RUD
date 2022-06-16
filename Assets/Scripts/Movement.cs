using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainEngineThrustPower = 1000f;
    [SerializeField] float rotationThrustPower = 500f;
    [SerializeField] ParticleSystem topLeftThrusterParticles;
    [SerializeField] ParticleSystem topRigthThrusterParticles;
    [SerializeField] ParticleSystem bottomLeftThrusterParticles;
    [SerializeField] ParticleSystem bottomRigthThrusterParticles;

    private Rigidbody rbComponent;
    private MainEngineHandler mehComponent;

    private bool hasLanded = false;

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
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
            ApplyThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void HandleRotationThrust()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (hasLanded)
        {
            StopRotationParticles();
        }
        else if (rbComponent.angularVelocity.z > 0 && rbComponent.angularVelocity.z < 1)
        {
            // Contraresting torque applyed to cancel excess left rotation, without particles.
            StopRotationParticles();
            RotateRight(false);
        }
        else if (rbComponent.angularVelocity.z > -1 && rbComponent.angularVelocity.z < 0)
        {
            // Contraresting torque applyed to cancel excess right rotation, without particles.
            StopRotationParticles();
            RotateLeft(false);
        }
        else if (rbComponent.angularVelocity.z > 0)
        {
            // Contraresting torque applyed to cancel excess left rotation.
            RotateRight();
        }
        else if (rbComponent.angularVelocity.z < 0)
        {
            // Contraresting torque applyed to cancel excess right rotation.
            RotateLeft();
        }
        else
        {
            StopRotationParticles();
        }
    }

    private void ApplyThrust()
    {
        rbComponent.AddRelativeForce(CalculateMainEngineThrustForce());

        if (hasLanded) hasLanded = false;

        mehComponent.StartEngine();
    }

    private void StopThrust()
    {
        mehComponent.StopEngine();
    }

    private void RotateLeft(bool withParticles = true)
    {
        if (withParticles)
        {
            topLeftThrusterParticles.Stop();
            if (!topRigthThrusterParticles.isPlaying) topRigthThrusterParticles.Play();
            if (!bottomLeftThrusterParticles.isPlaying) bottomLeftThrusterParticles.Play();
            bottomRigthThrusterParticles.Stop();
        }

        rbComponent.AddRelativeTorque(CalculateRotationThurstForce());
    }

    private void RotateRight(bool withParticles = true)
    {
        if (withParticles)
        {
            if (!topLeftThrusterParticles.isPlaying) topLeftThrusterParticles.Play();
            topRigthThrusterParticles.Stop();
            bottomLeftThrusterParticles.Stop();
            if (!bottomRigthThrusterParticles.isPlaying) bottomRigthThrusterParticles.Play();
        }

        rbComponent.AddRelativeTorque(-CalculateRotationThurstForce());
    }

    private Vector3 CalculateMainEngineThrustForce()
    {
        float force = mainEngineThrustPower * Time.deltaTime;

        return new Vector3(0, force, 0);
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

    public void StopRotationParticles()
    {
        topLeftThrusterParticles.Stop();
        topRigthThrusterParticles.Stop();
        bottomLeftThrusterParticles.Stop();
        bottomRigthThrusterParticles.Stop();
    }
}

