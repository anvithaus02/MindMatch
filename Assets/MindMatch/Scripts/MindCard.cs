using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindCard : MonoBehaviour
{
     [SerializeField] private Image _frontImage;

    public void SetImage(Sprite sprite)
    {
        _frontImage.sprite = sprite;
    }
}
