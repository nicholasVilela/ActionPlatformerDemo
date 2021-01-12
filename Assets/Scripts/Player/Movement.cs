using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private State _state;
    private PlayerInput _input;
    private Rigidbody2D _rb;
    private PlayerDirection _direction;

    private void Start() {
        _direction = GetComponent<PlayerDirection>();
        _state = GetComponent<State>();
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -25, 25));
        MovementController();
    }

    private void MovementController() {
        if (_state.isMoving && !_state.isDashing && !_state.isWallJumping) {
            if (_input.leftAnalog.x > _state.analogThreshold) Move(_state.moveSpeed);
            if (_input.leftAnalog.x < -_state.analogThreshold) Move(-_state.moveSpeed);
        }
        else if (!_state.isMoving && !_state.isSliding && !_state.isWallJumping && !_state.isDashing) Move(0f);
    }

    private void Move(float moveSpeed) => _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
}
