using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [Header("Constants")]
    public float moveSpeed;
    public float jumpSpeed;
    public float fallSpeed;
    public float toZeroSpeed;
    public float slideSpeed;
    public int maxJumps;
    public float groundedRaycastDistance;
    public float slidingRaycastDistance;
    public float wallRaycastDistance;
    public float analogThreshold;
    public float turningDuration;
    public float dashDuration;
    public float dashCooldown;
    public float dashSpeed;
    public float wallJumpDuration;
    public float landingDuration;
    public float attackDuration;

    [Header("State")]
    public bool isGrounded;
    public bool isLanding;
    public bool wasGrounded;
    public bool isMoving;
    public bool isAttacking;
    public bool isJumping;
    public bool isTurning;
    public bool isRising;
    public bool isFalling;
    public bool isDashing;
    public bool isSliding;
    public bool isOnLeftWall;
    public bool isOnRightWall;
    public bool isWallJumping;
    public bool canChangeDirection;
    public int jumpsRemaining;
    public bool canDash;
    public float currentDashDuration;

    [Header("Dependencies")]
    public Transform feetPos;
    public Transform leftPos;
    public Transform rightPos;
    public LayerMask groundLayer;

    private PlayerInput _input;
    private Rigidbody2D _rb;

    private void Start() {
        _input = GetComponent<PlayerInput>();
        _rb    = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        StateController();
    }

    private void StateController() {
        AttackingController();
        GroundedController();

        canChangeDirection = !isAttacking && !isDashing && !isWallJumping;
        isMoving   = _input.leftAnalog.x > analogThreshold || _input.leftAnalog.x < -analogThreshold;
        isRising   = !isGrounded && _rb.velocity.y > 0f;
        isFalling  = !isGrounded && _rb.velocity.y < 0f;
        jumpsRemaining = (isGrounded || isSliding) && (!_input.holdingJump && !_input.jump)
            ? maxJumps 
            : jumpsRemaining;
        
        WallController();
        SlidingController();
    }

    private void GroundedController() {
        wasGrounded = isGrounded;

        isGrounded  = !IsColliderNull(Physics2D.Raycast(feetPos.position, Vector2.down, groundedRaycastDistance, groundLayer));
        Debug.DrawRay(feetPos.position, new Vector2(0, -groundedRaycastDistance), Color.yellow);

        if (isGrounded && !wasGrounded) StartCoroutine(LandingDuration());
    }

    private bool IsColliderNull(RaycastHit2D hit) => hit.collider == null;

    private void WallController() {
        isOnLeftWall = 
            !isGrounded && 
            !IsColliderNull(Physics2D.Raycast(leftPos.position, Vector2.left, wallRaycastDistance, groundLayer));
        Debug.DrawRay(leftPos.position, new Vector2(-(1 * wallRaycastDistance), 0), Color.green);

        isOnRightWall = 
            !isGrounded &&
            !IsColliderNull(Physics2D.Raycast(rightPos.position, Vector2.right, wallRaycastDistance, groundLayer));
        Debug.DrawRay(rightPos.position, new Vector2((1 * wallRaycastDistance), 0), Color.red);
    }

    private void SlidingController() {
        if (
            (isOnLeftWall || isOnRightWall) && 
            _rb.velocity.x != 0f && 
            !isSliding && 
            !_input.holdingJump) {
            isSliding = true;

            if (isDashing) StartCoroutine(FinishDash());
        }
        else if (
            isSliding &&
                (
                    isDashing ||
                    isGrounded ||
                    isWallJumping ||
                    (!isOnLeftWall && !isOnRightWall)
                )
        ) {
            isSliding = false;
        }
    }
    
    private IEnumerator FinishDash() {
        yield return new WaitForSeconds(currentDashDuration);

        isSliding = 
            (isOnLeftWall || isOnRightWall) && 
            _rb.velocity.x != 0f && 
            !isSliding &&
            !_input.holdingJump;
    }

    private IEnumerator LandingDuration() {
        isLanding = true;

        yield return new WaitForSeconds(landingDuration);

        isLanding = false;
    }

    private void AttackingController() {
        if (_input.attacking && !isDashing && !isAttacking) {
            StartCoroutine(AttackingDuration());
        }
    }

    private IEnumerator AttackingDuration() {
        isAttacking = true;
        // _state.canChangeDirection = false;
        // _direction.LockDirection();

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        // _state.canChangeDirection = true;
        // _direction.UnlockDirection();
    }
}