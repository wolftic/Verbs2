using UnityEngine;
using SocketIO;
using LitJson;
using System.Collections;

public class OtherNetworkPlayer : MonoBehaviour {
	public PlayerPos p;
	private SocketIOComponent _socket;
    private bool _isDead = false;

	void Start () {
		p = new PlayerPos ();
        p.name = transform.name;
		_socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
		_socket.On ("OnMove", OnMove);
        _socket.On("OnDead", OnDead);
        _socket.On("OnRespawn", OnRespawn);
    }

	public void OnMove(SocketIOEvent e) {
		PlayerPos n = JsonMapper.ToObject<PlayerPos> (e.data.ToString());
        if (n.name == p.name)
        {
            p = n;
        }
    }

	public void Hit() {
		string position = JsonMapper.ToJson(p);
		_socket.Emit ("dead", new JSONObject(position));
        _isDead = true;
        gameObject.SetActive(false);
	}

    void OnDead(SocketIOEvent e)
    {
        PlayerPos d = JsonMapper.ToObject<PlayerPos>(e.data.ToString());
        if (d.name == p.name)
        {
            _isDead = true;
            gameObject.SetActive(false);
        }
    }

    void OnRespawn(SocketIOEvent e)
    {
        PlayerPos d = JsonMapper.ToObject<PlayerPos>(e.data.ToString());
        if (d.name == p.name)
        {
            _isDead = false;
            gameObject.SetActive(true);
        }
    }
}
