using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 1;
    public Camera cam;

    private bool collided = false;
    private Vector2 lastPos;

    //HEALTH
    public int health = 5;

    //SHOOTING
    private int gun = 8;
    private int numOfBullets = 1;
    private Bullet8Cont bullet8Cont;
    public GameObject standardBullet;
    public GameObject bulletSpawn;
    private float lastShot = 0;
    public float fireRate = .5f;

    // Use this for initialization
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    void move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal != 0) || (vertical != 0))
        {
            if (1 == 1)
            {
                rb.AddForce(new Vector2(speed * horizontal, speed * vertical));
                
            }
           
            if(collided)
            {
              
                
            }
            lastPos = transform.position;
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
       
    }

    void aim()
    {
        var mousePosition = Camera.allCameras[1].ScreenToWorldPoint(Input.mousePosition);
        
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition,
                                                -Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rb.angularVelocity = 0;
    }

    void shoot()
    {
       
       
        if (Input.GetMouseButtonDown(0) && Time.time > lastShot)
        {
            
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            lastShot = Time.time + fireRate;

            //WE NEED TO CHANGE THE OBJECT POOLER TO RETURN AN ARRAY OF POOLED OBJECTS
            GameObject[] bullets = ObjectPooler.SharedInstance.getPooledObjects("Bullet" + gun.ToString(), 2).ToArray();


            if (bullets.Length != 0)
            {
                
                    bullets[0].transform.position = bulletSpawn.transform.position;
                    bullets[0].transform.rotation = gameObject.transform.rotation;
                    bullets[0].transform.Rotate(bullets[0].transform.rotation.x, bullets[0].transform.rotation.y, (bullets[0].transform.rotation.z + -90f));

                    bullets[0].SetActive(true);
                    bullets[0].layer = 9;

                    bullets[1].transform.position = new Vector3(bulletSpawn.transform.position.x + .10f, bulletSpawn.transform.position.y, bulletSpawn.transform.position.z);

                    bullets[1].transform.rotation = gameObject.transform.rotation;
                    bullets[1].transform.Rotate(bullets[0].transform.rotation.x, bullets[0].transform.rotation.y, (bullets[0].transform.rotation.z + -90f));

                    bullets[1].SetActive(true);
                    bullets[1].layer = 9;
                






                Vector2 v = bulletSpawn.transform.up;
                Debug.Log("Before rotation: " + bulletSpawn.transform.up);
                //ROTATE THE UP VECTOR 45 DEGREES
                Quaternion rotation = Quaternion.Euler(0, 0, 45);
                v = rotation * v;

                Vector2 v2 = bulletSpawn.transform.up;
                Debug.Log("Before rotation: " + bulletSpawn.transform.up);
                //ROTATE THE UP VECTOR 45 DEGREES
                Quaternion rotation2 = Quaternion.Euler(0, 0, -45);
                v2 = rotation2 * v2;

                Debug.Log("After rotation: " + v);

                // DEPENDING ON WHICH GUN WE HAVE WE'LL NEED TO USE THE APPROPRIATE SCRIPT
                // NOTE: THIS SEEMS REDUNDANT TRY AND FIND A WAY TO REDUCE THIS
                switch (gun)
                {
                    case 4:
                        bullets[0].GetComponent<Bullet4Cont>().startMovement(-bulletSpawn.transform.up * 50);
                        break;
                    case 8:
                        bullets[0].transform.Rotate(0, 0, 45);
                        bullets[0].GetComponent<Bullet8Cont>().startMovement(-v * 50);
                        bullets[1].transform.Rotate(0, 0, 45);
                        bullets[1].GetComponent<Bullet8Cont>().startMovement(-v2 * 50);
                        break;

                    default:
                        bullets[0].GetComponent<Bullet8Cont>().startMovement(-bulletSpawn.transform.up * 50);
                        break;
                }
                
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //CHECKING TO SEE IF WE HAVE RAN INTO A WEAPON
        
        
        collided = true;
        if(collision.gameObject.tag.Equals("StandardBullet"))
        {
            --health;
            if(health <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collided = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse isn't broken");
        }
        shoot();
       
        aim();
        move();


    }

    // Update is called once per frame
    void FixedUpdate()
    {

            
        
    }

    public void changeGun(int g)
    {
        gun = g;
        
    }

}
