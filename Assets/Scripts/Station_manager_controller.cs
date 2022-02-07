using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_manager_controller : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stations;

    List<GameObject> force_destinations;
    int maxid = 1;

    // Start is called before the first frame update
    void Start()
    {
        force_destinations = new List<GameObject>(GameObject.FindGameObjectsWithTag("Destination"));
        int counter = 0;
        foreach (GameObject point in force_destinations)
        {
            counter++;
            point.GetComponent<Destination_controller>().Id = counter;
        }
        maxid = counter;
    }

    public GameObject calculateNextDestination(GameObject previousTarget)
    {

        int id = previousTarget.GetComponent<Destination_controller>().Id;
        int newId = Random.Range(1, maxid);
        while(newId == id)
        {
            newId = Random.Range(1, maxid);
        }
        return force_destinations.Find(x => x.GetComponent<Destination_controller>().Id == newId);
    }
}
