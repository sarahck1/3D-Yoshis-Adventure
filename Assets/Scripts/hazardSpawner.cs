using UnityEngine;
using System.Linq;


public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab; 
    public float spawnInterval = 2f;
    public float spawnZDistance = 30f;
    public float yPosition = 1; 
    public Transform playerTransform;

     [Header("Lane Settings")]
    public float laneWidth = 3f;             
    public float horizontalOffset = 0f;

    private float[] laneX = new float[] { -1, 0, 1 }; // Lanes 1-3

    void Start()
    {
        InvokeRepeating("SpawnBrick", 1f, spawnInterval);
    }

    void SpawnBrick()
{
   
    int bricksToSpawn = Random.Range(1, 3); 

    // Shuffle lane positions
    float[] shuffledLanes = laneX.OrderBy(x => Random.value).ToArray();

    for (int i = 0; i < bricksToSpawn; i++)
    {
        float x = (shuffledLanes[i] * laneWidth) + horizontalOffset;
        Vector3 spawnPos = new Vector3(x, yPosition, playerTransform.position.z + spawnZDistance);

        GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
        brick.tag = "brick";
    }
}

}