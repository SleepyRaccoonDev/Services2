using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int MinRangeValue = 0;
    private const int MaxRangeValue = 51;

    [Header("Wallet")]

    [SerializeField] private GameObject _ui;
    [SerializeField] private TextMeshProUGUI _textMeshDiamonds;
    [SerializeField] private TextMeshProUGUI _textMeshEnergy;
    [SerializeField] private TextMeshProUGUI _textMeshMoney;

    [SerializeField] private WalletCurrencyViewer _walletCurrencyViewerDiamonds;
    [SerializeField] private WalletCurrencyViewer _walletCurrencyViewerEnergy;
    [SerializeField] private WalletCurrencyViewer _walletCurrencyViewerMoney;

    private WalletService _walletService;

    private TimerService _timerService;

    [Header("Timer")]

    [SerializeField] private TextMeshProUGUI _textMeshTimer;
    [SerializeField] private Slider _sliderTimer;
    [SerializeField] private float _timerValue;

    [SerializeField] private TimerViewer _timerViewer;
    [SerializeField] private TimerSliderViewer _timerSliderViewer;
    [SerializeField] private TimerHealthViewer _timerHealthViewer;

    [SerializeField] private Transform _healthParent;
    [SerializeField] private Transform _healthIconPrefab;

    private InputSystem _inputSystem;

    private EnemyDestroyerService _enemyDestroyerService;

    [Header("Enemy")]

    [SerializeField] private Transform _enemyParent;
    [SerializeField] private EnemyExample _enemyPrefub;
    [SerializeField] private int _maxCountForDeath;

    private List<IDisposable> _disposables = new();

    private void Awake()
    {
        _inputSystem = new InputSystem();

        _walletService = new WalletService();

        _ui.SetActive(true);

        _walletCurrencyViewerDiamonds.Initialize(_walletService, _textMeshDiamonds, CurrencyType.Diamonds);
        _walletCurrencyViewerEnergy.Initialize(_walletService, _textMeshEnergy, CurrencyType.Energy);
        _walletCurrencyViewerMoney.Initialize(_walletService, _textMeshMoney, CurrencyType.Money);

        _timerService = new TimerService(this);

        _timerViewer.Initialize(_timerService, _textMeshTimer);
        _timerSliderViewer.Initialize(_timerService, _sliderTimer);
        _timerHealthViewer.Initialize(_timerService, _healthParent, _healthIconPrefab);

        _enemyDestroyerService = new EnemyDestroyerService();
    }

    private void OnDestroy()
    {
        foreach (IDisposable disposable in _disposables)
            disposable.Dispose();
    }

    private void Update()
    {
        if (_inputSystem.GetKeyDownAlphaQ())
        {
            int value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TrySpend(CurrencyType.Energy, value);

            value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TrySpend(CurrencyType.Diamonds, value);

            value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.TrySpend(CurrencyType.Money, value);
        }

        if (_inputSystem.GetKeyDownAlphaW())
        {
            int value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.Add(CurrencyType.Energy, value);

            value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.Add(CurrencyType.Diamonds, value);

            value = UnityEngine.Random.Range(MinRangeValue, MaxRangeValue);
            _walletService.Add(CurrencyType.Money, value);
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
            _timerService.TogglePause();
        }

        if (_inputSystem.GetKeyDownAlpha5())
        {
            EnemyExample enemy = EnemyCreator();
            _enemyDestroyerService.RegisterEnemy(enemy, () => enemy.IsDead);
        }

        if (_inputSystem.GetKeyDownAlpha6())
        {
            EnemyExample enemy = EnemyCreator();

            var timer = new TimerService(this);
            timer.Start(10);

            _enemyDestroyerService.RegisterEnemy(enemy, () => timer.IsUp);
        }

        if (_inputSystem.GetKeyDownAlpha7())
        {
            EnemyExample enemy = EnemyCreator();

            var timer = new TimerService(this);
            timer.Start(5);

            _enemyDestroyerService.RegisterEnemy(enemy, () => timer.IsUp && _enemyDestroyerService.EnemyCount <= 10);
        }

        _enemyDestroyerService.Update();
    }

    private EnemyExample EnemyCreator()
    {
        return GameObject.Instantiate(_enemyPrefub, _enemyParent);
    }
}