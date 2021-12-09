using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;

namespace RandomSites {
    public class Calculate_ViewModel {

        public static Stopwatch sw = new Stopwatch();

        public static Dictionary<string, UserContribution> getContributions(string url) {
            sw.Start();

            Dictionary<string, UserContribution> retDict = new Dictionary<string, UserContribution>();

            var web = new HtmlWeb();
            HtmlDocument historyDoc = web.Load(url);

            int state = 0;
            int exceptionCount = 0;
            int dateID = 1;
            int commitID = 1;

            //Each TimeLineItem
            while (true) {

                //Each HistoryCommit
                while (true) {
                    try {
                        string checkCommit = historyDoc.DocumentNode.SelectSingleNode(getXPath(dateID, commitID, 3)).InnerText.ToLower();
                        exceptionCount = 0;
                        if (!checkCommit.StartsWith("merge branch 'release") && !checkCommit.StartsWith("merge tag") && !checkCommit.StartsWith("merge branch 'develop'")) {
                            string author;
                            try {
                                author = historyDoc.DocumentNode.SelectSingleNode(getXPath(dateID, commitID, 1)).InnerText;
                            } catch (Exception c) {
                                author = historyDoc.DocumentNode.SelectSingleNode(getXPath(dateID, commitID, 4)).InnerText;
                            }
                            string commitLink = "https://github.com" + historyDoc.DocumentNode.SelectSingleNode(getXPath(dateID, commitID, 2)).GetAttributeValue("href", "Link not found");
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
                        exceptionCount++;
                        break;
                    }
                }
                dateID++;
                commitID = 1;
                if (exceptionCount == 2) {
                    dateID = 1;
                    commitID = 1;
                    try {
                        url = historyDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div/main/div[2]/div/div[3]/div/a[2]").GetAttributeValue("href", "Link not found");
                    } catch (Exception e) {
                        if (state == 0) {
                            url = historyDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div/main/div[2]/div/div[3]/div/a").GetAttributeValue("href", "Link not found");
                            state++;
                        } else {
                            break;
                        }
                    }
                    historyDoc = web.Load(url);
                }
            }

            sw.Stop();
            return retDict;
        }

        private static string getXPath(int dateID, int commitID, int type) {
            string closePath = "";
            if (type == 1) {
                //Author
                closePath += "/div[1]/div/div[2]/a";
            } else if (type == 2) {
                //Link
                closePath += "/div[2]/div[1]/a";
            } else if (type == 3) {
                //Title
                closePath += "/div[1]/p/a";
            } else {
                closePath += "/div[1]/div/div[2]/span";
            }
            return "/html/body/div[4]/div/main/div[2]/div/div[2]/div[" + dateID + "]/div[2]/ol/li[" + commitID + "]" + closePath;
        }
    }
}
