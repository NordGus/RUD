using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    // Physics based inplementation.
    
    Rigidbody rigidbodyComponent;
    LandingSensor landingSensorComponent;

    [SerializeField] float thrustPower = 500f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        landingSensorComponent = GetComponent<LandingSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            rigidbodyComponent.AddRelativeTorque(CalculateThrustForce());
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rigidbodyComponent.AddRelativeTorque(-CalculateThrustForce());
        }
        else if (rigidbodyComponent.angularVelocity.z > 0 && !landingSensorComponent.HasLanded())
        {
            // Contraresting torque applyed to cancel excess rotation.
            rigidbodyComponent.AddRelativeTorque(-CalculateThrustForce());
        }
        else if (rigidbodyComponent.angularVelocity.z < 0 && !landingSensorComponent.HasLanded())
        {
            // Contraresting torque applyed to cancel excess rotation.
            rigidbodyComponent.AddRelativeTorque(CalculateThrustForce());
        }
    }

    private Vector3 CalculateThrustForce()
    {
        float force = thrustPower * Time.deltaTime;
        
        return new Vector3(0, 0, force);
    }
}
