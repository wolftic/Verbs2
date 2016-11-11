using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private int _score;

    [SerializeField]
    private Text _scoreText;

	void Start () {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward.normalized * 1.5f, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward.normalized * 1.5f, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<OtherNetworkPlayer>().Hit();
                    _score++;
                    _scoreText.text = "Players Killed: " + _score;
                }
            }
        }
    }
}