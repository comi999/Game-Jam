using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : ISingleton< SoundController >
{
    Dictionary< string, AudioSource > AudioSources = new Dictionary< string, AudioSource >();

    public void Play( string a_Name, bool a_Repeat )
    {
        bool foundSource = AudioSources.TryGetValue( a_Name, out AudioSource source );
        bool foundPool = AudioPool.TryGetPool( a_Name, out AudioPool pool );

        if ( foundSource && foundPool )
        {
            source.Stop();
            source.clip = pool.Random;
            source.loop = a_Repeat;
            source.Play();
        }
        else if ( foundPool )
        {
            AudioSource newSource = ( new GameObject( "AudioSource-" + a_Name, typeof( AudioSource ) ) ).GetComponent< AudioSource >();
            AudioSources.Add( a_Name, newSource );

            newSource.clip = pool.Random;
            newSource.loop = a_Repeat;
            newSource.Play();
        }
    }

    public void Stop( string a_Name )
    {
        if ( AudioSources.TryGetValue( a_Name, out AudioSource source ) )
        {
            source.Stop();
            Destroy( source.gameObject );
            AudioSources.Remove( a_Name );
        }
    }
}