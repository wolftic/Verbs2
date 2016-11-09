using UnityEngine;
using SocketIO;
using LitJson;
using System.Collections;

public class OtherNetworkPlayer : MonoBehaviour {
	public PlayerPos p;
	private SocketIOComponent _socket;

	void Start () {
		p = new PlayerPos ();	
		_socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
		_socket.On ("OnMove", OnMove);
	}

	public void OnMove(SocketIOEvent e) {
		PlayerPos n = JsonMapper.ToObject<PlayerPos> (e.data.ToString());
		p = n;
	}

	public void Hit() {
		string position = JsonMapper.ToJson(p);
		_socket.Emit ("dead", new JSONObject(position));
	}
}
