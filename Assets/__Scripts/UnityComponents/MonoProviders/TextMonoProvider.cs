using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMonoProvider : MonoProvider<TextProvider>
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        if (Value.Value == null)
            Value = new TextProvider { Value = text };
    }
}
