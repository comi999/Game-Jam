using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuCrashController : ISingleton< MenuCrashController >
{
    public GameObject TrainPrefab;
    public Transform SpawnPosition;
    public Transform CrashPosition;

    public void TriggerCrash()
    {
        m_CrashAmount = 0.0f;
        m_FallSpeed = 0.0f;
        StartCoroutine( Crash() );
    }

    private IEnumerator Crash()
    {
        GameObject spawnedTrain = Instantiate( TrainPrefab, SpawnPosition.position, SpawnPosition.rotation );
        spawnedTrain.transform.localScale = SpawnPosition.localScale;
        m_MeshRenderer = spawnedTrain.GetComponentInChildren< SkinnedMeshRenderer >();

        while ( spawnedTrain.transform.position.x < CrashPosition.position.x )
        {
            spawnedTrain.transform.position += new Vector3( Time.deltaTime * 30.0f, 0, 0 );
            yield return null;
        }

        while ( m_CrashAmount < 100.0f )
        {
            m_CrashAmount += Time.deltaTime * 1500.0f;
            m_MeshRenderer.SetBlendShapeWeight( 0, m_CrashAmount );

            Vector3 centre = m_MeshRenderer.bounds.center;
            Vector3 extent = m_MeshRenderer.bounds.extents;

            //spawnedTrain.transform.position += new Vector3( centre.x + extent.x, 0, 0 );

            //spawnedTrain.transform.position = CrashPosition.position - new Vector3( extent.x, 0, 0 );

            yield return null;
        }

        yield return new WaitForSeconds( 0.3f );

        while ( spawnedTrain.transform.position.z > -3.0f )
        {
            m_FallSpeed += Time.deltaTime * 0.4f;
            spawnedTrain.transform.position -= new Vector3( 0, 0, m_FallSpeed );
            yield return null;
        }

        Destroy( spawnedTrain );
    }

    private float m_CrashAmount = 0.0f;
    private float m_FallSpeed = 0.0f;
    private SkinnedMeshRenderer m_MeshRenderer = null;
}