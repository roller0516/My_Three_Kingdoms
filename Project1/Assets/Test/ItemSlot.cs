using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ItemSlot : MonoBehaviour
{
    [SpineSkin] public string baseSkinName; // 복사 할 스킨의 이름
    public Material baseMaterial; // 기본 머터리얼

    public Sprite gogglesSprite; // 고글 스프라이트
    [SpineSlot] public string gogglesSlot; // 고글 슬롯의 이름
    [SpineAttachment] public string gogglesKey; // 고글 어테치먼트의 이름

    private void Start()
    {
        //SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>(); // SkeletonAnimation 컴포터넌트 가져오기
        //Spine.Skeleton skeleton = skeletonAnimation.Skeleton; // 스켈레톤 클래스

        //Spine.Skin newSkin = new Spine.Skin("custom skin"); // 새로운 스킨생성
        //var baseSkin = skeleton.Data.FindSkin(baseSkinName); // 기존에 있는 스킨 가져오기
        ///*newSkin.AddAttachments(baseSkin);*/ // 기존에 있는 스킨에 정의된 값을 새로운 스킨에 추가
        //newSkin.AddSkin(baseSkin);
        //int gogglesSlotIndex = skeleton.FindSlotIndex(gogglesSlot); // 고글 슬롯값 얻어오기
        //Spine.Attachment baseAttachment = baseSkin.GetAttachment(gogglesSlotIndex, gogglesKey); // 고글의 어테치먼트 얻어오기
        //Spine.Attachment newAttachment = baseAttachment.GetRemappedClone(gogglesSprite, baseMaterial); // 변경할 스프라이트로 다시 매핑된 어테치먼트 얻어오기
        //newSkin.SetAttachment(gogglesSlotIndex, gogglesKey, newAttachment); // 스킨에 변경된 어테치먼트 설정
        
    }
}
