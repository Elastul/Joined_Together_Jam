using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_station_controller : MonoBehaviour
{
    [SerializeField]
    GameObject force_member;
    [SerializeField]
    float spawnTime = 20f;

    public GameObject first_target;
    GameObject forceLeader;
    GameObject new_force_member;
    [SerializeField]
    int maxSpawnCount = 1;
    [SerializeField]
    int maxMembersPerSpawn = 5;

    int spawned = 0;
    float timer = 20f;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f && spawned < maxSpawnCount)
        {
            for (int i = 0; i < maxMembersPerSpawn; i++)
            {
                if(i == 0)
                {
                    new_force_member = Instantiate(force_member, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(Vector3.zero));
                    new_force_member.GetComponent<Force_member_controller>().IsLeader = true;
                    new_force_member.GetComponent<Force_member_controller>().ChangeTarget(first_target);
                    forceLeader = new_force_member;
                }
                else
                {
                    new_force_member = Instantiate(force_member, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(Vector3.zero));
                    new_force_member.GetComponent<Force_member_controller>().ChangeTarget(forceLeader);
                }
                spawned++;               
            }
            timer = spawnTime;
        }
    }
}
