using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public static ScoreController instance;

    public static void updatePlayerScore(string name, int score){
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://games.tatpiqat.co/api/v1/score");
        httpWebRequest.ContentType = "text/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{" +
                "'name': "+name +"," +
                "'name': "+score +"," +
                "}";            
            streamWriter.Write(json);
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            //Now you have your response.
            //or false depending on information in the response
            Debug.Log(responseText);            
        }   
    }

}
