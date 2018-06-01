using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeTarget : MonoBehaviour {

    public Animator anim;

	public void HitTarget()
    {
        anim.SetBool("Down", true);
    }
}
