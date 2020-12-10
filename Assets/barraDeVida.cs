using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barraDeVida : MonoBehaviour {
	public Image lifebar;
	private float characterFullLife;
	private controlheroe Vidaheroe;
	// Use this for initialization
	void Awake () {
		GameObject jugador = GameObject.Find ("Hero");
		Vidaheroe = jugador.GetComponent<controlheroe> ();
		characterFullLife = Vidaheroe.vida;
	}
	
	// Update is called once per frame
	void Update () {
		lifebar.fillAmount = Vidaheroe.vida / characterFullLife;
	}
}
