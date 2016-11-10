using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour {
	private float cooldownTime;

	void Start () {
	
	}

    void Update()
    {
        RaycastHit hit;

		Debug.DrawRay (transform.position, transform.forward.normalized * 1.5f, Color.red);

		if (cooldownTime < Time.time) {
			if (Input.GetButtonDown ("Fire1")) {
				if (Physics.Raycast (transform.position, transform.forward.normalized * 1.5f, out hit)) {
					if (hit.transform.tag == "Player") {
						hit.transform.GetComponent<OtherNetworkPlayer> ().Hit ();
					}
				}
			}

			cooldownTime = Time.time + 1f;
		}
    }
}