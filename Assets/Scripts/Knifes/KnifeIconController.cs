using System.Collections.Generic;
using UnityEngine;

public class KnifeIconController : MonoBehaviour
{
    [SerializeField] private GameObject _knifeIconPrefab;
    private Settings _settings;

    // Use for show knifes.
    private List<GameObject> _knifeIcons;
    // Use for deleteobjects.
    private List<GameObject> _knifeObjects;
    private void Start()
    {
        _knifeIcons = new List<GameObject>();
        _knifeObjects = new List<GameObject>();

        Init();
        KnifeThrower.Shoot += Shoot;
        WoodManager.UpdateCanvas += ResetIcons;
    }
    public void ResetIcons()
    {
        foreach (var knifeIcon in _knifeObjects)
            Destroy(knifeIcon);

        _knifeObjects.Clear();
        _knifeIcons.Clear();

        Init();
    }
    private void Init()
    {
        _settings = LevelController.Instance.GetLevel(GameValues.Instance.Level);
        for (var i = 0; i < _settings.GetFirmness; ++i)
        {
            var knifeIcon = Instantiate(_knifeIconPrefab, this.transform, false);
            _knifeIcons.Add(knifeIcon);
            _knifeObjects.Add(knifeIcon);
        }
        _knifeIcons.Reverse();
    }

    private void Shoot()
    {
        if (_knifeIcons.Count == 0)
            return;

        _knifeIcons[0].GetComponent<KnifeIcon>().ChangeIconKnife();
        _knifeIcons.RemoveAt(0);
    }
}
