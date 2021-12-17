using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEveryWhere_Spawn : MonoBehaviour
{
    public GameObject greenEnemy1;
    public GameObject greenEnemy2;
    public GameObject greenEnemy3;
    public GameObject greenEnemy4;

    public GameObject greenEnemy5;
    public GameObject greenEnemy6;
    public GameObject blueEnemy1;
    public GameObject blueEnemy2;
    public GameObject blueEnemy3;
    public GameObject blueEnemy4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            if (collision.gameObject.name == "Trap1")
            {
                greenEnemy1.SetActive(true);
                greenEnemy2.SetActive(true);
                greenEnemy3.SetActive(true);
                greenEnemy4.SetActive(true);
            }
            else if (collision.gameObject.name == "Trap2")
            {
                greenEnemy5.SetActive(true);
                greenEnemy6.SetActive(true);
                blueEnemy1.SetActive(true);
                blueEnemy2.SetActive(true);
                blueEnemy3.SetActive(true);
                blueEnemy4.SetActive(true);
            }
        }
    }
}
