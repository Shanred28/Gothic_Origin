using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI_STateBar : MonoBehaviour
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private Slider _sliderBar;
    [SerializeField] private TMP_Text _currentHp;
    [SerializeField] private TMP_Text _maxHp;

    private void Start()
    {
        Player.Instance.playerDes._reactMaxCurrentHp.Subscribe(v =>
        {
            _maxHp.text = v.ToString();
            _sliderBar.maxValue = v;
        }).AddTo(_disposables);

        Player.Instance.playerDes._reactCurrentHp.Subscribe(v =>
        {
            _currentHp.text = v.ToString();
            _sliderBar.value =  v;
        }).AddTo(_disposables);     
    }

    private void OnDestroy()
    {
        _disposables.Clear();
    }
}
