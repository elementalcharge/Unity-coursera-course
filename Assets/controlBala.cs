using UnityEngine;
using System.Collections;

public class controlBala : MonoBehaviour {
	public float speed = 6;
	public float lifeTime = 2;
	public Vector3 direction= new Vector3(-1,0,0); 
	public int daño = 25;

	Vector3 stepVector;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
		rb = GetComponent<Rigidbody2D> ();
		stepVector = speed * direction.normalized;
	}

	private void OnTriggerExit2D(Collider2D other){
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
			Destroy (gameObject);
		}
	
	}
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = stepVector;
	}
}
