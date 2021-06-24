using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchTrackScript : MonoBehaviour, IInteractable
{
    [Tooltip("The node this track is next of")]
    public NodeScript previousNode;
    [Tooltip("The node this track is previous of")]
    public NodeScript nextNode;

    public float spinAngle = 30;

    public UnityEvent onTrackSwapped;


    private Coroutine rotateRoutine;
    private bool isActive = false;

    

    void Start()
    {
        // Set the swapable on each node
        previousNode.swapNext = true;
        previousNode.swapable = nextNode;
        nextNode.swapNext = false;
        nextNode.swapable = previousNode;
    }

    public void OnInteract(Interaction a_Interaction)
    {
        // Swap the tracks
        previousNode.SwapTrack();
        nextNode.SwapTrack();

        // Toggle flag
        isActive = !isActive;

        // Rotate the track, stopping an existing coroutine if there is one
        if (rotateRoutine != null)
        {
            StopCoroutine(rotateRoutine);
        }
        rotateRoutine = StartCoroutine(Rotate(isActive));


        onTrackSwapped.Invoke();
    }

    private IEnumerator Rotate(bool spinPositive)
    {
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.Euler(0, spinPositive ? spinAngle : -spinAngle, 0);

        float t = 0;
        while (t < 1)
        {
            transform.rotation = Quaternion.Slerp(start, end, t);

            t += Time.deltaTime;
            yield return null;
        }
    }



    public void OnHoverEnter(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }
    public void OnHoverExit(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }
    public void OnHoverStay(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }
    public void OnUninteract(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }
}
