using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public Vector3 posMax;
    public Vector3 posMin;

    public float speed;

    public AudioClip[] sounds;
    private AudioSource audioSource;

    private Vector3 target;
    private float Timer = -1;
    private float SoundTimer = -1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        SoundTimer -= Time.deltaTime;

        if (Timer < 0)
        {
            Timer = Random.Range(1, 5.0f);
            target = Random.insideUnitSphere * 100;// on redéfinit une cible aléatoire
            // on vérifie que la nouvelle cible est dans le terrain
            if (target.x > posMax.x) { target.x = posMax.x; }
            else if (target.x < posMin.x) { target.x = posMin.x; }

            if (target.y > posMax.y) { target.y = posMax.y; }
            else if (target.y < posMin.y) { target.y = posMin.y; }

            if (target.z > posMax.z) { target.z = posMax.z; }
            else if (target.z < posMin.z) { target.z = posMin.z; }
        }

        if (SoundTimer < 0)
        {
            SoundTimer = Random.Range(1, 10.0f);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.volume = Random.Range(0.7f, 1);
            audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        }


        transform.LookAt(target);
        if (transform.position != target)
        {
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }
}
