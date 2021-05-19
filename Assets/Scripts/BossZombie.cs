using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombie : MonoBehaviour
{







	Vector3 localScale;



	
	public GameObject healthBar;
	public GameObject bloodParticle;
	public AudioSource audioSource;
	public AudioClip bloodClip;
	public GameObject bone;
	public GameObject boneSpawner;

	public float timeToShootBone = 3f;
	private float shootTime;
	// Start is called before the first frame update

	// Use this for initialization
	void Start()
	{
		
		shootTime = timeToShootBone;
		
		

	}

	// Update is called once per frame
	void Update()
	{
		shootTime -= Time.deltaTime;

		if (shootTime <= 0f)
		{
			StartCoroutine(ShootBone());
			shootTime = timeToShootBone;
		}
	
	}

	

	
	

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("bullet"))  // Boss Hiting With Bullet
		{

			Vector3 healthBarScale = healthBar.transform.localScale;
			healthBarScale.x -= 0.001f;
			healthBar.transform.localScale = healthBarScale;
			Instantiate(bloodParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

			if (healthBar.transform.localScale.x <= 0f)
			{
				FindObjectOfType<GameManager>().zombieCount--;
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
	IEnumerator ShootBone()
    {
		yield return new WaitForSeconds(0.6f);
      
			Instantiate(bone, boneSpawner.transform.position, Quaternion.identity);
			
		
	
	}
}
