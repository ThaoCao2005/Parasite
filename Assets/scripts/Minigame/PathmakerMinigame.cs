using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathmakerMinigame : MonoBehaviour
{
    public Transform floorTilePrefab; // Normal floor tile prefab
    public List<Transform> treasurePrefabs; // List of treasure prefabs
    public Transform pathmakerSpherePrefab; // Pathmaker prefab (optional if you want recursion)
    public int maxTiles = 150; // Total number of tiles
    public float treasureChance = 0.05f; // 5% chance for treasure tiles

    private int counter = 0; // Counter for the tiles generated
    public static int globalTileCount = 0;
    public static int maxGlobalTiles = 500; // Global tile limit

    private float gridSize = 10f; // Grid size for snapping and movement

    void Update()
    {
        // Destroy object if the global tile limit is reached
        if (globalTileCount >= maxGlobalTiles)
        {
            Destroy(gameObject);
            return;
        }

        // Continue generating tiles until the maxTiles or globalTileCount is reached
        if (counter < maxTiles && globalTileCount < maxGlobalTiles)
        {
            // Snap the current position to the grid
            transform.position = SnapToGrid(transform.position, gridSize);

            // Check if a tile already exists at this position
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            bool tileAlreadyExists = false;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Floor"))
                {
                    tileAlreadyExists = true;
                    break;
                }
            }

            if (!tileAlreadyExists)
            {
                // Choose a random tile (floor or treasure)
                Transform chosenTile = GetRandomTile();
                Instantiate(chosenTile, transform.position, Quaternion.identity);

                // Increment counters
                counter++;
                globalTileCount++;
            }

            // Move forward by a grid step and snap position
            transform.Translate(Vector3.forward * gridSize);
            transform.position = SnapToGrid(transform.position, gridSize);

            // Randomly turn left or right
            transform.Rotate(0, Random.Range(0, 2) == 0 ? 90 : -90, 0);
        }
        else
        {
            // Destroy the Pathmaker object once its job is done
            Destroy(gameObject);
        }
    }

    Transform GetRandomTile()
    {
        // Determine if this tile should be a treasure
        if (treasurePrefabs.Count > 0 && Random.Range(0f, 1f) < treasureChance)
        {
            return treasurePrefabs[Random.Range(0, treasurePrefabs.Count)];
        }
        return floorTilePrefab;
    }

    private Vector3 SnapToGrid(Vector3 originalPosition, float gridSize)
    {
        float snappedX = Mathf.Round(originalPosition.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(originalPosition.z / gridSize) * gridSize;
        return new Vector3(snappedX, originalPosition.y, snappedZ);
    }
}
