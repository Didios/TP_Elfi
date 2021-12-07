using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundScript : MonoBehaviour
{
    public Character player;

    public AudioSource outsideHouse;
    public AudioSource insideCave;

    // Update is called once per frame
    void Update()
    {
        if(player.actualSound == Place.PATH) 
        { 
            outsideHouse.volume = 1;
            insideCave.volume = 0;
        }
        else if (player.actualSound == Place.HOUSE) 
        {
            outsideHouse.volume = 0.5f;
            insideCave.volume = 0.1f;
        }
        else if (player.actualSound == Place.CAVE) 
        {
            outsideHouse.volume = 0;
            insideCave.volume = 0.15f;
        }
    }
}
