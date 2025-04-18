using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public GameObject unpauseButton;
    public GameObject pauseButton;
    public GameObject restartButton;
    public Text normalLevelText;
    public Text timeText;
    public Text levelText;
    public static bool normalMode;
    public Text finalTime;
    // Start is called before the first frame update
    void Start()
    {
        if(pauseButton){
            pauseButton.transform.position = new Vector3(-9f,4f,0f);
        }
        if(restartButton){
            restartButton.transform.position = new Vector3(-9f,2.8f,0f);
        }
        if(levelText){
            levelText.text = "Level " + GameManager.level.ToString();
        }
        if(normalLevelText){
            normalLevelText.text = "Level " + GameManager.level.ToString();
        }
        if(normalMode){
            Destroy(timeText);
            Destroy(levelText);
            Destroy(finalTime);
        }
        if(!normalMode){
            Destroy(normalLevelText);
        }
        if(finalTime){
            finalTime.text = "Your Time: " + GameManager.timeStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.pause){
            pause();
        }
        if(timeText){
            timeText.text = GameManager.timeStr;
        }
        if(GameManager.truePause){
            unpauseButton.transform.position = new Vector3(0f,0f,0f);
            pauseButton.transform.position = new Vector3(10000f,100000f,100000f);

        }

    }
    public void pause(){
        GameManager.pause = true;
        pauseButton.transform.position = new Vector3(10000f,100000f,100000f);
        if(character.onGround || GameManager.newLevel){
            GameManager.pause = false;
            GameManager.truePause = true;
            unpauseButton.transform.position = new Vector3(0f,0f,0f);
            
        }
        
    }
    public void unpause(){
        GameManager.truePause = false;
        unpauseButton.transform.position = new Vector3(10000f,100000f,100000f);
        pauseButton.transform.position = new Vector3(-9f,4f,0f);
    }
    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void playNormal(){
        normalMode = true;
        SceneManager.LoadScene("Level1");
    }
    public void playSpeedrun(){
        normalMode = false;
        SceneManager.LoadScene("Level1");
    }
    public void playAgain(){
        SceneManager.LoadScene("Menu");
        GameManager.level = 1;
        GameManager.hours = 0;
        GameManager.minutes = 0;
        GameManager.seconds = 0f;
    }
}
