# Sales Sub-System

> Name: **STUDENT_NAME**

| Mark | Area |
|:----:|:-----|
| **`TBA`**/1 | DISPLAYING – PRODUCTS FILTERED BY CATEGORY |
| **`TBA`**/3 | PROCESSING – ADD TO CART |
| **`TBA`**/1 | DISPLAYING – SALES - CLEAR CART |
| **`TBA`**/3 | DISPLAYING/PROCESSING –EDIT CART |
| **`TBA`**/6 | PROCESSING – PLACE ORDER |
| **`TBA`**/2 | DISPLAYING – DISPLAY INVOICE ON REFUND |
| **`TBA`**/5 | PROCESSING – PROCESS REFUND |
| **`TBA`**/1 | DISPLAYING – REFUND - CLEAR CART |
| | |
| **`TBA`**/**22** | **TOTAL** |

----

## Area Checklist

> **Note:** Additional notes/deductions may be added for unusual/problematic implementations.

- **`TBA`**/1 - DISPLAYING – PRODUCTS FILTERED BY CATEGORY
  - **UI**
    - [ ] Dropdown - Display list of categories 
    - [ ] Dropdown - Display products, filtered by category
  - **BLL**
    - [ ] Get list of categories 
    - [ ] Get list of products by category
- **`TBA`**/3 - PROCESSING – ADD TO CART
  - **UI**
    - [ ] Inform user of errors/success in adding to cart
    - [ ] Validation of Quantity to be positive integer if entered else numeric value - defaults to 1
    - [ ] check item not already on the cart
    - [ ] Totals correctly altered on Add
    - [ ] Totals calculation correct at all times
  - **BLL**
    - [ ] Does **not** alter tables
- **`TBA`**/1 - DISPLAYING – SALES - CLEAR CART
  - **UI**
    - [ ] Cart items cleared
    - [ ] Totals cleared
    - [ ] Product List cleared
    - [ ] Category re-set to drop-down prompt
    - [ ] Quantity set to default of 1
  - **BLL**
    - [ ] Does **not** alter tables
- **`TBA`**/3 - DISPLAYING/PROCESSING –EDIT CART
  - **UI**
    - [ ] UI - Validation quantity is positive numeric
    - [ ] UI - Update quantity of item in cart to value
    - [ ] UI - Remove item from cart
    - [ ] UI - Display all cart items / totals
    - [ ] UI - Totals calculation correct at all times
    - [ ] UI - Informs user after Update/Remove
  - **BLL**
    - [ ] Does **not** alter tables
- **`TBA`**/6 - PROCESSING – PLACE ORDER
  - **UI**
    - [ ] UI - Displays new invoice number
    - [ ] UI - Informs user of result of placing order
    - [ ] UI - Maintains display of sale items and totals
    - [ ] UI - Unable to add new items to sale after processing
  - **BLL**
    - [ ] SINGLE TRANSACTION!
    - [ ] Sales – create a sale record
    - [ ] SaleDetails – create saledetails 
    - [ ] StockItems – Update QuantityOnHand
- **`TBA`**/2 - DISPLAYING – DISPLAY INVOICE ON REFUND
  - **UI**
    - [ ] UI - Display list of Products 
    - [ ] UI - Display Restock Charge indicators correctly
    - [ ] UI - Display totals set to zero
  - **BLL**
    - [ ] Get requested invoice details
- **`TBA`**/5 - PROCESSING – PROCESS REFUND
  - **UI**
    - [ ] Inform user of errors/success in processing
    - [ ] Validation of Quantity to be positive integer
    - [ ] Display Refund Invoice # if successful
    - [ ] Totals calculation shows amount that was refunded including any restock charges
  - **BLL**
    - [ ] SINGLE TRANSACTION!
    - [ ] Validation
    - [ ] Item cannot be refund twice (once refunded, cannot be refunded again)
    - [ ] Uses full quantity sold as refund quantity
    - [ ] Products quantity on hand increased by refunded quantity
    - [ ] Invoice created for refund
    - [ ] Invoice totals calculated correctly, including any restock charges.
    - [ ] StoreRefunds record created for each refunded product.
    - [ ] Original Invoice record unaltered
- **`TBA`**/1 - DISPLAYING – REFUND - CLEAR CART
  - **UI**
    - [ ] No invoice displayed
    - [ ] Totals and invoice number cleared
