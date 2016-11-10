using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using LitJson;  

[System.Serializable]
public class Name
{
    public string name;
}

[System.Serializable]
public class Room
{
    public int id;
    public List<PlayerPos> players = new List<PlayerPos>();
}

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    private Name pname = new Name();

    [SerializeField]
    private List<Room> _rooms = new List<Room>();

    private SocketIOComponent _socket;

    [SerializeField]
    private GameObject hostPrefab;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("roomCreated", RoomCreated);
        _socket.On("name", SetName);
    }

    void SetName(SocketIOEvent e)
    {
        Name n = JsonMapper.ToObject<Name>(e.data.ToString());
        pname.name = n.name;
    }

    public void CreateRoom()
    {
        Room room = new Room();
        room.id = 10/*Random.Range(1, 100)*/;
        GameObject host = Instantiate(hostPrefab);
        host.GetComponent<NetworkHost>().self = room;
        _rooms.Add(room);

        PlayerPos player = new PlayerPos();
        player.name = pname.name;

        host.GetComponent<NetworkHost>().localPlayer = player;

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
        /*for (int i = 0; i < _rooms.Count; i++)
        {
            if(_rooms[i].id == 10)
            {
                PlayerPos player = new PlayerPos();
                player.name = pname.name;

                _rooms[i].players.Add(player);

                GameObject host = Instantiate(hostPrefab);
                host.GetComponent<NetworkHost>().self = _rooms[i];
                host.GetComponent<NetworkHost>().localPlayer = player;

                string roomString = JsonMapper.ToJson(_rooms[i]);
                _socket.Emit("joinRoom",new JSONObject(roomString));
            }
        }*/

        Room j = new Room();
        j.id = 10;

        PlayerPos player = new PlayerPos();
        player.name = pname.name;
        j.players.Add(player);

        GameObject host = Instantiate(hostPrefab);
        host.GetComponent<NetworkHost>().localPlayer = player;

        string roomString = JsonMapper.ToJson(j);
        _socket.Emit("joinRoom", new JSONObject(roomString));
    }
}
