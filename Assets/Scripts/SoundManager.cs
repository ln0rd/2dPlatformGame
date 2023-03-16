using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource sfx;
    public AudioSource music;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        //  não queremos que seja destruido quando seja passado para outra fase;
        // pois aqui queremos a musica constante;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }
}
