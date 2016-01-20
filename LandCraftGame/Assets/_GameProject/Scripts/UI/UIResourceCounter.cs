using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIResourceCounter : MonoBehaviour {
	public Text resourceAmountTxt;
	public Animator myAnimtor;

	public bool Visible { get; set; }
	public int TotalResources { get; set; }

	void Awake() {
		Visible = false;
	}

	public void AddResource(int amount) {
		TotalResources += amount;
		UpdateResourceAmountTxt();
	}

	public void RemoveResource(int amount) {
		TotalResources -= amount;		
		UpdateResourceAmountTxt();	
	}

	public void FadeIn() {
		if(!Visible) {
			Visible = true;
			myAnimtor.Play("UIResourceFadeIn");
		}
	}

	public void FadeOut() {
		if(Visible) {
			Visible = false;
			myAnimtor.Play("UIResourceFadeOut");
		}
	}

	private void UpdateResourceAmountTxt() {
		resourceAmountTxt.text = TotalResources.ToString();
	}
}
