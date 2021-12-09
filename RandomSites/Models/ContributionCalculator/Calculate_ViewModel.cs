using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace RandomSites {
    public class Calculate_ViewModel {

        public static Dictionary<string,UserContribution> getContributions(string url) {
            Dictionary<string, UserContribution> retDict = new Dictionary<string, UserContribution>();

            var web = new HtmlWeb();
            HtmlDocument historyDoc = web.Load(url);

            int dateID = 1;
            int commitID = 1;

            //Each TimeLineItem
            while (true) {
                try {

                    //Each HistoryCommit
                    while (true) {
                        try {
                            if (!historyDoc.DocumentNode.SelectSingleNode(getCommitPath(dateID, commitID, 3)).InnerText.StartsWith("Merge")) {
                                string author = historyDoc.DocumentNode.SelectSingleNode(getCommitPath(dateID, commitID, 1)).InnerText;
                                string commitLink = "https://github.com" + historyDoc.DocumentNode.SelectSingleNode(getCommitPath(dateID, commitID, 2)).GetAttributeValue("href", "Link not found");
                                if (!retDict.ContainsKey(author)) {
                                    retDict.Add(author, new UserContribution(author));
                                }
                                HtmlDocument commitDoc = web.Load(commitLink);
                                retDict[author].increaseAdditions(Int32.Parse(commitDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div/main/div[2]/div/div[3]/div[2]/strong[1]").InnerText.Split(" ")[0]));
                                retDict[author].increaseDeletions(Int32.Parse(commitDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div/main/div[2]/div/div[3]/div[2]/strong[2]").InnerText.Split(" ")[0]));
                                retDict[author].incrementCommit();
                            }
                            commitID++;
                        } catch (Exception e) {
                            break;
                        }
                    }
                    dateID++;
                    commitID = 1;
                } catch(Exception e) {
                    break;
                }
            }
            return retDict;
        }

        private static string getCommitPath(int dateID, int commitID, int type) {
            string closePath = "";
            if (type == 1) {
                //Author
                closePath += "/div[1]/div/div[2]";
            } else if(type == 2) {
                //Link
                closePath += "/div[2]/div[1]";
            } else {
                //Title
                closePath += "/div[1]/p";
            }
            return "/html/body/div[4]/div/main/div[2]/div/div[2]/div[" + dateID + "]/div[2]/ol/li[" + commitID + "]" + closePath + "/a";
        }
    }
}
