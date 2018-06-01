using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public bool aiming;
    public bool canRun;
    public bool walk;
    public bool shoot;
    public bool reloading;
    public bool onGround;

    public float horizontal;
    public float vertical;
    public Vector3 lookPosition;
    public Vector3 lookHitPosition;
    public LayerMask layerMask;

    public CharacterAudioManager audioManager;

    [HideInInspector]
    public HandleShooting handleShooting;
    [HideInInspector]
    public HandleAnimations handleAnim;

    void Start ()
    {
        audioManager = GetComponent<CharacterAudioManager>();
        handleShooting = GetComponent<HandleShooting>();
        handleAnim = GetComponent<HandleAnimations>();
    }
	
	void FixedUpdate ()
    {
        onGround = IsOnGround();
	}

    bool IsOnGround()
    {
        bool retVal = false;

        Vector3 origin = transform.position + new Vector3(0, 0.05f, 0);
        RaycastHit hit;

        if (Physics.Raycast(origin, -Vector3.up, out hit, 0.5f, layerMask))
        {
            retVal = true;
        }

        return retVal;
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
