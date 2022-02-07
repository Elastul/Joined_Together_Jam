using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crowd_manager : MonoBehaviour
{
    List<GameObject> Crowd = new List<GameObject>();
    List<GameObject> inactiveCrowd = new List<GameObject>();

    // [SerializeField]
    // float memberOffsetStep = 0.2f;
    int memberCounter = 0;
    float memberOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMember(List<GameObject> District_members)
    {
        foreach(GameObject member in District_members)
        {
            Crowd.Add(member);
            memberCounter++;
            AdjustOffset(false);
            member.GetComponent<Crowd_member_controller>().Member = true;
            member.GetComponent<Crowd_member_controller>().AdjustStopDistance(memberOffset);
        }
    }

    public void AdjustOffset(bool setAgain)
    {
        memberOffset = memberCounter / 1.5f;
        if(setAgain)
        {
            int i = 0;
            foreach (GameObject member in Crowd)
            {
                member.GetComponent<Crowd_member_controller>().AdjustStopDistance(i / 1.5f);
                i++;
            }
        }
    }

    public void GotHurtMember(GameObject member)
    {
        Crowd.Remove(member);
        inactiveCrowd.Add(member);
        member.SetActive(false);
        AdjustOffset(true);
    }
    void Lose()
    {
        SceneManager.LoadScene(2); //DeathScene
    }
    public void Win()
    {
        SceneManager.LoadScene(3); //WinScene
    }
    public void GotHurtPlayer()
    {
        if(Crowd.Count > 0)
        {
            int i = Random.Range(0, Crowd.Count);
            GameObject member = Crowd[i];
            inactiveCrowd.Add(member);
            Crowd.RemoveAt(i);
            member.SetActive(false);
            AdjustOffset(true);
        }
        else
        {
            Lose();
        }
    }

    public int CrowdCount()
    {
        return Crowd.Count;
    }
}
