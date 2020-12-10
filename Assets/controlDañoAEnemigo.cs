using UnityEngine;
using System.Collections;

public class controlDañoAEnemigo : MonoBehaviour {
	
	Collider2D colliderEnem=null;
	public int delayBajarPuntosEnemigo = 1;

	public AudioSource asource;

	public AudioClip damaged;
	// Use this for initialization
	void Start () {
		asource = GetComponent<AudioSource> ();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer==9)
		{
			Debug.Log ("colision con el enemigo");
			colliderEnem = other;
			asource.PlayOneShot (damaged);
			Invoke ("DañarEnemigo", delayBajarPuntosEnemigo);

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other == colliderEnem) 
		{
			Debug.Log ("salir de colision con el enemigo");
			colliderEnem = null;
			CancelInvoke("DañarEnemigo");
		}
	}
	void DañarEnemigo()
	{
		Debug.Log ("BajarPuntos");
		colliderEnem.gameObject.GetComponent<ControlEnemigo> ().Dañar ();

	}
	// Update is called once per frame

}
