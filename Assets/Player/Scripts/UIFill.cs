using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFill : MonoBehaviour
{
    public int maxValue;
    public Image fill;

    private int currentValue;

    private void Start()
    {
        currentValue = maxValue;
        fill.fillAmount = 1;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Add(10);

        if (Input.GetKeyDown(KeyCode.D))
            Remove(10);
    }


    public void Add(int value)
    {
        currentValue += value;

        if (currentValue >= maxValue)
        {
            currentValue = maxValue;
        }

        fill.fillAmount = (float)currentValue / maxValue;
    }

    public void Remove(int value) 
    {
        currentValue -= value;

        if (currentValue < 0)
        {
            currentValue = 0;
        }

        fill.fillAmount = (float)currentValue / maxValue;
    }
}
