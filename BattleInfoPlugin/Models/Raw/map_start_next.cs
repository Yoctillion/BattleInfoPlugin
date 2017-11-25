namespace BattleInfoPlugin.Models.Raw
{
    


    public class map_start_next
    {
        public Api_Cell_Data[] api_cell_data { get; set; }
        public int api_rashin_flg { get; set; }
        public int api_rashin_id { get; set; }
        public int api_maparea_id { get; set; }
        public int api_mapinfo_no { get; set; }
        public int api_no { get; set; }
        public int api_color_no { get; set; }
        public int api_event_id { get; set; }
        public int api_event_kind { get; set; }
        public int api_next { get; set; }
        public int api_bosscell_no { get; set; }
        public int api_bosscomp { get; set; }
        public Api_Airsearch api_airsearch { get; set; }
        public Api_Eventmap api_eventmap { get; set; }
        public int api_from_no { get; set; }
        public Api_Distance_Data[] api_distance_data { get; set; }
        // 以下next
        public int api_comment_kind { get; set; }
        public int api_production_kind { get; set; }
        public Api_Enemy api_enemy { get; set; }
        public Api_Happening api_happening { get; set; }
        public Api_Itemget[] api_itemget { get; set; }
        public Api_Itemget api_itemget_eo_result { get; set; }
        public Api_Itemget api_itemget_eo_comment { get; set; }
        public Api_Select_Route api_select_route { get; set; }
        public int api_ration_flag { get; set; }
        public Api_Destruction_Battle api_destruction_battle { get; set; }
    }

    public class Api_Cell_Data
    {
        public int api_id { get; set; }
        public int api_no { get; set; }
        public int api_color_no { get; set; }
        public int api_passed { get; set; }
    }

    public class Api_Airsearch
    {
        public int api_plane_type { get; set; }
        public int api_result { get; set; }
    }

    public class Api_Eventmap
    {
        public int api_max_maphp { get; set; }
        public int api_now_maphp { get; set; }
        public int api_dmg { get; set; }
    }

    public class Api_Distance_Data
    {
        public int api_mapcell_id { get; set; }
        public int api_distance { get; set; }
    }

    public class Api_Enemy
    {
        //public int api_enemy_id { get; set; }
        public int api_result { get; set; }
        public string api_result_str { get; set; }
    }

    public class Api_Happening
    {
        public int api_type { get; set; }
        public int api_count { get; set; }
        public int api_usemst { get; set; }
        public int api_mst_id { get; set; }
        public int api_icon_id { get; set; }
        public int api_dentan { get; set; }
    }

    public class Api_Itemget
    {
        public int api_getcount { get; set; }
        public int api_icon_id { get; set; }
        public int api_id { get; set; }
        public string api_name { get; set; }
        public int api_usemst { get; set; }
    }
    public class Api_Select_Route
    {
        public int[] api_select_cells { get; set; }
    }

    public class Api_Destruction_Battle : ICommonBattleMembers
    {
        public int[] api_formation { get; set; }
        public int[] api_ship_ke { get; set; }
        public int[] api_ship_lv { get; set; }
        public int[][] api_eSlot { get; set; }
        public int[] api_f_nowhps { get; set; }
        public int[] api_f_maxhps { get; set; }
        public Api_Air_Base_Attack[] api_air_base_attack { get; set; }
        public int api_lost_kind { get; set; }

        public int[] api_e_nowhps { get; set; }
        public int[] api_e_maxhps { get; set; }
        public int[] api_e_nowhps_combined { get; set; }
        public int[] api_e_maxhps_combined { get; set; }

        #region not exists
        int ICommonBattleMembers.api_deck_id { get; set; }
        int[] ICommonBattleMembers.api_ship_ke_combined { get; set; }
        int[] ICommonBattleMembers.api_ship_lv_combined { get; set; }
        int[] ICommonBattleMembers.api_f_nowhps_combined { get; set; }
        int[] ICommonBattleMembers.api_f_maxhps_combined { get; set; }
        int[][] ICommonBattleMembers.api_eSlot_combined { get; set; }
        int[][] ICommonBattleMembers.api_eKyouka { get; set; }
        int[][] ICommonBattleMembers.api_fParam { get; set; }
        int[][] ICommonBattleMembers.api_fParam_combined { get; set; }
        int[][] ICommonBattleMembers.api_eParam { get; set; }
        int[][] ICommonBattleMembers.api_eParam_combined { get; set; }
        #endregion
    }
}
