using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_BGM : MonoBehaviour
{
    public AudioClip BackgroundMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = BackgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
