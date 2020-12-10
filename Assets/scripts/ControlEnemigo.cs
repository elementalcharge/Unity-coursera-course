using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlEnemigo : MonoBehaviour {
	public float vel = -1f;
	Rigidbody2D rgb;
	Animator anim;
	// Use this for initialization
	public GameObject bulletPrototype;

	public Slider slider;
	public Text txt;
	public int energy = 100;
	public GameObject retroalimentacionEnergiaPrefab;
	public Transform retroalimentacionSpawnPoint;

	public AudioSource asource;

	public AudioClip shoot;
	public AudioClip death;

	public controlEscena ctrescena;
	bool statsent=false;


	void Update(){
		if (energy <= 0) {
			if(!statsent)
			{
			anim.SetTrigger ("morir");
			asource.PlayOneShot (death);
				statsent = true;
				ctrescena.registrarFin ();
			}
		}/*
		slider.value = energy;
		txt.text = energy.ToString ();*/
	}

	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		asource = GetComponent<AudioSource> ();
		ctrescena = GetComponent<controlEscena> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 v = new Vector2 (vel, 0);
		rgb.velocity=v;

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("walking") && Random.value < 1f / (60f * 3f)) {
			anim.SetTrigger ("apuntar");
		
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("aiming") && Random.value < 1f / 3f) {
			anim.SetTrigger ("disparar");
			if(!asource.isPlaying)
			asource.PlayOneShot (shoot);// necesito asegurarme que lo haga solo una vez
		} 
		else {
			anim.SetTrigger ("caminar");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Flip ();	
	}
	void Flip(){
		vel *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void Disparar()
	{
		anim.SetTrigger ("apuntar");

	}
	public void EmitirBala()
	{
		GameObject bulletCopy = Instantiate (bulletPrototype);
		bulletCopy.transform.position =new Vector3( transform.position.x,transform.position.y,-1);

		bulletCopy.GetComponent<controlBala> ().direction = new Vector3 (transform.localScale.x, 0, 0);
		energy--;
	}

	public void Dañar(){
		energy -= 20;
		int temp = -20;
		InstanciarRetroalimentacionEnergia (temp);
	}
	private void InstanciarRetroalimentacionEnergia(int incremento) {
		GameObject retroalimentcionGO = null;
		if (retroalimentacionSpawnPoint!=null)
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
		else
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);

		retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
	}
}
