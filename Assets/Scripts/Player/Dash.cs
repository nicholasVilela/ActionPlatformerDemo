using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private PlayerDirection _direction;
    private State _state;
    private PlayerInput _input;
    private Rigidbody2D _rb;

    private void Start() {
        _direction = GetComponent<PlayerDirection>();
        _state = GetComponent<State>();
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        DashingController();
    }

    private void ExecuteDash() => _rb.velocity = new Vector2((_direction.horizontalDirection == Directions.left ? -1 : 1) * _state.dashSpeed, 0f);

    private void DashingController() {
        if (_input.dashing && _state.canDash && !_state.isDashing && !_state.isAttacking) StartCoroutine(DashDuration());

        if (_state.isDashing && !_state.isAttacking) {
            ExecuteDash();

            _state.currentDashDuration -= Time.deltaTime;
            if (_state.currentDashDuration < 0f) _state.currentDashDuration = 0f;
        }
    }

    private IEnumerator DashDuration() {
        _state.isDashing = true;
        _state.currentDashDuration = _state.dashDuration;
        var originalGravityScale = _rb.gravityScale;
        _rb.gravityScale = 0f;

        StartCoroutine(StartDashCooldown());

        yield return new WaitForSeconds(_state.dashDuration);

        _state.isDashing = false;
        _rb.gravityScale = originalGravityScale;
    }

    private IEnumerator StartDashCooldown() {
        _state.canDash = false;

        yield return new WaitForSeconds(_state.dashCooldown);

        _state.canDash = true;
    }
}
