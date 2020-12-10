using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlPersonaje : MonoBehaviour {
	Rigidbody2D rgb;
	Animator anim;
	AudioSource aSource;
	public float maxVel = 5f;
	bool haciaDerecha = true;
	public Slider slider;
	public Text txtSalud;
	public int energy;
    public GameObject retroalimentacionEnergiaPrefab;
    Transform retroalimentacionSpawnPoint;

	public AudioClip cortandoUnArbol;
	public AudioClip ouch;
	public AudioClip dying;


	public int costoGolpeAlAire = 1;
	public int costoGolpeAlArbol = 3;
	public int premioArbol = 15;
	public int costoBala = 20;

	bool enFire1 = false;

	ControlArbol ctrArbol=null;
	GameObject hacha=null;

	public bool jumping = false;
    public bool isOnTheFloor = false;
	public float yJumpForce = 300;
	Vector2 jumpForce;



	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();
		hacha = GameObject.Find ("/orc/orc_body/orc _R_arm/orc _R_hand/orc_weapon");
		retroalimentacionSpawnPoint = GameObject.Find ("spawnPoint").transform;
		energy = 100;
		jumpForce = new Vector2 (0, 0);
		rgb.freezeRotation = true;
	}

	void Update() {
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("dead")) {
			if (energy <= 0) {
				energy = 0;
				anim.SetTrigger ("die");
				aSource.PlayOneShot(dying);
			}
		} else
			return;

		if (Mathf.Abs (Input.GetAxis ("Fire1")) > 0.01f) {
			//Debug.Log ("En Fire1");
			if (enFire1 == false) {
				enFire1 = true;
				hacha.GetComponent<BoxCollider2D>().enabled = false;
				anim.SetTrigger ("attack");
				if (ctrArbol != null) {
					if( ctrArbol.GolpeOrco () ) {
                        //Cambio por el metodo de incrementar energia
					    IncrementarEnergia(premioArbol);
						if( energy > 100 )
							energy = 100;
					}
					else {
                        //Cambio por el metodo de incrementar energia
                        IncrementarEnergia(costoGolpeAlArbol * -1);
						aSource.PlayOneShot(cortandoUnArbol);
					}
				}
				else {
                    //Cambio por el metodo de incrementar energia
                    IncrementarEnergia(costoGolpeAlAire * -1);
				}
				// Esto es mejor en la salida del estado Attack
				//hacha.GetComponent<CircleCollider2D>().enabled = true;
			}
		} else {
			enFire1 = false;
		}

        // La energia del slider nunca es menor que 0
	    if (energy < 0)
	        energy = 0;

		slider.value = energy;
		txtSalud.text = energy.ToString ();

	}

    private void IncrementarEnergia(int incremento) {
        energy += incremento;
        InstanciarRetroalimentacionEnergia(incremento);
    }

    private void InstanciarRetroalimentacionEnergia(int incremento) {
        GameObject retroalimentcionGO = null;
        if (retroalimentacionSpawnPoint!=null)
            retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
        else
            retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);

        retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
    }

	void FixedUpdate () {
        // Cambio de los metodos para verificar el input
	    if (energy > 0) {
            VerificarInputParaCaminar();
            VerificarInputParaSaltar();   
	    }
	}

    private void VerificarInputParaCaminar() {
        float v = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0, rgb.velocity.y);
        v *= maxVel;
        vel.x = v;
        rgb.velocity = vel;
        Debug.DrawLine(transform.position,
                        transform.position + new Vector3(vel.x, vel.y));
        //anim.SetFloat ("speed", vel.x);

        if (haciaDerecha && v < 0)
        {
            haciaDerecha = false;
            Flip();
        }
        else if (!haciaDerecha && v > 0)
        {
            haciaDerecha = true;
            Flip();
        }
    }

    private void VerificarInputParaSaltar() {
        // Nueva variable agregada para corregir el error del salto infinito
        isOnTheFloor = rgb.velocity.y == 0;

        if (Input.GetAxis("Jump") > 0.01f )// ||
            // Nueva tecla para el salto
            //Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ahora solo puede saltar si la velocidad del RGB es 0 en su componente 'y' es decir que no este cayendo o subiendo.
            if (!jumping && isOnTheFloor)
            {
                jumping = true;
                jumpForce.x = 0f;
                jumpForce.y = yJumpForce;
                rgb.AddForce(jumpForce);
            }
        }
        else
            jumping = false; // por el momento... esto debe ser despu'es de terminar la animaci'on
    }

    void Flip() {
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	public void SetControlArbol( ControlArbol ctr ) {
		ctrArbol = ctr;
	}

	public void RecibirBala() {
        //Cambio por el metodo de incrementar energia
        IncrementarEnergia(costoBala * -1);
		aSource.PlayOneShot (ouch);
	}

	public void activarColliderHacha() {
		GameObject hacha = GameObject.Find ("/orc/orc_body/orc _R_arm/orc _R_hand/orc_weapon");
		hacha.GetComponent<BoxCollider2D>().enabled = true;
	}
}
