using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd_Point_Controller : MonoBehaviour
{
    NavMeshAgent agent;
    private Transform target;

    private Transform pointTransform;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pointTransform = GetComponent<Transform>();
        pointTransform = target;
    }

    // Update is called once per frame
    void Update()
    {        
        if(target != null)
        {
            agent.SetDestination(new Vector3(target.position.x, target.position.y, 0));
        }
    }
}
