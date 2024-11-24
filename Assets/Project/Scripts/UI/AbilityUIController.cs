using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIController : MonoBehaviour
{
    private int autoattackLvl, ability1Lvl, ability2Lvl, ultimateLvl;
    private IMagicBase magic;
    [SerializeField]private GameObject autoattackUI, ability1UI, ability2UI, ultimateUI;
    [SerializeField] List<string> magics = new();
    [SerializeField] List<MagicIcons> _icons = new();
    Dictionary<string, MagicIcons> MagicsIcons = new Dictionary<string, MagicIcons>();
    private MagicIcons icons;

    private void Start()
    {
        for (int i = 0; i < magics.Count; i++)
        {
            MagicsIcons.Add(magics[i], _icons[i]);
        }

        magic = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();

        MagicsIcons.TryGetValue(magic.Name(), out icons);
    }

    private void Update()
    {
        GetMagicLevels();

        autoattackUI.transform.Find("LVL").GetComponent<TextMeshProUGUI>().text = autoattackLvl.ToString();
        ability1UI.transform.Find("LVL").GetComponent<TextMeshProUGUI>().text = ability1Lvl.ToString();
        ability2UI.transform.Find("LVL").GetComponent<TextMeshProUGUI>().text = ability2Lvl.ToString();
        ultimateUI.transform.Find("LVL").GetComponent<TextMeshProUGUI>().text = ultimateLvl.ToString();

        GameObject ability1CD = ability1UI.transform.Find("CD").gameObject;
        GameObject ability2CD = ability2UI.transform.Find("CD").gameObject;
        GameObject ultimateCD = ultimateUI.transform.Find("CD").gameObject;

        //ability1CD.SetActive(false);
        //ability2CD.SetActive(false);
        //ultimateCD.SetActive(false);

        if (ability1Lvl > 0) ability1UI.GetComponent<Image>().sprite = icons.Ability1[0];
        if (ability2Lvl > 0) ability2UI.GetComponent<Image>().sprite = icons.Ability2[0];
        if (ultimateLvl > 0) ultimateUI.GetComponent<Image>().sprite = icons.Ultimate[0];

        float[] currentCDs = magic.GetCDs();
        float[] maxCDs = magic.GetMaxCDs();

        Debug.Log($"Abilty1 CD:{currentCDs[0]/maxCDs[0]}");

        if (currentCDs[0] > 0) ability1CD.SetActive(true);
        else ability1CD.SetActive(false);
        if (currentCDs[1] > 0) ability2CD.SetActive(true);
        else ability2CD.SetActive(false);
        if (currentCDs[2] > 0) ultimateCD.SetActive(true);
        else ultimateCD.SetActive(false);

        ability1CD.GetComponent<Image>().fillAmount = currentCDs[0] / maxCDs[0];
        ability1CD.transform.Find("CD_Time").GetComponent<TextMeshProUGUI>().text = Math.Round(currentCDs[0],2).ToString();

        ability2CD.GetComponent<Image>().fillAmount = currentCDs[1] / maxCDs[1];
        ability2CD.transform.Find("CD_Time").GetComponent<TextMeshProUGUI>().text = Math.Round(currentCDs[1], 2).ToString();

        ultimateCD.GetComponent<Image>().fillAmount = currentCDs[2] / maxCDs[2];
        ultimateCD.transform.Find("CD_Time").GetComponent<TextMeshProUGUI>().text = Math.Round(currentCDs[2], 2).ToString();

    }

    private void GetMagicLevels()
    {
        int[] levels = magic.GetLevels();

        autoattackLvl = levels[0];
        ability1Lvl = levels[1];
        ability2Lvl = levels[2];
        ultimateLvl = levels[3];
    }


}
