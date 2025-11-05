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
        StartCoroutine(ChangeScale(playerBaseStats.Scale));
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
        if (currentEffectCoroutine != null)
        {
            StopAllCoroutines();
            ResetEffects();
        }
        thunderParticles.Play();
        StartCoroutine(ChangeScale(playerThunderStats.Scale));
        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }

    public void InvisibleEffect()
    {
        SetMaterial(invisibleMaterial);
        ChangeSpeed(playerBaseStats);
        if (currentEffectCoroutine != null)
        {
            StopAllCoroutines();
            ResetEffects();
        }
        invisibleParticles.Play();
        StartCoroutine(ChangeScale(playerBaseStats.Scale));
        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }

    public void RockEffect()
    {
        SetMaterial(rockMaterial);
        ChangeSpeed(playerRockStats);
        if (currentEffectCoroutine != null)
        {
            StopAllCoroutines();
            ResetEffects();
        }
        rockParticles.Play();
        StartCoroutine(ChangeScale(playerRockStats.Scale));
        currentEffectCoroutine = StartCoroutine(ReturnToBaseState());
    }

    IEnumerator ChangeScale(Vector3 scale)
    {
        float elapsedTime = 0f;
        float duration = 0.7f;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startingScale, scale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = scale;
    }
}