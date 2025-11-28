using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class LehrerListeLoader : MonoBehaviour
{
    public string lehrerUebersichtUrl = "https://www.htl-salzburg.ac.at/lehrerinnen.html";

    void Start()
    {
        StartCoroutine(LadeLehrerListe());
        Debug.Log("LehrerListeLoader gestartet.");
    }

    IEnumerator LadeLehrerListe()
    {
        UnityWebRequest www = UnityWebRequest.Get(lehrerUebersichtUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Fehler beim Laden der Lehrerübersicht: " + www.error);
            yield break;
        }

        string html = www.downloadHandler.text;

        // 1. Alle "element"-Divs extrahieren
        MatchCollection divMatches = Regex.Matches(
            html,
            @"<div class=""element.*?"">(.*?)</div>\s*</div>", // matcht einen Lehrerblock
            RegexOptions.Singleline
        );

        int count = Mathf.Min(5, divMatches.Count);

        for (int i = 0; i < count; i++)
        {
            string block = divMatches[i].Groups[1].Value;

            // 2. Link extrahieren
            Match linkMatch = Regex.Match(block, @"<a href=""(.*?)"".*?>(.*?)</a>", RegexOptions.Singleline);
            if (!linkMatch.Success) continue;

            string link = linkMatch.Groups[1].Value;
            string nameRaw = linkMatch.Groups[2].Value;

            // 3. HTML-Tags entfernen
            string name = Regex.Replace(nameRaw, "<.*?>", "").Trim();

            Debug.Log($"Lehrer {i + 1}: {name} | Link: {link}");
        }
    }
}
