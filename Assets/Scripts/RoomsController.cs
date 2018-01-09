using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : MonoBehaviour {

    public int numberOfEnemies = 2;
    public GameObject[] enemies;
    public Transform[] enemySpawns;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<StandardEnemyCont>().setPatroling(false);
                enemies[i].GetComponent<StandardEnemyCont>().setCanShoot(true);
                enemies[i].GetComponent<AIPath>().canMove = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<StandardEnemyCont>().setPatroling(true);
                enemies[i].GetComponent<StandardEnemyCont>().setCanShoot(false);
                enemies[i].GetComponent<AIPath>().canMove = false;
                enemies[i].transform.position = enemySpawns[i].position;
                enemies[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

    }
}
