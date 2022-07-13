using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    public Camera fpsCam;
    public ParticleSystem effect;

    public float damage = 10f;
    public float range = 100f;
    public float cooldown = 0.5f;

    private Animator anim;
    private float timeBetweenShoots = 0;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && timeBetweenShoots <= 0)
        {

            Shoot();

        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }

    }

    private void Shoot()
    {
        RaycastHit hit;

        effect.Play();
        anim.SetTrigger("Fire");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        timeBetweenShoots = cooldown;
    }
}
