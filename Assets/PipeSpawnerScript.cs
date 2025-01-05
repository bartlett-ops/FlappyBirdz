using System;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRateMin = 2.5F;
    public float spawnRateMax = 5F;
    private float spawnRate = 2F;
    private float pipeOffset = 0F;

    private float timer = 0F;
    public float heightOffset = 10F;
    public LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.gameRunning)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            } else 
            {
                timer = 0;
                spawnRate = UnityEngine.Random.Range(spawnRateMin, spawnRateMax);

                spawnPipe();
            }
        }
    }

    void spawnPipe() 
    {
        float lowestPoint;
        float highestPoint;
        float absLowest = transform.position.y - heightOffset;
        float absHighest = transform.position.y + heightOffset;
        if(spawnRate > 3)
        {
            lowestPoint = absLowest;
            highestPoint = absHighest;
        } else {
            lowestPoint = MathF.Max(absLowest, pipeOffset - (heightOffset * 0.75F));
            highestPoint = MathF.Min(absHighest, pipeOffset + (heightOffset * 0.75F));
        }
        pipeOffset = UnityEngine.Random.Range(lowestPoint, highestPoint);

        Instantiate(pipe, new Vector3(transform.position.x, pipeOffset, 0), transform.rotation);
    }

}
