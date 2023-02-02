using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillController : MonoBehaviour
{
    public float speed = 1, rotationSpeed = 1;

    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        direction = (new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f))).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        transform.localRotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.back);


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            Destroy(transform.gameObject);
    }
}
