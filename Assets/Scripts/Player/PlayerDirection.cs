using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : Direction
{
    private State _state;
    private PlayerInput _input;
    public SpriteRenderer _headSR;
    public SpriteRenderer _weaponSR;
    public SpriteRenderer _weaponEffectSR;
    public SpriteRenderer _cloakSR;
    public SpriteRenderer _bodySR;

    private void Start() {
        _state = GetComponent<State>();
        _input = GetComponent<PlayerInput>();

        horizontalDirection = _bodySR.flipX ? Directions.left : Directions.right;
    }

    private void Update() {
        DirectionController();
    }

    private void DirectionController() {
        if (_state.canChangeDirection) {
            HorizontalController();
            VerticalController();
        }
    }

    private void HorizontalController() {
        if (!_state.isSliding && horizontalDirection == Directions.right && _input.leftAnalog.x < -_state.analogThreshold) FlipLeft();
        else if (!_state.isSliding && horizontalDirection == Directions.left && _input.leftAnalog.x > _state.analogThreshold) FlipRight();
        else if (_state.isSliding && _state.isOnLeftWall && horizontalDirection == Directions.left) FlipRight();
        else if (_state.isSliding && _state.isOnRightWall && horizontalDirection == Directions.right) FlipLeft();
    }

    private void VerticalController() {

    }

    public override void FlipLeft() {
        if (!_state.isTurning && _state.canChangeDirection) StartCoroutine(TurningDuration(Directions.left)); 
    }

    public override void FlipRight() {
        if (!_state.isTurning && _state.canChangeDirection) StartCoroutine(TurningDuration(Directions.right));
    }

    public void FlipHorizontalDirection() {
        if (horizontalDirection == Directions.left) FlipRight();
        else {
            FlipLeft();
        }
    }

    private void FlipAllSprites() {
        _headSR.flipX = true;
        _weaponSR.flipX = true;
        _weaponEffectSR.flipX = true;
        _cloakSR.flipX = true;
        _bodySR.flipX = true;
    }

    private void UnFlipAllSprites() {
        _headSR.flipX = false;
        _weaponSR.flipX = false;
        _weaponEffectSR.flipX = false;
        _cloakSR.flipX = false;
        _bodySR.flipX = false;
    }
    private IEnumerator TurningDuration(string newDirection) {
        _state.isTurning = true;
        // LockDirection();

        yield return new WaitForSeconds(_state.turningDuration);

        horizontalDirection = newDirection;
        _state.isTurning = false;
        // UnlockDirection();

        if (newDirection == Directions.left) FlipAllSprites();
        else {
            UnFlipAllSprites();
        }

    }

    public void LockDirection() {
        _state.canChangeDirection = false;
    }

    public void UnlockDirection() {
        _state.canChangeDirection = true;
    }
}
