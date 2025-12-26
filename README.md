The application represents functionality for the creation and management of water vending machines.

# Water Vending Machine — Lab 3

## Class Description
The project extends the **Water Vending Machine** class from Lab-2 by adding
**constructor overloading** and **method overloading**.

---

## Constructors
The class contains **three overloaded constructors**:
- Default constructor (automatic field initialization)
- Constructor with full manual input:
  - `Address`
  - `OperatorName`
  - `Phone`
  - `CompanyName`
  - `WaterCapacityLiters`
- Constructor with partial input:
  - `WaterCapacityLiters` only  
  (other fields are set automatically)

---

## Fields and Properties
All fields are **private** and accessed via **public properties**.

### Key Properties
- `WaterCapacityLiters`
- `WaterLeftLiters`
- `RefillDate`
- `State`
- `Address` (auto-property with default value)
- `OperatorName`
- `Phone`
- `CompanyName`
- `WaterSoldLiters` (computed property)

---

## Methods
- `PutMoney(decimal cash)`
- `TakeWater(int volume)`
- `Refill()`
- `Refill(int liters)` — **overloaded method**
- `WithdrawCash()`

---

## Menu
1. Add object  
2. View all objects  
3. Find object  
4. Demonstrate behavior  
5. Delete object  
0. Exit  

---

## Object Creation Options
- Create default
- Manually enter data
- Manually enter data partly
- Exit to main menu
