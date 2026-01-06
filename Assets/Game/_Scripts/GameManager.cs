using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int MinRangeValue = -50;
    private const int MaxRangeValue = 51;

    [Header("Wallet")]

    [SerializeField] private GameObject _ui;
    [SerializeField] private TextMeshProUGUI _textMeshDiamonds;
    [SerializeField] private TextMeshProUGUI _textMeshEnergy;
    [SerializeField] private TextMeshProUGUI _textMeshMoney;

    private WalletService _walletService;

    private WalletCurrencyViewer _viewerDiamonds;
    private WalletCurrencyViewer _viewerEnergy;
    private WalletCurrencyViewer _viewerMoney;

    private TimerService _timerService;

    private TimerViewer _timerViewer;
    private TimerSliderViewer _timerSliderViewer;
    private TimerHealthViewer _timerHealthViewer;

    [Header("Timer")]

    [SerializeField] private TextMeshProUGUI _textMeshTimer;
    [SerializeField] private Slider _sliderTimer;
    [SerializeField] private float _timerValue;

    [SerializeField] private Transform _healthParent;
    [SerializeField] private Transform _healthIconPrefab;

    private InputSystem _inputSystem;

    private EnemyDestroyerService _enemyDestroyerService;

    [Header("Enemy")]

    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Enemy _enemyPrefub;
    [SerializeField] private int _maxCountForDeath;

    private void Awake()
    {
        _inputSystem = new InputSystem();

        _walletService = new WalletService();

        _ui.SetActive(true);

        _viewerDiamonds = new WalletCurrencyViewer(_walletService, _textMeshDiamonds, CurrencyType.Diamonds);
        _viewerEnergy = new WalletCurrencyViewer(_walletService, _textMeshEnergy, CurrencyType.Energy);
        _viewerMoney = new WalletCurrencyViewer(_walletService, _textMeshMoney, CurrencyType.Money);

        _timerService = new TimerService(this);

        _timerViewer = new TimerViewer(_timerService, _textMeshTimer);
        _timerSliderViewer = new TimerSliderViewer(_timerService, _sliderTimer);
        _timerHealthViewer = new TimerHealthViewer(_timerService, _healthParent, _healthIconPrefab);

        _enemyDestroyerService = new EnemyDestroyerService();
    }

    private void OnDestroy()
    {
        _viewerDiamonds.Disable();
        _viewerEnergy.Disable();
        _viewerMoney.Disable();

        _timerViewer.Disable();
        _timerSliderViewer.Disable();
        _timerHealthViewer.Disable();
    }

    private void Update()
    {
        if (_inputSystem.GetKeyDownAlpha1())
        {
            int value = Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TryChangeAmountOnCurrency(CurrencyType.Energy, value);

            value = Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TryChangeAmountOnCurrency(CurrencyType.Diamonds, value);

            value = Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TryChangeAmountOnCurrency(CurrencyType.Money, value);
        }

        if (_inputSystem.GetKeyDownAlpha2())
        {
            _timerService.Start(_timerValue);
        }

        if (_inputSystem.GetKeyDownAlpha3())
        {
            _timerService.Restart();
        }

        if (_inputSystem.GetKeyDownAlpha4())
        {
            _timerService.PauseTimer();
        }

        if (_inputSystem.GetKeyDownAlpha5())
        {
            Enemy enemy = EnemyCreator();
            _enemyDestroyerService.RegisterEnemy(enemy, () => _enemyDestroyerService.IsDeadCondition(enemy));
        }

        if (_inputSystem.GetKeyDownAlpha6())
        {
            Enemy enemy = EnemyCreator();
            enemy.SetLifeTime(_timerValue);
            _enemyDestroyerService.RegisterEnemy(enemy, () => _enemyDestroyerService.TimerCondition(enemy));
        }

        if (_inputSystem.GetKeyDownAlpha7())
        {
            Enemy enemy = EnemyCreator();
            _enemyDestroyerService.RegisterEnemy(enemy, () => _enemyDestroyerService.CountCondition(_maxCountForDeath));
        }

        _enemyDestroyerService.Update();
    }

    private Enemy EnemyCreator()
    {
        return GameObject.Instantiate(_enemyPrefub, _enemyParent);
    }
}