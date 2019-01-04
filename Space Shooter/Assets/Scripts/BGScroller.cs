using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;

    private Vector3 startPosition;

    private float tileSize;
    
	private void Start ()
    {
        startPosition = transform.position;
        tileSize = transform.localScale.y;	
	}
	
	private void Update ()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize); // 0, 34 --> dará 0, 1, 34 dara 1... hasta que valga 34, 34 devolvberá 0
        transform.position = startPosition + new Vector3(0, 0, newPosition); //se incrementara en 1, 2, 3.... la para que vaya haciendo el efecto de que sube
	}
}
