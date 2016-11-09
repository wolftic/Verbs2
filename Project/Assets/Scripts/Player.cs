using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkPlayer))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	private NetworkPlayer _networkPlayer;
	private Rigidbody _rigidbody;

	[SerializeField]
	private float _speed = 1f, _jumpForce = 100f;
	private bool _onGround = true;

	void Start () {
		_networkPlayer = GetComponent<NetworkPlayer> ();
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		//_networkPlayer.pos = transform.position;

		_onGround = Physics.Raycast (transform.position, Vector3.down, 1.1f);
		Debug.DrawRay (transform.position, Vector3.down * 1.1f);

		if (Input.GetButtonDown ("Jump") && _onGround) {
			_rigidbody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
		}
	}

	void FixedUpdate () {
		Vector2 axis = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		_rigidbody.velocity = new Vector3 (axis.x, _rigidbody.velocity.y, axis.y) * _speed;
	}
}
