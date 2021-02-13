# Racing Sub-System

> Name: **STUDENT_NAME**

| Mark | Area |
|:----:|:-----|
| **`TBA`**/2 | DISPLAYING – FILTER SEARCH TO ROSTER |
| **`TBA`**/6 | PROCESSING – ADD RACER |
| **`TBA`**/4 | PROCESSING – EDIT ROSTER DRIVER CART SELECTION |
| **`TBA`**/4 | DISPLAYING – EDIT REFUND ROSTER DRIVER |
| **`TBA`**/1 | DISPLAYING – RACE RESULTS |
| **`TBA`**/5 | PROCESSING – SAVE RACE TIMES |
| | |
| **`TBA`**/**22** | **TOTAL** |

----

## Area Checklist

> **Note:** Additional notes/deductions may be added for unusual/problematic implementations.

- **`TBA`**/2 - DISPLAYING – FILTER SEARCH TO ROSTER
  - **UI**
    - [ ]  Display Schedule Races list for selected date
    - [ ]  Display Roster list for selected race
  - **BLL**
    - [ ]  Get schedule list of races for selected date
    - [ ]  Get roster list for selected race
- **`TBA`**/6 - PROCESSING – ADD RACER
  - **UI**
    - [ ] Select driver
    - [ ] Select race classification
    - [ ] Optionally select car
    - [ ] Shows race fee on ADD
    - [ ] Refreshes roster display on Add showing new driver and correct values
  - **BLL**
	- [ ] Validation –
      - [ ] Driver classification matches race classification
      - [ ] If car is selected, driver classification meets car classification
      - [ ] Driver cannot already be registered for race
    - [ ] SINGLE TRANSACTION! ADD
      - [ ] create new RaceDetail with supplied data
      - [ ] create new Invoices record
- **`TBA`**/4 - PROCESSING – EDIT ROSTER DRIVER CART SELECTION
  - **UI**
    - [ ] Select Classification
    - [ ] Select cart by vin (only certified carts should be in list) 	Validation:
  - **BLL**
	- [ ] Validation –
      - [ ] Driver classification matches race classification
      - [ ] Cart classification matches driver classification
      - [ ] Cart state must be certified to run
      - [ ] Vehicle cannot be double-booked
    - [ ] SINGLE TRANSACTION!
      - [ ] Update RaceDetails with cart information
      - [ ] Update Invoice totals with cart fees
- **`TBA`**/4 - DISPLAYING – EDIT REFUND ROSTER DRIVER
  - **UI**
    - [ ] Reason must exist
    - [ ] Driver cannot get more than one refund for selected race
  - **BLL**
	- [ ] Validation:
      - [ ] Reason must exist
      - [ ] Only one refund for driver for selected race
    - [ ] SINGLE TRANSACTION!
      - [ ] Update RaceDetails with refund information
      - [ ] Update Invoice totals with refunded amount
- **`TBA`**/1 - DISPLAYING – RACE RESULTS
  - **UI**
    - [ ] Displays race results roster
  - **BLL**
    - [ ] Get race drivers who were not scratched
- **`TBA`**/5 - PROCESSING – SAVE RACE TIMES
  - **UI**
    - [ ] Racers without race times must have penalty
    - [ ] Penalty comment (optionally) is entered
    - [ ] Collect all finish times and send for processing
    - [ ] Placements are displayed if save is successful
    - [ ] Placements are correct
  - **BLL**
    - [ ] Validation:
      - [ ] Race times are exists for all drivers completing the race
      - [ ] Missing finish time must have a penalty
      - [ ] No negative race times
    - [ ] SINGLE TRANSACTION!
      - [ ] Update RaceDetails with race times
      - [ ] Update RaceDetails with placement finishes
      - [ ] Placement finishes are calculated in transaction
      - [ ] Update RaceDetails with any penalty and reason
