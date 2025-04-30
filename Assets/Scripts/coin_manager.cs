using UnityEngine;
using System.Collections;

public class coin_manager:  MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        player_movement.numberofCoins += 1;
     // Debug.Log(player_movement.numberofCoins); 
        Destroy(gameObject);
    }
}