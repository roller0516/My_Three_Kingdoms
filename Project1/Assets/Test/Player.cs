using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;
    private SkeletonRenderer skeletonRenderer;
    public float i = 1;

    public enum AnimState
    {
        Idle, move, Attack
    }

    private AnimState _AniState;
    //현재 애니메이션이 재생되고 있는지에 대한 변수
    private string CurrentAnimation;
    //움직임
    private Rigidbody2D rig;
    private float x;

    private void Awake() => rig = GetComponent<Rigidbody2D>();
    private void Start()
    {
        //skeletonAnimation.Skeleton.SetSkin("1");
        
        //해당스킨으로 변경
    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
      
        if (x == 0f)
            _AniState = AnimState.Idle;
        else
        {
            _AniState = AnimState.move;

            transform.localScale = new Vector2(x, 1);
        }
        if (Input.GetKey(KeyCode.Space) == true)
        {
            _AniState = AnimState.Attack;
        }
        SetCurrentAnimation(_AniState,i);
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector2(x * 300 * Time.deltaTime, rig.velocity.y);
    }
    private void _AsncAnimation(AnimationReferenceAsset aniClip, bool loop, float timeScale)//해당애니메이션으로 변경한다
    {
        if (aniClip.name.Equals(CurrentAnimation))//같은 애니메이션을 재생하려고 한다면 아래코드를 실행하지 않는다.
            return;
        skeletonAnimation.state.SetAnimation(0, aniClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        CurrentAnimation = aniClip.name;//현재 재생되고 있는 애니메이션 이름으로 변경
    }
    private void SetCurrentAnimation(AnimState _state,float _num)
    {
        switch (_state)
        {
            case AnimState.Idle:
                _AsncAnimation(AnimClip[(int)AnimState.Idle], true, 1f* _num);
                break;
            case AnimState.move:
                _AsncAnimation(AnimClip[(int)AnimState.move], true, 1f* _num);
                break;
            case AnimState.Attack:
                _AsncAnimation(AnimClip[(int)AnimState.Attack], true, 1f* _num);
                break;
        }
    }
    public void ButtonON()
    {
        skeletonAnimation.skeleton.SetAttachment("wep 1", "wep 1");
    }
    public void ButtonON2()
    {
        skeletonAnimation.skeleton.SetAttachment("wep 1", "wep 2");
    }
    public void DoubleSpeed()
    {
        Time.timeScale = 2;
        //i = 2;
        //print(i);
        //SetCurrentAnimation(_AniState, i);

    }
}
