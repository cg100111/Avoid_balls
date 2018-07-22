using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameController game_controller;
    private EnemyObjectPool enemy_objectpool;
    private Vector3 goal_position;
    private Vector3 direction;
    private int speed;

	// Use this for initialization
	void Start () {
        game_controller = GameObject.Find("GameManager").GetComponent<GameController>();
        enemy_objectpool = GameObject.Find("Enemys").GetComponent<EnemyObjectPool>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Recycle_myself();
    }

    public void Set_speed(int value)
    {
        speed = value;
    }

    public void Set_start_and_goal_position(Vector3 start, Vector3 goal)
    {
        transform.position = start;
        goal_position = goal;
        direction = goal_position - transform.position;
        direction.Normalize();
    }

    private void Move()
    {
        transform.position += direction / speed;
    }

    private void Recycle_myself()
    {
        if (transform.position.z > 2.2 || transform.position.z < -2.5 || transform.position.x < -2.9 || transform.position.x > 2.7)
            enemy_objectpool.Unuse_enemy(this.gameObject);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            game_controller.Reduce_HP();
        }
    }
}
