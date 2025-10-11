using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerSkin : MonoBehaviour
{
    public List<GunsPreferences> WeaponId;
    public List<PlayerSkinData> SkinId;

    public SpriteRenderer HatSprite;
    public SpriteRenderer BodySprite;
    public SpriteRenderer LeftLegUpSprite;
    public SpriteRenderer LeftLegDownSprite;
    public SpriteRenderer RightLegUpSprite;
    public SpriteRenderer RightLegDownSprite;
    public SpriteRenderer LeftEyebrow;
    public SpriteRenderer RightEyebrow;
    public SpriteRenderer Weapon;

    [Space]
    public int SkinData;

    private void Awake()
    {
        HatSprite.sprite = SkinId[SkinData].HatSprite;
        BodySprite.sprite = SkinId[SkinData].BodySprite;
        LeftLegUpSprite.sprite = SkinId[SkinData].LeftLegUpSprite;
        LeftLegDownSprite.sprite = SkinId[SkinData].LeftLegDownSprite;
        RightLegUpSprite.sprite = SkinId[SkinData].RightLegUpSprite;
        RightLegDownSprite.sprite = SkinId[SkinData].RightLegDownSprite;
        LeftEyebrow.sprite = SkinId[SkinData].LeftEyebrow;
        RightEyebrow.sprite = SkinId[SkinData].RightEyebrow;
        Weapon.sprite = WeaponId[SkinData].GunSprite;
    }
}
