using UnityEngine;
using System.Collections;

public class ControlLlavesYswitches : MonoBehaviour {
	public Animator[] anim;
	public string nombreDelTrigger = "open";
	// Use this for initialization
	void Start () {

	}

	// Update is called on2€#@@|#aqce per frame
	void OnTriggerEnter2D () {
		foreach (Animator temp in anim) 
		{
			temp.SetTrigger (nombreDelTrigger);
		}
	}
}
