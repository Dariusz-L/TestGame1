using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SpringBoardController : MonoBehaviour {

    [SerializeField] float _springStrength;
    [SerializeField] float _springLength;
    [SerializeField] float _coolDownTimeSeconds;

    float _coolDownTimer;
    bool _isPlayerOnSpringBoard;
    bool _execute;

    Transform _transform;
    Rigidbody _playerRb;
    Rigidbody _rb;

    Vector3 _defaultPos;
    Vector3 _limitPos;

    void Awake () {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start() {
        _defaultPos = _transform.position;

        _limitPos = _defaultPos;
        _limitPos.y += _springLength;

        _rb.isKinematic = false;
        _rb.useGravity = false;

        _playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        GetComponent<BoxCollider>().size = new Vector3(0.95f, 1.0f, 1.0f);
    }

    void Update() {

        Vector3 curPos = _transform.position;
        if (curPos.y >= _limitPos.y) {
            _transform.position = new Vector3(curPos.x, _limitPos.y, curPos.z);
            _rb.isKinematic = true;
            _coolDownTimer = 0.0f;

            Vector3 playerPos = _playerRb.transform.position;
            playerPos.y = curPos.y;
            _playerRb.transform.position = playerPos;
            _playerRb.velocity = new Vector3(_playerRb.velocity.x, _springStrength, 0);
        }

        if (_rb.isKinematic) {
            _coolDownTimer += Time.deltaTime;
            _transform.position = Vector3.Lerp(_limitPos, _defaultPos, _coolDownTimer / _coolDownTimeSeconds);
            if (_transform.position.y <= _defaultPos.y) {
                _transform.position = new Vector3(curPos.x, _defaultPos.y, curPos.z);
                _rb.isKinematic = false;

                if (_isPlayerOnSpringBoard) {
                    _execute = true;
                }
            }
        }
    }

    void FixedUpdate() {
        if (_execute) {
            _execute = false;
            _rb.velocity = new Vector3(0, _springStrength, 0);
        }
    }

    void OnCollisionEnter(Collision other) {
        string otherTag = other.collider.tag;
        if (otherTag == _playerRb.tag) {
            _isPlayerOnSpringBoard = true;
            if (_transform.position == _defaultPos) {
                _execute = true;
            }
        }
    }

    void OnCollisionExit(Collision other) {
        string otherTag = other.collider.tag;
        if (otherTag == _playerRb.tag) {
            _isPlayerOnSpringBoard = false;
        }
    }
}