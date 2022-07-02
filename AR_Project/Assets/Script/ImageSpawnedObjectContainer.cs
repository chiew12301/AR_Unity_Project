using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImageSpawnedObjectContainer : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_nameText = null;
    [SerializeField] private TextMeshPro m_descriptionText = null;

    [SerializeField] private float m_speed = 0.5f;

    public void Init(string name, string des)
    {
        this.m_nameText.text = "Name: " + name;
        this.m_descriptionText.text = "Description: " + des;
    }

    private void OnEnable()
    {
        this.TextByTextAnimation();
    }

    private void OnDisable()
    {
        this.ForceTextByTextAnimationStop();
    }

    private void TextByTextAnimation()
    {
        this.StartCoroutine(this.TextVisibleCO(this.m_nameText));
        this.StartCoroutine(this.TextVisibleCO(this.m_descriptionText));
    }

    private void ForceTextByTextAnimationStop()
    {
        this.StopCoroutine(this.TextVisibleCO(this.m_nameText));
        this.StopCoroutine(this.TextVisibleCO(this.m_descriptionText));
    }

    private IEnumerator TextVisibleCO(TextMeshPro textToDo)
    {
        int totalVisibleCharacters = textToDo.textInfo.characterCount;
        int counter = 0;

        while (counter < totalVisibleCharacters)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textToDo.maxVisibleCharacters = visibleCount;

            counter += 1;
            yield return new WaitForSeconds(this.m_speed);
        }
    }
}
