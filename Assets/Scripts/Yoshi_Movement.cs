using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoshi_Movement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    private CharacterController controller;
    private int jumpCount = 0;
    public int maxJumps = 2;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(HorizontalMove, 0, VerticalMove);
        moveDirection.Normalize();
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        controller.SimpleMove(moveDirection * magnitude * speed);

        // Gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Jump
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            ySpeed = jumpSpeed;
            jumpCount++;
        }

        // Apply movement
        Vector3 vel = moveDirection * magnitude;
        vel.y = ySpeed;
        controller.Move(vel * Time.deltaTime);

        // Reset jump count when grounded
        if (controller.isGrounded && ySpeed < 0)
        {
            ySpeed = -0.5f;
            jumpCount = 0;
        }

        // Rotate towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
    }
}