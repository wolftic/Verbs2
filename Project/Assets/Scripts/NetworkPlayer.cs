using UnityEngine;
using System.Collections;
using SocketIO;
using LitJson;

public class Pos
{
    public float x;
    public float y;
    public float z;
}

public class NetworkPlayer : MonoBehaviour {

    private SocketIOComponent _socket;

    public Vector3 pos;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
    }

    void Update()
    {
        Pos p = new Pos();
        p.x = pos.x;
        p.y = pos.y;
        p.z = pos.z;
        string position = JsonMapper.ToJson(p);
        _socket.Emit("move", new JSONObject(position));
    }
}
