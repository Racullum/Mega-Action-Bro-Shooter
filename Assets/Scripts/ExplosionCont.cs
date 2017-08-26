using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCont : MonoBehaviour {
    private float animationTime = .583f;
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(wait());
	}
    
    IEnumerator wait()
    {
        yield return new WaitForSeconds(animationTime);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
