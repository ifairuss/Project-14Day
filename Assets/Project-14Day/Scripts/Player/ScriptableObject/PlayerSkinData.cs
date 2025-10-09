using UnityEngine;

[CreateAssetMenu(fileName = "New_Skin", menuName = "Create new player skin / Add new skin", order = 3)]
public class PlayerSkinData : ScriptableObject
{
    [Header("Skin preferences")]
    public Sprite HatSprite;
    public Sprite BodySprite;
    public Sprite LeftLegUpSprite;
    public Sprite LeftLegDownSprite;
    public Sprite RightLegUpSprite;
    public Sprite RightLegDownSprite;
    public Sprite LeftEyebrow;
    public Sprite RightEyebrow;
}
