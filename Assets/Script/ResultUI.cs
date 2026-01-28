using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;

    void Start()
    {
        rankText.text = ResultData.rank;
    }
}
