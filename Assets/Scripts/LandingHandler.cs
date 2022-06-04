using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingHandler : MonoBehaviour
{
    private bool landed;

    // Start is called before the first frame update
    void Start()
    {
        landed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        landed = true;
    }

    public void TakeOff()
    {
        landed = false;
    }

    public bool HasLanded()
    {
        return landed;
    }
}
