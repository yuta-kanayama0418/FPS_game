using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject startMenu;
    private GameObject poseMenu;
    private GameObject gameEnd;
    private Text timeDisplay;
    private float surviveTime;
    private float targetTime = 60.0f;

    void Awake()
    {
        Time.timeScale = 0;
        Player.onGame = false;
        playerObject = GameObject.Find("Player");
        startMenu = GameObject.Find("StartMenu");
        poseMenu = GameObject.Find("PoseMenu");
        poseMenu.SetActive(false);
        gameEnd = GameObject.Find("GameEnd");
        gameEnd.SetActive(false);
        timeDisplay = GameObject.Find("SurviveTime").GetComponent<Text>();
    }

    public void StartMenu()
    {
        startMenu.SetActive(false);
        Player.onGame = true;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player died
        if (Player.playerHP <= 0)
        {
            gameEnd.transform.GetChild(0).GetComponent<Text>().text = "Game Over";
            Time.timeScale = 0;
            Player.onGame = false;
            gameEnd.SetActive(true);
            Cursor.visible = true;
            StartCoroutine(Finish(new Color(1.0f, 0.0f, 0.0f, 0.0f)));
        }
        //player survived
        else if (surviveTime > targetTime)
        {
            gameEnd.transform.GetChild(0).GetComponent<Text>().text = "Clear";
            Time.timeScale = 0;
            Player.onGame = false;
            gameEnd.SetActive(true);
            Cursor.visible = true;
            StartCoroutine(Finish(new Color(0.0f, 1.0f, 0.0f, 0.0f)));
        }

        surviveTime += Time.deltaTime;
        timeDisplay.text = ShowTime((int) surviveTime);
    }

    void Update()
    {
        //pose menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (poseMenu.activeSelf)
            {
                Player.onGame = true;
                poseMenu.SetActive(false);
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            else
            {
                Player.onGame = false;
                poseMenu.SetActive(true);
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
    }

    string ShowTime(int time)
    {
        int min = time / 60;
        string min_str;
        if (min < 10) min_str = "0" + min;
        else min_str = min.ToString();
        int sec = time % 60;
        string sec_str;
        if (sec < 10) sec_str = "0" + sec;
        else sec_str = sec.ToString();
        return min_str + ": " + sec_str;
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
                  UnityEngine.Application.Quit();
        # endif
    }

    IEnumerator Finish(Color color)
    {
        Text text = gameEnd.transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < 1000; i++)
        {
            text.color = color;
            color.a += (float)i / 1000;
            yield return null;
        }
    }
}
