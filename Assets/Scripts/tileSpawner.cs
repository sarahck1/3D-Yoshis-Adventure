using UnityEngine;
using System.Collections;

public class tileSpawner : MonoBehaviour
{
    public Transform tileObj;
    public Transform grapeObj;
    public Transform appleObj;

    private Vector3 nextTileSpawn;

    void Start()
    {
        nextTileSpawn = new Vector3(0, 1, 22);
        StartCoroutine(spawnTile());
    }

    IEnumerator spawnTile()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

        
            Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
            SpawnFruitOnTile(nextTileSpawn);
            nextTileSpawn.z += 4;

            
            Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
            SpawnFruitOnTile(nextTileSpawn);
            nextTileSpawn.z += 4;
        }
    }

    void SpawnFruitOnTile(Vector3 tilePosition)
{
    // Random X lane: -1, 0, or 1
    int[] validLanes = { -1, 0, 1 };
    int laneX = validLanes[Random.Range(0, validLanes.Length)];

    Vector3 fruitSpawn = new Vector3(laneX, 3, tilePosition.z);

    int randomFruit = Random.Range(0, 2); // 0 = grape, 1 = apple

    if (randomFruit == 0)
    {
        Instantiate(grapeObj, fruitSpawn, Quaternion.identity);
    }
    else
    {
        Vector3 appleSpawn = new Vector3(laneX, 0.4f, tilePosition.z); 
        Quaternion appleRotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(appleObj, appleSpawn, appleRotation);
    }
}


}
