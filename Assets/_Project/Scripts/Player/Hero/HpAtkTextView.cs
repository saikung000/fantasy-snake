using TMPro;
using UnityEngine;

public class HpAtkTextView : MonoBehaviour
{
    public TextMeshProUGUI HpAtkText;

    private int hp;
    private int atk;
    public void UpdateHpText(int hp)
    {
        this.hp = hp;
        HpAtkText.text = $"<color=green>HP:{hp}<br><br><color=red>ATK:{atk}";
    }

    public void UpdateAtkText(int atk)
    {
        this.atk = atk;
        HpAtkText.text = $"<color=green>HP:{hp}<br><br><color=red>ATK:{atk}";
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}