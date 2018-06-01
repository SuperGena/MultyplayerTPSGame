using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimations : MonoBehaviour {

    Animator anim;

    StateManager states;
    Vector3 lookDirection;

	void Start ()
    {
        states = GetComponent<StateManager>();
        SetupAnimator();
	}
	
	void FixedUpdate ()
    {
        if (PauseMenu.IsOn)
            return;

        states.reloading = anim.GetBool("Reloading");
        anim.SetBool("Aim", states.aiming);

        if (!states.canRun)
        {
            anim.SetFloat("Forward", states.vertical, 0.1f, Time.deltaTime);
            anim.SetFloat("Sideways", states.horizontal, 0.1f, Time.deltaTime);
        }
        else
        {
            float movement = Mathf.Abs(states.vertical) + Mathf.Abs(states.horizontal);

            bool walk = states.walk;

            movement = Mathf.Clamp(movement, 0, (walk || states.reloading) ? 0.5f : 1);

            anim.SetFloat("Forward", movement, 0.1f, Time.deltaTime);
        }
	}

    void SetupAnimator()
    {
        anim = GetComponent<Animator>();

        Animator[] anims = GetComponentsInChildren<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i] != anim)
            {
                anim.avatar = anims[i].avatar;
                Destroy(anims[i]);
                break;
            }
        }
    }

    public void StartReload()
    {
        if (!states.reloading)
        {
            anim.SetTrigger("Reload");
        }
    }
}
