namespace BattleInfoPlugin.Models.Raw
{
    /// <summary>
    /// 演習-昼戦
    /// </summary>
    public class practice_battle : ICommonBattleMembers, IBattleFormationInfo
    {
        public int api_deck_id
        {
            get { return this.api_dock_id; }
            set { this.api_dock_id = value; }
        }
        public int api_dock_id { get; set; }
        public int[] api_ship_ke { get; set; }
        public int[] api_ship_lv { get; set; }
        public int[] api_f_nowhps { get; set; }
        public int[] api_f_maxhps { get; set; }
        public int[] api_e_nowhps { get; set; }
        public int[] api_e_maxhps { get; set; }
        public int[] api_e_nowhps_combined { get; set; }
        public int[] api_e_maxhps_combined { get; set; }
        public int api_midnight_flag { get; set; }
        public int[][] api_eSlot { get; set; }
        public int[][] api_eKyouka { get; set; }
        public int[][] api_fParam { get; set; }
        public int[][] api_eParam { get; set; }
        public int[] api_search { get; set; }
        public int[] api_formation { get; set; }
        public Api_Kouku api_injection_kouku { get; set; }
        public int[] api_stage_flag { get; set; }
        public Api_Kouku api_kouku { get; set; }
        public int api_opening_taisen_flag { get; set; }
        public Hougeki api_opening_taisen { get; set; }
        public int api_opening_flag { get; set; }
        public Raigeki api_opening_atack { get; set; }
        public int[] api_hourai_flag { get; set; }
        public Hougeki api_hougeki1 { get; set; }
        public Hougeki api_hougeki2 { get; set; }
        public Hougeki api_hougeki3 { get; set; }
        public Raigeki api_raigeki { get; set; }

        #region not exists

        int[] ICommonBattleMembers.api_ship_ke_combined { get; set; }
        int[] ICommonBattleMembers.api_ship_lv_combined { get; set; }
        int[] ICommonBattleMembers.api_f_nowhps_combined { get; set; }
        int[] ICommonBattleMembers.api_f_maxhps_combined { get; set; }
        int[][] ICommonBattleMembers.api_eSlot_combined { get; set; }
        int[][] ICommonBattleMembers.api_fParam_combined { get; set; }
        int[][] ICommonBattleMembers.api_eParam_combined { get; set; }

        #endregion
    }
}
