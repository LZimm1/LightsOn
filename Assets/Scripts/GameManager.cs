using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static int level = 1;
    public static bool newLevel;
    private static GameManager instance;
    public static int hours;
    public static int minutes;
    public static float seconds;
    public static string timeStr;
    public static bool pause;
    public static bool truePause;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(newLevel){
            newLevel = false;
            StartCoroutine(newLevelLoad());
        }
        if(hours != 1 && truePause == false){
            seconds += Time.deltaTime;
        }
        updateTime();
        if(Input.GetButtonDown("Restart")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator newLevelLoad(){
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level" + level.ToString());
        if(level == 17){
            SceneManager.LoadScene("GameOver");
        }
    }
    void updateTime(){
        if(seconds > 60 && minutes < 60 && hours != 1){
            minutes++;
            seconds = 0;
        }
        else if(minutes == 60){
            hours++;
            minutes = 0;
        }
        if (seconds < 10 && minutes < 10){
            timeStr = "0" + minutes.ToString() + ":0"+Math.Round(seconds,2).ToString();
        }
        else if (seconds < 10){
            timeStr = minutes.ToString() + ":0" + Math.Round(seconds,2).ToString();
        }
        else if (minutes < 10){
            timeStr = "0"+ minutes.ToString() + ":" + Math.Round(seconds,2).ToString();
        }
        else{
            timeStr = minutes.ToString() + ":" + Math.Round(seconds,2).ToString();
        }
        if(hours == 1){
            timeStr = "01:00:00";
        }
        else if(minutes == 0 && seconds >=10){
            timeStr = Math.Round(seconds,2).ToString();
        }
        else if(minutes == 0 && seconds < 10){
            timeStr = "0"+ Math.Round(seconds,2).ToString();
        }
    }
}
