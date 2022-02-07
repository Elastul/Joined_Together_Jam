using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Force_member_controller : MonoBehaviour
{
    Crowd_manager crowd_Manager;
    Station_manager_controller manager_Controller;
    GameObject target;
    GameObject previousTarget;
    NavMeshAgent agent;

    bool isLeader = false;
    public bool IsLeader
    {
        get { return isLeader; }
        set { isLeader = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        manager_Controller = GameObject.FindObjectOfType<Station_manager_controller>();
        crowd_Manager = GameObject.FindObjectOfType<Crowd_manager>();
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        previousTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if(Vector2.Distance(this.gameObject.transform.position, target.transform.position) > 0.2f)
            {
                agent.SetDestination(target.transform.position);
            }
            else if(Vector2.Distance(this.gameObject.transform.position, target.transform.position) < 0.2f && isLeader)
            {
                ChangeTarget(manager_Controller.calculateNextDestination(previousTarget));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        GameObject otherObj = other.gameObject;
        if(otherObj.CompareTag("Player"))
        {
            crowd_Manager.GotHurtPlayer();
        }
        if(otherObj.CompareTag("Crowd_member") && otherObj.GetComponent<Crowd_member_controller>().Member)
        {
            crowd_Manager.GotHurtMember(otherObj);
        }
    }

    public void ChangeTarget(GameObject target)
    {
        previousTarget = this.target;
        this.target = target;
    }
}
