using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public GameObject[] spawnPoints;
    private int randomPoint;
    public bool isSpawnZombie;
    public bool isLevelThree;
    public bool isLevelFour;
    private float timeToSpawnZombie;
    public float zombieSpawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (isLevelFour)
        {
            timeToSpawnZombie = zombieSpawnTime;
        }
     
        isSpawnZombie = true;
       
    }
    private void Update()
    {
        if (isLevelThree)
        {
            if (FindObjectOfType<GameManager>().zombieCount > 0)
            {
                StartCoroutine(SpawnZombieEveryThreeSec());
            }
        }
        else if (isLevelFour)
        {
            if (FindObjectOfType<GameManager>().zombieCount > 0)
            {   
                StartCoroutine(SpawnZombieEveryTwoSec());
              
            }
            timeToSpawnZombie -= Time.deltaTime;
            
            if (timeToSpawnZombie <= 0f)
            {
                isSpawnZombie = true;
                StartCoroutine(SpawnZombieEveryTwoSec());
                timeToSpawnZombie = zombieSpawnTime;
            }
        }
    }
    public void SpawnZombie()  //Zombie Spawning
    {
        randomPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(zombie, spawnPoints[randomPoint].transform.position,Quaternion.identity);
    }
  
    public IEnumerator SpawnZombieEveryThreeSec() //Zombie Spawning after 3 sec
    {
        yield return new WaitForSeconds(1.5f);
        if (isSpawnZombie && isLevelThree)
        {
            SpawnZombie();
            isSpawnZombie = false;
        }
    }
    public IEnumerator SpawnZombieEveryTwoSec()//Zombie Spawning after 2 sec 
    {
        yield return new WaitForSeconds(3f);
        if (isSpawnZombie && isLevelFour)
        {
            SpawnZombie();
            isSpawnZombie = false;
        }
       
        
    }



}
