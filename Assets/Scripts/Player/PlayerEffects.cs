using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Player Renderer")]
    public Renderer _personajeRenderer;

    [Header("Materials")]
    public Material baseMaterial;
    public Material thunderMaterial;
    public Material invisibleMaterial;
    public Material rockMaterial;

    private PlayerMovement playerMovement;

    [Header("Player Movement")]
    [SerializeField] private PlayerStatsPreset playerBaseStats;
    [SerializeField] private PlayerStatsPreset playerThunderStats;
    [SerializeField] private PlayerStatsPreset playerRockStats;

    [Header("Effect Duration")]
    private Coroutine currentEffectCoroutine;
    public float effectDuration = 10f;

    [Header("Plarticles")]
    [SerializeField] private ParticleSystem thunderParticles;
    [SerializeField] private ParticleSystem invisibleParticles;
    [SerializeField] private ParticleSystem rockParticles;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start()
    {
        BaseState();
    }
    public void SetMaterial(Material targetMaterial)
    {
        if (_personajeRenderer != null && targetMaterial != null)
        {
            _personajeRenderer.material = targetMaterial;
        }
    }

    IEnumerator ReturnToBaseState()
    {
        yield return new WaitForSeconds(effectDuration);
        BaseState();
        currentEffectCoroutine = null;
    }

    public void ChangeSpeed(PlayerStatsPreset playerStats)
    {
        playerMovement.walkingSpeed = playerStats.walkingSpeed;
        playerMovement.runningSpeed = playerStats.runningSpeed;
        playerMovement.rotationSpeed = playerStats.rotationSpeed;
    }

    public void BaseState()
    {
        SetMaterial(baseMaterial);
        ChangeSpeed(playerBaseStats);
        ResetEffects();
    }

    public void ResetEffects()
    {
        thunderParticles.Stop();
        invisibleParticles.Stop();
        rockParticles.Stop();
    }

    public void ThunderEffect()
    {
        SetMaterial(thunderMaterial);
        ChangeSpeed(playerThunderStats);
        thunderParticles.Play();
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
        }

        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }

    public void InvisibleEffect()
    {
        SetMaterial(invisibleMaterial);
        ChangeSpeed(playerBaseStats);
        invisibleParticles.Play();
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
        }
        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }

    public void RockEffect()
    {
        SetMaterial(rockMaterial);
        ChangeSpeed(playerRockStats);
        rockParticles.Play();
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
        }
        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }
}