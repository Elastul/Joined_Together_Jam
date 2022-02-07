using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_condition : MonoBehaviour
{
    [SerializeField]
    int winReq = 20;
    Crowd_manager crowd_Manager;
 
    private void Start() 
    {
        crowd_Manager = GameObject.FindObjectOfType<Crowd_manager>().GetComponent<Crowd_manager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject otherObject = other.gameObject;
        int count = crowd_Manager.CrowdCount();
        if(otherObject.CompareTag("Player") && count >= winReq)
        {
            crowd_Manager.Win();
        }
    }

}
