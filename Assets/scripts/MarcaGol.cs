using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcaGol : MonoBehaviour
{
    public GameObject raquete;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        raquete.GetComponent<RaqueteAgent>().MarcouGol(collision.transform.position.x);
    }
}
