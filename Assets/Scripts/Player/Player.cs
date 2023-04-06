using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    Rigidbody2D myRigidbody2D;
    [SerializeField] float moveSpeed = 6f;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }

    void Start()
    {
        myRigidbody2D.gravityScale = 0f;
        input.EnableGameplayInput();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move(Vector2 moveInput)
    {
        //Vector2 moveAmount = moveInput * moveSpeed;
        myRigidbody2D.velocity = moveInput * moveSpeed;
    }

    void StopMove()
    {
        myRigidbody2D.velocity = Vector2.zero;
    }

}
