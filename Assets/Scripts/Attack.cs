using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Zombie.isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Zombie.isAttacking = false;
        }
    }
}
