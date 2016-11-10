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
        Room r = JsonMapper.ToObject<Room>(e.data.ToString());
        //self.players.Add(r.players[1]);
        self = r;
    }

    public void StartGame(bool l)
    {
        if (l)
        {
            SceneManager.LoadScene("lorenzo 1");
            Invoke("SpawnPlayers", 2.0f);
            this.GetComponent<Canvas>().GetComponent<Canvas>().enabled = false;
            _socket.Emit("StartGame");
        } else
        {
            SceneManager.LoadScene("lorenzo 1");
            this.GetComponent<Canvas>().GetComponent<Canvas>().enabled = false;
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
                GameObject local = Instantiate(_localPrefab,new Vector3(Random.Range(-6,3),1,Random.Range(-6,3)),Quaternion.identity) as GameObject;
                local.transform.name = self.players[i].name;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>().target = local.transform;
            }
            else
            {
                GameObject other = Instantiate(_otherPrefab);
                Debug.Log(self.players[i].name);
                other.transform.name = self.players[i].name;
            }
        }
    }
}
