using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject head;
    public GameController game_controller;
    public PillarObjectPool pillar_objectpool;
    public HandController[] hands;

    private GameObject extend_hand;
    private GameObject pillar_standed;
    private bool is_falldown;
    private Vector3 gravity = new Vector3(0f, -0.005f, 0f);

    // Use this for initialization
    void Start()
    {
        extend_hand = null;
        is_falldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pillar_standed != null && game_controller.Is_Start)
            Is_player_on_pillar();
        if (is_falldown)
        {
            Fall_down();
            if (transform.position.y < -1)
            {
                game_controller.Lose();
                is_falldown = false;
            }
        }
    }

    public void Set_init_position(GameObject target)
    {
        Vector3 new_pos = new Vector3(target.transform.position.x, 0.6f, target.transform.position.z);
        Set_player_position(new_pos);
        pillar_standed = target;
    }

    public void Move()
    {
        Vector3 player_new_pos = new Vector3(extend_hand.transform.position.x, 0.6f, extend_hand.transform.position.z);
        Recover_hands();
        Set_player_position(player_new_pos);
        game_controller.Set_canvas_position(player_new_pos);
        Is_player_Move_to_pillar(player_new_pos);
    }

    private void Set_player_position(Vector3 new_pos)
    {
        transform.position = new_pos;
    }

    private void Is_player_Move_to_pillar(Vector3 player_position)
    {
        Vector3 pillar_pos = new Vector3();
        foreach (GameObject pillar in pillar_objectpool.Used_Pillars)
        {
            pillar_pos.Set(pillar.transform.position.x, player_position.y, pillar.transform.position.z);
            if (Vector3.Distance(pillar_pos, player_position) < 0.3f)
            {
                pillar_standed = pillar;
                return;
            }
        }
        is_falldown = true;
        pillar_standed = null;
    }

    private void Is_player_on_pillar()
    {
        Vector2 pillar_pos = new Vector2(pillar_standed.transform.position.x, pillar_standed.transform.position.z);
        Vector2 player_position = new Vector2(head.transform.position.x, head.transform.position.z);
        if (Vector2.Distance(pillar_pos, player_position) >= 0.3f)
            is_falldown = true;
    }

    private void Recover_hands()
    {
        foreach (HandController hand in hands)
            hand.Recover_hand();
    }

    private void Fall_down()
    {
        Set_player_position(transform.position + gravity);
    }

    public GameObject handIsExtended
    {
        set
        {
            extend_hand = value;
        }
    }

    public bool Falldown
    {
        get
        {
            return is_falldown;
        }
    }
}
