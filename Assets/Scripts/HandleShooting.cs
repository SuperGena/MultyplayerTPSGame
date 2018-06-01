using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HandleShooting : NetworkBehaviour {

    StateManager states;
    public Animator weaponAnim;
    public float fireRate;
    float timer;
    public Transform bulletSpawnPoint;
    public GameObject smokeParticle;
    public ParticleSystem[] muzzle;
    public GameObject casingPrefab;
    public Transform caseSpawn;

    public Weapon weapon;

    private const string PLAYER_TAG = "Player";
    
    public int curBullets = 30;

	void Start ()
    {
        states = GetComponent<StateManager>();
	}

    bool shoot;
    bool dontShoot;
    bool emptyGun;
	
	void Update ()
    {
        if (PauseMenu.IsOn)
            return;

        shoot = states.shoot;

        if (Input.GetButtonDown("Fire1") && shoot)
        {
            if (timer <= 0)
            {
                weaponAnim.SetBool("Shoot", false);

                if (curBullets > 0)
                {
                    emptyGun = false;
                    states.audioManager.PlayGunSound();

                    //GameObject go = Instantiate(casingPrefab, caseSpawn.position, caseSpawn.rotation) as GameObject;
                    //Rigidbody rig = go.GetComponent<Rigidbody>();
                    //rig.AddForce(transform.right.normalized * 2 + Vector3.up * 1.3f, ForceMode.Impulse);
                    //rig.AddRelativeTorque(go.transform.right * 1.5f, ForceMode.Impulse);

                    for (int i = 0; i < muzzle.Length; i++)
                    {
                        muzzle[i].Emit(1);
                    }
                    RaycastShoot();

                    //weapon.Shoot();


                    curBullets = curBullets - 1;
                }
                else
                {
                    if (emptyGun)
                    {
                        states.handleAnim.StartReload();
                        curBullets = 30;
                    }
                    else
                    {
                        states.audioManager.PlayEffect("emty_gun");
                        emptyGun = true;
                    }
                }
                timer = fireRate;
            }
            else
            {
                
                    weaponAnim.SetBool("Shoot", true);

                    weapon.Shoot();

                
                    timer -= Time.deltaTime;

            }
        }
        else
        {
            timer = -1;
            weaponAnim.SetBool("Shoot", false);
        }
	}

    [Client]
    void RaycastShoot()
    {
        Vector3 direction = states.lookHitPosition - bulletSpawnPoint.position;
        RaycastHit hit;

        if (Physics.Raycast(bulletSpawnPoint.position, direction, out hit, 100, states.layerMask))
        {
            GameObject go = Instantiate(smokeParticle, hit.point, Quaternion.identity) as GameObject;
            go.transform.LookAt(bulletSpawnPoint.position);
                       
        }
    }



    //public void TargetLook()
    //{
    //    Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
    //    }
    //    else
    //    {
    //        targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward * 200, Time.deltaTime * 5);
    //    }
    //}
}
