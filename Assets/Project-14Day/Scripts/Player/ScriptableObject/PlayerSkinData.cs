using UnityEngine;

[CreateAssetMenu(fileName = "New_Skin", menuName = "Create new player skin / Add new skin", order = 3)]
public class PlayerSkinData : ScriptableObject
{
    [Header("Skin preferences")]
    public Sprite HeadSprite;
    public Sprite BodyTopSprite;
    public Sprite BodyRightSprite;
    public Sprite BodyBottomSprite;
    public Sprite LeftLegSprite;
    public Sprite RightLegSprite;
    public Sprite LeftHand;
    public Sprite RightHand;
}
