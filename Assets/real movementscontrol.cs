using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class realmovementscontrol : MonoBehaviour {
	int collidingobjects;

	public Slider slider;
	public Text txt;
	public float energy=100;

	// Use this for initialization
	void Start () {
		collidingobjects = 0;
	}
	void OnCollisionEnter2D(Collider other)
	{
		collidingobjects++;
	}
	void OnCollisionExit2D(Collider other)
	{
		collidingobjects--;
	}
	// Update is called once per frame
	void Update()
	{
		slider.value = energy;
		txt.text = energy.ToString ();
	}
}
