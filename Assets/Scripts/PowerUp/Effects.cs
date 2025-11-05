using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Effects : MonoBehaviour
{
    public PlayerEffects playerEffects;
    public SpawnEffect spawnEffect;

    public float rotationForce = 5f;
    public float frequency = 1f;
    public Vector3 rotationDirection = Vector3.forward;

    private Rigidbody rb;
    public enum EffectType
    {
        Thunder,
        Invisible,
        Rock,
    }
    public EffectType effectType;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnEffect = transform.parent.GetComponent<SpawnEffect>();
        playerEffects = FindFirstObjectByType<PlayerEffects>();
    }

    void FixedUpdate()
    {
        float applyTorque = Mathf.Sin(Time.time * frequency) * rotationForce;

        Vector3 torque = rotationDirection * applyTorque;

        rb.AddTorque(torque, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayEffect();
            spawnEffect.Spawn();
            Destroy(gameObject);
        }
    }
    public void PlayEffect()
    {
        switch(effectType)
        {
            case EffectType.Thunder:
                playerEffects.ThunderEffect();
                break;
            case EffectType.Invisible:
                playerEffects.InvisibleEffect();
                break;
            case EffectType.Rock:
                playerEffects.RockEffect();
                break;
        }

    }
}
