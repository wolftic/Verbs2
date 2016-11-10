﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour {

	void Start () {
	
	}

    void Update()
    {
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.forward.normalized, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<OtherNetworkPlayer>().Hit();
                }
            }
        }
    }
}