using UnityEngine;
using System.Collections;

public class Ball_Collision : MonoBehaviour
{
    public float speed = 10;
    public float modulo = 10.0f;
    public float sen = 10.0f;
    public float cos = 10.0f;
    public float novo_angulo = 0.0f;
    public float novo_direcao = 0.0f;
    public float modeltime = 0.0f;
    public int colisoes = 0;
    public bool direcao = false;
    public GameObject raquete1;
    public GameObject raquete2;
    public Vector3 ori_pos;
    public bool jadecidiu = false;
    public bool jacolidiu = false;
    public float pos_ini = 0.0f;
    void Start()
    {
        Vector3 vel = new Vector3(0.1f, 0, -1);
        vel.Normalize();
        GetComponent<Rigidbody>().velocity = vel * speed;
        ori_pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Physics.IgnoreLayerCollision(9, 11);

        pos_ini = gameObject.transform.position.z;
    }
    private void Update()
    {
        modulo = 3.0f;//Mathf.Sqrt((GetComponent<Rigidbody>().velocity.x * GetComponent<Rigidbody>().velocity.x) + (GetComponent<Rigidbody>().velocity.z * GetComponent<Rigidbody>().velocity.z));
        sen = (modulo * Mathf.Sin(Mathf.PI / 12));
        cos = (modulo * Mathf.Cos(Mathf.PI / 12));
        if ((gameObject.transform.position.z > 1.0f+ pos_ini) && (gameObject.transform.position.z < 2.0f + pos_ini) && (!jadecidiu))
        {
            raquete1.GetComponent<RaqueteAgent>().RequestDecision();
            raquete2.GetComponent<RaqueteAgent>().RequestDecision();
            jadecidiu = true;
            jacolidiu = false;
        }
        if ((gameObject.transform.position.z > (-1.0f) + pos_ini) && (gameObject.transform.position.z < 0.0f + pos_ini) && (!jadecidiu))
        {
            raquete1.GetComponent<RaqueteAgent>().RequestDecision();
            raquete2.GetComponent<RaqueteAgent>().RequestDecision();
            jadecidiu = true;
            jacolidiu = false;
        }
        if ((gameObject.transform.position.z > (-1.5f) + pos_ini) && (gameObject.transform.position.z < (-1.0f) + pos_ini))
        {
            jadecidiu = false;
        }
        if ((gameObject.transform.position.z > 2.0f + pos_ini) && (gameObject.transform.position.z < 2.5f + pos_ini))
        {
            jadecidiu = false;
        }
        if ((gameObject.transform.position.z < 0.5f + pos_ini) && (gameObject.transform.position.z > (0.0f) + pos_ini))
        {
            jadecidiu = false;
        }
        if ((gameObject.transform.position.z > 0.5f + pos_ini) && (gameObject.transform.position.z < 1.0f + pos_ini))
        {
            jadecidiu = false;
        }
        if ((Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, Vector3.forward) > 70) || (Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, Vector3.forward) < -70))
        {
            if ((Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, Vector3.back) > 70) || (Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, Vector3.back) < -70))
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colisoes++;
        
        if ((collision.gameObject.tag == "ParedesFinais_AI")||(collision.gameObject.tag == "ParedesFinais_Player"))
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, (GetComponent<Rigidbody>().velocity.z)*(-1));
            gameObject.transform.position = ori_pos;
           
            novo_angulo = Random.Range((-1)*Mathf.PI / 6, Mathf.PI / 6);
            novo_direcao = Random.Range(0.0f, 1.0f);
            if(novo_direcao > 0.5)
            {
                novo_direcao = 1.0f;
            }
            else
            {
                novo_direcao = -1.0f;
            }
            GetComponent<Rigidbody>().velocity = new Vector3(modulo*Mathf.Sin(novo_angulo), 0, novo_direcao * modulo*Mathf.Cos(novo_angulo));

        }
        else if (collision.gameObject.tag == "ParedeLateral")
        {

            GetComponent<Rigidbody>().velocity = new Vector3((GetComponent<Rigidbody>().velocity.x) * (-1), 0, GetComponent<Rigidbody>().velocity.z);
        }
        else if (jacolidiu)
        {
            raquete1.GetComponent<RaqueteAgent>().Done();
        }
        if(gameObject.GetComponent<Rigidbody>().velocity.x < 1.0f || gameObject.GetComponent<Rigidbody>().velocity.z < 1.0f)
        {
            raquete1.GetComponent<RaqueteAgent>().Done();
        }
        //raquete
        if(GetComponent<Rigidbody>().velocity.z > 0)
        {
            direcao = true;
        }
        else
        {
            direcao = false;
        }
        if (!jacolidiu)
        {
            jacolidiu = true;
            if (collision.gameObject.tag == "softesquerda")
            {
                if (direcao)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(modulo * Mathf.Sin(Mathf.PI / 12), 0, (-1) * modulo * Mathf.Cos(Mathf.PI / 12));
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3((-1) * modulo * Mathf.Sin(Mathf.PI / 12), 0, modulo * Mathf.Cos(Mathf.PI / 12));
                }
            }
            if (collision.gameObject.tag == "softdireita")
            {
                if (direcao)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3((-1) * modulo * Mathf.Sin(Mathf.PI / 12), 0, (-1) * modulo * Mathf.Cos(Mathf.PI / 12));
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(modulo * Mathf.Sin(Mathf.PI / 12), 0, modulo * Mathf.Cos(Mathf.PI / 12));
                }
            }
            if (collision.gameObject.tag == "hardesquerda")
            {
                if (direcao)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(modulo * Mathf.Sin(Mathf.PI / 6), 0, (-1) * modulo * Mathf.Cos(Mathf.PI / 6));
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3((-1) * modulo * Mathf.Sin(Mathf.PI / 6), 0, modulo * Mathf.Cos(Mathf.PI / 6));
                }
            }
            if (collision.gameObject.tag == "harddireita")
            {
                if (direcao)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3((-1) * modulo * Mathf.Sin(Mathf.PI / 6), 0, (-1) * modulo * Mathf.Cos(Mathf.PI / 6));
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(modulo * Mathf.Sin(Mathf.PI / 6), 0, modulo * Mathf.Cos(Mathf.PI / 6));
                }
            }
            if (collision.gameObject.tag == "central")
            {
                if (direcao)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, modulo * (-1));
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, modulo * (1));
                }
            }
            direcao = !direcao;
        }

    }

}

