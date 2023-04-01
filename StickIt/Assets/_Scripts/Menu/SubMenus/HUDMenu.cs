using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDMenu : MenuBase
{
    [field: SerializeField] public Button PauseButton { get; private set; }
    [field: SerializeField] public RectTransform RulesPanel { get; private set; }
    [field: SerializeField] public RectTransform RulePrefab { get; private set; }
    private Dictionary<BaseRule, RuleProgress> _ruleToRect = new Dictionary<BaseRule, RuleProgress>();
    public override void Init()
    {
        PauseButton.onClick.AddListener(ToPauseMenu);
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        _ruleToRect.Clear();
        if (newScene.name == "MainScene")
            return;
        BaseRule[] rules = RuleManager.Instance.Rules;
        for (int i = 0; i < rules.Length; i++)
        {
            RectTransform rule = Instantiate(RulePrefab, RulesPanel);
            RuleProgress ruleProgress = rule.GetComponent<RuleProgress>();
            rule.GetComponentInChildren<TextMeshProUGUI>().text = "- " + rules[i].RuleDescription;
            _ruleToRect.Add(rules[i], ruleProgress);
        }
        RuleManager.Instance.OnRuleChange += OnRuleChange;
    }

    private void OnRuleChange(RuleChangeType type, BaseRule rule, float value, bool isPositiveProgress)
    {
        switch (type)
        {
            case RuleChangeType.Progress:
                _ruleToRect[rule].SetProgress(value);
                _ruleToRect[rule].SwitchColor(isPositiveProgress);
                break;
            case RuleChangeType.Completion:
                _ruleToRect[rule].SetProgress(1f);
                _ruleToRect[rule].SwitchColor(true);
                _ruleToRect[rule].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
                break;
            case RuleChangeType.Fail:
                _ruleToRect[rule].SetProgress(1f);
                _ruleToRect[rule].SwitchColor(false);
                _ruleToRect[rule].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                break;
        }
    }

    private void OnRuleCompletion(BaseRule rule)
    {
        _ruleToRect[rule].GetComponentInChildren<Text>().color = Color.green;
    }
    // public override void SelectFirst()
    // {
    //     // set selection to null
    //     MenuManager.Instance.EventSystem.SetSelectedGameObject(null);
    // }
}