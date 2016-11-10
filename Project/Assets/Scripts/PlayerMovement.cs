using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkPlayer))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
	private NetworkPlayer _networkPlayer;
	private Rigidbody _rigidbody;

	[SerializeField]
	private float _speed = 1f, _jumpForce = 100f;
	private bool _onGround = true;

	void Start () {
		_networkPlayer = GetComponent<NetworkPlayer> ();
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update ()
    {
        _networkPlayer.pos = transform.position;
        _networkPlayer.rot = transform.rotation;

        _onGround = Physics.Raycast (transform.position, Vector3.down, 0.55f);
		Debug.DrawRay (transform.position, Vector3.down * 0.55f);

		if (Input.GetButtonDown ("Jump") && _onGround) {
			_rigidbody.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
		}

		if (transform.position.y < -10) {
			transform.position = new Vector3 (0, 10, 0);
		}
	}

	void FixedUpdate () {
		Vector2 axis = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector3 goToPosition = new Vector3 (axis.x, 0f, axis.y) * _speed * Time.fixedDeltaTime;
		_rigidbody.MovePosition(transform.TransformDirection(new Vector3 (0f, 0f, axis.y) * _speed * Time.fixedDeltaTime) + transform.position);
		transform.Rotate (transform.up.normalized, axis.x * _speed);
	}
}
