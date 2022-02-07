using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_controller : MonoBehaviour
{
    [SerializeField]
    GameObject moveOrb;
    [SerializeField]
    AudioSource soundSource;
    [SerializeField]
    AudioClip[] soundArray;
    Transform currentTarget;
    NavMeshAgent agent;
    public float updateTime = .25f;

    private GameObject existingMoveOrb;
    private District_controller currentDistrict = null;
    public District_controller CurrentDistrict 
    {
        get { return currentDistrict; }
        set { currentDistrict = value; }
    }
    private Transform existingMoveOrbTransform;
    private Transform playerTransform;
    private float timer = 0f;
    int combinationCounter = 0;
    int[] combination = new int[3] {4,4,4};
    public int CombinationCounter
    {
        get { return combinationCounter; }
        set { combinationCounter = value; }
    }
    
    private Vector2 _worldPosition;
    private Vector2 _screenPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
        existingMoveOrbTransform = playerTransform;
        currentTarget = playerTransform;
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if(existingMoveOrb != null)
            {
                Destroy(existingMoveOrb);
            }
            SpawnMovePoint();
            MoveToTarget(existingMoveOrbTransform, updateTime);
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if(currentDistrict != null)
        //     {
        //         currentDistrict.TransferMembers();
        //     }
        // }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(currentDistrict != null && combinationCounter < 4 && !soundSource.isPlaying)
                {
                    combination[combinationCounter] = 0;
                    //Debug.Log(combination[combinationCounter]);
                    soundSource.clip = soundArray[0];
                    soundSource.Play();
                    combinationCounter++;
                }  
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if(currentDistrict != null && combinationCounter < 4 && !soundSource.isPlaying)
                {
                    combination[combinationCounter] = 1;
                    //Debug.Log(combination[combinationCounter]);
                    soundSource.clip = soundArray[1];
                    soundSource.Play();
                    combinationCounter++;
                }  
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if(currentDistrict != null && combinationCounter < 4 && !soundSource.isPlaying)
                {
                    combination[combinationCounter] = 2;
                    //Debug.Log(combination[combinationCounter]);
                    soundSource.clip = soundArray[2];
                    soundSource.Play();
                    combinationCounter++;
                }  
            }
        if (currentDistrict != null && combinationCounter == 3)
        {
            Debug.Log("Transfer from player script elem 0 : " + combination[0]);
            Debug.Log("Transfer from player script elem 0 : " + combination[1]);
            Debug.Log("Transfer from player script elem 0 : " + combination[2]);
            currentDistrict.TransferMembers(combination);
            combinationCounter = 0;
            combination = new int[] {4,4,4};
        }

        if (timer <= 0f)
        {
                Debug.Log("Moved");
                MoveToTarget(existingMoveOrbTransform, updateTime);
        }
    }

    void SpawnMovePoint()
    {
        _screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        existingMoveOrb = Instantiate(moveOrb, _worldPosition, Quaternion.Euler(Vector3.zero));
        existingMoveOrbTransform = existingMoveOrb.transform;
    }

    void MoveToTarget(Transform target, float updateTime = .25f)
    {
        timer = updateTime;
        agent.SetDestination(new Vector3(target.position.x, target.position.y, 0));
    }
}
