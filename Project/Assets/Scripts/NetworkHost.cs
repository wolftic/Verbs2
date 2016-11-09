using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
        DontDestroyOnLoad(this.gameObject);
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("joinedRoom", JoinedRoom);
        _socket.On("otherStart", OtherStarted);
    }

    void JoinedRoom(SocketIOEvent e)
    {
        PlayerPos n = JsonMapper.ToObject<PlayerPos>(e.data.ToString());
        self.players.Add(n);
    }

    public void StartGame(bool l)
    {
        if (l)
        {
            SceneManager.LoadScene("lorenzo 1");
            Invoke("SpawnPlayers", 2.0f);
            _socket.Emit("StartGame");
        } else
        {
            SceneManager.LoadScene("lorenzo 1");
            Invoke("SpawnPlayers", 2.0f);
        }
    }

    void OtherStarted(SocketIOEvent e)
    {
        Debug.Log("hey");
        StartGame(false);
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < self.players.Count; i++)
        {
            if (self.players[i].name == localPlayer.name)
            {
                GameObject local = Instantiate(_localPrefab);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>().target = local.transform;
            }
            else
            {
                GameObject other = Instantiate(_otherPrefab);
            }
        }
    }
}
