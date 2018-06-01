using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

    public StateManager states;
    public Weapon weapon;

    public bool debugAiming;
    public bool isAiming;

    public void InputUpdate()
    {
        if (!debugAiming)
            states.aiming = Input.GetMouseButton(1);
        else
            states.aiming = isAiming;

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Shoot();
        }
    }
}
