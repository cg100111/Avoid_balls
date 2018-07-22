using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour {

    public GameObject enemy_prefab;
    public int max_count;

    private List<GameObject> unuse_enemy;
    private List<GameObject> used_enemy;
	// Use this for initialization
	void Awake () {
        unuse_enemy = new List<GameObject>();
        used_enemy = new List<GameObject>();
        Create();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set_enemy_speed(int speed)
    {
        foreach (GameObject enemy in unuse_enemy)
            enemy.GetComponent<Enemy>().Set_speed(speed);
    }

    public void Use_enemy(Vector3 start, Vector3 goal)
    {
        GameObject enemy = unuse_enemy[0];
        used_enemy.Add(enemy);
        unuse_enemy.Remove(enemy);
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().Set_start_and_goal_position(start, goal);
    }

    public void Unuse_enemy(GameObject enemy)
    {
        enemy.SetActive(false);
        used_enemy.Remove(enemy);
        unuse_enemy.Add(enemy);
    }

    public void Unuse_all_enemy()
    {
        while (used_enemy.Count != 0)
            Unuse_enemy(used_enemy[0]);
    }

    private void Create()
    {
        for (int num = 0; num < max_count; num++)
        {
            GameObject enemy = Instantiate(enemy_prefab);
            enemy.transform.parent = this.transform;
            enemy.SetActive(false);
            unuse_enemy.Add(enemy);
        }
    }

    public List<GameObject> Used_enemy
    {
        get
        {
            return used_enemy;
        }
    }
}
