using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character attributes:")]
    public float baseMovementSpeed = 1.0f;
    public float crosshairDistance = 1.0f;
    public float bulletInterval = 2.0f;
    public float bulletForce = 20f;


    [Space]
    [Header("Debug Stats:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject crosshair;
    public Transform firepoint;
    public GameObject playerBullet;
    public Camera cam;
    public WeaponbarBehaviour playerWeaponStatus;

    private float bulletCooldownTimer;
    float weaponReady;
    private float weaponReadyMax;
    Vector3 mousePos;
    Vector2 requiredVector;

    // Start is called before the first frame update
    void Start()
    {
        weaponReadyMax = bulletInterval;
        weaponReady = weaponReadyMax;
        playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Aim();
        Move();
        Animate();
        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
            weaponReady += Time.deltaTime;
            playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.gameHasEnded)
            {
                Shoot();
            }
        }
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * baseMovementSpeed;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", requiredVector.x);
        animator.SetFloat("Vertical", requiredVector.y);
        animator.SetFloat("Speed", movementSpeed);
    }

    void Aim()
    {
        var tempPos = Input.mousePosition;
        tempPos.z = 10;
        mousePos = cam.ScreenToWorldPoint(tempPos);
        requiredVector = (Vector2)mousePos - (Vector2)firepoint.position;

        // crosshair.transform.localPosition = crosshair.transform.position * crosshairDistance;
        LookAtTarget(mousePos, crosshair.transform);

    }

    void LookAtTarget(Vector3 target, Transform source)
    {
        Vector3 diff = target - source.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90f);
    }

    void Shoot()
    {

        if (bulletCooldownTimer > 0) return;

        weaponReady = 0;
        playerWeaponStatus.SetWeaponStatus(weaponReady, weaponReadyMax);
        bulletCooldownTimer = bulletInterval;

        float angle = Mathf.Atan2(requiredVector.y, requiredVector.x) * Mathf.Rad2Deg;
        GameManager.pinStuckColor = "null";
        Destroy(GameObject.Find("PlayerBullet(Clone)"));
        FindObjectOfType<AudioManager>().Play("PinFire");
        GameObject bullet = Instantiate(playerBullet, firepoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Rigidbody2D rigidBod = bullet.GetComponent<Rigidbody2D>();
        rigidBod.AddForce((Vector2)requiredVector * bulletForce, ForceMode2D.Impulse);
    }
}
