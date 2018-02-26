using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    NavMeshAgent agent;
	// Use this for initialization
	void Awake () {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.FindGameObjectWithTag("Goal").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
