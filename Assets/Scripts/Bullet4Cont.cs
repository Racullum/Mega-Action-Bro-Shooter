using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet4Cont : MonoBehaviour {

   
    public GameObject explosionPrefab;
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        Debug.Log("The bullet just hit " + collision.gameObject.tag);
        GameObject explosion = ObjectPooler.SharedInstance.getPooledObject(explosionPrefab.tag);
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);   
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
