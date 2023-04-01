using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RuleProgress : MonoBehaviour
{
    [field: SerializeField] public RectTransform ProgressBar { get; private set; }
    [field: SerializeField] public float MaxProgress { get; private set; }
    [field: SerializeField] public Image ProgressImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ProgressText { get; private set; }
    private float _currentProgress;

    public void SetProgress(float progress)
    {
        _currentProgress = progress;
        ProgressBar.sizeDelta = new Vector2(MaxProgress * _currentProgress, ProgressBar.sizeDelta.y);
    }

    public void SwitchColor(bool isPositiveProgress)
    {
        Debug.Log("Switching color");
        ProgressImage.color = isPositiveProgress ? new Color(0f, 0.5882352941f, 0f) : new Color(0.5882352941f, 0f, 0f);
    }
}