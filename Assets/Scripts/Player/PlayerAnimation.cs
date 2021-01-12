using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private State _state;
    private PlayerInput _input;
    private Rigidbody2D _rb;
    public Animator _headAnim;
    public Animator _weaponAnim;
    public Animator _weaponEffectAnim;
    public Animator _cloakAnim;
    public Animator _bodyAnim;

    private void Start() {
        _state = GetComponent<State>();
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        DashingController();
        GroundedController();
        OffGroundController();
        TurningController();
        AttackingController();
    }

    private void AttackingController() {
        if (!_state.isDashing) {
            if (_state.isAttacking) {
                _headAnim.Play("Player_Head_Attacking");
                _weaponAnim.Play("Player_Weapon_Sword_Attacking");
                _weaponEffectAnim.Play("Player_Weapon_Sword_Attacking_Effect");
                if (_state.isGrounded) _cloakAnim.Play("Player_Cloak_Attacking");
                _bodyAnim.Play("Player_Body_Attacking");
            }
        }
        
    }

    private void GroundedController() {
        if(_state.isGrounded && !_state.isDashing && !_state.isAttacking) {
            if (_state.isMoving && !_state.isTurning && !_state.isLanding) {
                var currentAnimName = _bodyAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

                if (currentAnimName == "Player_Body_Running") {
                    _headAnim.Play("Player_Head_Running");
                    _weaponAnim.Play("Player_Weapon_Sword_Idle");
                    _cloakAnim.Play("Player_Cloak_Running");
                    _bodyAnim.Play("Player_Body_Running");
                }
                else {
                    _headAnim.Play("Player_Head_Running_Transition");
                    _weaponAnim.Play("Player_Weapon_Sword_Idle");
                    _cloakAnim.Play("Player_Cloak_Running_Transition");
                    _bodyAnim.Play("Player_Body_Running_Transition");
                }
            }

            else if (!_state.isMoving && !_state.isLanding) {
                _headAnim.Play("Player_Head_Idle");
                _weaponAnim.Play("Player_Weapon_Sword_Idle");
                _cloakAnim.Play("Player_Cloak_Idle");
                _bodyAnim.Play("Player_Body_Idle");
            }

            else if (_state.isLanding) {
                _headAnim.Play("Player_Head_Landing");
                _weaponAnim.Play("Player_Weapon_Sword_Idle");
                _cloakAnim.Play("Player_Cloak_Landing");
                _bodyAnim.Play("Player_Body_Landing");
            }
        }
    }

    private void OffGroundController() {
        if (!_state.isGrounded && !_state.isDashing && !_state.isAttacking) {
            if (_state.isRising && _state.jumpsRemaining == (_state.maxJumps - 1)) {
                _headAnim.Play("Player_Head_Rising");
                _weaponAnim.Play("Player_Weapon_Sword_Idle");
                _cloakAnim.Play("Player_Cloak_Rising");
                _bodyAnim.Play("Player_Body_Rising");
            }

            else if (_state.jumpsRemaining < (_state.maxJumps - 1) && _state.maxJumps > 1 && _state.isRising && !_state.isTurning) {
                _headAnim.Play("Player_Head_Rising_Double_Jump");
                _weaponAnim.Play("Player_Weapon_Sword_Idle");
                _cloakAnim.Play("Player_Cloak_Rising_Double_Jump");
                _bodyAnim.Play("Player_Body_Rising_Double_Jump");
            }
            
            else if (_state.isFalling && !_state.isSliding) {
                var currentAnimName = _cloakAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                
                if (currentAnimName == "Player_Cloak_Falling") {
                    _headAnim.Play("Player_Head_Falling");
                    _weaponAnim.Play("Player_Weapon_Sword_Idle");
                    _cloakAnim.Play("Player_Cloak_Falling");
                    _bodyAnim.Play("Player_Body_Falling");
                }
                else {
                    _headAnim.Play("Player_Head_Falling_Transition");
                    _weaponAnim.Play("Player_Weapon_Sword_Idle");
                    _cloakAnim.Play("Player_Cloak_Falling_Transition");
                    _bodyAnim.Play("Player_Body_Falling_Transition");
                }
            }

            else if (_state.isSliding) {
                _headAnim.Play("Player_Head_Sliding");
                _weaponAnim.Play("Player_Weapon_Sword_Idle");
                _cloakAnim.Play("Player_Cloak_Sliding");
                _bodyAnim.Play("Player_Body_Sliding");
            }
        }
    }

    private void DashingController() {
        if (_state.isDashing && !_state.isAttacking) {
            _headAnim.Play("Player_Head_Dashing");
            _weaponAnim.Play("Player_Weapon_Sword_Idle");
            _cloakAnim.Play("Player_Cloak_Dashing");
            _bodyAnim.Play("Player_Body_Dashing");
        }
    }

    private void TurningController() {
        if (_state.isTurning && _state.jumpsRemaining != 0 && !_state.isFalling && !_state.isDashing && !_state.isAttacking) _headAnim.Play("Player_Head_Turning");
    }
}
