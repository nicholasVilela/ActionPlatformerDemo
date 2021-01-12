using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteraction : MonoBehaviour
{
    private PlayerDirection _direction;
    private PlayerInput _input;
    private State _state;
    private Rigidbody2D _rb;

    private void Start() {
        _direction = GetComponent<PlayerDirection>();
        _input = GetComponent<PlayerInput>();
        _state = GetComponent<State>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        WallSlideController();
        WallJumpController();
    }

    private void WallSlide() => _rb.velocity = new Vector2(_rb.velocity.x, -_state.slideSpeed);
    private void WallJump() => _rb.velocity = new Vector2(_direction.horizontalDirection == Directions.left ? -_state.moveSpeed : _state.moveSpeed, _state.jumpSpeed);

    private void WallSlideController() {
        if (_state.isSliding && !_state.isDashing && !_state.isWallJumping) {
            WallSlide();
            
            if (_state.isOnLeftWall && _direction.horizontalDirection == Directions.left) {
                _direction.FlipRight();
                _direction.LockDirection();
            }
            else if (_state.isOnRightWall && _direction.horizontalDirection == Directions.right) {
                _direction.FlipLeft();
                _direction.LockDirection();
            }
        }
    }

    private void WallJumpController() {
        if (_state.isSliding && _input.jump && !_state.isDashing) {
            WallJump();
            StartCoroutine(WallJumpDuration());
        }
    }

    private IEnumerator WallJumpDuration() {
        _state.isWallJumping = true;

        yield return new WaitForSeconds(_state.wallJumpDuration);

        _state.isWallJumping = false;
    }
}
