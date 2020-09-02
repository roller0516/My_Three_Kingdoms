using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Spine.Unity;

public class Enemy : MonoBehaviour
{
    public float movespeed = 5f;    //에디터에서 변경
    float rotatespeed = Mathf.PI * 8f;

    public Animator animator;                   // 에니메이션
   /* public SkeletonAnimation skeleton3D; */       // 3D용 스파인 컴포넌트

    private Rigidbody rigidbody;

    string anim_name = "idle";

    Vector3 movement;                       //이동
    float h_move;
    float v_move;
    bool isJumping = false;
    bool isGround = true;

    public bool isPlaying = false;

    // AI
    public Transform pos0;
    public Transform pos1;
    int enemy_move_pos = 0;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //AnimationSet("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) return;

        //h_move = Input.GetAxisRaw("Horizontal"); 
        //v_move = Input.GetAxisRaw("Vertical");

        if (enemy_move_pos == 0)
        {
            if (transform.position.x < pos0.position.x) h_move = 1f;
            else if (transform.position.x > pos0.position.x) h_move = -1f;
            if (Mathf.Abs(transform.position.x - pos0.position.x) < 0.5f) enemy_move_pos = 1;
        }
        if (enemy_move_pos == 1)
        {
            if (transform.position.x < pos1.position.x) h_move = 1f;
            else if (transform.position.x > pos1.position.x) h_move = -1f;
            if (Mathf.Abs(transform.position.x - pos1.position.x) < 0.5f) enemy_move_pos = 0;
        }


    }

    void FixedUpdate()
    {
        if (!isPlaying) return;

        Run();
        Turn();
    }

    void Run()
    {
        movement.Set(h_move, 0, v_move);
        movement = movement.normalized * movespeed * Time.deltaTime;

        rigidbody.MovePosition(transform.position + movement);

        if (isPlaying && isGround)
        {
            if (movement == Vector3.zero) AnimationSet("idle");
            else AnimationSet("run");
        }
    }

    void Turn()
    {
        if (h_move == 0 && v_move == 0) return;
        Quaternion newRotation = Quaternion.LookRotation(movement);
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotation, rotatespeed * Time.deltaTime);
    }

    public void AnimationSet(string _name, bool _loop = true)
    {
        if (anim_name == _name) return; //중복 동작 
        anim_name = _name;

        //if (skeleton3D != null) //스파인 적용하는 경우
        //{
        //    skeleton3D.AnimationState.SetAnimation(0, _name, _loop);   //스파인 에니메이션
        //}

        if (animator != null)   //에니메이션 컨트롤러 사용하는 경우
        {
            animator.Play(_name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter " + other.transform.name);
    }
}
