using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 leftAnalog;
    public bool jump;
    public bool holdingJump;
    public bool dashing;
    public bool attacking;

    private void Update() {
        leftAnalog = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jump = Input.GetButtonDown("Jump");
        holdingJump = Input.GetButton("Jump");
        dashing = Input.GetButtonDown("Dash");
        attacking = Input.GetButtonDown("Attack");
    }
}
