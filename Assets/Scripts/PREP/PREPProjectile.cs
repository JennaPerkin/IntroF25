using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PREPProjectile : MonoBehaviour
{
    //Objects
    [Header("Objects")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject shotObject;
    public Transform player;
    public LayerMask includeField;

    //Limits
    [Header("Limits")]
    public int totalShots;
    public float shotCooldown;

    //Shoot Variables
    [Header("Shoot Variables")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public float shootForce;
    public float shootUpwardForce;

    bool readyToShoot;

    //Audio
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pop;

    //Cam Shake
    [Header("Cam Shake")]
    //public CameraShake shakingScript;
    public float shakeDuration;
    public float shakeIntensity;

    [Header("Particles")]
    ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        fire = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && readyToShoot && totalShots > 0) //&& !PauseMenu.isPaused)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //spawn object
        GameObject cube = Instantiate(shotObject, attackPoint.position, cam.rotation);

        //get Rigidbody
        Rigidbody projectileRb = cube.GetComponent<Rigidbody>();

        //calculate direction
        Vector3 forceDirection = player.transform.forward;

        RaycastHit hit;
        PREPEnemyAI enemyScript;

        if (Physics.Raycast(player.position, player.forward, out hit, 500f, includeField))
        {
            Debug.Log("Hit Something");
            forceDirection = (hit.point - attackPoint.position).normalized;

            if (hit.transform.tag == "Enemy")
            {
                enemyScript = hit.transform.GetComponent<PREPEnemyAI>();
                enemyScript.health--;
            }
        }
        else 
        {
            Debug.Log("Hit Nothing");
            forceDirection = player.forward;
        }

        //shakingScript.ShakeCamera(shakeDuration, shakeIntensity);
        audioSource.PlayOneShot(pop);
        //fire.Play();

        //force to add
        Vector3 forceToAdd = forceDirection * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalShots--;

        Invoke(nameof(ResetShot), shotCooldown);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}