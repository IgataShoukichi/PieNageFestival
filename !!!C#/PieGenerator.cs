using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieGenerator : MonoBehaviour
{
    public GameObject PiePrefab;
    public float minTime;
    public float maxTime;
    public float xMinPosition;
    public float xMaxPosition;
    public float yMinPosition;
    public float yMaxPosition;
    public float zMinPosition;
    public float zMaxPosition;
    public static int count;
    private float interval;
    private float time;
    void Start()
    {
        count = 0;
        interval = GetRandomTime();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > interval&&count<40)
        {
            GameObject Pie = Instantiate(PiePrefab);
            Pie.transform.position = GetRandomPosition();
            time = 0f;
            interval = GetRandomTime();
            count += 1;
        }
    }
    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        return new Vector3(x, y, z);
    }

}
