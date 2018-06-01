using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    public GameObject mainCamera;

	void Start ()
    {    
           // mainCamera = Camera.main.gameObject;

        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();

            mainCamera.SetActive(false);

        }
        else
        {
            //Camera.main.gameObject.SetActive(false);

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUIMain ui = playerUIInstance.GetComponent<PlayerUIMain>();
            if (ui == null)
                Debug.Log("No PlauerUIMain component on prefab");
            ui.SetPlayer(GetComponent<Player>());

            GetComponent<Player>().PlayerSetup();

            string _username = "Loading...";
            if (UserAccountManager.IsLoggedIn)
                _username = UserAccountManager.LoggedIn_Username;
            else
                _username = transform.name;

            CmdSetUsername(transform.name, _username);
        }

        RegisterPlayer();
    }

    [Command]
    void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManagerMain.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + " has joined!");
            player.username = username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManagerMain.RegisterPlayer(_netID, _player);
    }

    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }   

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (mainCamera != null)
        {
            mainCamera.SetActive(true);
        }
        //if(isLocalPlayer)
        //GameManagerMain.instance.SetSceneCameraActive(true);

        GameManagerMain.UnRegisterPlayer(transform.name);
    }

}
