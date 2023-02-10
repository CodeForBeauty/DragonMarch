using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainGameLogic : MonoBehaviour
{
    /*
     * Main game logic. Contains basic functions and current stats
     */
    public GameObject explosion;
    public LevelScroll levelRoot;
    public MoveToPos head;
    public UIDocument hud;
    public UIDocument win;
    public UIDocument lose;
    public UIDocument pause;

    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource coinCollectSound;
    public AudioSource manDeathSound;
    public AudioSource explosionSound;

    public int MaxHumans = 10;
    public int coinsCounter = 0;
    public int stars = 0;
    private int startHumans;

    public bool isInfinite = false;
    public GameObject[] lastParts;
    public GameObject smallTail;
    public GameObject smallNotTail;

    private void Start()
    {
        smallTail.SetActive(false);
        // hide unnesasry UI
        win.rootVisualElement.visible = false;
        lose.rootVisualElement.visible = false;
        pause.rootVisualElement.visible = false;
        startHumans = MaxHumans;
    }

    public void Reload()
    {
        // reload current level
        SetTimeScale(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        winSound.Play();
        // calculate stars
        stars = (coinsCounter - ((startHumans - MaxHumans) * levelRoot.humansMultiplyer)) / levelRoot.starDivider;
        if (stars == 0) {
            stars = 1;
        }
        // if stars more than was before rewrite
        if (PlayerPrefs.HasKey(GameSaveLoad.currentLevel.ToString()) && GameSaveLoad.loadLevelStats(GameSaveLoad.currentLevel) < stars)
        {
            GameSaveLoad.saveLevelStats(GameSaveLoad.currentLevel, stars, "true");
        } 
        else if (!PlayerPrefs.HasKey(GameSaveLoad.currentLevel.ToString()))
        {
            GameSaveLoad.saveLevelStats(GameSaveLoad.currentLevel, stars, "true");
        }
        // pause game and show win screen
        SetTimeScale(0);
        win.rootVisualElement.visible = true;
        hud.rootVisualElement.visible = false;
        GameSaveLoad.setCoins(GameSaveLoad.getCoins() + coinsCounter);
    }

    public void Lose()
    {
        loseSound.Play();
        // calculate stars
        stars = ((coinsCounter - ((startHumans - MaxHumans) * levelRoot.humansMultiplyer)) / levelRoot.starDivider)-1;
        // same
        if (PlayerPrefs.HasKey(GameSaveLoad.currentLevel.ToString()) && GameSaveLoad.loadLevelStats(GameSaveLoad.currentLevel) < stars)
        {
            GameSaveLoad.saveLevelStats(GameSaveLoad.currentLevel, stars, "false");
        }
        else if (!PlayerPrefs.HasKey(GameSaveLoad.currentLevel.ToString()))
        {
            GameSaveLoad.saveLevelStats(GameSaveLoad.currentLevel, stars, "false");
        }
        // pause game and show lose screen
        levelRoot.speed = 0;
        head.isControlable = false;
        lose.rootVisualElement.visible = true;
        hud.rootVisualElement.visible = false;
        GameSaveLoad.setCoins(GameSaveLoad.getCoins() + coinsCounter);
    }

    public void Next()
    {
        // if this is last level get home
        if (GameSaveLoad.currentLevel == GameSaveLoad.lastLevel)
        {
            Home();
        }
        // else open next level
        else
        {
            GameSaveLoad.currentLevel += 1;
            Reload();
        }
    }

    public void Home()
    {
        // go to main menu
        SetTimeScale(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        // pause game and show pause menu
        SetTimeScale(0);
        hud.rootVisualElement.Q<Button>("Pause").SetEnabled(false);
        pause.rootVisualElement.visible = true;
    }

    public void SetTimeScale(int scale)
    {
        // set game time
        Time.timeScale = scale;
    }

    public void Continue()
    {
        // resume game from pause
        SetTimeScale(1);
        hud.rootVisualElement.Q<Button>("Pause").SetEnabled(true);
        pause.rootVisualElement.visible = false;
    }

    public void Explosion(Vector3 pos)
    {
        explosionSound.Play();
        // spawn explosion particle in position
        Instantiate(explosion, pos, Quaternion.identity);
    }

    public void ReduceSize()
    {
        // reduce size of dragon
        foreach (GameObject part in lastParts)
        {
            Destroy(part);
        }
        Destroy(smallNotTail);
        smallTail.SetActive(true);
    }
}
