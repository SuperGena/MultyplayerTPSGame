using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public Text killCount;
    public Text deathCount;

	void Start ()
    {
        if(UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.GetData(OnReceivedData);
	}

    void OnReceivedData(string data)
    {
        if (killCount == null || deathCount == null)
            return;

        killCount.text = DataTranslater.DataToKills(data).ToString() + " KILLS";
        deathCount.text = DataTranslater.DataToDeaths(data).ToString() + " DEATHS";
    }
	
	
}
