using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private Rigidbody2D myBody;
    private PlayerController playerController;
    private BossZombie bossZombie;
    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        bossZombie = FindObjectOfType<BossZombie>();
        myBody.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
  
}
