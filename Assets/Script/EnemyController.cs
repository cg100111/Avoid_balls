using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject[] North;
    public GameObject[] East;
    public GameObject[] Sourth;
    public GameObject[] West;
    public EnemyObjectPool enemy_pool;

    private int max_enemy_quantity = 0;
    private int start_position = 0;
    private int[] speed_levels = new int[] { 25, 50, 100, 200, 300};

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set_enemy_speed(int speed)
    {
        enemy_pool.Set_enemy_speed(speed_levels[speed - 1]);
    }

    public void Set_max_enemy_quantity(int quantity)
    {
        max_enemy_quantity = quantity;
    }

    public void Recycle_all_enemys()
    {
        enemy_pool.Unuse_all_enemy();
    }

    public void Start_create_enemys()
    {
        InvokeRepeating("Create_enemy", 1f, 1.5f);
    }

    public void Stop_create_enemy()
    {
        CancelInvoke("Create_enemy");
    }

    private void Create_enemy()
    {
        if (!Is_enemy_count_max())
        {
            Vector3 start, goal;
            start = Decide_start();
            goal = Decide_goal(start);
            enemy_pool.Use_enemy(start, goal);
        }
    }

    private bool Is_enemy_count_max()
    {
        if (enemy_pool.Used_enemy.Count == max_enemy_quantity)
            return true;
        return false;
    }

    private Vector3 Decide_start()
    {
        start_position = Get_random_position();
        return Get_random_point(start_position);
    }

    private Vector3 Decide_goal(Vector3 start)
    {
        int goal_position;
        do
        {
            goal_position = Get_random_position();
        }
        while (start_position == goal_position);
        return Get_random_point(goal_position);
    }
    
    private int Get_random_position()
    {
        return Random.Range(1, 4);
    }

    private Vector3 Get_random_point(int position)
    {
        if (position == 1)
            return Get_random_point_at_north();
        else if (position == 2)
            return Get_random_point_at_east();
        else if (position == 3)
            return Get_random_point_at_sourth();
        else
            return Get_random_point_at_west();
    }

    private Vector3 Get_random_point_at_north()
    {
        return North[Random.Range(0, North.Length)].transform.position;
    }

    private Vector3 Get_random_point_at_east()
    {
        return East[Random.Range(0, East.Length)].transform.position;
    }

    private Vector3 Get_random_point_at_sourth()
    {
        return Sourth[Random.Range(0, Sourth.Length)].transform.position;
    }

    private Vector3 Get_random_point_at_west()
    {
        return West[Random.Range(0, West.Length)].transform.position;
    }
}
