using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // == member variables ==
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // == member methods ==
    public void setMaxHealth(int max)
    {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
