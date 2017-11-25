namespace BattleInfoPlugin.Models.Raw
{
    /// <summary>
    /// 連合艦隊-機動部隊
    /// </summary>
    public class combined_battle_battle : ICommonBattleMembers, IBattleFormationInfo, ICommonFirstBattleMembers
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
        public int api_midnight_flag { get; set; }
        public int[][] api_eSlot { get; set; }
        public int[][] api_eKyouka { get; set; }
        public int[][] api_fParam { get; set; }
        public int[][] api_fParam_combined { get; set; }
        public int[][] api_eParam { get; set; }
        public int[] api_search { get; set; }
        public int[] api_formation { get; set; }
        public Api_Kouku api_injection_kouku { get; set; }
        public Api_Air_Base_Attack[] api_air_base_attack { get; set; }
        public int[] api_stage_flag { get; set; }
        public Api_Kouku api_kouku { get; set; }
        public int api_support_flag { get; set; }
        public Api_Support_Info api_support_info { get; set; }
        public int api_opening_taisen_flag { get; set; }
        public Hougeki api_opening_taisen { get; set; }
        public int api_opening_flag { get; set; }
        public Raigeki api_opening_atack { get; set; }
        public int[] api_hourai_flag { get; set; }
        public Hougeki api_hougeki1 { get; set; }
        public Raigeki api_raigeki { get; set; }
        public Hougeki api_hougeki2 { get; set; }
        public Hougeki api_hougeki3 { get; set; }

        #region not exists

        int[] ICommonBattleMembers.api_ship_ke_combined { get; set; }
        int[] ICommonBattleMembers.api_ship_lv_combined { get; set; }
        int[][] ICommonBattleMembers.api_eSlot_combined { get; set; }
        int[][] ICommonBattleMembers.api_eParam_combined { get; set; }

        #endregion
    }
}
