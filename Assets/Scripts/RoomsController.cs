using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : MonoBehaviour {


    public GameObject enemy;
    public Transform enemySpawn;

    private Transform initialTransform;
	// Use this for initialization
	void Start () {
        initialTransform = enemy.transform;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.GetComponent<StandardEnemyCont>().setPatroling(true);
        enemy.GetComponent<StandardEnemyCont>().setCanShoot(false);
        enemy.GetComponent<AIPath>().canMove = false;
        Debug.Log(initialTransform.position);
        enemy.transform.position = enemySpawn.position;
        enemy.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
