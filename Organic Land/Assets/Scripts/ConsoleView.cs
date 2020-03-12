using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleView : MonoBehaviour {

    public InputField inputField;
    public Text outputText;

    private CommandManager commandManager = new CommandManager();

    private void Start()
    {
        commandManager.consoleView = this;
        outputText.text = "";
    }

    private void OnEnable()
    {
        inputField.ActivateInputField();
    }

    public void SendCommand()
    {
        if(inputField.text != string.Empty)
        {
            commandManager.RunCommand(inputField.text);
            inputField.text = string.Empty;
        }
        inputField.ActivateInputField();
    }

    public void AppendLogLine(string text)
    {
        if (outputText.text != string.Empty)
            outputText.text += string.Format("\n{0}", text);
        else
            outputText.text = text;
    }

}
