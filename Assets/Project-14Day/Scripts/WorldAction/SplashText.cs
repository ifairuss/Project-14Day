using TMPro;
using UnityEngine;

public class SplashText : MonoBehaviour
{
    [Header("Splash panel properties")]
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private GameObject _splashPanel;

    private Animator _textAnimator;

    private void Start()
    {
        _textAnimator = GetComponent<Animator>();
    }

    public void ClosedSplashPanel ()
    {
        _splashPanel.SetActive(false);
        _textAnimator.SetTrigger("ClosedAnimation");
    }

    public void OpenSplashPanel (string wave)
    {
        _splashPanel.SetActive(true);
        _waveText.text = wave;
        _textAnimator.SetTrigger("SplashAnimation");
    }
}
