using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    /*
     * Allow player character interact with other objects in game
     * work with different parts of body
     */
    public MainGameLogic playerRoot;

    private void OnCollisionEnter(Collision collision)
    {
        // if colliding with coin
        if (collision.gameObject.CompareTag("coin"))
        {
            playerRoot.coinCollectSound.Play();
            // add 1 to coin counter and destroy gameObject
            playerRoot.coinsCounter++;
            Destroy(collision.gameObject);
        }
        // if colliding with human
        else if (collision.gameObject.CompareTag("human"))
        {
            playerRoot.manDeathSound.Play();
            collision.gameObject.GetComponent<ManDeath>().Death();
            if (!playerRoot.isInfinite)
            {
                // subtract 1 from human counter and destroy gameObject
                playerRoot.MaxHumans--;
                if (playerRoot.MaxHumans == 0)
                {
                    playerRoot.Lose();
                }
            }
        }
        // if colliding with trap
        else if (collision.gameObject.CompareTag("trap"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            // add explorion particle and show lose screen
            playerRoot.Explosion(collision.transform.position);
            playerRoot.Lose();
        }
        // if player ended level show win screen
        else if (collision.gameObject.CompareTag("levelEnd"))
        {
            playerRoot.Win();
        }
        // if player got off screen show lose screen
        else if (collision.gameObject.CompareTag("levelBorder"))
        {
            playerRoot.Lose();
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        // all the same
        if (other.gameObject.CompareTag("coin"))
        {
            playerRoot.coinsCounter++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("human"))
        {
            other.gameObject.GetComponent<ManDeath>().Death();
            if (playerRoot.isInfinite)
            {
                playerRoot.MaxHumans--;
                if (playerRoot.MaxHumans == 0)
                {
                    playerRoot.Lose();
                }
            }
        }
        else if (other.gameObject.CompareTag("trap"))
        {
            playerRoot.Explosion(other.transform.position);
            playerRoot.Lose();
        }
        else if (other.gameObject.CompareTag("levelEnd"))
        {
            playerRoot.Win();
        }
        else if (other.gameObject.CompareTag("levelBorder"))
        {
            playerRoot.Lose();
        }
    }
    */
}
