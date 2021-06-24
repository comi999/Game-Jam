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
        // 0 = empty
        // 1 = rail
        // 2 = filled by switch track object
        // >2 = switch track connection point
        int[,] tiles = new int[xTiles, yTiles];


		// Place switch tracks
		PlaceSwitchTracks(ref tiles, out List<GameObject> switchTracks);
        // Connect switch tracks together
        ConnectSwitchTracks(ref tiles);
        // Bring tracks out to the edges
        CompleteTrackLines(ref tiles);
        // Instantiate nodes and track prefabs along the rails
        CreateNodes(in tiles, ref switchTracks, out List<List<NodeScript>> nodes);
        // Link nodes together
        LinkNodes(in nodes);
    }


    // Place switch tracks randomly on the grid, setting connection points
    private void PlaceSwitchTracks(ref int[,] tiles, out List<GameObject> switchTracks)
    {
        //  NOTE: THIS USES THE BOTTOM RIGHT CORNER AS THE POSITION FOR THE SWITCH TRACK

        switchTracks = new List<GameObject>();
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
            switchTracks.Add(switchTrack);


            // Fill in tiles
            for (int xIt = 0; xIt < switchTrackSizeX; xIt++)
            {
                for (int yIt = 0; yIt < switchTrackSizeY; yIt++)
                {
                    tiles[x + xIt, y + yIt] = 2;
                }
            }

            // Bottom left
            tiles[x - switchTrackSizeX, y] = 3 + i;
            // Top right
            tiles[x, y - switchTrackSizeY] = 3 + i;
        }
    }

    // Connect switch track connection points together
    private void ConnectSwitchTracks(ref int[,] tiles)
	{
        // Find all connection tiles
        List<(int, int)> connectionTiles = new List<(int, int)>();
        for (int x = 0; x < xTiles; x++)
		{
            for (int y = 0; y < yTiles; y++)
            {
                if (tiles[x, y] > 2)
				{
                    connectionTiles.Add((x, y));
                }
            }
        }

        // Pair up connection tiles
        List<((int, int), (int, int))> connectionPairs = new List<((int, int), (int, int))>();
        for (int i = 0; i < switchTrackCount; i++)
		{
            // Get first random tile
            int firstIndex = Random.Range(0, connectionTiles.Count);
            (int, int) first = connectionTiles[firstIndex];
            connectionTiles.Remove(first);
            // Get second random tile
            int secondIndex = Random.Range(0, connectionTiles.Count);
            (int, int) second = connectionTiles[secondIndex];
            connectionTiles.Remove(second);

            // Add pair to list
            connectionPairs.Add((first, second));
        }


        //connect pairs using A* to create rail lines
    }

    // Complete track lines by making them go out to the edge of the grid
    private void CompleteTrackLines(ref int[,] tiles)
	{
        //use A* or something to bring rails out to the edges
	}

    // Create nodes for each path, passing refs to the switch tracks for their connection points, and instantiate rail prefabs
    private void CreateNodes(in int[,] tiles, ref List<GameObject> switchTracks, out List<List<NodeScript>> nodes)
	{
        // List of nodes in each rail
        nodes = new List<List<NodeScript>>();


        //look around edges to find the ends of rails

        //keep track of rail ends done

        //iterate over rail line, instantiating rail prefabs along the way

        //at connection tiles, create a node and pass a ref to the switch track it belongs to

        //at corners, use different prefab and create node
	}

    // Link nodes together to create tracks
    private void LinkNodes(in List<List<NodeScript>> nodes)
	{
        // For each rail, link the nodes in contains
        foreach (var itter in nodes)
		{
            for (int i = 1; i < itter.Count; i++)
			{
                itter[i].previous = itter[i-1];
                itter[i-1].next = itter[i];
			}

            itter[itter.Count - 1].previous = itter[itter.Count - 2];
            itter[itter.Count - 2].next = itter[itter.Count - 1];
        }
	}
}
