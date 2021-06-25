using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneScrollingController : MonoBehaviour
{
    [ Header( "Scrolling Objects" ) ]
    public GameObject[] ScrollingObjects;
    public Transform BeginningPosition;
    public Transform EndingPosition;
    public float[] ScrollingHeights;
    public float ScrollingSpeed;
    public float SpawnDelay;


    private void Awake()
    {
        int index = 0;
        Func< GameObject > onPopulate = () => { return Instantiate( ScrollingObjects[ index++ ] ); };
        Action< GameObject > doNothing = gameObject => { };
        
        m_ScrollingObjectPool = new ObjectPool< GameObject >( onPopulate, doNothing, doNothing, doNothing );
        m_ScrollingObjectPool.SelectOrder = ObjectPool< GameObject >.Order.RoundRobin;
        m_ScrollingObjectPool.Populate( ScrollingObjects.Length );

        m_ScrollingList = new List< GameObject >();

        foreach ( GameObject scrollingObject in m_ScrollingList )
        {
            scrollingObject.transform.position = BeginningPosition.position;
        }
    }

    private void FixedUpdate()
    {
        if ( !m_IsScrolling )
        {
            return;
        }

        for ( int i = 0; i < m_ScrollingList.Count; ++i )
        {
            GameObject scrollingObject = m_ScrollingList[ i ];
            scrollingObject.transform.position -= new Vector3( ScrollingSpeed * Time.deltaTime, 0, 0 );

            if ( scrollingObject.transform.position.x < EndingPosition.position.x )
            {
                m_ScrollingObjectPool.Despawn( scrollingObject );
                m_ScrollingList.RemoveAt( i );

                if ( i == m_ScrollingList.Count - 1 )
                {
                    break;
                }
                else
                {
                    ++i;
                }
            }
        }

        m_Timer += Time.fixedDeltaTime;

        if ( m_Timer > SpawnDelay )
        {
            m_Timer = 0.0f;
            SpawnScrollingObject();
        }
    }

    private void SpawnScrollingObject()
    {
        if ( m_ScrollingObjectPool.TrySpawn( out GameObject spawned ) )
        {
            m_ScrollingList.Add( spawned );
            spawned.transform.position = 
                new Vector3( 
                    BeginningPosition.position.x, 
                    ScrollingHeights[ UnityEngine.Random.Range( 0, ScrollingHeights.Length ) ], 
                    BeginningPosition.position.z );
        }
    }

    private ObjectPool< GameObject > m_ScrollingObjectPool;
    private List< GameObject > m_ScrollingList;
    [ SerializeField ] private bool m_IsScrolling;
    [ SerializeField ] private float m_Timer;
}
