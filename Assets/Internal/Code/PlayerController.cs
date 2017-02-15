using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] GroundChecker _groundChecker;

    float _moveDir;
    bool _jumpTriggered;

    Transform _transform;
    Rigidbody _rb;

    void Start() {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.A))
            _moveDir = -1;
        else if (Input.GetKey(KeyCode.D))
            _moveDir = 1;
        else
            _moveDir = 0;

        if (_groundChecker.isGrounded && Input.GetButtonDown("Jump")) {
            _jumpTriggered = true;
        }

        Vector3 pos = _transform.position;
        if (pos.x > 7) {
            pos.x = -5;
            _transform.position = pos;
        }
    }

    void FixedUpdate() {
        _rb.velocity = new Vector3(_moveDir * _speed, _rb.velocity.y, 0);

        if (_jumpTriggered) {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, 0); ;
            _jumpTriggered = false;
        }
    }
}