using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public int gun = 0;
    public PlayerController pc;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.transform.Find("Weapon" + collision.GetComponent<PlayerController>().getGunNumber().ToString()).gameObject.SetActive(false);
            collision.transform.Find("Weapon" + gun.ToString()).gameObject.SetActive(true);
            Destroy(gameObject);
        }

    }
}
