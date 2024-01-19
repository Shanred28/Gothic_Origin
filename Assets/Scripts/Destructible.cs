using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    #region reactive property
    private CompositeDisposable _disposables = new CompositeDisposable();
    public IntReactiveProperty  _reactCurrentHp = new IntReactiveProperty();
    public IntReactiveProperty _reactMaxCurrentHp = new IntReactiveProperty();

    #endregion

    public const int TeamIdNeutral = 0;

    public UnityAction<Destructible> OnGetDamage;
    public UnityEvent ChangeHp;

    [SerializeField] private UnityEvent _eventOnGetDamage;
    [SerializeField] private UnityEvent _eventOnDeath;
    public UnityEvent EventOnDeath => _eventOnDeath;

    [SerializeField] protected bool _isDestructible;
    public bool IsIndestructible => _isDestructible;

/*    [SerializeField] private int _maxHitPoints;
    public int MaxHitPoints => _maxHitPoints;*/

/*    private int _currentHitPoints;
    public int CurrentHitPoints => _currentHitPoints;*/

    [SerializeField] private int _teamId;
    public int TeamId => _teamId;

    public bool IsDestroy = false;

    private void Start()
    {

    }

    public void ApplyDamage(int damage, Destructible other)
    {
        if (_isDestructible) return;

        _reactCurrentHp.Value -= damage;
        ChangeHp.Invoke();
        OnGetDamage?.Invoke(other);
        _eventOnGetDamage?.Invoke();


        if (_reactCurrentHp.Value <= 0)
            OnDeath();
    }

    public void ApplyHill(int heal)
    {
        _reactCurrentHp.Value += heal;
        if (_reactCurrentHp.Value > _reactMaxCurrentHp.Value)
            _reactCurrentHp.Value = _reactMaxCurrentHp.Value;
    }

    protected virtual void OnDeath()
    {
        _eventOnDeath?.Invoke();
        IsDestroy = true;
        Destroy(gameObject);
    }

    protected void SetIdTeam(int idTeam)
    {
        _teamId = idTeam;
    }
}
