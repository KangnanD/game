using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AIZombie : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject parentObject;
    public GameObject bloodParticle;
    public AudioSource audioSource;
    public AudioClip bloodClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("bullet"))
        {
            Vector3 healthBarScale = healthBar.transform.localScale;
            healthBarScale.x -= 0.1f;
            healthBar.transform.localScale = healthBarScale;
            Instantiate(bloodParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            
            if (healthBar.transform.localScale.x <= 0f)
            {
                Destroy(parentObject);
                FindObjectOfType<GameManager>().zombieCount--;
                FindObjectOfType<GameManager>().CheckZombieCount();
                GameManager.playerScore++;
                FindObjectOfType<GameManager>().UpdatePlayerScore();
            }
            Destroy(collision.gameObject);
            audioSource.clip = bloodClip;
            audioSource.Play();
        }
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))  //moving zombie 
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,collision.gameObject.transform.position,0.5f*Time.deltaTime);
        }
    }

}
