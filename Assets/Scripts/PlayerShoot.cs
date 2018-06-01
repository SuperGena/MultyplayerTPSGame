using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (PauseMenu.IsOn)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 100, mask))
        {           
            Debug.Log("We hit " + _hit.collider.name);
           
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage, transform.name);                

            }

        }
        //Vector3 direction = states.lookHitPosition - bulletSpawnPoint.position;
        //RaycastHit hit;

        //if (Physics.Raycast(bulletSpawnPoint.position, direction, out hit, 100, states.layerMask))
        //{
        //    Debug.Log("We hit" + hit.collider.name);

        //}
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + "has been shot.");

        // Destroy(GameObject.Find(_ID));
        Player _player = GameManagerMain.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, _sourceID);

    }
}
