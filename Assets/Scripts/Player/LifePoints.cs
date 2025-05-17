using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour
{
    public int lifePoints = 3;
    public GameObject[] lifePointIcons;
    public ParticleSystem hitPulse; // Particle effect for hit
    public GameObject holyHandGrenade;

    private bool isHolyHandGrenadeActive = false;
    public float holyHandGrenadeTime = 0f;
    private float holyHandGrenadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        hitPulse.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (holyHandGrenadeTime + holyHandGrenadeDuration > Time.time)
        {
            holyHandGrenade.transform.localScale = new Vector3(
                20 * (Time.time - holyHandGrenadeTime),
                1,
                20 * (Time.time - holyHandGrenadeTime)
            );
        }
        else
        {
            holyHandGrenade.SetActive(false);
            isHolyHandGrenadeActive = false;
        }
    }

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;
        hitPulse.Play();
        UpdateLifePointIcons();

        holyHandGrenade.SetActive(true);
        holyHandGrenade.transform.localPosition = Vector3.zero;
        holyHandGrenade.transform.localRotation = Quaternion.identity;
        holyHandGrenadeTime = Time.time;
        isHolyHandGrenadeActive = true;
    }

    public void Heal(int healAmount)
    {
        lifePoints += healAmount;
        UpdateLifePointIcons();
    }

    private void UpdateLifePointIcons()
    {
        for (int i = 0; i < lifePointIcons.Length; i++)
        {
            if (i < lifePoints)
            {
                lifePointIcons[i].SetActive(true);
            }
            else
            {
                lifePointIcons[i].SetActive(false);
            }
        }
    }
}
