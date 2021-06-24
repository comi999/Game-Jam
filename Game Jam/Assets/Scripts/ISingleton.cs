using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISingleton< T > : MonoBehaviour where T : ISingleton< T >
{
    public static T Instance
    {
        get
        {
            if ( m_Instance == null )
            {
                m_Instance = FindObjectOfType< T >();

                if ( m_Instance == null )
                {
                    Debug.LogError( "No object of type " + m_Instance.GetType().Name + " could be found!" );
                }
            }

            return m_Instance;
        }
    }

    private static T m_Instance;
}
