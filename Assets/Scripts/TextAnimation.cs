using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour {

    public float textSpeed = 1f;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + textSpeed * Time.deltaTime, transform.position.z);
    }
}
