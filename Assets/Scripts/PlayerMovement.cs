using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontal;
    private float _speed = 6f;
    private float _jumpingPower = 16f;
    private bool _facingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Joystick joystick;

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= .2f) {
            _horizontal = 1;
        } else if (joystick.Horizontal <= -.2f) {
            _horizontal = -1;
        } else {
            _horizontal = 0;
        }
    
        animator.SetFloat("Speed", Mathf.Abs(_horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
            animator.SetBool("IsJumping", true);
        } else if (IsGrounded()) {
            animator.SetBool("IsJumping", false);
        }

        Flip();
    }

    public void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, _jumpingPower);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontal * _speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (_facingRight && _horizontal < 0f || !_facingRight && _horizontal > 0f) {
            _facingRight = !_facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
