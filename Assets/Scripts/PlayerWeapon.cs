using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private float bulletInterval = 5;

    private float bulletCooldownTimer;
    public float bulletForce = 20f;
    Vector3 mousePos;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(mousePos);

        if (Input.GetKeyDown("space"))
        {
            if (!GameManager.gameHasEnded)
            {
                shoot();
            }
        }
    }

    void shoot()
    {
        GameObject bullet = Instantiate(playerBullet, firepoint.position, firepoint.rotation);
        Rigidbody2D rigidBod = bullet.GetComponent<Rigidbody2D>();
        rigidBod.AddForce((Vector2)mousePos * bulletForce, ForceMode2D.Impulse);

        bulletCooldownTimer -= Time.deltaTime;

        if (bulletCooldownTimer > 0) return;

        bulletCooldownTimer = bulletInterval;
    }
}
