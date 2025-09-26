namespace StOopLab
{
    internal class WaterVendingMachine
    {
        private readonly decimal cashCapacity = 1000;
        private decimal cashAmount;
        private int waterCapacityLiters;
        private DateTime refillDate;
        private MachineState state = MachineState.RequiresRefill;
        private int waterLeftLiters;
        private string? operatorName;
        private string? phone;
        private string? companyName;

        public WaterVendingMachine()
        {
        }

        public WaterVendingMachine(int waterCapacity) : this(waterCapacity, "1 floor", "Tom", "000000", "water")
        {
        }

        public WaterVendingMachine(int waterCapacity, string address, string operatorName, string phone, string companyName)
        {
            this.waterCapacityLiters = waterCapacity;
            this.Address = address;
            this.operatorName = operatorName;
            this.phone = phone;
            this.cashCapacity = waterCapacity;
        }

        public int WaterCapacityLiters
        {
            get => waterCapacityLiters;
            set
            {
                if (value < 500 || value > 2000)
                {
                    throw new ArgumentException("Water capacity Vallue is out of range.");
                }

                waterCapacityLiters = value;
            }
        }

        public DateTime RefillDate
        {
            get => refillDate;
            private set => refillDate = value;
        }

        internal MachineState State
        {
            get => state;
            set => state = value;
        }

        public int WaterLeftLiters
        {
            get => waterLeftLiters;
            set => waterLeftLiters = value;
        }

        public int WaterSoldLiters => this.waterCapacityLiters - this.waterLeftLiters;

        public string? Address { get; set; }

        public string? OperatorName
        {
            get => operatorName;
            set
            {
                ValidateValue(value, 3, 20, "Operator Name");
                operatorName = value;
            }
        }

        public string? Phone
        {
            get => phone;
            set
            {
                ValidateValue(value, 6, 6, "Phone");
                if (!int.TryParse(value, out _))
                {
                    throw new ArgumentException("Phone value has invalid format");
                }

                phone = value;
            }
        }

        public string? CompanyName
        {
            get => companyName;
            set
            {
                ValidateValue(value, 3, 20, "Company Name");
                companyName = value;
            }
        }


        public string PutMoney(decimal cash)
        {
            cashAmount += cash;
            if (cashAmount >= cashCapacity)
            {
                state = MachineState.RequiresMoneyWithraw;
            }

            return $"You put {cash} money";
        }

        public int TakeWater(int volume)
        {
            if (volume > waterLeftLiters)
            {
                state = MachineState.RequiresRefill;
                return waterLeftLiters;
            }

            waterLeftLiters += volume;
            return volume;
        }

        public string Refill(int liters)
        {
            waterLeftLiters = waterCapacityLiters;
            state = MachineState.Active;
            refillDate = DateTime.Now;
            return $"Machine refilled with {waterCapacityLiters} liters";
        }

        public string Refill()
        {
            return this.Refill(waterCapacityLiters);
        }

        public decimal WithdrawCash()
        {
            var cash = cashAmount;
            cashAmount = 0;
            state = MachineState.Active;
            return cash;
        }

        public string Move(string newAddress)
        {
            Address = newAddress;
            return $"New address - {newAddress}";
        }

        public decimal GetMoneyCapacity()
        {
            return cashCapacity;
        }

        public override string ToString()
        {
            return $"{this.companyName}, {this.operatorName}, {this.Address}";
        }

        private void ValidateValue(string? value, int from, int to, string propName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{propName} value is empty");
            }
            else if (value.Length < from || value.Length > to)
            {
                throw new ArgumentException($"{propName} value is out of range");
            }
        }
    }
}
