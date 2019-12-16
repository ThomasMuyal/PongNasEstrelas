using UnityEngine;
using System.Collections;

public class ColisaoLateral : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,
                                                         GetComponent<Rigidbody>().velocity.y,
                                                         (GetComponent<Rigidbody>().velocity.z)*(-1));  /////vel.x * -1;
    }

}

