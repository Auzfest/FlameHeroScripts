using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Transform enemy;
    public Transform sword;
    public Animator anim;
    public Collider sword_collision;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Trigger sword swing animation or logic here
            anim.Play("SwordSwing");
            sword_collision.enabled = true;




        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.Play("SwordIdle");
            sword_collision.enabled = false;
        }
    }
}

