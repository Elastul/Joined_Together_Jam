using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd_member_controller : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    bool member = false;
    public bool Member 
    {
        get { return member; }
        set { member = value; }
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Point").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    void Update()
    {
        if(member)
        {
            agent.SetDestination(target.position);
        }
    }

    public void AdjustStopDistance(float addingNumber)
    {
        agent.stoppingDistance += addingNumber;
    }
}
