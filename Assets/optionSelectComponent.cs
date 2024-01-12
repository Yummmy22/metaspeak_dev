using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class optionSelectComponent : MonoBehaviour
{

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectButton(int index)
    {
        UIConversationButton[] childs = panel.GetComponentsInChildren<UIConversationButton>();
        UIConversationButton targetButton = childs[index];
        targetButton.OnHover(true);
        targetButton.OnClick();

    }
}
