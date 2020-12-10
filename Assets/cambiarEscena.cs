using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class cambiarEscena : MonoBehaviour {
	public string escenaACargar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer==8){
		SceneManager.LoadScene (escenaACargar);
		}
	}
}
