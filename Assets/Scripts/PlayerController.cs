using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myBody;
   // private Animator myAnim;
    private float movHorizontal;
    public float moveSpeed = 5f;
    public float jumpSpeed = 400f;
    private bool isGrounded;
    private bool isFacingRight;
    public GameObject bullet,assultBullet;
    public GameObject bulletSpawner,bulletSpawnerAssault;
    public float fireRate = 0.5f;
    public float nextFire = 0f;
    public AudioClip shotClip;
    public AudioSource audioSource;
    public Slider healthSlider;
    private float mouseDelta;
    public GameObject weaponOne, weaponTwo,weaponThree,weaponFour,weaponFive;
    private bool isScrolledMouse;
    public bool isLevelFive;
    private int weaponNo;

    // Start is called before the first frame update
    void Start()
    {
        weaponNo = 1;
        isFacingRight = true;
        isGrounded = true;
        myBody = GetComponent<Rigidbody2D>();
       // myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

     

        if (Input.GetAxis("Fire1") > 0)
        {
            switch (weaponNo)
            {
                case 1:


                    FireBulletByGun();

                    break;
                case 2:

                    FireBulletByAssault();

                    break;
                case 3:

                    FireBulletByAssault();

                    break;
                case 4:

                    FireBulletByAssault();

                    break;
                case 5:

                    FireBulletByAssault();

                    break;
            }
        }
      

       
        if (!isLevelFive)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                weaponOne.SetActive(true);
                weaponTwo.SetActive(false);
                weaponNo = 1;
                fireRate = 0.1f;
             
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                weaponOne.SetActive(false);
                weaponTwo.SetActive(true);
                weaponNo = 2;
                fireRate = 0.5f;
            
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                weaponOne.SetActive(true);
                weaponTwo.SetActive(false);
                weaponFour.SetActive(false);
                weaponFive.SetActive(false);
                weaponThree.SetActive(false);
                weaponNo = 1;
                fireRate = 0.1f;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                weaponOne.SetActive(false);
                weaponTwo.SetActive(true);
                weaponFour.SetActive(false);
                weaponFive.SetActive(false);
                weaponThree.SetActive(false);
                weaponNo =2;
                fireRate = 0.5f;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                weaponOne.SetActive(false);
                weaponTwo.SetActive(false);
                weaponFour.SetActive(false);
                weaponFive.SetActive(false);
                weaponThree.SetActive(true);
                weaponNo = 3;
                fireRate = 0.3f;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                weaponOne.SetActive(false);
                weaponTwo.SetActive(false);
                weaponThree.SetActive(false);
                weaponFive.SetActive(false);
                weaponFour.SetActive(true);

                weaponNo = 4;
                fireRate = 0.2f;
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                weaponOne.SetActive(false);
                weaponTwo.SetActive(false);
                weaponThree.SetActive(false);
                weaponFour.SetActive(false);
                weaponFive.SetActive(true);
                weaponNo = 5;
                fireRate = 0.4f;
            }
        }
      
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)   //Jumping
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpSpeed));
            
        }
        movHorizontal = Input.GetAxis("Horizontal");
        myBody.velocity = new Vector2(movHorizontal *moveSpeed,myBody.velocity.y);  // Moving Forward or backword
        if (movHorizontal > 0 && !isFacingRight)
        {
            FlipCharacter();
           // myAnim.SetBool("run", true);
        }
        if (movHorizontal < 0 && isFacingRight)
        {
            FlipCharacter();
           // myAnim.SetBool("run", true);
        }
        if (movHorizontal == 0)
        {
           // myAnim.SetBool("run", false);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag.Equals("Zombie"))  //Player Health Decreasing By Zombie Attack
        {
            healthSlider.value -= 1;
            if (healthSlider.value <= 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<GameManager>().GameFailed();
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag.Equals("Zombie"))    //Player Health Decreasing By Zombie Attack
        {
            healthSlider.value -= 1;
            if (healthSlider.value <= 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<GameManager>().GameFailed();  // Check Game Failed
            }
        }
        if (collision.gameObject.tag.Equals("Bone"))  //Bone Attack Of Boss Zombie
        {
            Destroy(collision.gameObject);
            healthSlider.value -= 3;
            if (healthSlider.value <= 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<GameManager>().GameFailed();
            }
        }
    }


    public void FlipCharacter() //Change Chracter Side
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void FireBulletByGun() // Shooting By First Weapon
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (isFacingRight)
            {
                Instantiate(bullet, bulletSpawner.transform.position, Quaternion.Euler(new Vector3(0f,0f,90f)));
          
            }
            else if (!isFacingRight)
            {
                Instantiate(bullet, bulletSpawner.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 90f)));
            }
            audioSource.clip = shotClip;
            audioSource.Play();
        }
        }
    public void FireBulletByAssault()// Shooting By Second Weapon
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (isFacingRight)
            {
                Instantiate(assultBullet, bulletSpawnerAssault.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 90f)));

            }
            else if (!isFacingRight)
            {
                Instantiate(assultBullet, bulletSpawnerAssault.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 90f)));
            }
            audioSource.clip = shotClip;
            audioSource.Play();
        }
    }

}
