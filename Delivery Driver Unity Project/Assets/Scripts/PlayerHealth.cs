using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 10000;

    [SerializeField]
    GameObject healthPanel;

    [SerializeField]
    Image healthBar;

    [SerializeField]
    float UIHideoutDuration = 3f;

    [SerializeField]
    GameObject playerObj;

    [SerializeField]
    Vector3 uiOffset = new Vector3(0,10,0);

    int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Die();
            }
            UpdateHealthUI();
        }
    }

    [SerializeField]
    bool isHit = false;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (playerObj == null) return;
        GetComponent<RectTransform>().position = playerObj.transform.position + uiOffset;
    }
    private void Die()
    {
        if (playerObj == null) return;
        Destroy(playerObj);
        //Death logic
    }

    private void UpdateHealthUI()
    {
        StopAllCoroutines();
        StartCoroutine(HideHealthUI());
    }

    IEnumerator HideHealthUI()
    {
        healthPanel.SetActive(true);
        Debug.Log((float)CurrentHealth / maxHealth);
        healthBar.fillAmount = (float)CurrentHealth / maxHealth;

        Image[] childImages = healthPanel.GetComponentsInChildren<Image>();
        float remainingHideoutDuration = UIHideoutDuration;

        //Begin fadeout
        while (remainingHideoutDuration > 0)
        {
            yield return new WaitForEndOfFrame();
            remainingHideoutDuration -= Time.deltaTime;
            foreach (Image childImage in childImages)
            {
                //Super expensive on frame rate, but should work
                childImage.color = new Color(
                    childImage.color.r,
                    childImage.color.g,
                    childImage.color.b,
                    remainingHideoutDuration / UIHideoutDuration
                    );
            }
        }

        //Finalize fadeout
        foreach(Image childImage in childImages)
        {
            childImage.color = new Color(
                childImage.color.r,
                childImage.color.g,
                childImage.color.b,
                1
                );
        }
        healthPanel.SetActive(false);
    }
}
