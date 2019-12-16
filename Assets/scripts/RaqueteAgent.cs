using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RaqueteAgent : Agent
{
	Rigidbody rBody;
	public float speed = 10; //velocidade da raquete AI
	public int placar_AI = 0; //quantos gols a AI tomou
	public int placar_Player = 0; //quantos gols o player tomou
	public int old_placar_AI = 0; //quantos gols a AI tomou
	public int old_placar_Player = 0; //quantos gols o player tomou
	public int placar_total = 0;

	public bool treinando = false;

	public float pos_inicial_x;
	public float pos_inicial_y;
	public float pos_inicial_z;
	public float futuro_x;
	public int futuro;

	public float tempo, desloc_x;

	public int raqueteID;

	public GameObject paredefinal_AI;
	public GameObject paredefinal_Player;
	public GameObject par_raquete;

	public int rebatidas_seguidas = 0;

	public Vector3 target_pos;

	//GameObject[] array_paredefinal_AI;
	//GameObject[] array_paredefinal_Player;
	// Start is called before the first frame update
	void Start(){
        rBody = GetComponent<Rigidbody>();
		pos_inicial_x = gameObject.transform.position.x;
		pos_inicial_y = gameObject.transform.position.y;
		pos_inicial_z = gameObject.transform.position.z;
		target_pos = new Vector3(pos_inicial_x, pos_inicial_y, pos_inicial_z);
		//paredefinal_AI = GameObject.FindWithTag("ParedesFinais_AI");
		//array_paredefinal_AI = GameObject.FindGameObjectsWithTag("ParedesFinais_AI");
		//array_paredefinal_Player = GameObject.FindGameObjectsWithTag("ParedesFinais_Player");
		//paredefinal_Player = GameObject.FindWithTag("ParedesFinais_Player");

	}
	
	void Update()
	{
		placar_AI = paredefinal_AI.GetComponent<ParedeAI_placar>().placar_AI;
		placar_Player = paredefinal_Player.GetComponent<ParedePlayer_placar>().placar_player;
		placar_total = placar_AI + placar_Player;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target_pos, speed * 0.03f);
	}
	public override void CollectObservations(){
		//adiciona vetores de caracteristica para a rede neural, entradas
		//padrao

		AddVectorObs(Target.transform.position.x); //onde esta a bola
		AddVectorObs(Target.transform.position.z - this.transform.position.z);
		AddVectorObs(Target.GetComponent<Rigidbody>().velocity.x); //velocidade da bola
		//														   //------------------------------
																   //heuristica, preprocessamento:


		/*
		 * abstração para teste do funcionamento da rede neural -- feito para aferir
		 * o funcionamento dos pacotes e APIs
		tempo = (gameObject.transform.position.z - Target.transform.position.z) / Target.GetComponent<Rigidbody>().velocity.z;
		desloc_x = tempo * Target.GetComponent<Rigidbody>().velocity.x;
		
		futuro_x = Target.transform.position.x + desloc_x;
		while (futuro_x > 2.0 || futuro_x < 0.0)
		{
			if (futuro_x > 2.0)
			{
				//3.2 -> reflete 1.2 em 2 fica 0.8
				//2 - (3.2 - 2) = 0.8
				futuro_x = 4 - futuro_x; //reflexao
			}
			if (futuro_x < 0.0)
			{
				futuro_x = -futuro_x;
			}
		}
		futuro = (int)(12 - (futuro_x / 2 * 12));
		AddVectorObs(futuro);
		*/

		//------------------------------
		//AddVectorObs(Target.GetComponent<Rigidbody>().velocity.z);
		//AddVectorObs(Vector3.Angle(Target.GetComponent<Rigidbody>().velocity, Vector3.forward));
		//AddVectorObs(Mathf.Acos( Target.GetComponent<Rigidbody>().velocity.z/(Target.GetComponent<Rigidbody>().velocity.x+0.1f) ));
		//AddVectorObs(this.transform.position.x); //onde esta a raquete

	}

	public void RecompensaColisao()
	{
		//AddReward(0.8f);
		rebatidas_seguidas++;
	}
	public void MarcouGol(float posicaogol)
	{
		//AddReward((-0.3f) * Mathf.Pow((gameObject.transform.position.x - posicaogol), 2));
		//AddReward(-1.0f);
	}
	public override float[] Heuristic()
	{
		var action = new float[1];
		if (Input.GetKey("q"))
		{
			action[0] = 1;
		}
		if (Input.GetKey("w"))
		{
			action[0] = 2;
		}
		if (Input.GetKey("e"))
		{
			action[0] = 3;
		}
		if (Input.GetKey("r"))
		{
			action[0] = 4;
		}
		if (Input.GetKey("t"))
		{
			action[0] = 5;
		}
		return action;
	}
	public override void AgentAction(float[] vectorAction, string textAction){
		//actions size 1, só eixo x
		//playerbrain
		if (!treinando)
		{
			Debug.Log(vectorAction[0]);
		}
		if (raqueteID == 1)
		{

			target_pos = new Vector3(0.3f + (vectorAction[0] * (1.7f / 10.5f)), pos_inicial_y, pos_inicial_z);
			//target_pos = new Vector3(0.3f + (vectorAction[0] * (1.7f / 12.0f)), pos_inicial_y, pos_inicial_z);
		}
		else
		{
			target_pos = new Vector3(vectorAction[0] * (1.7f / 12.0f), pos_inicial_y, pos_inicial_z);
		}
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target_pos, speed*0.005f);
		if (treinando)
		{
			if(futuro_x < target_pos.x + 0.15)
			{
				if(futuro_x > target_pos.x - 0.15)
				{
					AddReward(0.3f);
					if(Target.position.x > 0.5f || Target.position.x < 1.5)
					{
						AddReward(0.4f);
					}
				}
				else
				{
					AddReward(-0.05f);
					if(Target.position.x > 0.5f || Target.position.x < 1.5)
					{
						AddReward(-0.1f);
					}
				}
			}
			{
				AddReward(-0.04f);
			}
		}
		/*
		float moveamount = 0;
		if (vectorAction[0] == 0)
		{
			moveamount = 0.0f;
		}
		if (vectorAction[0] == 1)
		{
			moveamount = -0.01f;
		}
		if (vectorAction[0] == 2)
		{
			moveamount = 0.01f;
		}
		Vector3 controlsignal = Vector3.zero;
		controlsignal.x = moveamount*speed;
		//tratamento - atravessar a parede
		if(raqueteID == 1)
		{
			if(gameObject.transform.position.x + controlsignal.x >= 2.0)
			{
				gameObject.transform.position = new Vector3(1.999f, pos_inicial_y, pos_inicial_z);
			}
			if(gameObject.transform.position.x + controlsignal.x < 0.3)
			{
				gameObject.transform.position = new Vector3(0.3f, pos_inicial_y, pos_inicial_z);
			}
		}
		if(raqueteID == 2)
		{
			if(gameObject.transform.position.x + controlsignal.x > 1.7)
			{
				gameObject.transform.position = new Vector3(1.7f, pos_inicial_y, pos_inicial_z);
			}
			if(gameObject.transform.position.x + controlsignal.x < 0)
			{
				gameObject.transform.position = new Vector3(0.0f, pos_inicial_y, pos_inicial_z);
			}
		}
		//end tratamento
		gameObject.transform.Translate(controlsignal);

		//rewards
		*/
		/*
		if (placar_total >= 5)
		{
			Debug.Log("5 gols");
			paredefinal_AI.GetComponent<ParedeAI_placar>().placar_AI = 0;
			paredefinal_Player.GetComponent<ParedePlayer_placar>().placar_player = 0;
			placar_total = 0;
			//AddReward(placar_Player * (-1) * 0.5f);
			//AddReward(placar_AI * (0.5f));
			Done();
			par_raquete.GetComponent<RaqueteAgent>().Done();
		}
		*/
		
	}
	
	public Transform Target;

	public override void AgentReset(){
		if(treinando)
		gameObject.transform.position = new Vector3(pos_inicial_x, pos_inicial_y, pos_inicial_z);
		rebatidas_seguidas = 0;
	}

}
