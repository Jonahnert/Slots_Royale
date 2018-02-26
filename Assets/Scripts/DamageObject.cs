using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {

    public GameObject target;
    public int damage;
    public float speed = 1f;

    private void Start()
    {
        if(target)
        {
            transform.LookAt(target.transform);
        }
    }

    // Update is called once per frame
    void Update () {
        if(target)
        {
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        else
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject == target)
        {
            target.GetComponent<Health>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
