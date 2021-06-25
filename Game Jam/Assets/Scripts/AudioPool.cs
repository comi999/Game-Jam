using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ CreateAssetMenu( fileName = "New Audio Pool", menuName = "AudioPool" ) ]
public class AudioPool : ScriptableObject
{
    public int Count
    {
        get
        {
            return m_Items.Length;
        }
    }

    public AudioClip Random
    {
        get
        {
            return m_Items[ UnityEngine.Random.Range( 0, m_Items.Length ) ];
        }
    }

    private void OnEnable()
    {
        if ( m_Items == null )
        {
            m_Items = new AudioClip[ 0 ];
        }
    }

    public AudioClip GetAt( int a_Index )
    {
        if ( a_Index < 0 || a_Index >= m_Items.Length )
        {
            return default;
        }

        return m_Items[ a_Index ];
    }

    public static void PopulateRegistry( AudioPool[] a_Pools )
    {
        m_AllPools = new Dictionary< string, AudioPool >();
        Array.ForEach( a_Pools, pool => { m_AllPools.Add( pool.name, pool ); } );
    }

    public static bool TryGetPool( string a_Name, out AudioPool o_Pool )
    {
        return m_AllPools.TryGetValue( a_Name, out o_Pool );
    }

    [ SerializeField ] private AudioClip[] m_Items;

    private static Dictionary< string, AudioPool > m_AllPools;
}