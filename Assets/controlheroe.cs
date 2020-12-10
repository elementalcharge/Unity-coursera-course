using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class controlheroe : MonoBehaviour {
	Rigidbody2D rgb;
	Animator anim;
	SpriteRenderer ren;
	public float maxvel=5f;
	bool haciaDerecha=true;
	controlpisada ctrswitch;
	public Slider slider;
	public Text txt;
	public float energy=100;
	public int costoSalto=20;
	public int costoSalto2=60;
	public bool jumping = false;
	public float yjumpeforce = 250;
	Vector2 jumpforce;
	public float vida=100;
	bool isOnTheFloor;
	public GameObject retroalimentacionEnergiaPrefab;
	public Transform retroalimentacionSpawnPoint;
	public string nameOfLevel;
	public AudioSource asource;

	public AudioClip damaged;
	public AudioClip death;

	public controlEscena ctrescena;
	bool statsent;
	public int touchplatform;

	public void OnCollisionEnter2D(Collision2D other)
	{
		print ("enter funciona");
		if (other.gameObject.layer == 12) 
		{
			touchplatform++;
		}
	}

	public void OnCollisionExit2D(Collision2D other)
	{
		print ("exit funciona");
		if (other.gameObject.layer == 12) 
		{
			touchplatform--;
		}
	}
		
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim=GetComponent<Animator> ();
		ren = GetComponent<SpriteRenderer> ();
		asource = GetComponent<AudioSource> ();
		ctrescena = GetComponent<controlEscena> ();


	}
	void Update()
	{
		slider.value = energy;
		txt.text = energy.ToString ();
	}
	// Update is called once per frame
	void FixedUpdate () {
		float v=Input.GetAxis ("Horizontal");
		Vector2 vel = new Vector2 (0,rgb.velocity.y);
		v *= maxvel;
		vel.x = v;
		rgb.velocity = vel;
		anim.SetFloat ("speed", vel.x);
		anim.SetFloat ("vspeed", vel.y);
		if (haciaDerecha && Input.GetAxis ("Horizontal") < 0f)
			{
			haciaDerecha = false;
			Flip ();
			print ("esta andando horizontal <0");
			} 
		else if (!haciaDerecha && Input.GetAxis ("Horizontal") > 0f) 
			{
			print ("esta andando horizontal >0");
			haciaDerecha = true;
			Flip ();
			}
		VerificarInputSaltar ();
		if(Input.GetButtonDown("Jump"))
		{	
			/*
			if (!jumping && energy>costoSalto && Input.GetAxis("Fire1")<0.1f)  {
				jumping = true;
				jumpforce.x = 0f;
				jumpforce.y = yjumpeforce;
				energy -= costoSalto;
				rgb.AddForce (jumpforce);

				anim.SetTrigger ("saltando");
					
				} 
		else if (!jumping && energy>costoSalto2 && Input.GetAxis("Fire1")>0.1f) {
				energy -= costoSalto2;
				jumpforce.y = yjumpeforce*1.5F;
				rgb.AddForce (jumpforce);
				anim.SetTrigger ("saltando");
				}
				
			else
			{
			  anim.SetTrigger ("cayendo");
			}
			*/
		}
		/*
		if (Input.GetAxis ("Jump") < 0.01f && rgb.velocity.y<0.1f && rgb.velocity.y>-0.1f) 
		{
			jumping = false;
		}*/
		if (energy < 100) 
		{
			energy += 20*Time.deltaTime;
		}
	}

	void Flip(){
		
		ren.flipX = !ren.flipX;
	}
	public void setControlSwitch (controlpisada ctr)
	{
		
		
	}
	public void RecibirDaño(int daño)
	{
		if (vida > 100)
			vida = 100;
		vida -= daño;
		if (daño > 0) 
		{
			asource.PlayOneShot (damaged);
		}
		InstanciarRetroalimentacionEnergia (daño*-1);
		if (vida <= 1.101f) {
			vida = 0;
			print ("asource.PlayOneShot (death);");
			asource.PlayOneShot (death);
			print ("nim.SetTrigger (\"muerto\");");
			anim.SetTrigger ("muerto");
			//SceneManager.LoadScene (nameOfLevel);
			/*if(!statsent)
			{	
				statsent = true;
				ctrescena.registrarFin ();
				
			}*/
			print ("SceneManager.LoadScene (nameOfLevel);");
			SceneManager.LoadScene (nameOfLevel);


		}

	}

	private void VerificarInputSaltar()
	{
	
		isOnTheFloor = rgb.velocity.y == 0f;
		anim.SetBool("isOnFloor",isOnTheFloor);
		if (Input.GetAxis ("Jump") > 0.01f && isOnTheFloor)
		{
		
			if ( touchplatform>0 && energy>costoSalto )
			{
				
					if( energy>costoSalto2 && Input.GetAxis("Fire1")>0.1f)
					{
						energy -= costoSalto2;

						jumping = true;
						jumpforce.x = 0f;
						jumpforce.y = yjumpeforce*1.5f;
						rgb.AddForce (jumpforce);
					}
					else {
				energy -= costoSalto;
				
				jumpforce.x = 0f;
				jumpforce.y = yjumpeforce;
				rgb.AddForce (jumpforce);
					}
			}

		}
		else{
			}
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
