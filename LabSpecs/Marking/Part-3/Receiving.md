# Receiving Sub-System

> Name: **STUDENT_NAME**

| Mark | Area |
|:----:|:-----|
| **`TBA`**/1 | DISPLAYING – PURCHASE ORDERS TO BE RECEIVED |
| **`TBA`**/3 | DISPLAYING – SELECT ORDER FOR PROCESSING |
| **`TBA`**/2 | DISPLAYING – UNORDERED PRODUCTS |
| **`TBA`**/5 | PROCESSING – FORCED CLOSURE |
| **`TBA`**/11 | PROCESSING – RECEIVE ORDER W. RETURNS |
| | |
| **`TBA`**/**22** | **TOTAL** |

----

## Area Checklist

> **Note:** Additional notes/deductions may be added for unusual/problematic implementations.

- **`TBA`**/1 - DISPLAYING – PURCHASE ORDERS TO BE RECEIVED
  - **UI**
    - [ ] Outstanding Order View displays requested data
  - **BLL**
    - [ ] Get Order List of only outstanding placed orders
- **`TBA`**/3 - DISPLAYING – SELECT ORDER FOR PROCESSING
  - **UI**
    - [ ] Display Vendor information
    - [ ] Display existing outstanding order
    - [ ] Values correct
  - **BLL**
    - [ ] Obtains Outstanding Order
    - [ ] Outstanding quantities correctly calculated
    - [ ] OrderDetail quantities correct
    - [ ] Empty UnOrderedItems database table
- **`TBA`**/2 - DISPLAYING – UNORDERED PRODUCTS
  - **UI**
    - [ ] List/Add/Remove UI
  - **BLL**
    - [ ] Add an unordered product
    - [ ] Remove an unordered product
    - [ ] Add an unordered product
    - [ ] Remove an unordered product
- **`TBA`**/5 - PROCESSING – FORCED CLOSURE
  - **UI**
    - [ ] Refresh view after receiving
    - [ ] Outstanding order list refreshed
    - [ ] User is prompted for confirmation
  - **BLL**
    - [ ] SINGLE TRANSACTION!
    - [ ] Validation reason exists
    - [ ] Orders – Update
      - [ ] .Closed = true
      - [ ] .Notes = reason
    - [ ] Products – Update
      - [ ] .QuantityOnOrder -= outstanding quantity values correct
- **`TBA`**/11 - PROCESSING – RECEIVE ORDER W. RETURNS
  - **UI**
    - [ ] Refresh view after receiving
    - [ ] Single call to BLL with
      - [ ] Order number submitted
      - [ ] All Order details submitted that need processing
    - [ ] Outstanding order list refreshed (in case order was closed)
  - **BLL**
    - [ ] SINGLE TRANSACTION!
    - [ ] Validation
      - [ ] Minimum one item from order list or minimum one unordered item to process
      - [ ] Values validated
      - [ ] Returns have a reason
      – [ ] cannot receive more items than order outstanding quantity to next highest order unit size
      - [ ] returned items must have a reason
    - [ ] ReceiveOrders – new record
    - [ ] ReceiveOrderDetails – new record for each item for which product has been received
    - [ ] ReturnedOrderDetails – new record for each item for which product has been returned
    - [ ] Unordered Items – new ReturnedOrderDetail for each unorder item
    - [ ] Products – Update
      - [ ] .QuantityOnHand += details quantity
      - [ ] .QuantityOnOrder -= details quantity
    - [ ] Orders – Update to closed if all product has been received for the order
