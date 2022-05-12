using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using BicycleShopDB.Data.FilePath;

namespace BicycleShopDB.Data.InitClasses
{
    public class ImageInit
    {
        public static void Initialize(BicycleContext context)
        {
            if (!context.Images.Any())
            {
                string imagesJson = ReadTestDataJson();
                IList<JToken> images = ParseJson(imagesJson);
                List<Tables.Image> imageList = SetImages(images);
                AddImagesToTheDB(context, imageList);
            }
        }
        static string ReadTestDataJson()
        {
            string imagesJson = String.Empty;
            using (StreamReader sr = new StreamReader(GetJSONFilePath()))
            {
                imagesJson = sr.ReadToEnd();
            }
            return imagesJson;
        }
        static string GetJSONFilePath() => PathToTestDataJSONFolder.Path + "image.json";
        static IList<JToken> ParseJson(string imageJson)
        {
            JObject imagesData = JObject.Parse(imageJson);
            return imagesData["data"].Children().ToList();
        }
        static List<Tables.Image> SetImages(IList<JToken> images)
        {
            List<Tables.Image> imageList = new List<Tables.Image>();
            foreach (var image in images)
            {
                imageList.Add(new Tables.Image
                {
                    BicycleId = Convert.ToInt32(image["bicycleId"].ToString()),
                    ImagePath = image["imagePath"].ToString()
                });
            }
            return imageList;
        }
        static void AddImagesToTheDB(BicycleContext context, List<Tables.Image> images)
        {
            context.Images.AddRange(images);
            context.SaveChanges();
        }
    }
}
