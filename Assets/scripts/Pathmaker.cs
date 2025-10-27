using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
    private int counter = 0;
    public Transform landTilePrefab;
    public Transform grassTilePrefab;
    public Transform pathmakerSpherePrefab;
    public List<Transform> flowerPrefabs;

    public static int globalTileCount = 0;
    public static int maxGlobalTiles = 500;

    private int maxLifetime;
    private float turnProbability;
    private float newPathmakerProbability;
    private float landTileProbability;

    private static bool hasPlayedSound = false;
    public AudioClip startSound;
    private AudioSource audioSource;

    void Start()
    {
        maxLifetime = Random.Range(30, 100);
        turnProbability = Random.Range(0.1f, 0.4f);
        newPathmakerProbability = Random.Range(0.01f, 0.05f);
        landTileProbability = Random.Range(0.5f, 0.8f);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = startSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (globalTileCount >= maxGlobalTiles)
        {
            Destroy(gameObject);
            return;
        }

        if (counter < maxLifetime)
        {
            // Branching
            float randomNumber = Random.Range(0.0f, 1.0f);
            if (randomNumber < turnProbability)
            {
                transform.Rotate(0, Random.Range(0, 2) == 0 ? 90 : -90, 0);
            }
            else if (randomNumber > 1.0f - newPathmakerProbability)
            {
                Instantiate(pathmakerSpherePrefab, transform.position, transform.rotation);
            }

            // Snap position to grid before placing tiles
            Vector3 snappedPosition = SnapToGrid(transform.position, 10f);

            Collider[] colliders = Physics.OverlapSphere(snappedPosition, 1f);
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
                // Remove overlapping tiles
                Collider[] overlappingObjects = Physics.OverlapSphere(snappedPosition, 0.5f);
                foreach (Collider collider in overlappingObjects)
                {
                    if (collider.CompareTag("Floor"))
                    {
                        Destroy(collider.gameObject);
                    }
                }

                // Tile type to instantiate
                Transform chosenTile;
                float flowerChance = FlowerSliderController.flowerPercentage / 100f;
                if (flowerPrefabs.Count > 0 && Random.Range(0.0f, 1.0f) < flowerChance)
                {
                    chosenTile = flowerPrefabs[Random.Range(0, flowerPrefabs.Count)];
                }
                else
                {
                    chosenTile = Random.Range(0.0f, 1.0f) < landTileProbability ? landTilePrefab : grassTilePrefab;
                }

                Instantiate(chosenTile, snappedPosition, Quaternion.identity);

                // Play sound after the first tile placement
                if (!hasPlayedSound)
                {
                    audioSource.Play();
                    hasPlayedSound = true;
                }
                counter++;
                globalTileCount++;
            }



            // Move forward by a grid step (every tile 10 unit -> no overlapping)
            transform.Translate(Vector3.forward * 10f);
            transform.position = SnapToGrid(transform.position, 10f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private Vector3 SnapToGrid(Vector3 originalPosition, float gridSize)
    {
        float snappedX = Mathf.Round(originalPosition.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(originalPosition.z / gridSize) * gridSize;
        return new Vector3(snappedX, originalPosition.y, snappedZ);
    }

    void OnDestroy()
    {
        hasPlayedSound = false;
    }
}
