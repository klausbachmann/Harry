using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HarryController : MonoBehaviour
{

    public Text text;
    public Slider slider;
    public AudioSource audio;
    public GameObject[] Bullets;
    public GameObject[] HarryLives;
    public int bulletsInWeapon = 5;
    public GameObject Blood;

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
    public float reloadTime = 1;
    public int health = 100;
    private int lives = 3;


    // Harry wird getroffen
    public void damage(int damage)
    {
        if (!animator.GetBool("isDead"))
        {
            health -= damage;
            float sl = (health / 100f);
            slider.value = sl;

            // Bloodsplatt
            GameObject blood = Instantiate(Blood, transform.position, Quaternion.identity);
            Destroy(blood, 1f);

            if (health <= 0)
            {
                animator.SetBool("isDead", true);
                decreaseLives();
            }
        }
    }

    // Wenn health <= 0, dann wird diese Funktion aus der Animation aufgerufen
    public void reanimateHarry() {
        health = 100;
        animator.SetBool("isDead", false);
        if (lives == 0) {
            SceneManager.LoadScene("gameover");
        }
    }

    public void decreaseLives() {       
        HarryLives[lives - 1].SetActive(false);
        lives--;
        
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        text.text = "Score: 0";

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        bool f = CrossPlatformInputManager.GetButton("Fire1");
        animator.SetFloat("walk", h);
        animator.SetBool("fire", f);



        // Sprite flippen, wenn Harry links oder rechts geht
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") <= -0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") >= 0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            //text.text = "Idle";
        }

        // Harry bewegen
        Vector2 xPos = new Vector2(Mathf.Clamp(transform.position.x + CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed, -5, 95), 0);
        transform.position = xPos;


        // Muzzleflash je nach Ausrichtung anpassen
        Vector3 actualPosition;
        Vector3 actualRotation;
        Vector2 shootDirection;

        // Abfeuern
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if (time > offset && bulletsInWeapon > 0)
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


                // Patronenanimation
                kickBullet();

                // Feuersound abspielen
                audio.Play();

                // Muzzleflash erzeugen und zerstoeren
                GameObject e = Instantiate(muzzleFlash, actualPosition, Quaternion.Euler(actualRotation));
                Destroy(e, 0.05f);


                // Projektil erzeugen und abfeuern
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


        // Reload
        if (CrossPlatformInputManager.GetButton("Fire2"))
        {
            for (int i = 0; i < 5; i++)
            {
                Bullets[i].SetActive(true);
            }
            bulletsInWeapon = 5;
            offset = time + reloadTime;
            GameObject.Find("ReloadButton").GetComponent<Animator>().SetBool("gunEmpty", false);
        }

    }

    // Bullets rauswerfen
    void kickBullet()
    {
        Bullets[bulletsInWeapon - 1].SetActive(false);
        bulletsInWeapon--;
        if (bulletsInWeapon == 0) {
            GameObject.Find("ReloadButton").GetComponent<Animator>().SetBool("gunEmpty", true);
        }
    }

    public void increaseHealth(int healthPoints) {
        Debug.Log("HEALTH OLD: " + health);
        health = Mathf.Clamp(health + healthPoints, 0, 100);

        Debug.Log("HEALTH NEW: " + health);
        float sl = (health / 100f);
        slider.value = sl;
    }
}
