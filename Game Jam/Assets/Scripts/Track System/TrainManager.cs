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


    public UnityEvent OnTrainCrash;
    public UnityEvent OnTrainReachedEnd;


    [Tooltip("Demo mode is used for the main menu")]
    public bool isDemoMode = false;



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

    public void CreateRandomTrain()
    {
        // create a random train prefab
        int trainI = Random.Range(0, trainPrefabs.Length);
        GameObject obj = Instantiate(trainPrefabs[trainI], Vector3.zero, Quaternion.identity);
        TrainLogic newTrain = obj.GetComponent<TrainLogic>();
        trains.Add(newTrain);

        // Get random end node
        int nodeI = Random.Range(0, endNodes.Count);
        NodeScript node = endNodes[nodeI] as NodeScript;

        // Setup train
        newTrain.previous = node;
        newTrain.next = (node.next == null) ? node.previous : node.next;

        // Play sound effect
        SoundController.Instance.Play("Train Spawns", false);
    }

    // Called when a train crashes into another train
    public void TrainCrash()
    {
        //stop all trains
        foreach (TrainLogic it in trains)
        {
            it.enabled = false;
        }

        // Show game over menu and play sound effect
        UIController.Instance.LoadScoreMenu();
        SoundController.Instance.Play("Train Crashs", false);

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
