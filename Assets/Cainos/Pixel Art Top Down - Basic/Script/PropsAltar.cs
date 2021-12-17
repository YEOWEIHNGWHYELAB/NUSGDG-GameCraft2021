using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;

        public GameManager gm;
        PopUpSystem pop;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
            {
                if (GameManager.enemyCount <= 0)
                { 
                    gm.EndGame(true);
                } else
                {
                    pop.PopUp("Kill all enemies to proceed");
                }
            }
        }

        private void Start()
        {
            gm = FindObjectOfType<GameManager>();
            pop = FindObjectOfType<PopUpSystem>();
            pop.gameObject.SetActive(true);
        }

        private void Update()
        {
            targetColor = new Color(1, 1, 1, 1);
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
            targetColor = new Color(1, 1, 1, 0);
        }
    }
}
