using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class UnderlineText : MonoBehaviour
{
	void Start()
	{
		var transform = base.transform as RectTransform;
		// make a stripped down copy of the Text
		var go = GameObject.Instantiate(transform, transform.position, transform.rotation) as Transform;
		foreach (var component in go.GetComponents<MonoBehaviour>())
		{
			if (component is Text)
				continue;
			else
				GameObject.Destroy(component);
		}
		go.SetParent(transform.parent);
		// make sure transform has original values
		go.localScale = transform.localScale;
		var TextComponent = go.GetComponent<Text>();
		TextComponent.rectTransform.sizeDelta = transform.sizeDelta;
		// use the copy to create an underline
		TextComponent.text = new System.Text.RegularExpressions.Regex(".").Replace(TextComponent.text, "_");
	}
}