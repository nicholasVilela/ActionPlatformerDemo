using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentItem : MonoBehaviour
{
    public string type;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll != null && coll.gameObject.tag == "Player") {
            ApplyEffect(coll.GetComponent<State>());
        }
    }

    public void ApplyEffect(State _state) {
        if (type == "Jump") _state.maxJumps += 1;

        Destroy(gameObject);
    }
}
