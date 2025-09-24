using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class MindCard : MonoBehaviour
{
    [Header("Card Components")]
    [SerializeField] private GameObject frontFace; // always visible initially
    [SerializeField] private Image iconImage;
    [SerializeField] private Button cardButton;

    private Sprite _cardIcon;
    private bool _isFlipped = false;
    private bool _isMatched = false;

    public UnityAction<MindCard> OnCardSelected;

    private void Awake()
    {
        if (cardButton != null)
        {
            cardButton.onClick.AddListener(OnCardClicked);
        }
    }

    private void OnDestroy()
    {
        if (cardButton != null)
        {
            cardButton.onClick.RemoveListener(OnCardClicked);
        }
    }

    public void SetImage(Sprite icon)
    {
        _cardIcon = icon;
        iconImage.sprite = icon;
        ShowFront(); // always start with front face
    }

    private void OnCardClicked()
    {
        if (_isFlipped || _isMatched)
            return;

        FlipToIcon();
        OnCardSelected?.Invoke(this);
    }

    public void ShowFront()
    {
        frontFace.SetActive(true);
        iconImage.enabled = false;
        _isFlipped = false;
    }

    public void FlipToIcon()
    {
        frontFace.SetActive(false);
        iconImage.enabled = true;
        _isFlipped = true;
    }

    public void SetMatched()
    {
        _isMatched = true;
        transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }

    public void ResetCard()
    {
        ShowFront();
    }

    public bool IsMatched => _isMatched;
    public bool IsFlipped => _isFlipped;
    public Sprite CardIcon => _cardIcon;
}
