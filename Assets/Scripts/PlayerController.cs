﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 1;
    public Camera cam;
    

    private bool collided = false;
    private Vector2 lastPos;

    //SPAWNING
    private GameObject player;
    public Transform playerSpawn;

    //HEALTH
    public float max_health = 20;
    private float current_health;
    public Slider healthBar;

    //SHOOTING
    private int gun = 8;
    private int numOfBullets = 1;
 
    public GameObject standardBullet;
    public GameObject bulletSpawn;
    private float lastShot = 0;
    public float fireRate = .5f;


    // Use this for initialization
    void Start()
    {
        player = gameObject;
        current_health = max_health;
        healthBar.value = calculateHealth();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void respawn()
    {
        
        player.SetActive(false);
        rb.transform.position = playerSpawn.position;
        //rb.constraints = RigidbodyConstraints2D.FreezePosition;
        current_health = max_health;
        healthBar.value = calculateHealth();
        player.SetActive(true);
       // rb.constraints = RigidbodyConstraints2D.None;
    
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


 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision.gameObject.tag.Contains("Bullet"))
        {
            --current_health;
            healthBar.value = calculateHealth();
            if (current_health <= 0)
            {
                current_health = 0;
                respawn();
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
        
    
        aim();
        move();


    }

    float calculateHealth()
    {
        return current_health / max_health;
    }


    public void changeGunNumber(int g)
    {
        gun = g;
        Debug.Log("Gun is now : " + gun);
    }

    public int getGunNumber()
    {
        return gun;
    }

}
