using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Attached to a card object, handling the display of sprites and values
/// </summary>
public class Card : MonoBehaviour
{
    public bool showBack = false;
    public string title = "Default", description = "Summary";

    private Dictionary<string, Sprite> sprites;             // Holds sprites for card front, card back, gem front, gem back, icon and unit image
    [SerializeField] private string cardFrame, gemFront, gemBack, icon;      // Holds the address of resources

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Load sprites into a dictionary
        sprites = new Dictionary<string, Sprite>();
        sprites.Add("CardFront", Resources.Load<Sprite>("CardResources/cardsFront/card_front_" + cardFrame));
        sprites.Add("CardBack", Resources.Load<Sprite>("CardResources/cardsBack/card_back_" + cardFrame));
        sprites.Add("OvalFrame", Resources.Load<Sprite>("CardResources/ovalFrames/oval_frame_1_" + cardFrame));
        sprites.Add("GemFront", Resources.Load<Sprite>("CardResources/gems/ovalShaped/" + gemFront));
        sprites.Add("GemBack", Resources.Load<Sprite>("CardResources/gems/rarity/" + gemBack));
        sprites.Add("Icon", Resources.Load<Sprite>("CardResources/icons/" + icon));
        sprites.Add("UnitImage", Resources.Load<Sprite>("Isle of Lore 2 Strategy Figures/Assets/Figures/180x180/Framed Standard/_" + title));
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites["CardFront"];    // Set the card frame
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            // for children in card:
            GameObject face = gameObject.transform.GetChild(i).gameObject;
            if (face.name == "Front") {
                // card front
                for (int j = 0; j < face.transform.childCount; j++) {
                    GameObject child = face.transform.GetChild(j).gameObject;
                    Debug.LogWarning(child.name);
                    if (child.name == "Title")
                        child.transform.GetChild(0).GetComponent<TextMeshPro>().text = title;   // Set the description
                    else if (child.name == "Unit_Image")
                        child.GetComponent<SpriteRenderer>().sprite = sprites["UnitImage"];     // Set the unit image
                    else if (child.name == "Summary")
                        child.transform.GetChild(0).GetComponent<TextMeshPro>().text = description;
                    else if (child.name == "Oval_Frame") {
                        child.GetComponent<SpriteRenderer>().sprite = sprites["OvalFrame"];     // Set the oval frame
                        GameObject buff = child.transform.GetChild(0).gameObject;               // Store the object that holds the coin and gem at the front
                        buff.GetComponent<SpriteRenderer>().sprite = sprites["GemFront"];       // Set gem front
                        buff.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites["Icon"];     // Set the icon
                    }
                }
                if (showBack) face.SetActive(false);
            } else {
                // card back
                face.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites["GemBack"];  // Set the gem at the back
                if (showBack) face.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
