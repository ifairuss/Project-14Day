using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerSkin : MonoBehaviour
{
    [Header("All player skin components")]
    [Space]
    [SerializeField] private SpriteRenderer _headSprite;
    [SerializeField] private SpriteRenderer _bodySprite;
    [SerializeField] private SpriteRenderer _leftLegSprite;
    [SerializeField] private SpriteRenderer _rightLegSprite;
    [SerializeField] private SpriteRenderer _leftHand;
    [SerializeField] private SpriteRenderer _rightHand;
    [SerializeField] private SpriteRenderer _weapon;

    [Header("Need components")]
    [SerializeField] private SkinManager _skinManager;


    private int _skinData;

    private List<GunsPreferences> _weaponId;
    private List<PlayerSkinData> _skinId;

    private void Start()
    {
        _skinData = _skinManager.SkinData;

        _weaponId = _skinManager.WeaponId;
        _skinId = _skinManager.SkinId;

        SpriteSwitch();
    }

    public void SwitchSkin()
    {
        if (_skinData < _skinId.Count - 1  && _skinData < _weaponId.Count - 1)
        {
            _skinData += 1;

            SpriteSwitch();

            _skinManager.SkinData = _skinData;
        }
        else
        {
            _skinData = 0;

            SpriteSwitch();

            _skinManager.SkinData = _skinData;
        }
    }

    private void SpriteSwitch()
    {
        _headSprite.sprite = _skinId[_skinData].HeadBottomSprite;
        _bodySprite.sprite = _skinId[_skinData].BodyBottomSprite;
        _leftLegSprite.sprite = _skinId[_skinData].LeftLegSprite;
        _rightLegSprite.sprite = _skinId[_skinData].RightLegSprite;
        _leftHand.sprite = _skinId[_skinData].LeftHand;
        _rightHand.sprite = _skinId[_skinData].RightHand;
        _weapon.sprite = _weaponId[_skinData].GunSprite;
    }
}
