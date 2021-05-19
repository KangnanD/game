using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	float dirX;

	[SerializeField]
	float moveSpeed = 3f;

	Rigidbody2D rb;

	bool facingRight = false;

	Vector3 localScale;

	public static bool isAttacking = false;

	Animator anim;
	public GameObject healthBar;
	public GameObject bloodParticle;
	public AudioSource audioSource;
	public AudioClip bloodClip;
	public ZombieSpawner zombieSpawner;
	// Start is called before the first frame update

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D> ();
		dirX = -1f;

        anim = GetComponent<Animator>();
		zombieSpawner = FindObjectOfType<ZombieSpawner>();

	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < -9f)
			dirX = 1f;
		else if (transform.position.x > 9f)
			dirX = -1f;

		if (isAttacking)
			anim.SetBool ("zombieattack", true);  //attacking zombie
		else
			anim.SetBool ("zombieattack", false);
			
	}

	void FixedUpdate()
	{
        if (!isAttacking)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);  //Moving Zombie
        else
            rb.velocity = Vector2.zero;
	}

	void LateUpdate()
	{
		CheckWhereToFace();
	}

	void CheckWhereToFace()  //
	{
		if (dirX > 0)
			facingRight = true;
		else if (dirX < 0)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag.Equals("bullet"))  //Zombie Hitting With Bullet
		{

			Vector3 healthBarScale = healthBar.transform.localScale;
			healthBarScale.x -= 0.1f;
			healthBar.transform.localScale = healthBarScale;
			Instantiate(bloodParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

			if (healthBar.transform.localScale.x <= 0f)
			{
				FindObjectOfType<GameManager>().zombieCount--;
				if (FindObjectOfType<GameManager>().zombieCount > 0)
				{
					zombieSpawner.isSpawnZombie = true;
				}
				FindObjectOfType<GameManager>().CheckZombieCount();
				GameManager.playerScore++;
				FindObjectOfType<GameManager>().UpdatePlayerScore();
				Destroy(this.gameObject);

			}
			Destroy(collision.gameObject);
			audioSource.clip = bloodClip;
			audioSource.Play();
		}
	}
    private void OnCollisionEnter2D(Collision2D collision)
	{
		

        if (collision.gameObject.tag.Equals("Player"))
        {
			isAttacking = true;
		}
        else
		{
			isAttacking = false;

		}
	}

}
