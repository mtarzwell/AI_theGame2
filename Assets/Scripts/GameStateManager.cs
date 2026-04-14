using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private Dictionary<string, bool> _flags = new Dictionary<string, bool>();
    private Dictionary<string, int> _stats = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetFlag(string key, bool value)
    {
        _flags[key] = value;
        Debug.Log($"Flag Set: {key} = {value}");
    }

    public bool GetFlag(string key)
    {
        return _flags.ContainsKey(key) && _flags[key];
    }

    public void ChangeStat(string key, int delta)
    {
        if (!_stats.ContainsKey(key)) _stats[key] = 0;
        _stats[key] += delta;
        Debug.Log($"Stat Changed: {key} = {_stats[key]} (Delta: {delta})");
    }

    public int GetStat(string key)
    {
        return _stats.ContainsKey(key) ? _stats[key] : 0;
    }
}
