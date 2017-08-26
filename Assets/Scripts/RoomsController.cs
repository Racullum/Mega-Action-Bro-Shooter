using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : MonoBehaviour {


    public GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            enemy.GetComponent<StandardEnemyCont>().setPatroling(false);
            enemy.GetComponent<StandardEnemyCont>().setCanShoot(true);
            enemy.GetComponent<AIPath>().canMove = true;
        }
    }
}
