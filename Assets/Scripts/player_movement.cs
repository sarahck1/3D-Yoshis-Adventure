using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{
    public KeyCode moveL;
    public KeyCode moveR;
    public KeyCode jumpKey = KeyCode.Space;

    public float horizVel = 0;
    public float jumpForce = 5.5f;
    public int laneNum = 2;
    public int minLane = 1;
    public int maxLane = 3;
    public bool controlLock = false;
    public bool isGrounded = true;

    public static int numberofCoins;
    public Text coinsText;

    public GameObject gameOverUI;

    private Rigidbody rb;

    public float laneWidth = 2.5f;
    

    void Start()
    {
        numberofCoins = 0;
        rb = GetComponent<Rigidbody>();

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
   
    }

    void Update()
    {
        rb.linearVelocity = new Vector3(horizVel, rb.linearVelocity.y, 4.15f);

        if ((Input.GetKeyDown(moveL)) && (laneNum > minLane) && !controlLock)
        {
            horizVel = -laneWidth;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            controlLock = true;
        }
        if ((Input.GetKeyDown(moveR)) && (laneNum < maxLane) && !controlLock)
        {
            horizVel = laneWidth;
            StartCoroutine(stopSlide());
            laneNum += 1;
            controlLock = true;
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        coinsText.text = "Coins: " + numberofCoins;

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
    if (numberofCoins > savedHighScore)
    {
        PlayerPrefs.SetInt("HighScore", numberofCoins);
        PlayerPrefs.Save();
    }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        horizVel = 0;
        controlLock = false;
    }

    void OnCollisionEnter(Collision collision)
{


    if (collision.gameObject.CompareTag("brick"))
    {
        Object.FindFirstObjectByType<GameManager>().GameOver();
    }


    if (collision.contacts[0].normal.y > 0.5f)
    {
        isGrounded = true;
    }
}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("lethal"))
    {
       
        if (!Object.FindFirstObjectByType<playerHealth>().isInvincible)
        {
           
            Object.FindFirstObjectByType<playerHealth>().TakeDamage(1);
        }

        Destroy(other.gameObject); 

        //iframes
        StartCoroutine(Object.FindFirstObjectByType<playerHealth>().TemporaryInvincibility(1f));
    }
}




}