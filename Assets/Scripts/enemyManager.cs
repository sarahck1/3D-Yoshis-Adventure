using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public int damage = 1; // enemy removes 1 hp from player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            other.GetComponent<playerHealth>().TakeDamage(damage);
        }
    }
}
