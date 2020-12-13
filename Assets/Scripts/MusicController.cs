using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public bool inBattle;

    // Start is called before the first frame update
    void Start()
    {
        if (!inBattle)
        {
            MusicSingleton.GetInstance().PlayOverworldSong();
        }
        else
        {
            MusicSingleton.GetInstance().PlayBattleSong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
