using UnityEngine;
using System.Collections;
using SocketIO;
using LitJson;

[System.Serializable]
public class PlayerPos
{
    public string name;
    public double x;
    public double y;
    public double z;
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
        PlayerPos p = new PlayerPos();
        p.x = (double)pos.x;
        p.y = (double)pos.y;
        p.z = (double)pos.z;
        string position = JsonMapper.ToJson(p);
        _socket.Emit("move", new JSONObject(position));
    }
}
