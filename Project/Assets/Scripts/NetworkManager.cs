using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using LitJson;  

[System.Serializable]
public class Room
{
    public int id;
    public List<PlayerPos> players = new List<PlayerPos>();
}

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    private List<Room> _rooms = new List<Room>();

    private SocketIOComponent _socket;

    [SerializeField]
    private GameObject hostPrefab;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("roomCreated", RoomCreated); 
    }

    public void CreateRoom()
    {
        Room room = new Room();
        room.id = 10/*Random.Range(1, 100)*/;
        GameObject host = Instantiate(hostPrefab);
        host.GetComponent<NetworkHost>().self = room;
        _rooms.Add(room);

        PlayerPos player = new PlayerPos();
        player.name = "hey";

        room.players.Add(player);

        string roomString = JsonMapper.ToJson(room);
        _socket.Emit("newRoom", new JSONObject(roomString));
    }

    void RoomCreated(SocketIOEvent e)
    {
        Room room = JsonMapper.ToObject<Room>(e.data.ToString());
        _rooms.Add(room);
    }

    public void JoinRoom()
    {
        PlayerPos player = new PlayerPos();
        player.name = "hoi";

        for (int i = 0; i < _rooms.Count; i++)
        {
            if(_rooms[i].id == 10)
            {
                _rooms[i].players.Add(player);

                GameObject host = Instantiate(hostPrefab);
                host.GetComponent<NetworkHost>().self = _rooms[i];

                string roomString = JsonMapper.ToJson(_rooms[i]);
                _socket.Emit("joinRoom",new JSONObject(roomString));
            }
        }
    }
}
