using UnityEngine;
using System.Collections;

public class shyguy_spawn : MonoBehaviour
{
    public GameObject enemyShyGuy;
    public int enemyCount = 0;
    public float spawnDistZ = 40f;
    public float yPos = 0.37f;
    public float zPos;

    //x position range: -6.4 - -4.1
    public float xPos;
    void Start()
    {
        StartCoroutine(Spawn());
    }

   private IEnumerator Spawn(){
    while(enemyCount < 100){
        xPos = Random.Range(-1f, 1f); //might need to readjust values since shy guy spawned weird
        zPos = Random.Range(40f, 1000f); //unsure of last number for range
        Instantiate(enemyShyGuy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        
        yield return new WaitForSeconds(4);
        enemyCount += 1;
    }
   }
}
