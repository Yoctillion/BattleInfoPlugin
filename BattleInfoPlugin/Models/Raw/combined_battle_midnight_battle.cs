namespace BattleInfoPlugin.Models.Raw
{
    /// <summary>
    /// 連合艦隊-夜戦
    /// </summary>
    public class combined_battle_midnight_battle : ICommonBattleMembers
    {
        public int api_deck_id { get; set; }
        public int[] api_ship_ke { get; set; }
        public int[] api_ship_lv { get; set; }
        public int[] api_f_nowhps { get; set; }
        public int[] api_f_maxhps { get; set; }
        public int[] api_f_nowhps_combined { get; set; }
        public int[] api_f_maxhps_combined { get; set; }
        public int[] api_e_nowhps { get; set; }
        public int[] api_e_maxhps { get; set; }
        public int[] api_e_nowhps_combined { get; set; }
        public int[] api_e_maxhps_combined { get; set; }
        public int[][] api_eSlot { get; set; }
        public int[][] api_eKyouka { get; set; }
        public int[][] api_fParam { get; set; }
        public int[][] api_fParam_combined { get; set; }
        public int[][] api_eParam { get; set; }
        public int[] api_touch_plane { get; set; }
        public int[] api_flare_pos { get; set; }
        public Midnight_Hougeki api_hougeki { get; set; }

        #region not exists

        int[] ICommonBattleMembers.api_ship_ke_combined { get; set; }
        int[] ICommonBattleMembers.api_ship_lv_combined { get; set; }
        int[][] ICommonBattleMembers.api_eSlot_combined { get; set; }
        int[][] ICommonBattleMembers.api_eParam_combined { get; set; }

        #endregion
    }
}
