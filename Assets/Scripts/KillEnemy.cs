using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    /*EnemyAI enemyScript;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
            enemyScript = collision.gameObject.GetComponent<EnemyAI>();
            enemyScript.health--;
        }
    }*/

    public void OnCollisionEnter(Collision collisionObj)
    {
        if (collisionObj.gameObject.tag != "Enemy" && collisionObj.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
