using System.Collections;
using UnityEngine;

public class objectSpawner : MonoBehaviour
{
    public Transform grapeObj;
    public Transform appleObj;
    private Vector3 nextFruitSpawn;
    private int randX;

    void Start()
    {
        StartCoroutine(spawnObj());
    }

    IEnumerator spawnObj()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);

            // Random X position, fixed Y = 1
            randX = Random.Range(-1, 2); // -1, 0, or 1
            nextFruitSpawn = new Vector3(randX, 1, transform.position.z);

            // Randomly choose between grape (0) and apple (1)
            int randomFruit = Random.Range(0, 2);

            Transform fruit = null;

            if (randomFruit == 0)
            {
                // Spawn grape with no rotation
                fruit = Instantiate(grapeObj, nextFruitSpawn, Quaternion.identity);
               
            }
            else
            {
                // Rotate apple 90 degrees on X axis
                Quaternion appleRotation = Quaternion.Euler(90, 0, 0);
                fruit = Instantiate(appleObj, nextFruitSpawn, appleRotation);
               
            }

            fruit.gameObject.AddComponent<FruitCollisionHandler>();
        }
    }
}

public class FruitCollisionHandlers : MonoBehaviour
{
    // This will handle when the fruit collides with the player
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
