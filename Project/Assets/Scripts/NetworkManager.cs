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

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    private Name pname = new Name();

    [SerializeField]
    private List<Name> _players = new List<Name>();

    private SocketIOComponent _socket;

    [SerializeField]
    private GameObject _localPrefab;
    [SerializeField]
    private GameObject _otherPrefab;

    void Start()
    {
        _socket = GameObject.Find("Socket").GetComponent<SocketIOComponent>();
        _socket.On("name", SetName);
        _socket.On("otherStart", otherStart);
    }

    void SetName(SocketIOEvent e)
    {
        Name n = JsonMapper.ToObject<Name>(e.data.ToString());
        pname.name = n.name;
        _players.Add(n);

        SpawnPlayers();
    }

    public void StartGame()
    {
        GameObject.Find("Button").active = false;

        GameObject local = Instantiate(_localPrefab, new Vector3(Random.Range(-6, 3), 1, Random.Range(-6, 3)), Quaternion.identity) as GameObject;
        local.transform.name = pname.name;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>().target = local.transform;

        string d = JsonMapper.ToJson(pname);
        _socket.Emit("onOtherStart", new JSONObject(d));
    }

    void otherStart(SocketIOEvent e)
    {
        Name n = JsonMapper.ToObject<Name>(e.data.ToString());
        _players.Add(n);
        GameObject other = Instantiate(_otherPrefab);
        other.transform.name = n.name;

    }

    void SpawnPlayers()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].name != pname.name)
            {
                GameObject other = Instantiate(_otherPrefab);
                Debug.Log(_players[i].name);
                other.transform.name = _players[i].name;
            }
        }
    }
}
