using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    AudioClip openSound;
    [SerializeField]
    AudioClip closeSound;

    // Start is called before the first frame update
    void Start()
    {
        //Assignation de son propre animator en tant que variable pour pouvoir y accéder plus simplement
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //déclence l'animation d'ouverture des portes
    //Y intégrer le jeu d'un son ? Le lancement d'une corroutine ?
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("In", true); 
        
        if (openSound != null) { GetComponent<AudioSource>().PlayOneShot(openSound); }
        else { Debug.Log("Son ouverture"); }
    }

    //déclence l'animation de fermeture des portes
    //Y intégrer le jeu d'un son ? Le lancement d'une corroutine ?
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("In", false);

        if (closeSound != null) { GetComponent<AudioSource>().PlayOneShot(closeSound); }
        else { Debug.Log("Son fermeture"); }
    }

    //Créer une fonction publique à appeler lors d'un animation event ?

}
