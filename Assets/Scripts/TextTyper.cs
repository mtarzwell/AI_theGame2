using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class TextTyper : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public bool isTyping { get; private set; }

    private Coroutine _typeCoroutine;
    private string _fullText;
    private Label _targetLabel;

    public void TypeText(string text, Label label, System.Action onComplete = null)
    {
        if (_typeCoroutine != null) StopCoroutine(_typeCoroutine);
        _fullText = text;
        _targetLabel = label;
        _typeCoroutine = StartCoroutine(DoTyping(onComplete));
    }

    public void Skip()
    {
        if (isTyping)
        {
            StopCoroutine(_typeCoroutine);
            _targetLabel.text = _fullText;
            isTyping = false;
        }
    }

    private IEnumerator DoTyping(System.Action onComplete)
    {
        isTyping = true;
        _targetLabel.text = "";
        
        foreach (char c in _fullText)
        {
            _targetLabel.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        onComplete?.Invoke();
    }
}
