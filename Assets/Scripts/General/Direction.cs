using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public string horizontalDirection;
    public string verticalDirection;

    private SpriteRenderer _sr;
    private State _state;

    private void Start() {
        _state = GetComponent<State>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public virtual void FlipLeft() {
        _sr.flipX = true;
        horizontalDirection = Directions.left;
    }

    public virtual void FlipRight() {
        _sr.flipX = false;
        horizontalDirection = Directions.right;
    }
}
