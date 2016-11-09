using UnityEngine;
using System.Collections;
using SocketIO;
using LitJson;

public class NetworkHost : MonoBehaviour {

    [SerializeField]
    public Room self;

    private SocketIOComponent _socket;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("joinedRoom", JoinedRoom);
    }

    void JoinedRoom(SocketIOEvent e)
    {

    }
}
