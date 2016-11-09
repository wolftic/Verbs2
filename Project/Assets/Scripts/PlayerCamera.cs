using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	public Transform target;
	[SerializeField]
	private float _rotationSpeed = 5f, _zoomSpeed = 5f;
	private float _zoomLevel = 5, _maxZoom = 5f, _minZoom = 1f;
	
	void Update () {
		if (target == null)
			return;
		
		_zoomLevel += -Input.GetAxis ("Mouse ScrollWheel") * _zoomSpeed;
		_zoomLevel = Mathf.Clamp (_zoomLevel, _minZoom, _maxZoom);
		Vector3 pos = new Vector3 (0, _zoomLevel, -_zoomLevel);
		transform.position = Vector3.Lerp(transform.position, target.TransformDirection(pos) + target.position, Time.deltaTime * _rotationSpeed);	
		transform.LookAt (target);
	}
}
