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
    // Only spawn a fruit 15% of the time 
    if (Random.value > 0.15f) return;

    int[] validLanes = { -1, 0, 1 };
    int laneX = validLanes[Random.Range(0, validLanes.Length)];

    Vector3 fruitSpawn = new Vector3(laneX, 3, tilePosition.z);

    int randomFruit = Random.Range(0, 2); // 0 = grape, 1 = apple
    Transform fruit;

    if (randomFruit == 0)
    {
        fruit = Instantiate(grapeObj, fruitSpawn, Quaternion.identity);
    }
    else
    {
        Vector3 appleSpawn = new Vector3(laneX, 0.4f, tilePosition.z);
        Quaternion appleRotation = Quaternion.Euler(-90, 0, 0);
        fruit = Instantiate(appleObj, appleSpawn, appleRotation);
    }

    if (fruit != null)
    {
        Collider col = fruit.GetComponent<Collider>();
        if (col == null)
            col = fruit.gameObject.AddComponent<BoxCollider>();

        col.isTrigger = true;

        if (fruit.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = fruit.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        fruit.gameObject.AddComponent<FruitCollisionHandler>();
    }
}

}

// Embedded collision handling script
public class FruitCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
