using System;
using UnityEngine;

[Serializable]
public class BoundaryAsteroids
{
    public float xMin, xMax;
}

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;
    public bool esAsteroideGrande;
    public GameObject otherAsteroid;
    public BoundaryAsteroids boundary;

    private GameController gameController;
    private GameObject element;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        UpdateBoundary();
    }

    private void UpdateBoundary()
    {
        Vector2 half = Camera.main.GetHalfDimensionsInWorldUnits();
        boundary.xMin = -half.x + 0.7f;
        boundary.xMax = half.x - 0.7f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;
        if (other.CompareTag("AsteroidBig")) return;

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        /******** FUNCIONALIDAD AÑADIDA POR ISMAEL: ******/
        if (esAsteroideGrande) //si es un asteroide grande (se marca en el prefab el boleano)
        {
            //Vamos a poner las posiciones en las que se pondrán los dos mini asteroides:

            //********Random.onUnitSphere genera un punto aleatorio de una esfera con radio 1**********//

            //1º. Sumamos su posicion al de la esfera para desplazarlo un poco desde donde ha sido destruido
            //2º. Le metemos los limites para que no se salga de la pantalla en la "x"(configurada como 600x900), la "y" la dejamos a 0 y la "z" como estaba
            var posicionAsteroideTrozo1 = gameObject.transform.position + UnityEngine.Random.onUnitSphere * 2;
            posicionAsteroideTrozo1 = new Vector3(Mathf.Clamp(posicionAsteroideTrozo1.x, boundary.xMin, boundary.xMax), 0f, posicionAsteroideTrozo1.z);

            var posicionAsteroideTrozo2 = gameObject.transform.position + UnityEngine.Random.onUnitSphere * 2;
            posicionAsteroideTrozo2 = new Vector3(Mathf.Clamp(posicionAsteroideTrozo2.x, boundary.xMin, boundary.xMax), 0f, posicionAsteroideTrozo2.z);

            //Instanciamos los 2 asteroides
            Instantiate(otherAsteroid, posicionAsteroideTrozo1, Quaternion.identity);
            Instantiate(otherAsteroid, posicionAsteroideTrozo2, Quaternion.identity);
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
        
    }
}
