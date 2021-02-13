# Purchasing Sub-System

> Name: **STUDENT_NAME**

| Mark | Area |
|:----:|:-----|
| **`TBA`**/2 | DISPLAYING – VENDORS PROCESSING |
| **`TBA`**/4 | PROCESSING – ADD ITEMS TO CURRENT ACTIVE ORDER |
| **`TBA`**/3 | DISPLAYING/PROCESSING – UPDATE/REMOVE ITEMS FROM CURRENT ACTIVE ORDER |
| **`TBA`**/3 | PROCESSING – DELETE/CANCEL CURRENT ACTIVE ORDER |
| **`TBA`**/4 | PROCESSING – SAVE CURRENT ACTIVE ORDER |
| **`TBA`**/6 | PROCESSING – PLACE CURRENT ACTIVE ORDER |
| | |
| **`TBA`**/**22** | **TOTAL** |

----

## Area Checklist

> **Note:** Additional notes/deductions may be added for unusual/problematic implementations.

- **`TBA`**/2 - DISPLAYING – VENDORS PROCESSING
  - **UI**
    - [ ] Wire Vendor List to form
    - [ ] Display Current Active order (if existing)
    - [ ] Display vendor inventory by category by product
    - [ ] Display vendor information on selection
    - [ ] Subtotals correct for Current Active order (if existing) otherwise set to zero.
  - **BLL**
    - [ ] Get Vendor List
    - [ ] Obtains data for existing Current Active order
- **`TBA`**/4 - PROCESSING – ADD ITEMS TO CURRENT ACTIVE ORDER
  - **UI**
    - [ ] Add item to purchase list
    - [ ] Subtotals correct for Current Active order (if existing) otherwise set to zero.
    - [ ] Product can only appear once on current order
    - [ ] Handling of additional adds of product to current order (message or automatic update)
    - [ ] Unable to alter vendor once order is started / retrieved unless saved, placed, cancelled or deleted
  - **BLL**
    - [ ] No alternations to database
- **`TBA`**/3 - DISPLAYING/PROCESSING – UPDATE/REMOVE ITEMS FROM CURRENT ACTIVE ORDER
  - **UI**
    - [ ] Remove item from order (remove from list)
    - [ ] Extended costs correct
    - [ ] Subtotals display correctly when adding/removing from order
    - [ ] Allows price and/or quantity to be altered
    - [ ] Validation positive values only
    - [ ] Warning symbol properly appears
  - **BLL**
    - [ ] No alternations to database
- **`TBA`**/3 - PROCESSING – DELETE/CANCEL CURRENT ACTIVE ORDER
  - **UI**
    - [ ] Clears display
    - [ ] Reset for Vendor selection
    - [ ] Reset totals
    - [ ] Cancel clears display, web page process ONLY
  - **BLL**
    - [ ] SINGLE TRANSACTION!
      - [ ] OrderDetails – remove Current Active order detail records if present on database
      - [ ] Orders – remove Current Active order record
- **`TBA`**/4 - PROCESSING – SAVE CURRENT ACTIVE ORDER
  - **UI**
    - [ ] Informs user on result of processing order
    - [ ] Collects all data for processing Current Active order
  - **BLL**
    - [ ] Validation
      - [ ] Refreshes display with correct totaling
    - [ ] SINGLE TRANSACTION!
      - [ ] OrderDetails – Creates/Updates/Deletes record(s) values as appropriate
      - [ ] Order –
          - [ ] Create record if new current order
          - [ ] Update subtotal and gst on exsiting current order
- **`TBA`**/6 - PROCESSING – PLACE CURRENT ACTIVE ORDER
  - **UI**
    - [ ] Informs user on result of placing order
    - [ ] Collects all data for processing Current Active order
  - **BLL**
    - [ ] Validation
      - [ ] Refreshes display with correct totaling
    - [ ] TRANSACTION:
      - [ ] Does an Update of Current Active Order
      - [ ] Generates new OrderNumber (next highest value)
      - [ ] Sets OrderDate to Today
      - [ ] Update Products QuantityOnOrder
