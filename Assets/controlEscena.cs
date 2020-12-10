using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
public class controlEscena : MonoBehaviour {

	int numObjetoHeroe=0;
	int numObjetoEnemigo=0;
	float version =0.1f;
	// Use this for initialization
	void Start () {
		registrarInicio ();
	}
	public void registrarInicio()
	{
		print ("Regitrando inicio");
		Analytics.CustomEvent ("Game Start", new Dictionary<string, object>{ 
		
		});
	}
	public void registrarFin()
	{
		print ("Regitrando fin");
		float secsJuego = Time.time;
		float vidaHero = GameObject.Find ("Hero").GetComponent<controlheroe>().vida;
		int vidaEnemigo=GameObject.Find ("Enemigo").GetComponent<ControlEnemigo>().energy;
		Analytics.CustomEvent ("Game end", new Dictionary<string, object>
			{
				{"time",secsJuego},
				{"vidaHero", vidaHero},
				{"numObjetoHeroe", numObjetoHeroe},
				{"numObjetoEnemigo",numObjetoEnemigo},
				{"vidaEnemigo",vidaEnemigo},
				{"version",version}
		});
	}

	// Update is called once per frame
	void Update () {
	
	}
}
