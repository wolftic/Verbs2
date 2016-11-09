using UnityEngine;
using System.Collections;
using SocketIO;
using LitJson;

public class NetworkHost : MonoBehaviour {

    public PlayerPos localPlayer;

    public Room self;

    private SocketIOComponent _socket;

    [SerializeField]
    private GameObject _localPrefab;
    [SerializeField]
    private GameObject _otherPrefab;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("joinedRoom", JoinedRoom);
    }

    void JoinedRoom(SocketIOEvent e)
    {
        PlayerPos n = JsonMapper.ToObject<PlayerPos>(e.data.ToString());
        self.players.Add(n);
    }

    public void StartGame()
    {
        for (int i = 0; i < self.players.Count; i++)
        {
            if(self.players[i].name == localPlayer.name)
            {
                GameObject local = Instantiate(_localPrefab);
				GameObject.FindGameObjectWithTag ("Camera").GetComponent<PlayerCamera> ().target = local.transform;
            } else
            {
                GameObject other = Instantiate(_otherPrefab);
            }
        }
    }
}
