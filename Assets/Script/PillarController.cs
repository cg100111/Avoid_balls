using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour {

    public PillarObjectPool pillar_pool;

    private const float MAX_Z = 1.25f;
    private const float MIN_Z = -1.25f;
    private const float MAX_X = 1.25f;
    private const float MIN_X = -1.25f;

    void Start()
    {
    }

    public void Recycle_all_pillars()
    {
        pillar_pool.Unuse_all_pillars();
    }

    public void Create_area(int quantity)
    {
        for (int num = 0; num < quantity; num++)
        {
            Vector3 pos = Decide_position_of_pillar();
            pillar_pool.Use_pillar(pos);
        }
    }

    public GameObject Get_start_position()
    {
        return pillar_pool.Used_Pillars[Random.Range(0, pillar_pool.Used_Pillars.Count)];
    }

    private Vector3 Decide_position_of_pillar()
    {
        Vector3 pos;
        do
        {
            pos = Produce_new_posiion();
        }
        while (Is_overlap(pos) || Is_too_far(pos));
        return pos;
    }

    private bool Is_overlap(Vector3 pos)
    {
        foreach (GameObject pillar in pillar_pool.Used_Pillars)
            if (Vector3.Distance(pillar.transform.position, pos) < 0.6f)
                return true;  
        return false;
    }

    private bool Is_too_far(Vector3 pos)
    {
        int count = 0;
        foreach (GameObject pillar in pillar_pool.Used_Pillars)
            if (Vector3.Distance(pillar.transform.position, pos) > 1.0f)
                count++;
        if (pillar_pool.Used_Pillars.Count == 0)
            return false;
        return count == pillar_pool.Used_Pillars.Count;
    }

    private Vector3 Produce_new_posiion()
    {
        return new Vector3(Random.Range(MIN_X, MAX_X), -0.5f, Random.Range(MIN_Z, MAX_Z));
    }
}
