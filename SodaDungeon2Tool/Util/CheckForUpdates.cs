using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace SodaDungeon2Tool.Util
{
    public static class CheckForUpdates
    {
        internal class Release
        {
            public string Html_Url {get; set;}
            public string Tag_Name {get; set;}

            public override string ToString()
            {
                return $"HTML_URL: {Html_Url} | Version: {Tag_Name}";
            }
        }
        /// <summary>
        /// Gets the Releases of the project from github and compares the latest release version with the assembly version
        /// </summary>
        /// <returns></returns>
        public static async Task InformLatestRelease()
        {
            string releasesURL = "https://api.github.com/repos/Death-Truction/SodaDungeon2Tool/releases";
            string allReleases;
            allReleases = await DownloadAsString(releasesURL);
            if(allReleases == "") //e.g. happens when api quota reached
                return;

            Release latestRelease = JsonConvert.DeserializeObject<List<Release>>(allReleases)[0];          

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string currentVersion = fvi.FileVersion;
            if(string.Compare(currentVersion, latestRelease.Tag_Name) < 0)
            {
                MessageBoxResult result = MessageBox.Show("A new Version has been Released!\nWould you like to go to the download page?" , "Soda Dungeon 2 Tool Update", MessageBoxButton.YesNo, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Process.Start(latestRelease.Html_Url);
                        break;
                    default:
                        break;
                }
            }
            
        }

        /// <summary>
        /// Download a string from a given url
        /// </summary>
        /// <param name="URL">the url to the file that should be downloaded</param>
        /// <returns>The string that has been downloaded</returns>
        private static async Task<string> DownloadAsString(string URL)
        {
            WebClient client = new WebClient();
            client.Headers.Add("User-Agent", "SodaDungeon2Tool");

            try{
                return  await client.DownloadStringTaskAsync(URL);
            }catch(Exception)
            {
                return "";
            }
        }
    }
}
