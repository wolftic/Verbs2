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
    public double rotX;
    public double rotY;
    public double rotZ;
}

public class NetworkPlayer : MonoBehaviour {

    private SocketIOComponent _socket;

    public Vector3 pos;
    public Quaternion rot;

	PlayerPos p;

    bool isDead = false;

    void Start()
    {
		p = new PlayerPos();
        p.name = transform.name;
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("OnDead", OnDead);
    }

    void Update()
    {
        p.x = (double)pos.x;
        p.y = (double)pos.y;
        p.z = (double)pos.z;
        p.rotX = (double)rot.x;
        p.rotY = (double)rot.y;
        p.rotZ = (double)rot.z;
        string position = JsonMapper.ToJson(p);
        _socket.Emit("move", new JSONObject(position));
    }

    void OnDead(SocketIOEvent e)
    {
        PlayerPos d = JsonMapper.ToObject<PlayerPos>(e.data.ToString());
        if (d.name == p.name)
        {
            isDead = true;
            gameObject.SetActive(false);
            Invoke("Respawn", 5f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
        string position = JsonMapper.ToJson(p);
        _socket.Emit("respawn", new JSONObject(position));
    }
}
