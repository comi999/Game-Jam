using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ResourcePool< T > : ScriptableObject
{
    public int Count
    {
        get
        {
            return m_Items.Length;
        }
    }

    public T Random
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
            m_Items = new T[ 0 ];
        }
    }

    public T GetAt( int a_Index )
    {
        if ( a_Index < 0 || a_Index >= m_Items.Length )
        {
            return default;
        }

        return m_Items[ a_Index ];
    }

    [ RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.AfterSceneLoad ) ]
    private static void OnLoad()
    {
        m_AllPools = new Dictionary< string, ResourcePool< T > >();
        ResourcePool< T >[] allPools = Resources.FindObjectsOfTypeAll< ResourcePool< T > >();
        Array.ForEach( allPools, pool => { m_AllPools.Add( pool.name, pool ); } );
    }

    public static bool TryGetPool( string a_Name, out ResourcePool< T > o_Pool )
    {
        return m_AllPools.TryGetValue( a_Name, out o_Pool );
    }

    [ SerializeField ] private T[] m_Items;

    private static Dictionary< string, ResourcePool< T > > m_AllPools;
}
