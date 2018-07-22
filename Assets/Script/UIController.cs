using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text Level;
    public Text HP;
    public Text Timer;
    public Text Log;
    public Button restart;
    public GameObject start_button;

    private const float distance = 2.0f;

	// Use this for initialization
	void Start () {
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set_distance_between_player_and_Canvas(Vector3 player)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.z + distance);
    }

    public void Win_the_game()
    {
        Log.gameObject.SetActive(true);
        Log.text = "Congratulations";
    }

    public void Lose_the_game()
    {
        Log.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        Log.text = "lose";
    }

    public void Close_restart_button()
    {
        restart.gameObject.SetActive(false);
    }

    public void Close_log()
    {
        Log.gameObject.SetActive(false);
    }

    public void Close_start_button()
    {
        start_button.SetActive(false);
    }

    public void Open_start_button()
    {
        start_button.SetActive(true);
    }

    public void Update_Timer(string time)
    {
        Timer.text = "Time : " + time;
    }

    public void Update_HP(string hp)
    {
        HP.text = "HP : " + hp;
    }

    public void Update_Level(string level)
    {
        Level.text = "Level : " + level;
    }

    private void Initialize()
    {
        Initialize_text();
    }

    private void Initialize_text()
    {
        HP.text = "HP : 100";
        Level.text = "Level : 1";
        Timer.text = "Time : 60";
    }
}
