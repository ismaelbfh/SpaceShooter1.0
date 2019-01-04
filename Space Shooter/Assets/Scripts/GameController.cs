using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public GameObject[] hazardArray;
    private GameObject hazardSelection;

    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    public Text scoreText;

    public GameObject restartGameObject;
    public GameObject gameOverGameObject;
    private bool restart;
    private bool gameOver;

	private void Start ()
    {
        UpdateSpawnValues();
        restart = false;
        gameOver = false;
        gameOverGameObject.SetActive(false);
        restartGameObject.SetActive(false);
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void UpdateSpawnValues()
    {
        Vector2 half = Camera.main.GetHalfDimensionsInWorldUnits();
        spawnValues = new Vector3(half.x - 0.7f, 0f, half.y + 6f);
        Debug.Log(half);
    }

    private void Update()
    {
        if(restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++) //genera una oleada de asteroides de x asteroides que lanzara
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                hazardSelection = hazardArray[Random.Range(0, hazardArray.Length)];
                Instantiate(hazardSelection, spawnPosition, Quaternion.identity);
                //esto se usa cuando lanzamos una corrutina  para que espere determinado tiempo hasta que ejecute la siguiente vuelta
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait); //esperara unos segundos mas antes de lanzar la siguiente oleada

            if (gameOver)
            {
                restartGameObject.SetActive(true);
                restart = true;
                break;
            }
        }
	}

    //Para añadir puntaje cuando destruimos un asteroide y llamarlo desde el script DestroyByContact
    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverGameObject.SetActive(true);
        gameOver = true;
    }
}
