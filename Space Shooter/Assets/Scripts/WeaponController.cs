using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float delay; //tiempo de espera inicial
    public float fireRate; //tiempo en volver a disparar


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start ()
    {
        InvokeRepeating("Fire", delay, fireRate);
	}
	
	private void Update ()
    {
		
	}
    

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
