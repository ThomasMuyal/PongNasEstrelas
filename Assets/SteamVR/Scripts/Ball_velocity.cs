//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Masks out pixels that cannot be seen through the connected hmd.
//
//=============================================================================

using UnityEngine;
using System.Collections;

public class Ball_velocity : MonoBehaviour
{
    public float speed = 10;
	void Start()
	{
        Vector3 vel = new Vector3(0.1f, 0, -1);
        vel.Normalize();
        GetComponent<Rigidbody>().velocity = vel * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 vel = new Vector3(1, 0, -1);
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x *(Random.Range(-2.5f, 2.5f)) , 0, GetComponent<Rigidbody>().velocity.z * (-1.02f));  /////vel.x * -1;
    }

}

