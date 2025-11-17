using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP_System : MonoBehaviour
{
    [SerializeField] private Image _middlegroundImage;
    [SerializeField] private Image _forwardgroundImage;

    public IEnumerator HealthSystems(float health)
    {
        health = health / 100;

        _forwardgroundImage.fillAmount = health;

        yield return new WaitForSeconds(0.1f);

        if (_middlegroundImage.fillAmount > _forwardgroundImage.fillAmount)
        {
            _middlegroundImage.fillAmount -= 0.5f * Time.fixedDeltaTime;
        }
        else if (_middlegroundImage.fillAmount < _forwardgroundImage.fillAmount)
        {
            _middlegroundImage.fillAmount = _forwardgroundImage.fillAmount;
        }
    }
}
