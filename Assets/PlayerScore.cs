using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour {

    int lastKills = 0;
    int lastDeaths = 0;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreloop());
    }

    void OnDestroy()
    {
        if(player != null)
            SyncNow();
    }

    IEnumerator SyncScoreloop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }        
    }

    void SyncNow()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataRecieves);
        }
    }

    void OnDataRecieves(string data)
    {
        if (player.kills <= lastKills && player.deaths <= lastDeaths)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeaths;

        int kills = DataTranslater.DataToKills(data);
        int deaths = DataTranslater.DataToDeaths(data);

        int newKills = killsSinceLast + kills;
        int newDeaths = deathsSinceLast + deaths;

        string newData = DataTranslater.ValuesToData(newKills, newDeaths);

        Debug.Log("Syncing" + newData);

        lastKills = player.kills;
        lastDeaths = player.deaths;

        UserAccountManager.instance.SendData(newData);
    }

}
