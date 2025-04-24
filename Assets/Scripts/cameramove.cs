using UnityEngine;
using System.Collections;

public class cameramove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3 (0,0,4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
