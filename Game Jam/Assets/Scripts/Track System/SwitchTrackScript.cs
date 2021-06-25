using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchTrackScript : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public struct ConnectedNode
    {
        public NodeScript node;
        [Tooltip("Is this switch track next of the node?")]
        public bool isNext;
    }

    public ConnectedNode node1;
    public ConnectedNode node2;

    public float spinAngle = 30;
    public float spinTime = 0.5f;

    public UnityEvent onTrackSwapped;


    private Coroutine rotateRoutine;
    private bool isActive = false;

    private Quaternion initRot;



    void Start()
    {
        initRot = transform.rotation;

        // Set the swapable on each node
        node1.node.swapNext = node1.isNext;
        node1.node.swapable = node2.node;
        node2.node.swapNext = node2.isNext;
        node2.node.swapable = node1.node;
    }

    public void OnInteract(Interaction a_Interaction)
    {
        // Swap the tracks
        node1.node.SwapTrack();
        node2.node.SwapTrack();

        // Toggle flag
        isActive = !isActive;

        // Rotate the track, stopping an existing coroutine if there is one
        if (rotateRoutine != null)
        {
            StopCoroutine(rotateRoutine);
        }
        rotateRoutine = StartCoroutine(Rotate(isActive));

        // Play audio
        SoundController.Instance.Play("Track Switches", false);

        onTrackSwapped.Invoke();
    }

    private IEnumerator Rotate(bool spinPositive)
    {
        Quaternion start = transform.rotation;
        Quaternion end = initRot * Quaternion.Euler(0, spinPositive ? spinAngle : 0, 0);

        float t = 0;
        while (t < spinTime)
        {
            transform.rotation = Quaternion.Slerp(start, end, t / spinTime);

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
