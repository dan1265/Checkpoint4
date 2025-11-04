using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Effects : MonoBehaviour
{
    public PlayerEffects playerEffects;

    public enum EffectType
    {
        Thunder,
        Invisible,
        Rock,
    }
    public EffectType effectType;
    void Start()
    {
        playerEffects = FindFirstObjectByType<PlayerEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayEffect();
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
