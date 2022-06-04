using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    [SerializeField] float thrustPower = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            rigidbodyComponent.AddRelativeForce(CalculateThrustForce());
        }
    }

    private Vector3 CalculateThrustForce()
    {
        float force = thrustPower * Time.deltaTime;
        
        return new Vector3(0, force, 0);
    }
}
