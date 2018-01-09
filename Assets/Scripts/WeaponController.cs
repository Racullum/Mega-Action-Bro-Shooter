using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public int gunNumber = 1;
    private float lastShot = 0;
    public float fireRate = .1f;
    public float power = 100f;
    private AudioSource source;
    public AudioClip gun_shot;  

    private Transform bulletSpawn;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        bulletSpawn = transform.Find("ProjectileSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastShot)
        {
            source.PlayOneShot(gun_shot);
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            lastShot = Time.time + fireRate;

            GameObject bullet = ObjectPooler.SharedInstance.getPooledObject("Bullet" + gunNumber.ToString());

            Debug.Log(bullet);
            bullet.transform.position = bulletSpawn.transform.position;
            bullet.transform.rotation = bulletSpawn.transform.rotation;
            bullet.SetActive(true);
            bullet.layer = 9;
            bullet.GetComponent<BulletCont>().startMovement(transform.up * -power);
            //bullet.GetComponent<Bullet1Cont>().startMovement(transform.up * -power);
        }
    }
}
