using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using LitJson;
using Newtonsoft.Json;
using Siccity.GLTFUtility;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class JsonFile : MonoBehaviour
{
    private string json;
    private JsonData jsonData;
    public GameObject cube1;

    //private Transform sample;
    //private Vector3[] sample1 = new Vector3[3];
    public void Start()
    {
     
       var filePath = "Assets/Models/Bath.glb";
        GameObject model = Importer.LoadFromFile(filePath);
         json = File.ReadAllText("Assets/JsonFile.json");
         jsonData = JsonMapper.ToObject(json);

         var x1= jsonData["data"]["plan"]["walls"][0]["from"]["x"];
         var y1 =jsonData["data"]["plan"]["walls"][0]["from"]["y"];
         var z1 =jsonData["data"]["plan"]["walls"][0]["from"]["z"];
         var height1 =jsonData["data"]["plan"]["walls"][0]["height"];
     
         //Debug.Log(x +" "+ y +" "+" " + z);
         
         var x2 =jsonData["data"]["plan"]["walls"][0]["to"]["x"];
         var y2 =jsonData["data"]["plan"]["walls"][0]["to"]["y"];
         var z2 =jsonData["data"]["plan"]["walls"][0]["to"]["z"];
         
         //Debug.Log(x2 +" "+ y2 +" "+" " + z2);
         float cube1SizeX =  (float)x2 - (float)x1 ;
         
         float cube1SizeZ =  (float)z2 - (float)z1 ;

         var c1 = Instantiate(cube1, new Vector3((float)x1,(float)y1,(float) z1), Quaternion.identity);
         c1.transform.localScale = new Vector3(cube1SizeX,(float)height1,cube1SizeZ);
         
        // Cube2
         var x3= jsonData["data"]["plan"]["walls"][1]["from"]["x"];
         var y3 =jsonData["data"]["plan"]["walls"][1]["from"]["y"];
         var z3 =jsonData["data"]["plan"]["walls"][1]["from"]["z"];
         var height3 =jsonData["data"]["plan"]["walls"][1]["height"];
     
         //Debug.Log(x +" "+ y +" "+" " + z);
         
         var x4 =jsonData["data"]["plan"]["walls"][1]["to"]["x"];
         var y4 =jsonData["data"]["plan"]["walls"][1]["to"]["y"];
         var z4 =jsonData["data"]["plan"]["walls"][1]["to"]["z"];
         
          
          float cube2SizeX =  (float)x4 - (float)x3 ;
         
          float cube2SizeZ =  (float)z4 - (float)z3 ;
          
          var c2 = Instantiate(cube1, new Vector3(0,0,0), Quaternion.identity);
          
          c2.transform.localScale = new Vector3(cube2SizeX,(float)height3,cube2SizeZ);
          
          //cube 3
           var x5= jsonData["data"]["plan"]["walls"][2]["from"]["x"];
           var y5 =jsonData["data"]["plan"]["walls"][2]["from"]["y"];
           var z5 =jsonData["data"]["plan"]["walls"][2]["from"]["z"];
           var height5 =jsonData["data"]["plan"]["walls"][2]["height"];
          
           //Debug.Log(x +" "+ y +" "+" " + z);
          
           var x6 =jsonData["data"]["plan"]["walls"][2]["to"]["x"];
           var y6 =jsonData["data"]["plan"]["walls"][2]["to"]["y"];
           var z6 =jsonData["data"]["plan"]["walls"][2]["to"]["z"];
          
          
           float cube3SizeX =  (float)x6 - (float)x5 ;
          
           float cube3SizeZ =  (float)z6 - (float)z5;
          
           var c3 = Instantiate(cube1, new Vector3((float)x6,(float)y6,(float) z6), Quaternion.identity);
          
           c3.transform.localScale = new Vector3(cube3SizeX,(float)height5,cube3SizeZ);
          
          //cube 4
          
          var x7= jsonData["data"]["plan"]["walls"][3]["from"]["x"];
          var y7 =jsonData["data"]["plan"]["walls"][3]["from"]["y"];
          var z7 =jsonData["data"]["plan"]["walls"][3]["from"]["z"];
          var height7 =jsonData["data"]["plan"]["walls"][3]["height"];
          
          //Debug.Log(x +" "+ y +" "+" " + z);
          
          var x8 =jsonData["data"]["plan"]["walls"][3]["to"]["x"];
          var y8 =jsonData["data"]["plan"]["walls"][3]["to"]["y"];
          var z8 =jsonData["data"]["plan"]["walls"][3]["to"]["z"];
          
          
          
          float cube4SizeX =  (float)x8- (float)x7 ;
          
          float cube4SizeZ =  (float)z8 - (float)z7 ;
          
          var c4 = Instantiate(cube1, new Vector3(-3,0,0), Quaternion.identity);
          
          c4.transform.localScale = new Vector3(cube4SizeX,(float)height7,cube4SizeZ);
          
          //Model Intialize
          
          var model1X =jsonData["data"]["plan"]["products"][0]["position"]["x"];
          var model1Y =jsonData["data"]["plan"]["products"][0]["position"]["y"];
          var model1Z =jsonData["data"]["plan"]["products"][0]["position"]["z"];
          
          var model1XRot =jsonData["data"]["plan"]["products"][0]["rotation"]["x"];
          var model1YRot =jsonData["data"]["plan"]["products"][0]["rotation"]["y"];
          var model1ZRot =jsonData["data"]["plan"]["products"][0]["rotation"]["z"];
          var model1WRot =jsonData["data"]["plan"]["products"][0]["rotation"]["w"];
          
          Debug.Log(model1X + " " + model1Y + "  "+ model1Z);
          Debug.Log(model1XRot + " " + model1YRot + "  "+ model1ZRot + " " + model1WRot);

          

         model.transform.localPosition = new Vector3((float)model1X, (float)model1Y, (float)model1Z);
         model.transform.rotation = new Quaternion((float)model1XRot,(float) model1YRot,(float) model1ZRot, (float)model1WRot);
    }
}

