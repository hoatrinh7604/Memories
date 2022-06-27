using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowNumberController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] string character = "";
    [SerializeField] Image image;
    [SerializeField] Button button;
    private int value;
    private int index;
    private void Start()
    {
        button.onClick.AddListener(() => OnPress());
    }

    public void SetInfo(int value, Sprite sprite)
    {
        this.value = value;
        numberText.text = value.ToString();
        character = value.ToString();
        image.sprite = sprite;
    }

    public void SetIndex(int value)
    {
        index = value;
    }

    public void DisplayControl(bool isShown)
    {
        numberText.gameObject.SetActive(isShown);
    }

    public void OnPress()
    {
        //
        GamePlayController.Instance.OnPressHandle(index, value);
    }
}
