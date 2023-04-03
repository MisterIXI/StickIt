using UnityEngine;
using UnityEngine.UI;
public class ButtonClickSound : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SoundManager.PlayButtonClick();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}