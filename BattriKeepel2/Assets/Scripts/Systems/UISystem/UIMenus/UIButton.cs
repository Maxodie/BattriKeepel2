using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text title;
    public string Title {set { title.text = value; } get {return title.text;}}
    public Button Button;
}
