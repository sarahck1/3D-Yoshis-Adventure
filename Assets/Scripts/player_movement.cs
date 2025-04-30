using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    public KeyCode moveL;
    public KeyCode moveR;

    public float horizVel = 0; 
    public int laneNum = 2;
    public int minLane = 1;
    public int maxLane = 3;
    public bool controlLock = false;

    // coin generator code
    public static int numberofCoins;
    public Text coinsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberofCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
            GetComponent<Rigidbody>().linearVelocity = new Vector3 (horizVel,0,4);
       
            if((Input.GetKeyDown(moveL)) && (laneNum > minLane)&& (controlLock == false))
            {
                horizVel = -2;
                StartCoroutine(stopSlide());
                laneNum -= 1;
                controlLock = true;
            }
             if((Input.GetKeyDown(moveR)) && (laneNum < maxLane) && (controlLock == false))
            {
                horizVel = 2;
                StartCoroutine(stopSlide());
                laneNum += 1;
                controlLock = true;
            }

            coinsText.text = "Coins: " + numberofCoins;
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controlLock = false;
    }


}
