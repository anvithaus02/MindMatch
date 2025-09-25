using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private UnityAction _callback;

    public void Initialize(UnityAction callback)
    {
        _callback = callback;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        transform.DOKill();
        transform.DOScale(0.9f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(1f, 0.1f).OnComplete(() =>
            {
                _callback?.Invoke();
            });
        });

    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
    }
}
