using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // == member variables ==
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // == member methods ==
    public void setMaxHealth(int max)
    {
        // set health slider bars to full hp
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        // set slider value to player health
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
   
}
