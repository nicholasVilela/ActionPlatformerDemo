using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    private PlayerInput _input;
    private State _state;

    private void Start() {
        _input = GetComponent<PlayerInput>();
        _state = GetComponent<State>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("START");
        // Debug.Log(other != null);
        Debug.Log(other.gameObject.tag == "Item");
        // Debug.Log(_input.leftAnalog.y < -_state.analogThreshold);

        if (other != null && other.gameObject.tag == "Item" && _input.leftAnalog.y < -_state.analogThreshold) {
            Debug.Log("AHh");
            var componentItem = other.gameObject.GetComponent<ComponentItem>();

            componentItem.ApplyEffect(_state);
        }
    }

    private void grab() {

    }
}
