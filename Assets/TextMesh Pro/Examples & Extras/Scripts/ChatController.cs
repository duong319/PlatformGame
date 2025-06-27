using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{
    public TMP_InputField ChatInputField;
    public TMP_Text ChatDisplayOutput;
    public Scrollbar ChatScrollbar;

    void OnEnable()
    {
        // Thêm listener cho end edit (khi user ấn Enter hoặc rời focus)
        ChatInputField.onEndEdit.AddListener(HandleChatInput);
    }

    void OnDisable()
    {
        ChatInputField.onEndEdit.RemoveListener(HandleChatInput);
    }

    void HandleChatInput(string input)
    {
        // Kiểm tra nếu ấn Enter (trên nền desktop không có mobile keyboard)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                AddToChatOutput(input);
            }

            // Clear input sau khi gửi
            ChatInputField.text = string.Empty;

            // Reactivate input field
            ChatInputField.ActivateInputField();

            // Đặt scrollbar xuống cuối cùng
            ChatScrollbar.value = 0;
        }
    }

    void AddToChatOutput(string newText)
    {
        string timeStamp = System.DateTime.Now.ToString("[<#FFFF80>HH:mm:ss</color>] ");

        if (ChatDisplayOutput != null)
        {
            if (string.IsNullOrEmpty(ChatDisplayOutput.text))
                ChatDisplayOutput.text = timeStamp + newText;
            else
                ChatDisplayOutput.text += "\n" + timeStamp + newText;
        }
    }
}