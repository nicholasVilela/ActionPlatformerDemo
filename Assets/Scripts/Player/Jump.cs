using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private PlayerInput _input;
    private State _state;
    private Rigidbody2D _rb;

    private void Start() {
        _input = GetComponent<PlayerInput>();
        _state = GetComponent<State>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        JumpingController();
    }

    private void Rise() => _rb.velocity = new Vector2(_rb.velocity.x, _state.jumpSpeed);

    private void JumpingController() {
        if (_input.jump && _state.jumpsRemaining > 0) {
            _state.jumpsRemaining -= 1;
            Rise();
        }

        else if ((_state.isRising && !_input.holdingJump) || _state.isFalling) {
            Fall();
        }
    }
    
    private void Fall() {
        if (_rb.velocity.y > 0f) _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, 0f, Time.deltaTime * _state.toZeroSpeed));
        else {
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, -25f, Time.deltaTime * _state.fallSpeed));
        }
    }   
}
