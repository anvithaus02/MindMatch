using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;
namespace MindMatch.Gameplay
{
    public class MindCard : MonoBehaviour
    {
        [Header("Card Components")]
        [SerializeField] private GameObject _frontFaceGO;
        [SerializeField] private Image _iconImageUI;
        [SerializeField] private Button _cardButton;

        private Sprite _cardIcon;
        private bool _isFlipped;
        private bool _isMatched;

        private const float MatchScaleDuration = 0.3f;
        public event Action<MindCard> OnCardSelected;
        public bool IsMatched => _isMatched;
        public bool IsFlipped => _isFlipped;
        public Sprite CardIcon => _cardIcon;

        private void OnEnable() => _cardButton.onClick.AddListener(OnCardClicked);
        private void OnDisable() => _cardButton.onClick.RemoveListener(OnCardClicked);
        public void SetImage(Sprite icon)
        {
            _cardIcon = icon;
            _iconImageUI.sprite = icon;
            SetFlipped(false);
        }

        public void SetMatched()
        {
            _isMatched = true;
            AudioManager.Instance.PlayAudio(AudioType.MatchSuccess);
            transform.DOScale(Vector3.zero, MatchScaleDuration)
                     .OnComplete(() => gameObject.SetActive(false));
        }

        public void ResetCard()
        {
            AudioManager.Instance.PlayAudio(AudioType.CardFlip);
            SetFlipped(false);
        }
        private void OnCardClicked()
        {
            if (_isFlipped || _isMatched) return;

            SetFlipped(true);
            AudioManager.Instance.PlayAudio(AudioType.CardClick);
            OnCardSelected?.Invoke(this);
        }
        private void SetFlipped(bool showIcon)
        {
            _frontFaceGO.SetActive(!showIcon);
            _iconImageUI.enabled = showIcon;
            _isFlipped = showIcon;
        }
    }
}