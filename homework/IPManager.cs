using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSearcher
{
    internal class IPManager
    {

        private static List<IPRange> ipRangeList = new List<IPRange>();
        private static IPStats ipStats = new IPStats();
        private static Mutex mutex = new Mutex();
        private static bool needCalculateTotal = true;

        public static void Reset()
        {
            ipRangeList.Clear();
            needCalculateTotal = true;
            ipStats.Reset();
        }

        public static String GetIP()
        {
            mutex.WaitOne();
            while (ipRangeList.Count > 0) {
                String[] fromIp, toIp;
                if (needCalculateTotal)
                {
                    ipStats.totalIp = 0;
                    ipStats.ipChecked = 0;
                    needCalculateTotal = false;
                    fromIp = ipRangeList[0].ipFrom.Split('.');
                    toIp = ipRangeList[0].ipTo.Split('.');
                    while (fromIp[0] != toIp[0] || fromIp[1] != toIp[1] || fromIp[2] != toIp[2] || fromIp[3] != toIp[3])
                    {
                        int[] newValues = { int.Parse(fromIp[0]), int.Parse(fromIp[1]), int.Parse(fromIp[2]), int.Parse(fromIp[3]) + 1 };
                        for (int i = 3; i >= 0; i--)
                        {
                            if (newValues[i] > 255)
                            {
                                newValues[i] = 0;
                                newValues[i - 1]++;
                            }
                        }
                        for (int i = 0; i <= 3; i++)
                        {
                            fromIp[i] = newValues[i].ToString();
                        }
                        ipStats.totalIp++;
                    }
                }
                fromIp = ipRangeList[0].ipFrom.Split('.');
                toIp = ipRangeList[0].ipTo.Split('.');
                if (fromIp[0] != toIp[0] || fromIp[1] != toIp[1] || fromIp[2] != toIp[2] || fromIp[3] != toIp[3])
                {
                    int[] newValues = { int.Parse(fromIp[0]), int.Parse(fromIp[1]), int.Parse(fromIp[2]), int.Parse(fromIp[3]) + 1 };
                    for (int i = 3; i >= 0; i--)
                    {
                        if (newValues[i] > 255)
                        {
                            newValues[i] = 0;
                            newValues[i - 1]++;
                        }
                    }
                    for (int i = 0; i <= 3; i++)
                    {
                        fromIp[i] = newValues[i].ToString();
                    }
                    ipStats.ipChecked++;
                    ipRangeList[0].ipFrom = String.Join(".", fromIp);
                    mutex.ReleaseMutex();
                    return ipRangeList[0].ipFrom;
                }
                else
                {
                    needCalculateTotal = true;
                    ipRangeList.RemoveAt(0);
                }
            }
            mutex.ReleaseMutex();
            return null;
        }

        public static void AddIPRange(IPRange range)
        {
            ipRangeList.Add(range);
        }

        public static IPStats GetStats()
        {
            return ipStats;
        }

    }

    internal class IPStats
    {
        public int ipChecked { get; set; } = 0;
        public int totalIp { get; set; } = 0;

        public void Reset()
        {
            ipChecked = 0;
            totalIp = 0;
        }
    }

    internal class IPRange
    {
        public string ipFrom { get; set; } = "";
        public string ipTo { get; set; } = "";

        public IPRange(string ipFrom, string ipTo)
        {
            String[] ipFromParts = ipFrom.Split('.');
            ipFromParts[3] = (int.Parse(ipFromParts[3]) - 1).ToString();
            this.ipFrom = String.Join(".", ipFromParts);
            this.ipTo = ipTo;
        }
    }
}
