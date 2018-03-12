using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class HarryController : MonoBehaviour {

    public Text text;
    public Slider slider;
    public AudioSource audio;
    public GameObject[] Bullets;
    public int bulletsInWeapon = 5;


    public float moveSpeed = 2;
    private float h;
    private Animator animator;
    public GameObject Explosion;

    public GameObject muzzleFlash;

    public Vector3 muzzlePosition;

    public float fireRate = 2;
    private float offset = 2;
    private float time;
    public float score;

    public int health = 100;

    public void damage(int damage) {
        health -= damage;
        float sl = (health / 100f);
        slider.value = sl;
        Debug.Log(health + " " + sl);
    }


    void Start () {
        animator = GetComponent<Animator>();
        text.text = "Score: 0";
        
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        bool f = CrossPlatformInputManager.GetButton("Fire1");
        animator.SetFloat("walk", h);
        animator.SetBool("fire", f);



		// Sprite flippen, wenn Harry links oder rechts geht
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") <= -0.1f){
            GetComponent<SpriteRenderer>().flipX = true;
        } else if(CrossPlatformInputManager.GetAxisRaw("Horizontal") >= 0.1f){ 
			GetComponent<SpriteRenderer>().flipX = false;
		} else {
            //text.text = "Idle";
        }

        // Harry bewegen
        Vector2 xPos = new Vector2(Mathf.Clamp(transform.position.x + CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed, -5, 95), 0);
        //transform.Translate(xPos, 0, 0);
        transform.position = xPos;



        // Muzzleflash je nach Ausrichtung anpassen
        Vector3 actualPosition;
        Vector3 actualRotation;
        Vector2 shootDirection;

        // Abfeuern
        if (CrossPlatformInputManager.GetButton("Fire1")) {
            if (time > offset && bulletsInWeapon>0)
            {
                if (!GetComponent<SpriteRenderer>().flipX)
                {
                    actualPosition = new Vector3(transform.position.x + 2, 0.3f, transform.position.z);
                    actualRotation = new Vector3(0, 0, -90);
                    shootDirection = Vector2.right;
                }
                else
                {
                    actualPosition = new Vector3(transform.position.x - 2, 0.3f, transform.position.z);
                    actualRotation = new Vector3(0, 0, 90);
                    shootDirection = Vector2.left;
                }


                kickBullet();
                audio.Play();
                GameObject e = Instantiate(muzzleFlash, actualPosition, Quaternion.Euler(actualRotation));
                Destroy(e, 0.05f);

                

                RaycastHit2D hit2D;
                hit2D = Physics2D.Raycast(actualPosition, shootDirection, 12);
                if (hit2D.collider != null)
                {
                    Debug.Log(hit2D.collider.gameObject.name);
                    EnemyController enemyController = hit2D.collider.gameObject.GetComponent<EnemyController>();
                    score += enemyController.hit();
					text.text = "Score: " + score;
                }

                offset = time + fireRate;
            }
        }

        if (CrossPlatformInputManager.GetButton("Fire2")) {
            for (int i = 0; i < 5; i++) {
                Bullets[i].SetActive(true);
            }
            bulletsInWeapon = 5;
            offset = time + 3;
        }

    }

    void kickBullet() {
        Bullets[bulletsInWeapon - 1].SetActive(false);
        bulletsInWeapon--;
    }
}
