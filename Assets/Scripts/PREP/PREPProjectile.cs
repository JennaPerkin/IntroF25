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

    [Header("Animation")]
    public Animation anim;
    public AnimationClip shoot;

    //Cam Shake
    [Header("Cam Shake")]
    public PREPShakingCam shakingScript;
    public float shakeDuration;
    public float shakeIntensity;

    [Header("Particles")]
    public ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        //fire = GetComponent<ParticleSystem>();
        anim = GetComponent<Animation>();
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

        shakingScript.ShakeCamera(shakeDuration, shakeIntensity);
        anim.Play("Firing");
        fire.Play();
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
                Debug.Log("Enemy Hit");
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