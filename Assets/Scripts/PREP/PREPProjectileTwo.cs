using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PREPProjectileTwo : MonoBehaviour
{

    public GameObject projectile;

    public float shootForce, upwardForce;

    public float shootingCooldown, spread, shotCooldown;
    public int quantity;

    int projectilesLeft, projectilesShot;

    bool shooting, readyToShoot, reloading, allowInvoke;

    public Camera cam;
    public Transform attackPoint;

    private void Awake()
    {
        projectilesLeft = quantity;
        readyToShoot = true;
    }

    void Update()
    {
        FireInput();
    }

    private void FireInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && projectilesLeft > 0)
        {
            projectilesShot = 0;

            Fire();
        }
    }

    private void Fire()
    {
        readyToShoot = false;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentProjectile = Instantiate(projectile, attackPoint.position, Quaternion.identity);

        currentProjectile.transform.forward = directionWithSpread.normalized;

        currentProjectile.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentProjectile.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        projectilesLeft--;
        projectilesShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot", shootingCooldown);
            allowInvoke = false;
        }
        ResetShot();
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
