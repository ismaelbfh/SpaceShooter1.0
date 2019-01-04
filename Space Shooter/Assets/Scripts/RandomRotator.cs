using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        //Asigna una velocidad de rotacion con un vector 3 ue asigna un numero aleatorio entre -1 y 1 y lo normaliza para que no siempre tenga la misma velocidad de giro
        //Vector3 angularVelocityNormalized = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        //Si queremos ahorrarnos lineas y hacerlo de otra manera insideUnitSphere lo que hace es obtiene un punto aleatorio dentro de una esfera de radio 1, sin normalizar, no tendra siempre la misma velocidad de giro asi queda mas real
        rig.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
