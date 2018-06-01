using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIMain : MonoBehaviour {

    [SerializeField]
    RectTransform healthBaerFill;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    private Player player;
    private PlayerMovement controller;

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        PauseMenu.IsOn = false;
    }

	void Update ()
    {
        SetHealthAmount(player.GetHealthPct());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }

    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
    }

    void SetHealthAmount(float _amount)
    {
        healthBaerFill.localScale = new Vector3(1f, _amount, 1f);
    }

}
