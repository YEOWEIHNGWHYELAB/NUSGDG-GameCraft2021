using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private float bulletInterval = 5;
    Vector2 requiredVector;
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
        var tempPos = Input.mousePosition;
        tempPos.z = 10;
        mousePos = cam.ScreenToWorldPoint(tempPos);

        requiredVector = (Vector2)mousePos - (Vector2)firepoint.position;

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
        float angle = Mathf.Atan2(requiredVector.y, requiredVector.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(playerBullet, firepoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Rigidbody2D rigidBod = bullet.GetComponent<Rigidbody2D>();
        rigidBod.AddForce((Vector2)requiredVector * bulletForce, ForceMode2D.Impulse);

        bulletCooldownTimer -= Time.deltaTime;

        if (bulletCooldownTimer > 0) return;

        bulletCooldownTimer = bulletInterval;
    }
}
