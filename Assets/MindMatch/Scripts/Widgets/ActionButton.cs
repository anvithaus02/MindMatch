using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine.Events;

namespace MindMatch.UI
{
    public class ActionButton : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Button _button;

        private UnityAction _callback;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Initialize(string text, UnityAction onClick)
        {
            _callback = onClick;
            SetButtonText(text);
        }

        private void SetButtonText(string text)
        {
            _buttonText.text = text;
        }

        private void OnClicked()
        {
            AudioManager.Instance.PlayAudio(AudioType.ButtonClick);

            transform.DOKill();
            transform.DOScale(0.9f, 0.1f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.1f).OnComplete(() =>
                {
                    _callback?.Invoke();
                });
            });

        }

    }
}
