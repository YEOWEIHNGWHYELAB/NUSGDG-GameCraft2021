using UnityEngine;
using UnityEngine.UI;

public class WeaponbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetWeaponStatus(float weaponReady, float weaponReadyMax)
    {
        slider.value = weaponReady * 1.0f;
        slider.maxValue = weaponReadyMax * 1.0f;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
