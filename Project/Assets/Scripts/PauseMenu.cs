using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	private Canvas canvas;

	void Start () {
		canvas = GetComponent<Canvas> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			canvas.enabled = !canvas.enabled;
		}
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
