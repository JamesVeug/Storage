using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class SampleScene : MonoBehaviour
{
    public enum SampleEnum
    {
        Option0 = 0,
        Option1 = 1,
        Option2 = 2,
        Option3 = 3,
        Option4 = 4,
    }
    
    public InputField StringInputField;
    public Toggle BoolToggle;
    public Dropdown EnumDropdown;
    public InputField IntInputField;
    public InputField FloatInputField;
    public InputField DoubleInputField;
    
    // List
    public Text listTextSize;
    public GameObject listTextTemplate;
    public GameObject listContainer;
    
    // Array
    public Text arrayTextSize;
    public GameObject arrayTextTemplate;
    public GameObject arrayContainer;

    private string stringValue;
    private bool boolValue;
    private SampleEnum enumValue;
    private int intValue;
    private float floatValue;
    private double doubleValue;
    private List<string> listOfStrings;
    private string[] arrayOfStrings;

    private void Start()
    {
        string[] options = Enum.GetNames(typeof(SampleEnum));
        EnumDropdown.ClearOptions();
        EnumDropdown.AddOptions(new List<string>(options));
        
        listTextTemplate.SetActive(false);
        arrayTextTemplate.SetActive(false);
    }

    public void Load()
    {
        stringValue = Storage.instance.Load("string_key", "nothing previously saved");
        boolValue = Storage.instance.Load("bool_key", false);
        enumValue = Storage.instance.Load("enum_key", SampleEnum.Option0);
        intValue = Storage.instance.Load("int_key", -1);
        floatValue = Storage.instance.Load("float_key", -1.0f);
        doubleValue = Storage.instance.Load("double_key", -1.0);
        listOfStrings = Storage.instance.LoadList("stringList_key", new List<string>());
        arrayOfStrings = Storage.instance.LoadArray("stringArray_key", new string[0]);

        StringInputField.text = stringValue;
        BoolToggle.isOn = boolValue;
        EnumDropdown.value = (int)enumValue;
        IntInputField.text = intValue.ToString();
        FloatInputField.text = floatValue.ToString();
        DoubleInputField.text = doubleValue.ToString();
        PopulateList();
        PopulateArray();
    }
    
#region List UI
    private void PopulateList()
    {
        for (int i = 1; i < listContainer.transform.childCount; i++)
        {
            Destroy(listContainer.transform.GetChild(i).gameObject);
        }

        listTextSize.text = listOfStrings == null ? "null" : listOfStrings.Count.ToString();
        if (listOfStrings != null)
        {
            for (int i = 0; i < listOfStrings.Count; i++)
            {
                AddItemToList(i);
            }
        }
    }

    public void AddNewItemToList()
    {
        listOfStrings.Add("item" + listOfStrings.Count);
        AddItemToList(listOfStrings.Count - 1);
    }
    
    public void AddItemToList(int index)
    {
        GameObject button = Instantiate(listTextTemplate, listContainer.transform);
        button.transform.GetChild(0).GetComponent<Text>().text = listOfStrings[index] == null ? "null" : listOfStrings[index];
        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            int j = index; 
            RemoveItemFromList(j); 
        });
        button.gameObject.SetActive(true);
        listTextSize.text = listOfStrings == null ? "null" : listOfStrings.Count.ToString();
    }
    
    public void RemoveItemFromList(int index)
    {
        listOfStrings.RemoveAt(index);
        PopulateList();
    }
#endregion
    
#region Array UI
    public void IncreaseArraySize()
    {
        string[] copy = new string[arrayOfStrings.Length + 1];
        for (int i = 0; i < arrayOfStrings.Length; i++)
        {
            copy[i] = arrayOfStrings[i];
        }

        arrayOfStrings = copy;
        PopulateArray();
    }
    
    public void DecreaseArraySize()
    {
        string[] copy = new string[Mathf.Max(0,arrayOfStrings.Length - 1)];
        for (int i = 0; i < copy.Length; i++)
        {
            copy[i] = arrayOfStrings[i];
        }

        arrayOfStrings = copy;
        PopulateArray();
    }

    public void PressedArrayItem(GameObject button)
    {
        int childIndex = button.transform.GetSiblingIndex() - 1;
        arrayOfStrings[childIndex] = arrayOfStrings[childIndex] == null ? "item" + childIndex : null;
        PopulateArray();
    }

    private void PopulateArray()
    {
        for (int i = 1; i < arrayContainer.transform.childCount; i++)
        {
            Destroy(arrayContainer.transform.GetChild(i).gameObject);
        }

        arrayTextSize.text = arrayOfStrings == null ? "null" : arrayOfStrings.Length.ToString();
        if (arrayOfStrings != null)
        {
            for (int i = 0; i < arrayOfStrings.Length; i++)
            {
                GameObject button = Instantiate(arrayTextTemplate, arrayContainer.transform);
                button.transform.GetChild(0).GetComponent<Text>().text = arrayOfStrings[i] == null ? "null" : arrayOfStrings[i];
                button.GetComponent<Button>().onClick.AddListener(delegate
                {
                    PressedArrayItem(button); 
                });
                button.gameObject.SetActive(true);
                listTextSize.text = arrayOfStrings == null ? "null" : arrayOfStrings.Length.ToString();
            }
        }
    }
#endregion

    public void Save()
    {
        Storage.instance.Save("string_key", StringInputField.text);
        Storage.instance.Save("bool_key", BoolToggle.isOn);
        Storage.instance.Save("enum_key", (SampleEnum)EnumDropdown.value);
        Storage.instance.Save("int_key", int.Parse(IntInputField.text));
        Storage.instance.Save("float_key", float.Parse(FloatInputField.text));
        Storage.instance.Save("double_key", double.Parse(DoubleInputField.text));
        Storage.instance.SaveList("stringList_key", listOfStrings);
        Storage.instance.SaveArray("stringArray_key", arrayOfStrings);
    }
    
    public void Clear()
    {
        StringInputField.text = "";
        BoolToggle.isOn = false;
        EnumDropdown.value = 0;
        IntInputField.text = "";
        FloatInputField.text = "";
        DoubleInputField.text = "";
        listOfStrings = null;
        PopulateList();
        arrayOfStrings = null;
        PopulateArray();
    }
    
    public void DeletePlayerPrefs()
    {
        Storage.instance.DeleteAll();
    }
}
