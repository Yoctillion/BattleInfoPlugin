using System;
using System.Collections.Generic;
using System.Linq;
using BattleInfoPlugin.Models.Raw;
using Grabacr07.KanColleWrapper;
using Livet;

namespace BattleInfoPlugin.Models
{
    public partial class BattleData : NotificationObject
    {
        public static BattleData Current { get; } = new BattleData();

        private string _practiceEnemyName;

        #region State

        private BattleState _State;

        public BattleState State
        {
            get { return this._State; }
            set
            {
                if (this._State != value)
                {
                    this._State = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region IsInBattle

        private bool _IsInBattle;

        public bool IsInBattle
        {
            get { return this._IsInBattle; }
            internal set
            {
                if (this._IsInBattle != value)
                {
                    this._IsInBattle = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region BattleResultRank

        private BattleResultRank _BattleResult;

        public BattleResultRank BattleResult
        {
            get { return this._BattleResult; }
            internal set
            {
                if (this._BattleResult != value)
                {
                    this._BattleResult = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region NextCell

        private MapCellInfo _NextCell;

        public MapCellInfo NextCell
        {
            get { return this._NextCell; }
            set
            {
                if (this._NextCell != value)
                {
                    this._NextCell = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region Name変更通知プロパティ
        private string _Name;

        public string Name
        {
            get
            { return this._Name; }
            set
            {
                if (this._Name == value)
                    return;
                this._Name = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region UpdatedTime変更通知プロパティ
        private DateTimeOffset _UpdatedTime;

        public DateTimeOffset UpdatedTime
        {
            get
            { return this._UpdatedTime; }
            set
            {
                if (this._UpdatedTime == value)
                    return;
                this._UpdatedTime = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region BattleSituation変更通知プロパティ

        private BattleSituation _BattleSituation;

        public BattleSituation BattleSituation
        {
            get
            { return this._BattleSituation; }
            set
            {
                if (this._BattleSituation == value)
                    return;
                this._BattleSituation = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region FriendFleet

        private BattleFleet _FriendFleet = new BattleFleet(FleetType.Friend);

        public BattleFleet FriendFleet
        {
            get { return this._FriendFleet; }
            private set
            {
                if (this._FriendFleet != value)
                {
                    this._FriendFleet = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region EnemyFleet変更通知プロパティ

        private BattleFleet _EnemyFleet = new BattleFleet(FleetType.Enemy);

        public BattleFleet EnemyFleet
        {
            get { return this._EnemyFleet; }
            private set
            {
                if (this._EnemyFleet != value)
                {
                    this._EnemyFleet = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region FriendAirSupremacy変更通知プロパティ
        private AirSupremacy _FriendAirSupremacy = AirSupremacy.航空戦なし;

        public AirSupremacy FriendAirSupremacy
        {
            get
            { return this._FriendAirSupremacy; }
            set
            {
                if (this._FriendAirSupremacy == value)
                    return;
                this._FriendAirSupremacy = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region LandBaseAirCombatResults

        private LandBaseAirCombatResult[] _LandBaseAirCombatResults = new LandBaseAirCombatResult[0];

        public LandBaseAirCombatResult[] LandBaseAirCombatResults
        {
            get { return this._LandBaseAirCombatResults; }
            set
            {
                if (this._LandBaseAirCombatResults != value)
                {
                    this._LandBaseAirCombatResults = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region InjectionAirCombatResults

        private AirCombatResult[] _InjectionAirCombatResults = new AirCombatResult[0];

        public AirCombatResult[] InjectionAirCombatResults
        {
            get { return this._InjectionAirCombatResults; }
            set
            {
                if (!this._InjectionAirCombatResults.Equals(value))
                {
                    this._InjectionAirCombatResults = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region AirCombatResults変更通知プロパティ
        private AirCombatResult[] _AirCombatResults = new AirCombatResult[0];

        public AirCombatResult[] AirCombatResults
        {
            get
            { return this._AirCombatResults; }
            set
            {
                if (this._AirCombatResults.Equals(value))
                    return;
                this._AirCombatResults = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region DropShipName変更通知プロパティ
        private string _DropShipName;

        public string DropShipName
        {
            get
            { return this._DropShipName; }
            set
            {
                if (this._DropShipName == value)
                    return;
                this._DropShipName = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region FriendFleetStatus

        private FleetStatus _FriendFleetStatus;

        public FleetStatus FriendFleetStatus
        {
            get { return this._FriendFleetStatus; }
            set
            {
                if (this._FriendFleetStatus != value)
                {
                    this._FriendFleetStatus = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region EnemyFleetStatus

        private FleetStatus _EnemyFleetStatus;

        public FleetStatus EnemyFleetStatus
        {
            get { return this._EnemyFleetStatus; }
            set
            {
                if (this._EnemyFleetStatus != value)
                {
                    var st = new FleetStatus();
                    this._EnemyFleetStatus = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        private int CurrentDeckId { get; set; }

        private BattleData()
        {
            var proxy = KanColleClient.Current.Proxy;

            #region Start / Next

            proxy.Observe<map_start_next>("/kcsapi/api_req_map/start")
                .Subscribe(x => this.UpdateFleetsByStartNext(x.Data, x.Request["api_deck_id"]));

            proxy.Observe<map_start_next>("/kcsapi/api_req_map/next")
                .Subscribe(x => this.UpdateFleetsByStartNext(x.Data));

            #endregion

            #region Practice

            proxy.Observe<member_practice_enemyinfo>("/kcsapi/api_req_member/get_practice_enemyinfo")
                .Subscribe(x => this._practiceEnemyName = x.Data.api_deckname);

            proxy.Observe<practice_battle>("/kcsapi/api_req_practice/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<practice_midnight_battle>("/kcsapi/api_req_practice/midnight_battle")
                .Subscribe(x => this.Update(x.Data));

            #endregion

            #region Normal

            proxy.Observe<sortie_battle>("/kcsapi/api_req_sortie/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<battle_midnight_battle>("/kcsapi/api_req_battle_midnight/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<battle_midnight_sp_midnight>("/kcsapi/api_req_battle_midnight/sp_midnight")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<sortie_airbattle>("/kcsapi/api_req_sortie/airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<sortie_ld_airbattle>("/kcsapi/api_req_sortie/ld_airbattle")
                .Subscribe(x => this.Update(x.Data));

            #endregion

            #region Friend Combined

            proxy.Observe<combined_battle_battle>("/kcsapi/api_req_combined_battle/battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_battle_water>("/kcsapi/api_req_combined_battle/battle_water")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_midnight_battle>("/kcsapi/api_req_combined_battle/midnight_battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_sp_midnight>("/kcsapi/api_req_combined_battle/sp_midnight")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_airbattle>("/kcsapi/api_req_combined_battle/airbattle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_ld_airbattle>("/kcsapi/api_req_combined_battle/ld_airbattle")
                .Subscribe(x => this.Update(x.Data));

            #endregion

            #region Enemy Combined

            proxy.Observe<combined_battle_ec_battle>("/kcsapi/api_req_combined_battle/ec_battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_ec_midnight_battle>("/kcsapi/api_req_combined_battle/ec_midnight_battle")
                .Subscribe(x => this.Update(x.Data));

            #endregion

            proxy.Observe<combined_battle_each_battle>("/kcsapi/api_req_combined_battle/each_battle")
                .Subscribe(x => this.Update(x.Data));

            proxy.Observe<combined_battle_each_battle_water>("/kcsapi/api_req_combined_battle/each_battle_water")
                .Subscribe(x => this.Update(x.Data));

            #region Result

            proxy.Observe<battle_result>("/kcsapi/api_req_practice/battle_result")
                .Subscribe(x => this.UpdateBattleResult(x.Data));

            proxy.Observe<battle_result>("/kcsapi/api_req_sortie/battleresult")
                .Subscribe(x => this.UpdateBattleResult(x.Data));

            proxy.Observe<battle_result>("/kcsapi/api_req_combined_battle/battleresult")
                .Subscribe(x => this.UpdateBattleResult(x.Data));

            #endregion
        }

        #region Update From Battle SvData


        private void Update(sortie_battle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack);

                this.Shelling(data.api_hougeki1);
                this.Shelling(data.api_hougeki2);

                this.Torpedo(data.api_raigeki);
            }, "通常 - 昼戦");
        }

        public void Update(battle_midnight_battle data)
        {
            this.Update(() =>
            {
                this.UpdateFleetsHPs(data);

                this.Shelling(data.api_hougeki);
            }, "通常 - 夜戦");
        }

        public void Update(battle_midnight_sp_midnight data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.Shelling(data.api_hougeki);
            }, "通常 - 開幕夜戦");
        }

        public void Update(combined_battle_airbattle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);

                this.AirCombat(data.api_kouku, "1回目/");
                this.Support(data.api_support_info, data.api_support_flag);
                this.AirCombat(data.api_kouku2, "2回目/", false);
            }, "連合艦隊 - 航空戦 - 昼戦");
        }

        public void Update(combined_battle_battle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack, 2, 1);

                this.Shelling(data.api_hougeki1, 2, 1);
                this.Torpedo(data.api_raigeki, 2, 1);
                this.Shelling(data.api_hougeki2, 1, 1);
                this.Shelling(data.api_hougeki3, 1, 1);
            }, "連合艦隊 - 機動部隊 - 昼戦");
        }

        public void Update(combined_battle_battle_water data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack, 2, 1);

                this.Shelling(data.api_hougeki1, 1, 1);
                this.Shelling(data.api_hougeki2, 1, 1);
                this.Shelling(data.api_hougeki3, 2, 1);
                this.Torpedo(data.api_raigeki, 2, 1);
            }, "連合艦隊 - 水上部隊 - 昼戦");
        }

        public void Update(combined_battle_midnight_battle data)
        {
            this.Update(() =>
            {
                this.UpdateFleetsHPs(data);

                this.Shelling(data.api_hougeki, 2, 1);
            }, "連合艦隊 - 夜戦");
        }

        public void Update(combined_battle_sp_midnight data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.Shelling(data.api_hougeki, 2, 1);
            }, "連合艦隊 - 開幕夜戦");
        }

        public void Update(practice_battle data)
        {
            this.Clear();
            this.Update(() =>
            {
                this.UpdateInfoPractice(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirCombat(data.api_kouku);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack);

                this.Shelling(data.api_hougeki1);
                this.Shelling(data.api_hougeki2);
                this.Torpedo(data.api_raigeki);
            }, "演習 - 昼戦");
        }

        public void Update(practice_midnight_battle data)
        {
            this.Update(() =>
            {
                this.UpdateFleetsHPs(data);

                this.Shelling(data.api_hougeki);
            }, "演習 - 夜戦");
        }

        private void Update(sortie_airbattle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku, "1回目/");
                this.Support(data.api_support_info, data.api_support_flag);
                this.AirCombat(data.api_kouku2, "2回目/", false);
            }, "航空戦 - 昼戦");
        }

        private void Update(sortie_ld_airbattle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);
            }, "空襲戦 - 昼戦", BattleResultType.LdAirBattle);
        }

        private void Update(combined_battle_ld_airbattle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);
            }, "連合艦隊 - 空襲戦 - 昼戦", BattleResultType.LdAirBattle);
        }

        private void Update(combined_battle_ec_battle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                // guess
                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack);

                this.Shelling(data.api_hougeki1);
                this.Torpedo(data.api_raigeki);
                this.Shelling(data.api_hougeki2);
                this.Shelling(data.api_hougeki3);
            }, "敵連合艦隊 - 昼戦");
        }

        private void Update(combined_battle_ec_midnight_battle data)
        {
            this.Update(() =>
            {
                this.UpdateFleetsHPsEc(data);

                var friendFleetIndex = data.api_active_deck[0];
                var enemyFleetIndex = data.api_active_deck[1];
                this.Shelling(data.api_hougeki, friendFleetIndex, enemyFleetIndex);
            }, "敵連合艦隊 - 夜戦");
        }

        private void Update(combined_battle_each_battle data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack);

                this.Shelling(data.api_hougeki1);
                this.Shelling(data.api_hougeki2);
                this.Torpedo(data.api_raigeki);
                this.Shelling(data.api_hougeki3);
            }, "機動部隊 - 敵連合艦隊 - 昼戦");
        }

        private void Update(combined_battle_each_battle_water data)
        {
            this.Update(() =>
            {
                this.UpdateInfo(data);

                this.InjectionAirCombat(data.api_injection_kouku);
                this.AirBaseAttack(data.api_air_base_attack);
                this.AirCombat(data.api_kouku);
                this.Support(data.api_support_info, data.api_support_flag);

                this.Shelling(data.api_opening_taisen);
                this.Torpedo(data.api_opening_atack);

                this.Shelling(data.api_hougeki1);
                this.Shelling(data.api_hougeki2);
                this.Shelling(data.api_hougeki3);
                this.Torpedo(data.api_raigeki);
            }, "水上部隊 - 敵連合艦隊 - 昼戦");
        }

        #endregion

        public void UpdateBattleResult(battle_result data)
        {
            this.DropShipName = data.api_get_ship?.api_ship_name;

            switch (data.api_win_rank)
            {
                case "S":
                    this.BattleResult = this.FriendFleetStatus.LostGauge > 0
                            ? BattleResultRank.勝利S
                            : BattleResultRank.完全勝利S;
                    break;
                case "A":
                    this.BattleResult = BattleResultRank.勝利A;
                    break;
                case "B":
                    this.BattleResult = BattleResultRank.戦術的勝利B;
                    break;
                case "C":
                    this.BattleResult = BattleResultRank.戦術的敗北C;
                    break;
                case "D":
                    this.BattleResult = BattleResultRank.敗北D;
                    break;
                case "E":
                    this.BattleResult = BattleResultRank.敗北E;
                    break;
            }

            this.FriendFleet.Fleets[1].UpdateMVP(data.api_mvp);
            this.FriendFleet.Fleets[2]?.UpdateMVP(data.api_mvp_combined);
        }

        private void UpdateFleetsByStartNext(map_start_next startNext, string api_deck_id = null)
        {
            this.IsInBattle = false;

            this.Clear();

            if (api_deck_id != null) this.CurrentDeckId = int.Parse(api_deck_id);
            if (this.CurrentDeckId < 1) return;

            this.UpdateFriendFleets();

            this.NextCell = new MapCellInfo(startNext);
        }

        private void UpdateFriendFleets(int deckId = -1)
        {
            if (deckId == -1) deckId = this.CurrentDeckId;
            if (KanColleClient.Current.Homeport.Organization.Combined && deckId == 1)
            {
                this.UpdateFriendFleetsByIndex(1, 2);
            }
            else
            {
                this.UpdateFriendFleetsByIndex(deckId);
            }
        }

        private void UpdateEnemyFleets(ICommonBattleMembers data, string enemyName)
        {
            this.EnemyFleet.Update(data.GetEnemyFleets());
            this.EnemyFleet.Name = enemyName;
        }

        private void Update(Action updateAction, string name, BattleResultType battleResultType = BattleResultType.Normal)
        {
            this.Name = name;
            this.IsInBattle = true;
            this.UpdatedTime = DateTimeOffset.Now;

            updateAction();

            if (battleResultType == BattleResultType.Normal)
            {
                this.BattleResult = this.PredictResult();
            }
            else
            {
                this.BattleResult = this.PredictResult2();
            }
        }

        private void UpdateInfoPractice(practice_battle data)
        {
            this.State = BattleState.Practice;

            this.NextCell = null;

            this.UpdateFriendFleets(data.api_dock_id);
            this.UpdateEnemyFleets(data, this._practiceEnemyName);
            this.UpdateFleetsHPs(data);

            this.UpdateFormation(data);
        }

        private void UpdateInfo<T>(T data)
            where T : ICommonBattleMembers, IBattleFormationInfo
        {
            this.State = BattleState.InSortie;

            // init
            this.AirCombatResults = new AirCombatResult[0];
            this.LandBaseAirCombatResults = new LandBaseAirCombatResult[0];

            this.UpdateFriendFleets();
            this.UpdateEnemyFleets(data, this.NextCell.EnemyName);
            this.UpdateFleetsHPs(data);

            this.UpdateFormation(data);
        }

        private void UpdateFriendFleetsByIndex(params int[] fleetIndex)
        {
            var fleets = fleetIndex
                .Select(i => KanColleClient.Current.Homeport.Organization.Fleets[i])
                .ToArray();

            this.FriendFleet.Update(
                fleets
                    .Select(f => new FleetData(f.Ships.Select(s => new MembersShipData(s))))
                    .ToArray()
            );

            this.FriendFleet.Name = fleets.Length == 1 ? fleets[0].Name : "";
        }

        private void UpdateFormation(IBattleFormationInfo data)
        {
            if (data.api_formation == null) return;

            this.FriendFleet.Formation = (Formation)data.api_formation[0];
            this.EnemyFleet.Formation = (Formation)data.api_formation[1];

            this.BattleSituation = (BattleSituation)data.api_formation[2];
        }

        private void UpdateFleetsHPs(ICommonBattleMembers data)
        {
            this.FriendFleet.Fleets[1].UpdateHPs(data.api_f_maxhps.GetFriendData(), data.api_f_nowhps.GetFriendData());
            this.EnemyFleet.Fleets[1].UpdateHPs(data.api_e_maxhps.GetEnemyData(), data.api_e_nowhps.GetEnemyData());

            if (this.FriendFleet.FleetCount > 1)
            {
                this.FriendFleet.Fleets[2].UpdateHPs(data.api_f_maxhps_combined.GetFriendData(), data.api_f_nowhps_combined.GetFriendData());
            }
            if (this.EnemyFleet.FleetCount > 1)
            {
                this.EnemyFleet.Fleets[2].UpdateHPs(data.api_e_maxhps_combined.GetEnemyData(), data.api_e_nowhps_combined.GetEnemyData());
            }
        }

        private void UpdateFleetsHPsEc(ICommonBattleMembers data)
        {
            this.FriendFleet.Fleets[1].UpdateHPs(data.api_f_maxhps.GetFriendData(), data.api_f_nowhps.GetFriendData());
            this.EnemyFleet.Fleets[1].UpdateHPs(data.api_e_maxhps.GetEnemyData(), data.api_e_nowhps.GetEnemyData());

            if (this.FriendFleet.FleetCount > 1)
            {
                // each_battle->ec_midnight_battle api_maxhp_combined[1..6]==api_nowhp_combined[1..6]
                this.FriendFleet.Fleets[2].UpdateNowHPs(data.api_f_nowhps_combined.GetFriendData());
            }
            if (this.EnemyFleet.FleetCount > 1)
            {
                this.EnemyFleet.Fleets[2].UpdateHPs(data.api_e_maxhps_combined.GetEnemyData(), data.api_e_nowhps_combined.GetEnemyData());
            }
        }

        private void Clear()
        {
            this.UpdatedTime = DateTimeOffset.Now;
            this.Name = "";
            this.DropShipName = "";

            this.BattleSituation = BattleSituation.なし;
            this.FriendAirSupremacy = AirSupremacy.航空戦なし;
            this.InjectionAirCombatResults = new AirCombatResult[0];
            this.LandBaseAirCombatResults = new LandBaseAirCombatResult[0];
            this.AirCombatResults = new AirCombatResult[0];
            this.FriendFleet.Clear();
            this.EnemyFleet.Clear();

            this.BattleResult = BattleResultRank.なし;
        }
    }
}
