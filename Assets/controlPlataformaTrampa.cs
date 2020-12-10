using UnityEngine;
using System.Collections;

public class controlPlataformaTrampa : MonoBehaviour {
	public Animator[] anim;
	public string nombreDelTrigger = "caer";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called on2€#@@|#aqce per frame
	void OnCollisionEnter2D () {
		foreach (Animator temp in anim) 
		{
			temp.SetTrigger (nombreDelTrigger);
		}
	}
}
