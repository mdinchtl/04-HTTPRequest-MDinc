using UnityEngine;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using TMPro;


 

 

 

public class httpreq : MonoBehaviour
{

 

    //string newURLH = "http://192.168.10.221/H";
    //string newURLL = "http://192.168.10.221/L";
    string newURLH = "https://www.htl-salzburg.ac.at/startseite.html";
    string newURLL = "https://www.htl-salzburg.ac.at/startseite.html";

 

 

    // See https://aka.ms/new-console-template for more information
    [SerializeField]
    string responseString = string.Empty;

 

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

 

    // Update is called once per frame
    void Update()
    {

 

            Console.WriteLine("Hello, World Framework!");

 

            doSomething();

 

 

    }

 

 

    void doSomething()
    {
        Task.Run(async () =>
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the specified URL
                    HttpResponseMessage response = await client.GetAsync(newURLH);
                    // Ensure the request was successful
                    response.EnsureSuccessStatusCode();
                    // Read the response content as a string
                    responseString = await response.Content.ReadAsStringAsync();
                    // Log the response string to the Unity console
                    Debug.Log("Response: " + responseString);
                    Console.WriteLine("Response: " + responseString);

 

 

                }
                catch (HttpRequestException e)
                {
                    Debug.LogError("Request error: " + e.Message);
                    Console.WriteLine("Request error: " + e.Message);
                }
            }
        }).GetAwaiter().GetResult();
    }
}