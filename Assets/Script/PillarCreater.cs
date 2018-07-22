using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCreater : MonoBehaviour {

    public GameObject[] stand_points;
    public Material now_material;
    public Material next_material;
    public Material last_material;

    private const float MAX_Z = 1.25f;
    private const float MIN_Z = -1.25f;
    private const float MAX_X = 1.25f;
    private const float MIN_X = -1.25f;

    private Vector3 new_position;

    public void Initialize(int now_stand_point)
    {
        stand_points[now_stand_point].transform.position = new Vector3(0f, 0f, -1f);
        Decide_next_position(now_stand_point);
    }

    public void Decide_next_position(int now_stand_point)
    {
        new_position = Create_new_position();
        while (Is_overlapping(now_stand_point))
        {
            new_position = Create_new_position();
        }
        stand_points[(now_stand_point + 1) % 3].transform.position = new_position;
        Set_color(now_stand_point);
    }

    private void Set_color(int now_stand_point)
    {
        stand_points[now_stand_point].GetComponent<Renderer>().material = now_material;
        stand_points[(now_stand_point + 1) % 3].GetComponent<Renderer>().material = next_material;
        stand_points[(now_stand_point + 2) % 3].GetComponent<Renderer>().material = last_material;
    }

    private bool Is_overlapping(int now_stand_point)
    {
        if (Vector3.Distance(new_position, stand_points[now_stand_point % 3].transform.position) <= 1)
            return true;
        return false;
    }

    private Vector3 Create_new_position()
    {
        return new Vector3(Random.Range(MIN_X, MAX_X), 0f, Random.Range(MIN_Z, MAX_Z));
    }

    public GameObject[] StandPoints
    {
        get
        {
            return stand_points;
        }
    }
}
