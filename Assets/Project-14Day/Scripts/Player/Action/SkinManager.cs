using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [Header("Skin ID")]
    public int SkinData;

    [Space]
    [Header("All skin variable")]
    public List<GunsPreferences> WeaponId;
    public List<PlayerSkinData> SkinId;
}
