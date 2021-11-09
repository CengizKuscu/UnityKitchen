using System;
using System.Collections.Generic;

namespace UKitchen.ExternalPackages
{
    [Serializable]
    public class Packages
    {
        public List<Package> packages;
    }

    [Serializable]
    public class Package
    {
        public string id;
        public string name;
        public string url;
        public string version;
        public List<string> requireIds;
        public bool selected;
    }
}