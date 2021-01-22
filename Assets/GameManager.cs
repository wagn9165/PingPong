using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static int currScore1;
    public static int currScore2;

    public static bool helpBool;

    GameObject[] pauseObjects;
    GameObject[] helpObjects;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Debug.Log(topRight);

        Instantiate(ball);
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;

        paddle1.Init(true); // Left paddle
        paddle2.Init(false); // Right paddle

        currScore1 = 0;
        currScore2 = 0;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        helpObjects = GameObject.FindGameObjectsWithTag("Help");
        hideHelp();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseControl();
        }
        if(Input.GetKey("escape"))
        {
            Debug.Log("escaped");
            Application.Quit();
        }
    }

    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        if (pauseObjects == null)
            Debug.Log("pauseObjects is null");
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void toggleHelp()
    {
        if(helpBool)
        {
            hideHelp();
        }
        else
        {
            showHelp();
        }
    }

    public void showHelp()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(true);
        }
        helpBool = true;
    }

    public void hideHelp()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(false);
        }
        helpBool = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public static void Score(bool right)
    {
        if(right)
        {
            currScore1++;
        }
        if (!right)
        {
            currScore2++;
        }
    }

    private GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
    {
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.white;

        GUI.Label(new Rect((Screen.width / 2 - 162), 20, 100, 100), "" + currScore1, guiStyle);
        GUI.Label(new Rect((Screen.width / 2 + 162), 20, 100, 100), "" + currScore2, guiStyle);
    }
}
