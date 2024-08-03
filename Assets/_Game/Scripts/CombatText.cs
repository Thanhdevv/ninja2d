using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] Text Hptext;
    public void OnInit(float damage)
    {
        Hptext.text = damage.ToString();
        Invoke(nameof(OnDespam), 1f);
    }

    public void OnDespam()
    {
        Destroy(gameObject);
    }
}
