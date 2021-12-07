using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveScript : MonoBehaviour
{
    public float YBase;
    public float YFinal;
    public float speed = 0.2f;


    [SerializeField]
    AudioClip[] spawnSounds;
    [SerializeField]
    AudioClip[] zombieSounds;

    private float Timer;
    private AudioSource audioSource;

    private bool visible = false;
    private bool start = true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(transform.position.x, YBase, transform.position.z);
        Timer = Random.Range(3, 10.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0)
        {
            if (visible)
            {
                Timer = Random.Range(1.0f, 5.0f);
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.volume = Random.Range(0.8f, 1);
                audioSource.PlayOneShot(zombieSounds[Random.Range(0, zombieSounds.Length)]);
            }
            else
            {
                if (start)
                {
                    audioSource.PlayOneShot(spawnSounds[Random.Range(0, spawnSounds.Length)]);
                    start = false;
                }

                if (transform.position.y > YFinal - 0.01f)
                {
                    Timer = Random.Range(1.0f, 5.0f);
                    visible = true;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, YFinal, transform.position.z), speed * Time.deltaTime);
                }
            }
        }        
    }
}
