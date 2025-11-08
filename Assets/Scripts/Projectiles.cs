using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    [Header("Objects")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject shotObject;
    public Transform player;
    public LayerMask includeFields;

    [Header("Limits")]
    public int totalShots;
    public float shotCooldown;
    public float reloadCooldown;

    [Header("Shoot Variables")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public float shootForce;
    public float shootUpwardForce;
    bool readyToShoot;
    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && readyToShoot && totalShots > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            totalShots = 10;
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        GameObject projectile = Instantiate(shotObject, attackPoint.position, cam.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = player.transform.forward;

        RaycastHit hit;
        EnemyAI enemyScript;

        if (Physics.Raycast(player.position, player.forward, out hit, 500f, includeFields))
        {
            Debug.Log("Hit Something");
            forceDirection = (hit.point - attackPoint.position).normalized;

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Hit");
                enemyScript = hit.transform.GetComponent<EnemyAI>();
                enemyScript.health--;
            }
        }
        else
        {
            Debug.Log("Hit Nothing");
            forceDirection = player.forward;
        }

        Vector3 forceToAdd = forceDirection * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalShots--;

        if (totalShots == 0)
        {
            Invoke(nameof(Reload), reloadCooldown);
        }

        Invoke(nameof(ResetShot), shotCooldown);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        totalShots = 10;
        readyToShoot = true;
    }
}
