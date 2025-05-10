using UnityEngine;
using System.Collections;

public class shyguy_spawn : MonoBehaviour
{
    public GameObject enemyPreFab;
    public Transform playerTransform;
    public int enemyCount = 0;
    //public float spawnDistZ = 40f;
    private float yPos = 0.6f; //0.37f;
    public float zPosMinOffSet = 20.0f;
    public float zPosMaxOffSet = 48.5f;


    //x position range: -6.4 - -4.1
    public float xPos;
    void Start()
    {
        StartCoroutine(enemySpawn());
    }

   private IEnumerator enemySpawn(){
    while(enemyCount < 100){
        xPos = Random.Range(-1.2f, 1.7f); //might need to readjust values since shy guy spawned weird
        float zPos = playerTransform.position.z + Random.Range(zPosMinOffSet, zPosMaxOffSet); //unsure of last number for range
        Instantiate(enemyPreFab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(-90, 0, 180)); //Quaternion.Euler(-90, 0, 180));
        
        yield return new WaitForSeconds(5); // spawns every 3 seconds
        enemyCount += 1;
    }
   }
}
