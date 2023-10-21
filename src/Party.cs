using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionSystem
{
    internal class Party
    {
        public int partyID;
        public string partyName;
        public Dictionary<int, int> cityVotes = new Dictionary<int, int>();
       // cityVotes["Anahtar1"] = 100;

    }
}
