using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SBKManager : MonoBehaviour
{
    public int sbkNote_Count;
    public TMP_Text sbkNote_Text;

    public Image sbkNote_Help_Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sbkNote_Text.text = sbkNote_Count.ToString();
    }
}
