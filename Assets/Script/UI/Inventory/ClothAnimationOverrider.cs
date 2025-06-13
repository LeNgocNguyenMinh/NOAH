using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothAnimationOverrider : MonoBehaviour
{
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    void Awake()
    {
        animator = FindObjectOfType<PlayerControl>().GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }
    public void EquipAnimation(string itemID)
    {

        string[] parts = itemID.Split('_');
        if (parts.Length != 3)
        {
            Debug.LogWarning($"Invalid itemID format: {itemID}");
            return;
        }
        string category = parts[0]; // "FireCloth"
        string bodyPartType = parts[1]; // "Hat"
        string bodyPartName = parts[2]; // "FireHat"
        string[] stateArray = {"Idle", "Dash", "Dead", "Walk"};
        for(int i = 0; i <stateArray.Length; i++)
        {
            string animationPath = $"OutfitAnimation/{category}/{bodyPartType}/{bodyPartName + stateArray[i]}";
            AnimationClip animationClip = Resources.Load<AnimationClip>(animationPath);
            if (animationClip != null)
            {              
                string stateName = bodyPartType + stateArray[i]; // Tạo tên trạng thái, ví dụ: "HatIdle"            
                animatorOverrideController[stateName] = animationClip; 
                Debug.Log($"Animation overridden: {stateName} -> {animationPath}");
            }
            else
            {
                Debug.LogWarning($"Animation not found for itemID: {itemID}, path: {animationPath}");
            }
        }   
    }
}
