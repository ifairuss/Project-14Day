using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchPlayerSkin : MonoBehaviour
{
    [Header("All player skin components")]
    [Space]
    [SerializeField] private SpriteRenderer _hatSprite;
    [SerializeField] private SpriteRenderer _bodySprite;
    [SerializeField] private SpriteRenderer _leftLegUpSprite;
    [SerializeField] private SpriteRenderer _leftLegDownSprite;
    [SerializeField] private SpriteRenderer _rightLegUpSprite;
    [SerializeField] private SpriteRenderer _rightLegDownSprite;
    [SerializeField] private SpriteRenderer _leftEyebrow;
    [SerializeField] private SpriteRenderer _rightEyebrow;
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
        _hatSprite.sprite = _skinId[_skinData].HatSprite;
        _bodySprite.sprite = _skinId[_skinData].BodySprite;
        _leftLegUpSprite.sprite = _skinId[_skinData].LeftLegUpSprite;
        _leftLegDownSprite.sprite = _skinId[_skinData].LeftLegDownSprite;
        _rightLegUpSprite.sprite = _skinId[_skinData].RightLegUpSprite;
        _rightLegDownSprite.sprite = _skinId[_skinData].RightLegDownSprite;
        _leftEyebrow.sprite = _skinId[_skinData].LeftEyebrow;
        _rightEyebrow.sprite = _skinId[_skinData].RightEyebrow;
        _weapon.sprite = _weaponId[_skinData].GunSprite;
    }
}
