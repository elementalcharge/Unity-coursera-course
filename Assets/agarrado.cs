using UnityEngine;
using System.Collections;

public class agarrado : MonoBehaviour {
	Animator anim;
	public bool soyPota;
	bool cure=false;
	public int curaEnNegativo=-100;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 8)
		{
			controlheroe ctr = other.gameObject.GetComponent<controlheroe> ();
			if (ctr != null && soyPota && !cure) 
			{
				ctr.RecibirDaño (curaEnNegativo);
				cure = true;
			}
			anim.SetTrigger ("collected");
		}
		else if (other.gameObject.layer == 13) 
		{
			anim.SetTrigger ("broken");
		}
	}
	public void romper()
	{
		anim.SetTrigger ("broken");
	}
}
