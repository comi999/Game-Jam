using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainManager : ISingleton<TrainManager>
{
    // Contains all TrainLogic instances
    private ArrayList trains;
    private ArrayList endNodes;

    public GameObject[] trainPrefabs;

    public GameObject explosionPrefab;


    public UnityEvent OnTrainCrash;
    public UnityEvent OnTrainReachedEnd;


    [Tooltip("Demo mode is used for the main menu")]
    public bool isDemoMode = false;

    [Space]
    public int maxTrainCount = 5;
    public float minDistanceFromOtherTrains = 5;
    public float spawnTime = 2f;

    private float spawnTimmer = 0;



    void Start()
    {
        trains = new ArrayList();
        endNodes = new ArrayList();

        // Search through all node scripts to find end nodes
        foreach (var itter in FindObjectsOfType<NodeScript>())
        {
            if ((itter.next == null) !=/*XOR*/ (itter.previous == null))
            {
                endNodes.Add(itter);
            }
        }


        if (isDemoMode)
        {
            CreateRandomTrain();
        }
    }

    void Update()
    {
        // Only run if in play mode
        if (isDemoMode)
        {
            return;
        }

        // Dont do anything if there are too many trains
        if (trains.Count >= maxTrainCount)
        {
            return;
        }


        spawnTimmer += Time.deltaTime;
        if (spawnTimmer >= spawnTime)
        {
            spawnTimmer = 0;
            CreateRandomTrain();
        }
    }

    public void CreateRandomTrain()
    {
        // Get random end node
        int nodeI = Random.Range(0, endNodes.Count);
        NodeScript node = endNodes[nodeI] as NodeScript;

        // If there are any trains in range of the node, do nothing
        foreach (TrainLogic train in trains)
        {
            if (Vector3.Distance(train.transform.position, node.transform.position) < minDistanceFromOtherTrains)
            {
                return;
            }
        }


        // Create a random train prefab
        int trainI = Random.Range(0, trainPrefabs.Length);
        GameObject obj = Instantiate(trainPrefabs[trainI], Vector3.zero, Quaternion.identity);
        TrainLogic newTrain = obj.GetComponent<TrainLogic>();
        trains.Add(newTrain);

        // Setup train
        newTrain.previous = node;
        newTrain.next = (node.next == null) ? node.previous : node.next;

        // Play sound effect
        SoundController.Instance.Play("Train Spawns", false);
    }

    public void ResetManager()
    {
        // Destroy all trains and return to demo mode
        while (trains.Count > 0)
        {
            Destroy((trains[0] as TrainLogic).gameObject);
            trains.RemoveAt(0);
        }

        isDemoMode = true;
        CreateRandomTrain();
    }

    // Called when a train crashes into another train
    public void TrainCrash(Vector3 collisionPoint)
    {
        if (isDemoMode)
            return;

        //stop all trains
        foreach (TrainLogic it in trains)
        {
            it.enabled = false;
        }

        // Show game over menu and play sound effect
        UIController.Instance.LoadScoreMenu();
        SoundController.Instance.Play("Train Crashs", false);

        // Create explosion effect for 3 seconds
        Destroy( Instantiate(explosionPrefab, collisionPoint, Quaternion.identity), 2);

        isDemoMode = true;

        OnTrainCrash.Invoke();
    }

    public void TrainReachedEnd(TrainLogic train)
    {
        // Remove the train
        trains.Remove(train);
        Destroy(train.gameObject);

        // In demo mode, spawn a new train every time one reaches the end
        if (isDemoMode)
        {
            CreateRandomTrain();
        }
        // In play mode, give the player score
        else
        {
            UIController.Instance.Score += 50;
            SoundController.Instance.Play("Points Gained", false);
        }

        OnTrainReachedEnd.Invoke();
    }
}
