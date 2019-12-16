using UnityEngine;
using System.Collections;

public class BallCollision_Squash : MonoBehaviour
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
    public Vector3 ori_pos;
    public bool jadecidiu = false;
    public bool jacolidiu = false;
    public float pos_ini = 0.0f;
    void Start()
    {
        ori_pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Physics.IgnoreLayerCollision(9, 11);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward;
    }
    private void Update()
    {
        modulo = 3.0f;//Mathf.Sqrt((GetComponent<Rigidbody>().velocity.x * GetComponent<Rigidbody>().velocity.x) + (GetComponent<Rigidbody>().velocity.z * GetComponent<Rigidbody>().velocity.z));
        sen = (modulo * Mathf.Sin(Mathf.PI / 12));
        cos = (modulo * Mathf.Cos(Mathf.PI / 12));
        if ((gameObject.transform.position.z > 1.0f + pos_ini) && (gameObject.transform.position.z < 2.0f + pos_ini) && (!jadecidiu))
        {
            raquete1.GetComponent<RaqueteAgent>().RequestDecision();
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
    private void reseta_bola()
    {
        gameObject.transform.position = ori_pos;
        novo_angulo = Random.Range((-1) * Mathf.PI / 9, Mathf.PI / 9);
        GetComponent<Rigidbody>().velocity = new Vector3(modulo * Mathf.Sin(novo_angulo), 0, modulo * Mathf.Cos(novo_angulo));
        jadecidiu = false;
        raquete1.GetComponent<RaqueteAgent>().RequestDecision();
    }
    private void OnCollisionEnter(Collision collision)
    {
        colisoes++;

        if ((collision.gameObject.tag == "ParedesFinais_AI") || (collision.gameObject.tag == "ParedesFinais_Player"))
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, (GetComponent<Rigidbody>().velocity.z)*(-1));
            raquete1.GetComponent<RaqueteAgent>().MarcouGol(gameObject.transform.position.x);
            raquete1.GetComponent<RaqueteAgent>().Done();
            reseta_bola();

        }
        else if (collision.gameObject.tag == "ParedeLateral")
        {
            GetComponent<Rigidbody>().velocity = new Vector3((GetComponent<Rigidbody>().velocity.x) * (-1), 0, GetComponent<Rigidbody>().velocity.z);
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.x < 1.0f || gameObject.GetComponent<Rigidbody>().velocity.z < 1.0f)
        {
            //raquete1.GetComponent<RaqueteAgent>().Done();
        }
        
            if (collision.gameObject.tag == "softesquerda")
            {
                raquete1.GetComponent<RaqueteAgent>().RecompensaColisao();
            
                jadecidiu = false;  
                reseta_bola();
        }
            if (collision.gameObject.tag == "softdireita")
        {
            raquete1.GetComponent<RaqueteAgent>().RecompensaColisao();
            jadecidiu = false;
            reseta_bola();

        }
            if (collision.gameObject.tag == "hardesquerda")
        {
            raquete1.GetComponent<RaqueteAgent>().RecompensaColisao();
            jadecidiu = false;
            reseta_bola();

        }
            if (collision.gameObject.tag == "harddireita")
        {
            raquete1.GetComponent<RaqueteAgent>().RecompensaColisao();
            jadecidiu = false;
            reseta_bola();

        }
            if (collision.gameObject.tag == "central")
        {
            raquete1.GetComponent<RaqueteAgent>().RecompensaColisao();
            jadecidiu = false;
            reseta_bola();

        }
        

    }

}

