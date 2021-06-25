using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLogic : MonoBehaviour
{
    public NodeScript next;
    public NodeScript previous;

    // Holds the previous node for the back point when front and back are on different tracks
    private  NodeScript backPrevious;

    public float speed = 1f;
    public float pointDistance = 1f;


    private float frontDistance;
    private float backDistance;

    // The real distance of the current track previous to next
    private float trackDistance;
    // The distance of the track the back point is on (if applicable)
    private float backTrackDistance = 0;



    void Start()
    {
        trackDistance = Vector3.Distance(next.transform.position, previous.transform.position);

        backDistance = 0;
        frontDistance = (speed / trackDistance) * pointDistance;
    }

    void Update()
    {
        // Move front and back points
        frontDistance += (speed / trackDistance) * Time.deltaTime;
        if (backTrackDistance > 0)
        {
            backDistance += (speed / backTrackDistance) * Time.deltaTime;
        }
        else
        {
            backDistance += (speed / trackDistance) * Time.deltaTime;
        }
        

        // Update tracks
        if (frontDistance > 1)
        {
            ChangeTrack();
        }
        if (backDistance > 1)
        {
            ChangeBackTrack();
        }


        Vector3 frontPos = Vector3.Lerp(previous.transform.position, next.transform.position, frontDistance);
        //back pos depends on if it is on a different track
        Vector3 backPos;
        if (backPrevious == null)
        {
            backPos = Vector3.Lerp(previous.transform.position, next.transform.position, backDistance);
        }
        else
        {
            backPos = Vector3.Lerp(backPrevious.transform.position, previous.transform.position, backDistance);
        }

        
        //update train pos and rot
        transform.position = (frontPos + backPos) * 0.5f;
        transform.forward = backPos - frontPos;
    }

    void OnCollisionEnter(Collision collision)
    {
        // If collided with a train, tell the manager
        if (collision.gameObject.CompareTag("Train"))
        {
            TrainManager.Instance.TrainCrash(collision.GetContact(0).point);
        }
    }

    private void ChangeTrack()
    {
        // If the next node is missing a node, it is an end node
        if (next.next == null || next.previous == null)
        {
            TrainManager.Instance.TrainReachedEnd(this);
            return;
        }


        backPrevious = previous;
        previous = next;

        // If we were on a disabled track
        if (next.swapable != null & next.swapable == backPrevious)
        {
            next = (next.swapNext) ? next.previous : next.next;
        }
        else
        {
            // The next node depends on the direction we are traveling
            next = (next.next == backPrevious) ? next.previous : next.next;
        }

        // Update track distances
        backTrackDistance = trackDistance;
        trackDistance = Vector3.Distance(next.transform.position, previous.transform.position);

        frontDistance = 0;
    }

    private void ChangeBackTrack()
    {
        backPrevious = null;
        backTrackDistance = 0;
        backDistance = 0;
    }
}
