using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D myRB;
    public float bulletMoveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>().transform.localScale.x < 0f)
        {
            myRB.AddForce(new Vector2(-1f, 0f) * bulletMoveSpeed,ForceMode2D.Impulse);
        }
        else
        {
            myRB.AddForce(new Vector2(1f, 0f) * bulletMoveSpeed,ForceMode2D.Impulse);
        }

      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

   
}
