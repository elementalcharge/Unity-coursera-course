using UnityEngine;
using System.Collections;

public class tocarpinches : MonoBehaviour {
	public int daño = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer == 14) {
			agarrado ctr = other.gameObject.GetComponent<agarrado> ();
			if (ctr != null)
				ctr.romper ();
			Destroy (gameObject);
		}
		else if(other.gameObject.layer==8)
		{
			controlheroe ctr = other.gameObject.GetComponent<controlheroe> ();
			if (ctr != null) 
			{
				ctr.RecibirDaño (daño);
			}

		}
	}
}
