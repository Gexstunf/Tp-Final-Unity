using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject coinPrefab;

    [SerializeField] private float _spawnChance = 0.6f;

    public int totalCoins = 10;
    public int collectedCoins;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        // Encuentra todos los spawn points
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("CoinSpawn");

        foreach (GameObject spawn in spawnPoints)
        {
            if (Random.value <= _spawnChance)
            {
                Instantiate(coinPrefab, spawn.transform.position, Quaternion.identity);
                totalCoins++;
            }
        }

        Debug.Log("Monedas generadas: " + totalCoins);
    }

    public void AddCoin()
    {
        collectedCoins++;
        FindObjectOfType<CoinUI>().UpdateCounter();

        if (collectedCoins >= totalCoins)
            GameCompleted();
    }

    void GameCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("¡Juego completado! Todas las monedas generadas fueron recogidas.");
    }
}
