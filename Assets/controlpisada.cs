using UnityEngine;
using System.Collections;

public class controlpisada : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	public void activarSwitch()
	{
		anim.SetTrigger ("caer");
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
