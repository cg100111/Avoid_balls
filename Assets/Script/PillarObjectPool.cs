using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarObjectPool : MonoBehaviour {

    public GameObject pillar_prefab;
    public int max_count;

    private List<GameObject> unuse_pillars;
    private List<GameObject> use_pillars;

    void Awake()
    {
        unuse_pillars = new List<GameObject>();
        use_pillars = new List<GameObject>();
        Create();
    }

    public void Use_pillar(Vector3 pos)
    {
        GameObject pillar = unuse_pillars[0];
        use_pillars.Add(pillar);
        unuse_pillars.Remove(pillar);
        pillar.SetActive(true);
        pillar.transform.position = pos;
    }

    public void Unuse_pillar(GameObject pillar)
    {
        pillar.SetActive(false);
        use_pillars.Remove(pillar);
        unuse_pillars.Add(pillar);
    }

    public void Unuse_all_pillars()
    {
        while(use_pillars.Count != 0)
            Unuse_pillar(use_pillars[0]);
    }

    private void Create()
    {
        for (int num = 0; num < max_count; num++)
        {
            GameObject pillar = Instantiate(pillar_prefab);
            pillar.transform.parent = this.transform;
            pillar.SetActive(false);
            unuse_pillars.Add(pillar);
        }
    }

    public List<GameObject> Used_Pillars
    {
        get
        {
            return use_pillars;
        }
    }
}
