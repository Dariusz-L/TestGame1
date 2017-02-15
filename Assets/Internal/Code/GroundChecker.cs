using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundChecker : MonoBehaviour {

    public bool isGrounded { get; private set; }
    [SerializeField] List<string> _groundTags;

    void Start() {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other) {
        string otherTag = other.tag;
        foreach (var tag in _groundTags) {
            if (tag == otherTag) {
                isGrounded = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        string otherTag = other.tag;
        foreach (var tag in _groundTags) {
            if (tag == otherTag) {
                isGrounded = false;
            }
        }
    }
}