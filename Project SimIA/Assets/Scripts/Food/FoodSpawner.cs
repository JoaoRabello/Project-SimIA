using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;

    [SerializeField] private GameObject food;
    [SerializeField] private float timeToSpawnFood;
    private bool canSpawn = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        canSpawn = false;
        yield return new WaitForSeconds(timeToSpawnFood);
        SpawnFood();
    }

    private void SpawnFood()
    {
        float x = Random.Range(-width, width);
        float z = Random.Range(-height, height);

        Vector3 position = new Vector3(x, 0.23f, z);
        Quaternion rotation = Quaternion.Euler(0, Random.Range(-180, 180), 90);
        
        Instantiate(food, position, rotation);

        canSpawn = true;
    }
}
