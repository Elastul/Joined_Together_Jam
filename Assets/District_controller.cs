using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District_controller : MonoBehaviour
{
    [SerializeField]
    GameObject crowd_Member;
    [SerializeField]
    Player_controller player;
    [SerializeField]
    int spawnMembersValue = 5;

    [SerializeField]
    AudioClip[] soundArray;
    [SerializeField]
    AudioSource audioSource;

    Transform thisTransform;
    Crowd_manager crowd_Manager;
    List<GameObject> District_members = new List<GameObject>();
    [SerializeField]
    float respawnTime = 60f;
    float timer = 10f;
    bool spawned = false;
    int[] recruitOrder = new int[3] {0,0,0};
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = gameObject.transform;
        timer = respawnTime;
        crowd_Manager = GameObject.FindObjectOfType<Crowd_manager>().GetComponent<Crowd_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= timer >= 0f ? Time.deltaTime : 0f;
        //Debug.Log(timer);
        if(timer <= 0f && spawned == false)
        {
            spawned = true;
            //Debug.Log("Respawn Time!");
            for (int i = 0; i < spawnMembersValue; i++)
            {
                //Instantiate(crowd_Member, new Vector2(thisTransform.position.x + Random.Range(-1.5f, 1.5f), thisTransform.position.y  + Random.Range(-1.5f, 1.5f)), Quaternion.Euler(Vector3.zero));
                Instantiate(crowd_Member, new Vector3(this.transform.position.x, this.transform.position.y, 10.70421f), Quaternion.Euler(Vector3.zero));
                //Debug.Log("Spawned"); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject otherObject = other.gameObject;
        Debug.Log("Entered_dist");
        if(otherObject.CompareTag("Crowd_member") && otherObject.GetComponent<Crowd_member_controller>().Member == false)
        {
            //Debug.Log("Member");
            District_members.Add(otherObject);
        }
        if(otherObject.CompareTag("Player") && District_members.Count > 0)
        {
            player.CurrentDistrict = gameObject.GetComponent<District_controller>();
            Debug.Log("Вербовка");
            StartCoroutine(StartRecruiting());
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Debug.Log("Leaved_dist");
        GameObject otherObject = other.gameObject;
        if(otherObject.CompareTag("Player") && District_members.Count > 0)
        {
            Debug.Log("Вербовка прервана");
            
            player.CurrentDistrict = null;
            player.CombinationCounter = 0;

            StopAllCoroutines();
        }
    }

    public IEnumerator StartRecruiting()
    {        
        for (int i = 0; i < recruitOrder.Length; i++)
        {
            recruitOrder[i] = Random.Range(0, 3);
            Debug.Log(recruitOrder[i]);
        }
        int j = 0;

        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = soundArray[recruitOrder[j]];
        audioSource.Play();
        j++;

        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = soundArray[recruitOrder[j]];
        audioSource.Play();
        j++;

        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = soundArray[recruitOrder[j]];
        audioSource.Play();
        j++;
    }

    public void TransferMembers(int[] comb)
    {
        bool transfer = false;
        if(recruitOrder[0] == comb[0] && recruitOrder[1] == comb[1] && recruitOrder[2] == comb[2])
        {
            transfer = true;
        }
        if(transfer)
        {
            crowd_Manager.AddMember(District_members);
            District_members.Clear();
            timer = respawnTime;
            spawned = false;
            
            player.CurrentDistrict = null;
            player.CombinationCounter = 0;
            StopAllCoroutines();
        }
    }
}
