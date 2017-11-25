using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleInfoPlugin.Models.Raw;
using Grabacr07.KanColleWrapper;

namespace BattleInfoPlugin.Models.Repositories
{
    internal class EnemyDataUpdater
    {
        private readonly EnemyDataProvider provider;

        private EnemyData EnemyData => this.provider.EnemyData;


        private string currentEnemyID;

        private int previousCellNo;

        private map_start_next currentStartNext;


        public EnemyDataUpdater(EnemyDataProvider provider)
        {
            this.provider = provider;
            this.RegisterListener();
        }

        #region Observe

        public void RegisterListener()
        {
            var proxy = KanColleClient.Current.Proxy;

            proxy.Observe<battle_midnight_sp_midnight>("/kcsapi/api_req_battle_midnight/sp_midnight")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_airbattle>("/kcsapi/api_req_combined_battle/airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_battle>("/kcsapi/api_req_combined_battle/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_battle_water>("/kcsapi/api_req_combined_battle/battle_water")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_sp_midnight>("/kcsapi/api_req_combined_battle/sp_midnight")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<sortie_airbattle>("/kcsapi/api_req_sortie/airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<sortie_battle>("/kcsapi/api_req_sortie/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<sortie_ld_airbattle>("/kcsapi/api_req_sortie/ld_airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_ld_airbattle>("/kcsapi/api_req_combined_battle/ld_airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_ec_battle>("/kcsapi/api_req_combined_battle/ec_battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_each_battle>("/kcsapi/api_req_combined_battle/each_battle")
                .Subscribe(x => this.Update(x.Data));


            proxy.Observe<map_start_next>("/kcsapi/api_req_map/start")
                .Subscribe(x => this.UpdateMapData(x.Data));

            proxy.Observe<map_start_next>("/kcsapi/api_req_map/next")
                .Subscribe(x => this.UpdateMapData(x.Data));

            proxy.Observe<battle_result>("/kcsapi/api_req_sortie/battleresult")
                .Subscribe(x => this.UpdateEnemyName(x.Data));

            proxy.Observe<battle_result>("/kcsapi/api_req_combined_battle/battleresult")
                .Subscribe(x => this.UpdateEnemyName(x.Data));
        }

        #endregion

        public void Update(ICommonFirstBattleMembers data)
        {
            this.UpdateEnemyData(
                data.api_ship_ke,
                data.api_ship_ke_combined,
                data.api_formation,
                data.api_eSlot,
                data.api_eSlot_combined,
                data.api_eKyouka,
                data.api_eParam,
                data.api_eParam_combined,
                data.api_ship_lv,
                data.api_ship_lv_combined,
                data.api_e_maxhps,
                data.api_e_maxhps_combined);
            this.UpdateBattleTypes(data);
        }

        public void UpdateMapData(map_start_next startNext)
        {
            this.currentStartNext = startNext;

            if (this.UpdateMapRoute(startNext) || this.UpdateMapCellData(startNext))
            {
                this.provider.Save();
            }
        }

        public void UpdateBattleTypes<T>(T battleApi)
        {
            var battleTypeName = typeof(T).Name;
            var mapInfo = GetMapInfoId(this.currentStartNext);
            var cellId = this.currentStartNext.api_no;

            if (this.EnemyData.MapCellBattleTypes.GetOrAddNew(mapInfo).Update(cellId, battleTypeName))
            {
                this.provider.Save();
            }
        }

        public void UpdateEnemyName(battle_result result)
        {
            if (result?.api_enemy_info == null) return;

            if (this.EnemyData.EnemyNames.Update(this.currentEnemyID, result.api_enemy_info.api_deck_name))
            {
                this.provider.Save();
            }
        }

        private bool UpdateMapEnemyData(int mapInfoId, int mapCellId, string enemyId)
        {
            var modified = this.EnemyData.MapEnemyData.GetOrAddNew(mapInfoId).GetOrAddNew(mapCellId).Add(enemyId);

            var rank = Master.Current.MapInfos[mapInfoId].Rank;
            modified |= this.EnemyData.EnemyRank.Update(enemyId, rank);

            return modified;
        }

        private bool UpdateMapRoute(map_start_next startNext)
        {
            var mapInfo = GetMapInfoId(startNext);
            if (!this.EnemyData.MapRoute.ContainsKey(mapInfo))
                this.EnemyData.MapRoute.Add(mapInfo, new HashSet<KeyValuePair<int, int>>());

            var modified = this.EnemyData.MapRoute.GetOrAddNew(mapInfo).Add(new KeyValuePair<int, int>(this.previousCellNo, startNext.api_no));

            this.previousCellNo = 0 < startNext.api_next ? startNext.api_no : 0;

            return modified;
        }

        private bool UpdateMapCellData(map_start_next startNext)
        {
            var modified = false;

            var mapInfo = GetMapInfoId(startNext);
            if (!this.EnemyData.MapCellDatas.ContainsKey(mapInfo))
            {
                this.EnemyData.MapCellDatas.Add(mapInfo, new List<MapCellData>());
                modified = true;
            }

            var areaId = startNext.api_maparea_id;
            var mapId = startNext.api_mapinfo_no;
            var cellId = startNext.api_no;
            var currentCell = Master.Current.MapAreas[areaId][mapId][cellId];
            var mapCellData = new MapCellData
            {
                MapAreaId = areaId,
                MapInfoIdInEachMapArea = mapId,
                No = cellId,
                ColorNo = startNext.api_color_no,
                CommentKind = startNext.api_comment_kind,
                EventId = startNext.api_event_id,
                EventKind = startNext.api_event_kind,
                ProductionKind = startNext.api_production_kind,
                SelectCells = startNext.api_select_route != null ? startNext.api_select_route.api_select_cells : new int[0],
                Distance = startNext.api_distance_data?.FirstOrDefault(x => x.api_mapcell_id == currentCell.Id)?.api_distance ?? 0,
            };

            var exists = this.EnemyData.MapCellDatas[mapInfo].FirstOrDefault(x => x.No == mapCellData.No);
            if (!mapCellData.Equals(exists))
            {
                if (exists != null)
                {
                    this.EnemyData.MapCellDatas[mapInfo].Remove(exists);
                }
                this.EnemyData.MapCellDatas[mapInfo].Add(mapCellData);
                modified = true;
            }

            return modified;
        }

        private static int GetMapInfoId(map_start_next startNext)
        {
            return GetMapInfoId(startNext.api_maparea_id, startNext.api_mapinfo_no);
        }

        private static int GetMapInfoId(int areaId, int mapId)
        {
            return Master.Current.MapAreas[areaId][mapId].Id;
        }

        private static readonly int[] spliter = { -1 };

        public void UpdateEnemyData(
            int[] api_ship_ke,
            int[] api_ship_ke_combined,
            int[] api_formation,
            int[][] api_eSlot,
            int[][] api_eSlot_combined,
            int[][] api_eKyouka,
            int[][] api_eParam,
            int[][] api_eParam_combined,
            int[] api_ship_lv,
            int[] api_ship_lv_combined,
            int[] api_e_maxhps,
            int[] api_e_maxhps_combined)
        {
            var formation = (Formation)api_formation[1];
            var enemies = api_ship_ke.Where(x => x != -1).Concat(api_ship_ke_combined.ValueOrEmpty()).ToArray();
            var lvs = api_ship_lv.Concat(api_ship_lv_combined.ValueOrEmpty()).ToArray();
            var hps = api_e_maxhps.GetEnemyData()
                .Concat(api_e_maxhps_combined != null ? spliter.Concat(api_e_maxhps_combined.GetEnemyData()) : new int[0])
                .ToArray();
            var slots = api_eSlot.Concat(api_eSlot_combined.ValueOrEmpty()).ToArray();
            var param = api_eParam.Concat(api_eParam_combined.ValueOrEmpty()).ToArray();

            var mapInfoId = GetMapInfoId(this.currentStartNext);
            var mapCellId = this.currentStartNext.api_no;
            var rank = Master.Current.MapInfos[mapInfoId].Rank;

            string enemyId;
            if (
                !this.GetEnemyId(mapInfoId, mapCellId, rank, enemies, formation, slots, api_eKyouka, param, lvs, hps,
                    out enemyId))
            {
                this.EnemyData.EnemyDictionary[enemyId] = enemies;
                this.EnemyData.EnemyFormation[enemyId] = formation;
                this.EnemyData.EnemySlotItems[enemyId] = slots;
                this.EnemyData.EnemyUpgraded[enemyId] = api_eKyouka;
                this.EnemyData.EnemyParams[enemyId] = param;
                this.EnemyData.EnemyLevels[enemyId] = lvs;
                this.EnemyData.EnemyHPs[enemyId] = hps;
            }

            if (this.UpdateMapEnemyData(mapInfoId, mapCellId, enemyId))
            {
                this.provider.Save();
            }

            this.currentEnemyID = enemyId;
        }

        private bool GetEnemyId(
            int mapInfoId,
            int cellIdInMapInfo,
            int rank,
            int[] api_ship_ke,
            Formation api_formation,
            int[][] api_eSlot,
            int[][] api_eKyouka,
            int[][] api_eParam,
            int[] api_ship_lv,
            int[] api_e_maxhps,
            out string id)
        {
            var keys = this.EnemyData.MapEnemyData.GetOrAddNew(mapInfoId).GetOrAddNew(cellIdInMapInfo).ToArray();

            keys = this.EnemyData.EnemyDictionary.Filter(keys, api_ship_ke);
            keys = this.EnemyData.EnemyFormation.Filter(keys, api_formation);
            keys = this.EnemyData.EnemySlotItems.Filter(keys, api_eSlot);
            keys = this.EnemyData.EnemyUpgraded.Filter(keys, api_eKyouka);
            keys = this.EnemyData.EnemyParams.Filter(keys, api_eParam);
            keys = this.EnemyData.EnemyLevels.Filter(keys, api_ship_lv);
            keys = this.EnemyData.EnemyHPs.Filter(keys, api_e_maxhps);
            keys = this.EnemyData.EnemyRank.Filter(keys, rank);

            id = keys.Any() ? keys.First() : Guid.NewGuid().ToString();

            return keys.Any();
        }
    }
}
