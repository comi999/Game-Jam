using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTrackGenerator : MonoBehaviour
{
    public int xTiles = 20;
    public int yTiles = 20;
    public float tileSize;

    public float turnWeight;
    public int switchTrackCount;

    public GameObject trackPrefab;
    public GameObject switchTrackPrefab;


    private int switchTrackSizeX = 3;
    private int switchTrackSizeY = 4;




    public void GenerateRandomRails()
    {
        // 1 = switch track connection point
        // 2 = filled by switch track object
        int[,] tiles = new int[xTiles, yTiles];


        // Place switch tracks
        List<GameObject> switchTracks = PlaceSwitchTracks(ref tiles);


    }


    private List<GameObject> PlaceSwitchTracks(ref int[,] tiles)
    {
        //  NOTE: THIS USES THE BOTTOM RIGHT CORNER AS THE POSITION FOR THE SWITCH TRACK

        List<GameObject> result = new List<GameObject>();

        // Object to make switch tracks a child of
        Transform switchTrackParent = new GameObject("Switch Tracks").transform;

        for (int i = 0; i < switchTrackCount; i++)
        {
            //TODO: generate cardinal direction to place switch track with


            int x = Random.Range(switchTrackSizeX, xTiles);
            int y = Random.Range(switchTrackSizeY, yTiles);


            // Check that there is no switch track in this area
            for (int xIt = 0; xIt < switchTrackSizeX; xIt++)
            {
                for (int yIt = 0; yIt < switchTrackSizeY; yIt++)
                {
                    // If tile is not empty, find a new position
                    if (tiles[x + xIt, y + yIt] != 0)
                    {
                        i--;
                        continue;
                    }
                }
            }


            // Create switch track object
            GameObject switchTrack = Instantiate(switchTrackPrefab, new Vector3( (x - switchTrackSizeX * 0.5f) * tileSize, (y - switchTrackSizeY * 0.5f) * tileSize, 0 ), Quaternion.identity, switchTrackParent);
            result.Add(switchTrack);


            // Fill in tiles
            for (int xIt = 0; xIt < switchTrackSizeX; xIt++)
            {
                for (int yIt = 0; yIt < switchTrackSizeY; yIt++)
                {
                    tiles[x + xIt, y + yIt] = 2;
                }
            }

            // Bottom left
            tiles[x - switchTrackSizeX, y] = 1;
            // Top right
            tiles[x, y - switchTrackSizeY] = 1;
        }

        return result;
    }
}
