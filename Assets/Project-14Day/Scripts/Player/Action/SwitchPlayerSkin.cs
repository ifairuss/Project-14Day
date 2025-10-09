using UnityEngine;

public class SwitchPlayerSkin : MonoBehaviour
{
    public PlayerSkinData skinData;

    public SpriteRenderer HatSprite;
    public SpriteRenderer BodySprite;
    public SpriteRenderer LeftLegUpSprite;
    public SpriteRenderer LeftLegDownSprite;
    public SpriteRenderer RightLegUpSprite;
    public SpriteRenderer RightLegDownSprite;
    public SpriteRenderer LeftEyebrow;
    public SpriteRenderer RightEyebrow;

    private void Update()
    {
        HatSprite.sprite = skinData.HatSprite;
        BodySprite.sprite = skinData.BodySprite;
        LeftLegUpSprite.sprite = skinData.LeftLegUpSprite;
        LeftLegDownSprite.sprite = skinData.LeftLegDownSprite;
        RightLegUpSprite.sprite = skinData.RightLegUpSprite;
        RightLegDownSprite.sprite = skinData.RightLegDownSprite;
        LeftEyebrow.sprite = skinData.LeftEyebrow;
        RightEyebrow.sprite= skinData.RightEyebrow;
    }
}
