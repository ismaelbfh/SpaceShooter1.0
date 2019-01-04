using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    private Rigidbody rig;
    
	private void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}
	
	private void Update ()
    {
        //asigna a la velocidad apuntando a una velocidad de una unidad por segundo aplicada sobre el eje z
        // con el eje "y" sería up y eje x sería right
        rig.velocity = new Vector3(0, 0, 1f) * speed;
	}
}
