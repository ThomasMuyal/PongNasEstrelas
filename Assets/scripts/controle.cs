using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject raquete;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        raquete.transform.position = new Vector3(((gameObject.transform.position.x - 0.55f)*2)-0.09f, raquete.transform.position.y, raquete.transform.position.z);

    }
}
