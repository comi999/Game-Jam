using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public NodeScript next;
    public NodeScript previous;

    // Holds the previous node for the back point when front and back are on different tracks
    private NodeScript backPrevious;

    public float speed = 1f;
    public float pointDistance = 1f;


    private float frontDistance = 0.7f;
    private float backDistance = 0.3f;

    // The real distance of the current track previous to next
    float trackDistance;
    // The distance of the track the back point is on (if applicable)
    float backTrackDistance = 0;



    void Start()
    {
        trackDistance = Vector3.Distance(next.transform.position, previous.transform.position);

        backDistance = 0;
        frontDistance = (speed / trackDistance) * pointDistance;
    }

    void OnEnable()
    {
        // Make sure there are nodes to travel between
        if (next == null || previous == null)
        {
            this.enabled = false;
        }
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
        transform.up = backPos - frontPos;
    }

    private void ChangeTrack()
    {
        backPrevious = previous;

        previous = next;
        // The next node depends on the direction we are traveling
        if (next.next == backPrevious)
        {
            next = next.previous;
        }
        else
        {
            next = next.next;
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
