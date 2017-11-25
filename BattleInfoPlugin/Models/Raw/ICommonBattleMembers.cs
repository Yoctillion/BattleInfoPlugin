using System.Collections.Generic;
using System.Linq;
using Grabacr07.KanColleWrapper;

namespace BattleInfoPlugin.Models.Raw
{
    public interface ICommonBattleMembers
    {
        int api_deck_id { get; set; }
        int[] api_ship_ke { get; set; }
        int[] api_ship_ke_combined { get; set; }
        int[] api_ship_lv { get; set; }
        int[] api_ship_lv_combined { get; set; }
        int[] api_f_nowhps { get; set; }
        int[] api_f_maxhps { get; set; }
        int[] api_f_nowhps_combined { get; set; }
        int[] api_f_maxhps_combined { get; set; }
        int[][] api_eSlot { get; set; }
        int[][] api_eSlot_combined { get; set; }
        int[][] api_eKyouka { get; set; }
        int[][] api_fParam { get; set; }
        int[][] api_fParam_combined { get; set; }
        int[][] api_eParam { get; set; }
        int[][] api_eParam_combined { get; set; }
        int[] api_e_nowhps { get; set; }
        int[] api_e_maxhps { get; set; }
        int[] api_e_nowhps_combined { get; set; }
        int[] api_e_maxhps_combined { get; set; }
    }

    public static class CommonBattleMembersExtensions
    {
        public static FleetData[] GetEnemyFleets(this ICommonBattleMembers data)
        {
            var fleet1 = data.GetEnemyFirstFleetData();
            var fleet2 = data.GetEnemySecondFleetData();
            if (fleet2 != null)
            {
                return new[] { new FleetData(fleet1), new FleetData(fleet2) };
            }
            else
            {
                return new[] { new FleetData(fleet1) };
            }
        }

        public static MastersShipData[] GetEnemyFirstFleetData(this ICommonBattleMembers data)
        {
            return data.api_ship_ke
                .ToMastersShipDataArray(
                    data.api_ship_lv.ToArray(),
                    data.api_e_maxhps.ToArray(),
                    data.api_e_nowhps.ToArray(),
                    data.api_eParam,
                    data.api_eSlot);
        }

        public static MastersShipData[] GetEnemySecondFleetData(this ICommonBattleMembers data)
        {
            return data.api_ship_ke_combined?.GetSection(0)
                .ToMastersShipDataArray(
                    data.api_ship_lv_combined.GetSection(0).ToArray(),
                    data.api_e_maxhps_combined.GetEnemyData().ToArray(),
                    data.api_e_nowhps_combined.GetEnemyData().ToArray(),
                    data.api_eParam_combined,
                    data.api_eSlot_combined);
        }

        public static MastersShipData[] ToMastersShipDataArray(
            this IEnumerable<int> ids,
            int[] lvs,
            int[] maxHps,
            int[] nowHps,
            int[][] param,
            int[][] slot)
        {
            var master = KanColleClient.Current.Master;
            return ids
                .TakeWhile(x => x != -1)
                .Select((id, i) =>
                {
                    return new MastersShipData(master.Ships[id])
                    {
                        Level = lvs[i],
                        Firepower = param[i][0],
                        Torpedo = param[i][1],
                        AA = param[i][2],
                        Armer = param[i][3],
                        Slots = slot[i].Where(s => 0 < s)
                            .Select(s => new ShipSlotData(master.SlotItems[s]))
                            .ToArray()
                    };
                })
                .ToArray();
        }
    }
}
