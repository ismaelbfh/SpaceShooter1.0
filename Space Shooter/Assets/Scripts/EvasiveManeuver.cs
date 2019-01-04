using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public Vector2 startWait; //x -> tiempo min y la y el tiempo max
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    public float tilt;

    private float targetManeuver;
    private Rigidbody rbd;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        UpdateBoundary();
        StartCoroutine(Evade());
	}

    private void UpdateBoundary()
    {
        Vector2 half = Camera.main.GetHalfDimensionsInWorldUnits();
        //Utils.GetHalfDimensionsInWorldUnits();
        // - 6, 6, -4, 8
        boundary.xMin = -half.x + 0.9f;
        boundary.xMax = half.x - 0.9f;
        boundary.zMin = 0f;
        boundary.zMax = 0f;
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y)); //pasados los segundos aleatorios establecera una velocidad

        while (true)
        {
            //Para que redondee a la posicion y establezca hacia que lado queremos redirigir la nave para que haga maniobra evasiva
            //targetMenewver lo que hará es definir un punto a donde se dirigirá la nave
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x); //si transform.position.x < 0 ? 1 : -1

            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0f;
            
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
    
	private void FixedUpdate ()
    {
        //le metemos la velocidad que se vaya acelerando por cada frame
        //movemos desde el valor primero hasta el targetManeuver sin exceder del tercer parametro
        float newManeuver = Mathf.MoveTowards(rbd.velocity.x, targetManeuver, Time.deltaTime * smoothing); //partimos de un valor, queremos que se acerce al segundo, con un incremento
        
        rbd.velocity = new Vector3(newManeuver, 0.0f, rbd.velocity.z);
        rbd.position = new Vector3(Mathf.Clamp(rbd.position.x, boundary.xMin, boundary.xMax), 0f, rbd.position.z);
        rbd.rotation = Quaternion.Euler(0f, 0f, rbd.velocity.x * -tilt);
    }
}