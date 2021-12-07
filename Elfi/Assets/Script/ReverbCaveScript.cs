using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbCaveScript : MonoBehaviour
{
    public Character player;

    private AudioReverbZone audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioReverbZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.actualSound == Place.CAVE)
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }
    }
}
