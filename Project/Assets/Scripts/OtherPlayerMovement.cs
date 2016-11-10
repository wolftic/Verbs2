using UnityEngine;
using System.Collections;

[RequireComponent(typeof(OtherNetworkPlayer))]
public class OtherPlayerMovement : MonoBehaviour {
	private OtherNetworkPlayer _onp;
	[SerializeField]
	private float _speed = 5f;

	void Start () {
		_onp = GetComponent<OtherNetworkPlayer> ();
	}

	void Update () {
		transform.position = Vector3.Lerp (transform.position, new Vector3 ((float)_onp.p.x, (float)_onp.p.y, (float)_onp.p.z), _speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler((float)_onp.p.rotX, (float)_onp.p.rotY, (float)_onp.p.rotZ), _speed * Time.deltaTime);
	}
}
