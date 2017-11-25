using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace BattleInfoPlugin.Models
{
    public class BattleFleet : NotificationObject
    {
        #region Name

        private string _name;

        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Formation

        private Formation _formation;

        public Formation Formation
        {
            get { return this._formation; }
            set
            {
                if (this._formation != value)
                {
                    this._formation = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Rank

        private int[] _rank;

        public int[] Rank
        {
            get
            { return this._rank; }
            set
            {
                if (this._rank != value)
                {
                    this._rank = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        public FleetType Type { get; }

        #region Fleets

        private IReadOnlyList<FleetData> _fleets;

        public IReadOnlyList<FleetData> Fleets
        {
            get { return this._fleets; }
            private set
            {
                if (this._fleets != value)
                {
                    this._fleets = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        public int FleetCount => this._fleets.Count;

        public BattleFleet(FleetType type)
        {
            this.Type = type;
            this.Fleets = EmptyFleet;
        }

        public void Update(params FleetData[] fleets)
        {
            for (var i = 0; i < fleets.Length; i++)
            {
                fleets[i].Index = i;
                fleets[i].FleetType = this.Type;
            }
            this.Fleets = new ItemsCollection<FleetData>(fleets);
        }

        public ShipData GetShip(int index)
        {
            index -= 1;
            var fleetIndex = index / this.Fleets[1].Ships.Count + 1;
            var shipIndex = index % this.Fleets[1].Ships.Count + 1;
            return this.Fleets[fleetIndex].Ships[shipIndex];
        }

        public void Clear()
        {
            this.Name = "";
            this.Formation = Formation.なし;
            this.Rank = DefaultRank;
            this.Fleets = EmptyFleet;
        }

        public static implicit operator BattleFleet(FleetData fleet)
        {
            var result = new BattleFleet(fleet.FleetType)
            {
                Name = fleet.Name,
                Formation = fleet.Formation,
                Rank = fleet.Rank
            };
            result.Update(fleet);

            return result;
        }

        public static implicit operator FleetData(BattleFleet fleet)
        {
            return new FleetData(fleet.Fleets.SelectMany(f => f.Ships), fleet.Name, fleet.Type, fleet.Rank)
            {
                Formation = fleet.Formation
            };
        }

        private static readonly IReadOnlyList<FleetData> EmptyFleet = new ItemsCollection<FleetData>(new FleetData[0]);
        private static readonly int[] DefaultRank = { 0 };
    }
}
