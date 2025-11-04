using UnityEngine;

[CreateAssetMenu(fileName = "StatsPreset", menuName = "StatsPreset", order = 1)]
public class PlayerStatsPreset : ScriptableObject
{
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5.0f;
    public float rotationSpeed = 15f;
}
