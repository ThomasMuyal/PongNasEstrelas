using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedeAI_placar : MonoBehaviour
{
    // Start is called before the first frame update
    public int placar_AI = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bola")
        {
            placar_AI++;
        }
    }
}
