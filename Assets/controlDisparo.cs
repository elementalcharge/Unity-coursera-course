using UnityEngine;
using System.Collections;

public class controlDisparo : MonoBehaviour {
	Collider2D disparandoA=null;
	public float probabilidadDeDisparo = 1f;


	ControlEnemigo ctr;
	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("enemigo").GetComponent<ControlEnemigo>();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name.Equals("potion")&&disparandoA==null)
		{
			decidaSiDispara (other);
		}

		if(other.gameObject.name.Equals("Hero")&&disparandoA==null)
		{
			Disparar();
			disparandoA = other;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other == disparandoA) 
		{
			disparandoA = null;
		}
	}
	public void decidaSiDispara(Collider2D other)
	{
		if (Random.value < probabilidadDeDisparo) {
			Disparar ();
			disparandoA = other;
		}
	}

	void Disparar()
	{
		ctr.Disparar ();
		//GameObject bulletCopy = Instantiate (bulletPrototype);
		//bulletCopy.transform.position =new Vector3( transform.parent.position.x,transform.parent.position.y,-1);

		//bulletCopy.GetComponent<controlBala> ().direction = new Vector3 (transform.parent.localScale.x, 0, 0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
