using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject coinPrefab;

    [SerializeField] private float spawnChance = 0.6f;
    [SerializeField] private int totalCoins = 10;
    [SerializeField] private int collectedCoins;

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
            if (Random.value <= spawnChance)
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

        if (collectedCoins >= totalCoins)
        {
            GameCompleted();
        }
    }

    void GameCompleted()
    {
        Debug.Log("¡Juego completado! Todas las monedas generadas fueron recogidas.");
    }
}
