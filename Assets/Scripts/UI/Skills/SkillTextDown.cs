using TMPro;

public class SkillTextDown : SkillView
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnUpdateView(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth} / {maxHealth}";
    }
}
