using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour {

    public PlayerController player_controller;
    public UIController ui_controller;
    public PillarController pillar_controller;
    public EnemyController enemy_controller;

    private const int LEVEL = 0;
    private const int PILLAR = 1;
    private const int ENEMY_QUANTITY = 2;
    private const int ENEMY_SPEED = 3;

    private ReadFile reader;
    private bool is_start = false;
    private List<List<int>> level_info;
    private float time = 60f;
    private int HP = 100;
    private int now_level = 0;

    void Awake()
    {
        reader = new ReadFile();
        level_info = reader.Get_file_info();
    }

    // Use this for initialization
    void Start() {
        Init();
        Camera_position_init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            Start_the_game();
        if (is_start)
            timer_count_down();
        if (time <= 0)
            Next_level();
    }

    public void Restart_the_game()
    {
        HP = 100;
        now_level = 0;
        Init();
        Start_the_game();
    }

    public void Start_the_game()
    {
        is_start = true;
        ui_controller.Close_start_button();
        Reset_pillar_position();
        Reset_enemy();
        Set_player_init_position();
        enemy_controller.Start_create_enemys();
        Camera_position_init();
    }

    public void Reduce_HP()
    {
        HP -= 10;
        ui_controller.Update_HP(HP.ToString());
        if (HP <= 0)
            Lose();
    }

    private void Camera_position_init()
    {
        SteamVR.instance.hmd.ResetSeatedZeroPose();
    }

    private void Init()
    {
        Reset_parameter();
        Reset_pillar_position();
        Set_player_init_position();
        Reset_enemy();
        Reset_UI();
    }

    private void timer_count_down()
    {
        time -= Time.deltaTime;
        ui_controller.Update_Timer(time.ToString());
    }

    public void Lose()
    {
        is_start = false;
        enemy_controller.Stop_create_enemy();
        enemy_controller.Recycle_all_enemys();
        Set_player_init_position();
        ui_controller.Lose_the_game();
    }

    public void Set_canvas_position(Vector3 player)
    {
        ui_controller.Set_distance_between_player_and_Canvas(player);
    }

    private void Next_level()
    {
        if ((now_level + 1) == level_info.Count)
        {
            ui_controller.Win_the_game();
            return;
        }
        now_level++;
        Init();
    }

    private void Reset_parameter()
    {
        time = 60f;
        is_start = false;
    }

    private void Reset_pillar_position()
    {
        pillar_controller.Recycle_all_pillars();
        pillar_controller.Create_area(level_info[now_level][PILLAR]);
    }

    private void Reset_enemy()
    {
        enemy_controller.Stop_create_enemy();
        enemy_controller.Recycle_all_enemys();
        enemy_controller.Set_max_enemy_quantity(level_info[now_level][ENEMY_QUANTITY]);
        enemy_controller.Set_enemy_speed(level_info[now_level][ENEMY_SPEED]);
    }

    private void Set_player_init_position()
    {
        GameObject target = pillar_controller.Get_start_position();
        player_controller.Set_init_position(target);
        Set_canvas_position(player_controller.transform.position);
    }

    private void Reset_UI()
    {
        ui_controller.Close_restart_button();
        ui_controller.Open_start_button();
        ui_controller.Close_log();
        ui_controller.Update_Level(level_info[now_level][LEVEL].ToString());
        ui_controller.Update_HP(HP.ToString());
        ui_controller.Update_Timer(time.ToString());
    }

    public bool Is_Start
    {
        get
        {
            return is_start;
        }
    }
}
