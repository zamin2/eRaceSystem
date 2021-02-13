# Receiving Development Plan  
***
## Implentation Plan  
### UI Interaction  
  - **Page_Load** - Have a dropdown list filled with orders that are not complete using the Method `List<SelectionItem> ListOrders()`  
      - Will only grab Orders that have a `Closed` value of `0`  
      - Will also grab the `Vendor Name` and add it to the end of the `Text` value
  - **Open_Click** - Used to grab the information needed for the form  
      - The customer information will come from the Method `VendorContactInfo GetVendorContact(int orderId)`
      - The OrderDetail information will come from the Method `List<OrderDetailInfo> GetOrderDetails(int orderId)`
      - Clear the `UnOrderedItems` Table with the Method `void ClearUnOrderedItems()`
  - **ForceClose_Click** - Used to close the Force the Order Closed if the order can't be completed
      - Prompt the user to ensure they want to close the order
      - Will edit the information of each OrderDetail with the Method `void ForceCloseOrder(ForceCloseDetails order)`
  - **ReceiveShipment_Click** - Used to receive the current shimpment
      - Will call the Method `void ReceiveShipmentOrder(ReceivedOrderDetails order)`
          
### BLL  
The BLL will consist of a `ReceivingController` with the following methods
```csharp
public List<SelectionItem> ListOrders ()
{ /* query from Orders */ }

public VendorContactInfo GetVendorContact (int orderId)
{ /* query from Orders & Vendors */ }

public List<OrderDetailInfo> GetOrderDetails (int orderId)
{ /* query from Orders, OrderDetails, ReceivedOrderItems & Products */ }

public void ClearUnOrderedItems ()
{ /* use CRUD to delete all items in table (Try .Clear()) */ }

public void ForceCloseOrder (ForceCloseDetails order)
{ /* command modify Products, Order and OrderDeatails */ }

public void ReceiveShimpmentOrder (ReceivedOrderDetails order)
{ /* command modify Products, ReceiveOrders, ReceiveOrderItems and ReturnOrderItems */ }
```
### View Models  
#### Queries
```csharp
public class SelectionItem
{
  public int IDValue { get; set; }
  public string DisplayValue { get; set; }
}

public class VendorContactInfo
{
  public id VendorId { get; set; }
  public string Name { get; set; }
  public string Address { get; set; } // looks like (Address) (City)
  public string Contact { get; set; }
  public string Phone { get; set; }
}

public class OrderDetailInfo
{
  public string ProductName { get; set; }
  public int BulkQuantityOrdered { get; set; }
  public int ItemUnitSize { get; set; }
  public int ItemQuantityReceived { get; set; }
}
```
#### Commands
```csharp
public class ForceCloseDetails
{
  public int OrderID { get; set; }
  public bit Closed { get; set; }
  public string Comment { get; set; }
  public IEnumerable<ForceCloseItems> Items { get; set; }
}

public class ForceCloseItems 
{
  public int OrderDetailID { get; set; }
  public int Quantity { get; set; }
  public int QuantityOnOrder { get; set; }
}

public class ReceivedOrderDetails
{
  public int OrderID { get; set; }
  public bit Closed { get; set; }
  public string Comment { get; set; }
  public ReceiveOrdersInfo ReceivedOrder { get; set; }
  public IEnumerable<ReceiveOrderItemDetails> ReceiveOrderItems { get; set; }
  public IEnumerable<ProductDetails> Products { get; set; }
  public IEnumerable<ReturnOrderItemDetails> ReturnOrderItems { get; set; }
}

public class ReceiveOrdersInfo
{
  public int OrderID { get; set; }
  public datetime ReceiveDate { get; set; }
  public int EmployeeID { get; set; }
}

public class ReceiveOrderItem
{
  public int ReceiveOrderID { get; set; }
  public int OrderDetailID { get; set; }
  public int ItemQuantity { get; set; }
}

public class ProductDetails
{
  public int QunatityOnOrder { get; set; }
  public int QuantityOnHand { get; set; }
}

public class ReturnOrderItemDetails
{
  public int ReceiveOrderID { get; set; }
  public int? OrderDetailID { get; set; }
  public string UnOrderedItem { get; set; }
  public int ItemQuantity { get; set; }
  public string Comment { get; set; }
  public int? VendorProductID { get; set; }
}
```
