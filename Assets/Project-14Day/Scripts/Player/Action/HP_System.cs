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

        yield return new WaitForSeconds(1f);

        if (_middlegroundImage.fillAmount > _forwardgroundImage.fillAmount)
        {
            _middlegroundImage.fillAmount -= 1f *Time.deltaTime;
        }
        else if (_middlegroundImage.fillAmount < _forwardgroundImage.fillAmount)
        {
            _middlegroundImage.fillAmount = _forwardgroundImage.fillAmount;
        }
    }
}
