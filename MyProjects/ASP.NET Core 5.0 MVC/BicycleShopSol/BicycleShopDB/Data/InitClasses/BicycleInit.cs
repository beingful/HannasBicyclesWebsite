using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BicycleShopDB.Tables;
using Newtonsoft.Json.Linq;
using BicycleShopDB.Data.FilePath;

namespace BicycleShopDB.Data.InitClasses
{
    public class BicycleInit
    {
        public static void Initialize(BicycleContext context)
        {
            if (!context.Bicycles.Any())
            {
                string bicyclesJson = ReadTestDataJson();
                IList<JToken> bicycles = ParseJson(bicyclesJson);
                List<Tables.Bicycle> bicycleList = SetBicycles(bicycles);
                AddBicyclesToTheDB(context, bicycleList);
            }
        }
        static string ReadTestDataJson()
        {
            string bicyclesJson = String.Empty;
            using (StreamReader sr = new StreamReader(GetJSONFilePath()))
            {
                bicyclesJson = sr.ReadToEnd();
            }
            return bicyclesJson;
        }
        static string GetJSONFilePath() => PathToTestDataJSONFolder.Path + "bicycle.json";
        static IList<JToken> ParseJson(string bicyclesJson)
        {
            JObject bicyclesData = JObject.Parse(bicyclesJson);
            return bicyclesData["data"].Children().ToList();
        }
        static List<Tables.Bicycle> SetBicycles(IList<JToken> bicycles)
        {
            List<Tables.Bicycle> bicycleList = new List<Tables.Bicycle>();
            int tryMaxWeight;
            foreach (var bicycle in bicycles)
            {
                bicycleList.Add(new Bicycle
                {
                    BicycleName = bicycle["bicycleName"].ToString(),
                    Brand = bicycle["brand"].ToString(),
                    Type = bicycle["type"].ToString(),
                    ReleaseYear = Convert.ToInt32(bicycle["releaseYear"].ToString()),
                    FrameMaterial = bicycle["frameMaterial"].ToString(),
                    WheelSize = Convert.ToDouble(bicycle["wheelSize"].ToString()),
                    BrakeType = string.IsNullOrEmpty(bicycle["brakeType"].ToString()) ? default(string) : bicycle["brakeType"].ToString(),
                    SpeedQuantity = Convert.ToInt32(bicycle["speedQuantity"].ToString()),
                    MaxWeight = int.TryParse(bicycle["maxWeight"].ToString(), out tryMaxWeight) ? tryMaxWeight : default(int?),
                    Price = Convert.ToInt32(bicycle["price"].ToString()),
                    Discount = Convert.ToInt32(bicycle["discount"].ToString()),
                    Total = (int)(Convert.ToInt32(bicycle["price"].ToString()) * (1 - Convert.ToInt32(bicycle["discount"].ToString()) / 100.0)),
                    Quantity = Convert.ToInt32(bicycle["quantity"].ToString()),
                    ImagePath = bicycle["imagePath"].ToString()
                });
            }
            return bicycleList;
        }
        static void AddBicyclesToTheDB(BicycleContext context, List<Tables.Bicycle> bicycles)
        {
            context.Bicycles.AddRange(bicycles);
            context.SaveChanges();
        }
    }
}
