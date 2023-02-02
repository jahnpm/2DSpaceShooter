using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pillPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0.0f, 1.0f) <= 0.01f)
        {
            Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width),
                Random.Range(0, Screen.height), 0));
            randomPos.z = 0;

            Instantiate(pillPrefab, randomPos, Quaternion.identity);
        }
    }
}
