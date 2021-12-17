using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Control : MonoBehaviour
{
    public GameObject yellowEnemy;
    public GameObject redEnemy;
    public GameObject greenEnemy;
    public GameObject blueEnemy;
    PopUpSystem pop;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1_Tutorial")
        {
            pop = FindObjectOfType<PopUpSystem>();
            pop.gameObject.SetActive(true);

            ControlsInfo();
        }
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void ControlsInfo()
    {
        pop.PopUp("WASD - Move Up, Left, Down and Right\n\nMouse - Aim\n\nMouse Left Button - Fire Pin");
    }

    void EnemyDescription()
    {
        pop.PopUp("In this game, there are 4 different enemies with different tint (green, red, blue, yellow). They have different behaviours and are always in patrol mode until when you get close to it. Push the enemy away if you are being cornered or unable to run away from enemy.");
    }

    void CollisionDescription()
    {
        pop.PopUp("Some objects like boxes are movable which can be moved out of the way to clear the path or used as a shield!");
    }

    void Plot()
    {
        pop.PopUp("You are a wizard who is physically weaker than the others but with magic you can turn the tables. You can a turn into a Voodoo and then turn your opponent's strength against themself. In other words, you can lose (take damage deliberately in voodoo mode) in order to win (kill your opponents)!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            if (collision.gameObject.name == "TriggerStory")
            {
                Plot();
            }
            else if (collision.gameObject.name == "TriggerEnemyDescription")
            {
                EnemyDescription();
            }
            else if (collision.gameObject.name == "TriggerCollisionDescription")
            {
                CollisionDescription();
            }
            else if (collision.gameObject.name == "TriggerGreenEnemy")
            {
                pop.PopUp("Green Enemies can only perform Melee Damage to you and will chase you when they detect you!");
                greenEnemy.SetActive(true);
            }
            else if (collision.gameObject.name == "TriggerRedEnemy")
            {
                pop.PopUp("Red Enemies comes near you upon detection and will start shooting once it gets in range of you. However if you come too close to it, it will start chasing you continuously and melee you! So keep your distance!");
                redEnemy.SetActive(true);
            }
            else if (collision.gameObject.name == "TriggerBlueEnemy")
            {
                pop.PopUp("Blue Enemies comes near you also and will start shooting you once it gets in range of you and will never ");
                blueEnemy.SetActive(true);
            }
            else if (collision.gameObject.name == "TriggerYellowEnemy")
            {
                pop.PopUp("Yellow Enemies will keep chasing you and shoot you when they detect you and only stop chasing once you out run them!");
                yellowEnemy.SetActive(true);
            }
        }
    }
}
