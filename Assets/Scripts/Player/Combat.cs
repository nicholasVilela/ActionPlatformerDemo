using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private PlayerInput _input;
    private State _state;
    private PlayerDirection _direction;
    private Rigidbody2D _rb;
    public Transform attackPoint;
    public Vector2 offset;

    private void Start() {
        _direction = GetComponent<PlayerDirection>();
        _input = GetComponent<PlayerInput>();
        _state = GetComponent<State>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        AttackPointController();
        // AttackController();
    }

    private void AttackPointController() {
        attackPoint.transform.localPosition = _direction.horizontalDirection == Directions.left
            ? new Vector2(-offset.x, offset.y)
            : new Vector2(offset.x, offset.y);
    }
}
