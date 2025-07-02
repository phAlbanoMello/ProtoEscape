using UnityEditor.Animations;
using UnityEngine;

public enum AnimID
{
    F1_down_walk,
    F7_down_walk,
    F8_down_walk,
}


public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimID animID;

    [ContextMenu("Play")]
    void Play()
    {
        PlayAnimation(animID.ToString());
    }


    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }
}
