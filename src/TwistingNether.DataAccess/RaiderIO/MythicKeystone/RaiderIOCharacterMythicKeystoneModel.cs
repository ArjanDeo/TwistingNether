using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistingNether.DataAccess.RaiderIO.MythicKeystone
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Affix
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string icon_url { get; set; }
        public string wowhead_url { get; set; }
    }

    public class All
    {
        public double score { get; set; }
        public string color { get; set; }
    }

    public class Dps
    {
        public int score { get; set; }
        public string color { get; set; }
    }

    public class Healer
    {
        public double score { get; set; }
        public string color { get; set; }
    }

    public class MythicPlusBestRun
    {
        public string dungeon { get; set; }
        public string short_name { get; set; }
        public int mythic_level { get; set; }
        public DateTime completed_at { get; set; }
        public int clear_time_ms { get; set; }
        public int keystone_run_id { get; set; }
        public int par_time_ms { get; set; }
        public int num_keystone_upgrades { get; set; }
        public int map_challenge_mode_id { get; set; }
        public int zone_id { get; set; }
        public int zone_expansion_id { get; set; }
        public string icon_url { get; set; }
        public string background_image_url { get; set; }
        public double score { get; set; }
        public List<Affix> affixes { get; set; }
        public string url { get; set; }
    }

    public class MythicPlusScoresBySeason
    {
        public string season { get; set; }
        public Scores scores { get; set; }
        public Segments segments { get; set; }
    }

    public class RaiderIOCharacterMythicKeystoneModel
    {
        public string name { get; set; }
        public string race { get; set; }
        public string @class { get; set; }
        public string active_spec_name { get; set; }
        public string active_spec_role { get; set; }
        public string gender { get; set; }
        public string faction { get; set; }
        public int achievement_points { get; set; }
        public string thumbnail_url { get; set; }
        public string region { get; set; }
        public string realm { get; set; }
        public DateTime last_crawled_at { get; set; }
        public string profile_url { get; set; }
        public string profile_banner { get; set; }
        public List<MythicPlusScoresBySeason> mythic_plus_scores_by_season { get; set; }
        public List<MythicPlusBestRun> mythic_plus_best_runs { get; set; }
    }

    public class Scores
    {
        public double all { get; set; }
        public int dps { get; set; }
        public double healer { get; set; }
        public int tank { get; set; }
        public int spec_0 { get; set; }
        public double spec_1 { get; set; }
        public int spec_2 { get; set; }
        public int spec_3 { get; set; }
    }

    public class Segments
    {
        public All all { get; set; }
        public Dps dps { get; set; }
        public Healer healer { get; set; }
        public Tank tank { get; set; }
        public Spec0 spec_0 { get; set; }
        public Spec1 spec_1 { get; set; }
        public Spec2 spec_2 { get; set; }
        public Spec3 spec_3 { get; set; }
    }

    public class Spec0
    {
        public int score { get; set; }
        public string color { get; set; }
    }

    public class Spec1
    {
        public double score { get; set; }
        public string color { get; set; }
    }

    public class Spec2
    {
        public int score { get; set; }
        public string color { get; set; }
    }

    public class Spec3
    {
        public int score { get; set; }
        public string color { get; set; }
    }

    public class Tank
    {
        public int score { get; set; }
        public string color { get; set; }
    }


}
