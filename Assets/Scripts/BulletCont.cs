using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCont : MonoBehaviour {

    public GameObject bulletSpawn;
    private Rigidbody2D rb;
    private PlayerController pc;
    public int damage = 1;
    // Use this for initialization
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

    }

    public int getDamage()
    {
        return damage;
    }

    public void startMovement(Vector2 vec)
    {
        // The way the sprite is drawn means that it needs to rotate -90 degrees to line up with 
        // Unity's 2d system
        transform.Rotate(0, 0, -90);
        GetComponent<Rigidbody2D>().AddForce(vec);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("The bullet just hit " + collision.gameObject.tag);
        gameObject.SetActive(false);
    }

    private bool isOutOfBounds()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if ((pos.x > Screen.width) || (pos.y > Screen.height))
        {
            return true;
        }
        else if ((pos.x < 0) || (pos.y < 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isOutOfBounds())
        {
            gameObject.SetActive(false);
        }

    }
}
