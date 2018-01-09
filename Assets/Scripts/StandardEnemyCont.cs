    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyCont : MonoBehaviour
{
    Rigidbody2D rb;
    Transform object_trans;

    //PATROL
    private int patrolLength = 30;
    private int currentPatrolLength = 0;
    private float patrolSpeed = 1f;
    private bool patroling = true;

    //ANIMATION
    private Animator anim;

    //HEALTH
    public int health = 3;

    //SHOOTING
    public GameObject standardBullet;
    public GameObject bulletSpawn;
    private bool canShoot = false;
    private float lastShot = 0;
    public float fireRate = .5f;
    private AudioSource source;
    public AudioClip gun_shot;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.tag.Contains("Bullet") && collision.gameObject.layer.Equals(9))
        {
            health = health - collision.gameObject.GetComponent<BulletCont>().damage;
            if(health <= 0)
            {
                gameObject.SetActive(false);
            }
           
        }
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        gameObject.GetComponent<AIPath>().canMove = false;
        object_trans = GetComponent<Transform>();
        rb = gameObject.GetComponent<Collider2D>().attachedRigidbody;
        
    }

    void patrol()
    {
        if(currentPatrolLength < patrolLength)
        {
            rb.velocity = object_trans.up * patrolSpeed;
            currentPatrolLength++;
        }
        else
        {
            currentPatrolLength = 0;
            object_trans.Rotate(0, 0, 180);
            //patrolSpeed = -patrolSpeed;
        }
    }

    void shoot()
    {
        if (Time.time > lastShot)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            lastShot = Time.time + fireRate;
            GameObject bullet = ObjectPooler.SharedInstance.getPooledObject(standardBullet.tag.ToString());
            if (bullet != null)
            {
               source.PlayOneShot(gun_shot);
                bullet.transform.position = bulletSpawn.transform.position;
                bullet.transform.rotation = gameObject.transform.rotation;
                bullet.transform.Rotate(bullet.transform.rotation.x, bullet.transform.rotation.y, (bullet.transform.rotation.z + 90f));

                bullet.SetActive(true);
                bullet.layer = 10;
                bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.transform.up * 50);
                source.PlayOneShot(gun_shot);

            }

        }


    }

    private void FixedUpdate()
    {

        if (patroling)
        {
            patrol();
        }
    }

    public void setPatroling(bool b)
    {
        patroling = b;
    }

    public void setCanShoot(bool b)
    {
        canShoot = b;
    }

    

    // Update is called once per frame
    void Update()
    {

       
        Debug.DrawRay(object_trans.position, object_trans.up);
        RaycastHit2D hit = Physics2D.CircleCast(object_trans.position, .3f,
                                                object_trans.up, Mathf.Infinity, 256);
        if (hit.collider != null)
        {
            if(canShoot)
            {
                shoot();
            }
            
            //Debug.Log("We hit something");
            if (!patroling)
            {

                anim.SetBool("running", true);
                // gameObject.GetComponent<AIPath>().canMove = true;
                //Debug.Log("Detected player");
            }
        }
    }
}
