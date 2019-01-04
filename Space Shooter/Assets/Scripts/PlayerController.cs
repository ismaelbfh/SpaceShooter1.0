using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    
    [Header("Movement")]
    public float speed = 10f;
    public float tilt = 4f;
    public Boundary boundary;

    [Header("Shooting")]
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;

    private float nextFire;
    private Rigidbody rig;

    private AudioSource audioSource;
    
    private void Awake ()
    {
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateBoundary();
    }

    private void UpdateBoundary()
    {
        Vector2 half = Camera.main.GetHalfDimensionsInWorldUnits();
            //Utils.GetHalfDimensionsInWorldUnits();
        // - 6, 6, -4, 8
        boundary.xMin = -half.x + 0.7f;
        boundary.xMax = half.x - 0.7f;
        boundary.zMin = -half.y + 6f;
        boundary.zMax = half.y - 2f;
    }

    private void Update()
    {
        //Si se pulsa la tecla y aparte la hora actual es mayor que la tasa de disparo que hemos establecido
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; // aquí se suma la tasa de disparo para que no vuelva a entrar hasta pasado ese tiempo
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            audioSource.Play();
        }
    }

    private void FixedUpdate ()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        //Movimiento aplicado en funcion de las teclas pulsadas.
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rig.velocity = movement * speed;
        //Con clamp limitamos a que solo pueda ir hasta la posicion y marcar unos limites en la pantalla
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        //Para rotarlo en el eje z y crear un efecto 3D muy chulo la nave girando cuando vamos hacia los lados
        rig.rotation = Quaternion.Euler(0f, 0f, rig.velocity.x * -tilt);
    }
}