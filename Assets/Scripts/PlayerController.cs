using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;
    public float speed = 1;
    public GameObject bulletPrefab;

    private float shotCooldown = 0;
    private float shieldPower = 1;
    private bool shieldActive = false;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal" + playerNumber);
        float yAxis = Input.GetAxis("Vertical" + playerNumber);
        float xAxisR = Input.GetAxis("HorizontalR" + playerNumber);
        float yAxisR = Input.GetAxis("VerticalR" + playerNumber);

        Vector2 direction = (new Vector2(xAxis, yAxis)).normalized;
        Vector2 directionR = (new Vector2(xAxisR, yAxisR));

        transform.position += (Vector3)direction * speed * Time.deltaTime;
        if (directionR.magnitude >= 0.1f)
            transform.localRotation = Quaternion.FromToRotation(Vector2.up, directionR);

        if (Input.GetAxis("TriggerR" + playerNumber) > 0 && shotCooldown == 0 && !shieldActive)
        {
            shotCooldown = 0.2f;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletController bCon = bullet.GetComponent<BulletController>();
            bCon.direction = transform.localRotation * Vector2.up;
            bCon.playerNumber = playerNumber;
            SpriteRenderer sRen = bullet.GetComponent<SpriteRenderer>();
            sRen.color = GetComponent<SpriteRenderer>().color;
        }

        if (Input.GetAxis("TriggerL" + playerNumber) > 0 && shieldPower > 0)
        {
            shieldActive = true;
            transform.GetChild(0).gameObject.SetActive(true);
            shieldPower -= Time.deltaTime;
            if (shieldPower < 0)
                shieldPower = 0;
        }
        else
        {
            shieldActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Input.GetAxis("TriggerL" + playerNumber) <= 0)
        {
            shieldPower += Time.deltaTime / 4.0f;
            if (shieldPower > 1)
                shieldPower = 1;
        }

        shotCooldown -= Time.deltaTime;
        if (shotCooldown < 0)
            shotCooldown = 0;

        transform.localScale = new Vector3(0.2f, 0.2f, 0) * (health / 100.0f);

        if (health <= 0)
            Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pill"))
        {
            health += 10;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletController bCon = collision.gameObject.GetComponent<BulletController>();

            if (bCon.playerNumber != playerNumber && !shieldActive)
                health -= 10;

            if (bCon.playerNumber != playerNumber)
                Destroy(collision.gameObject);
        }
    }
}
