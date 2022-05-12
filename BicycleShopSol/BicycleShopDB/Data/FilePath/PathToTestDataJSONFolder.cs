using System.IO;

namespace BicycleShopDB.Data.FilePath
{
    public static class PathToTestDataJSONFolder
    {
        public static string Path { get; } = (Directory.GetCurrentDirectory() + "\\")
                                            .Replace("BicycleShop\\", "BicycleShopDB\\Data\\TestDataJSON\\");
    }
}
