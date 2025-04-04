using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistingNether.DataAccess.BattleNet.WoW.Character
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Character
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public Realm realm { get; set; }
    }

    public class Key
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Quest
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Realm
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }

    public class WoWCharacterCompletedQuestsModel
    {
        public Links _links { get; set; }
        public Character character { get; set; }
        public List<Quest> quests { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }


}
