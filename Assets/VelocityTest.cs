using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movevector = Vector3.zero;
        movevector.x = 1;
        movevector.y = 1;
        movevector.z = 1;
        rBody.AddForce(movevector, ForceMode.VelocityChange);
    }
}
