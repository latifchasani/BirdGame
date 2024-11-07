using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Text nameText;
    public SpriteRenderer charSprite;
    public int coin;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        } else
        {
            loadChar();
        }
        updateCharacter(selectedOption);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        updateCharacter(selectedOption);
        saveChar();
    }

    public void backOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        updateCharacter(selectedOption);
        saveChar();
    }

    public void updateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        charSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;

    }

    private void loadChar()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void saveChar()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        Controller.core.setButtonSelect(selectedOption);
    }

    public int returnSprite()
    {
        // return charSprite.sprite;
        return PlayerPrefs.GetInt("selectedOption");
    }

    
}
