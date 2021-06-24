using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainManager : ISingleton<TrainManager>
{
    // Contains all TrainLogic instances
    private ArrayList trains;

    [Tooltip("The nodes that are the end of a track")]
    public NodeScript[] endNodes;

    public GameObject[] trainPrefabs;


    public UnityEvent OnTrainCrash;
    public UnityEvent OnTrainReachedEnd;


    [Tooltip("Demo mode is used for the main menu")]
    public bool isDemoMode = false;



    void Start()
    {
        trains = new ArrayList();

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
        int nodeI = Random.Range(0, endNodes.Length);
        NodeScript node = endNodes[nodeI];

        // Setup train
        newTrain.previous = node;
        newTrain.next = (node.next == null) ? node.previous : node.next;
    }

    // Called when a train crashes into another train
    public void TrainCrash()
    {
        //stop all trains
        foreach (TrainLogic it in trains)
        {
            it.enabled = false;
        }

        //show game over menu

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

        OnTrainReachedEnd.Invoke();
    }
}
