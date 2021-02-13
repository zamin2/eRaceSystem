# Implementation planning
## UI Interactions
- **Page_Load** - Have a list of Vendors in a DropDownList. This will be from the BLL method ```List<SelectionItem> ListVendors()```
- **_Click()**
    - Select -> Gathers information from the form and calls the BLL methods: ```VendorInfo GetVendorInfo(int vendorid)```, ```VendorProducts GetVendorProducts``` & ```OrderInfo GetOrder(int vendorid)```
    - Place Order -> Gathers information from the form and calls the BLL method ```void PlaceOrder(UpdatedOrder item)```
    - Save -> Gathers information from the form and calls the BLL method ```void SaveOrder(UpdatedOrder item)```
    - Delete -> Gathers information from the form and calls the BLL method ```void DeleteOrder(int vendorid)```
    - Cancel -> It will clear the web page and reset the vendor list to the vendor prompt line. After clicking it, only the vendor dropdown list and select button will be visible in the webpage.
    - ![refresh-button](./refresh-button.PNG) -> It will gather information from the form and calls the BLL method ```decimal GetItemCost(int productid)```
    
## ViewModels
### Queries
```csharp
public class SelectionItem
{
    public string IDValue { get; set; }
    public string DisplayText { get; set; }
}
```
```csharp
public class VendorInfo {
    public string Name {get;set;}
    public string Address {get;set;}
    public string City {get;set;}
    public string PostalCode {get;set;}
    public string Phone {get;set;}
    
}
```
```csharp
public class VendorProducts {
    public IEnumerable<VendorCatalogInfo> Items { get; set; }
}
```
```csharp
public class VendorCatalogInfo{
    public int VendorCatalogID {get;set;}
    public string Category {get;set;}
    public int ProductID {get;set;}
    public string ProductName {get;set;}
    public int ReorderQty {get;set;}
    public int QuantityOnHand {get;set;}
    public int QuantityOnOrder {get;set;}
    public string UnitSize {get;set;}
}
```
```csharp
public class OrderInfo{
    public decimal SubTotal {get;set;}
    public decimal Tax {get;set;}
    public decimal Total {get;set;}
    public string VendorComment {get;set;}
    public List<OrderDetailInfo> OrderDetails {get;set;}
}
```
```csharp
public class OrderDetailInfo{
    public int OrderDetailID {get;set;}
    public int ProductID {get;set;}
    public string ProductName {get;set;}
    public int OrderQty {get;set;}
    public int UnitSize {get;set;}
    public decimal UnitCost {get;set;}
    public decimal ItemCost {get;set;}
    public decimal ExtendedCost {get;set;}
}
```


### Commands
```csharp
public class UpdatedOrder{
    public int? OrderID {get;set}
    public int? OrderNumber {get;set;}
    public DateTime? OrderDate {get;set;}
    public int VendorID {get;set;}
    public string VendorComment {get;set;}
    public decimal SubTotal {get;set;}
    public decimal Tax {get;set;}
    public decimal Total {get;set;}
    public List<UpdatedOrderDetail> UpdatedOrderDetails {get;set;} 
}
```
```csharp
public class UpdatedOrderDetail{
    public int? OrderDetailID{get;set;}
    public int ProductID {get;set;}
    public decimal UnitCost {get;set;}
    public int OrderQty {get;set;}
}
```

## BLL
The BLL will consist of an ```PurchasingController``` supporting the following methods:

```csharp
public List<SelectionItem> ListVendors()
{ /* query from Vendors */ }
```
```csharp
public VendorInfo GetVendorInfo(int vendorid){
    /* query from Vendors */
}
```
```csharp
public VendorProducts GetVendorProducts(int vendorid){
    /* query from Vendors, VendorCatalogs & Products */
}
```
```csharp
public  OrderInfo GetOrder(int vendorid){
    /* query from Orders, OrderDetails, Products*/
}
```
```csharp
public decimal GetItemCost(int productid){
    /* query from Products table */
}
```
```csharp
public void PlaceOrder(UpdatedOrder item){
    /* command modifying the Orders, OrderDetails tables. */
}
```
```csharp
public void SaveOrder(UpdatedOrder item){
    /* command modifying the Orders, OrderDetails tables. This function will keep OrderDate and OrderNumber of the Order table to NULL.
    */
}
```
```csharp
public void DeleteOrder(int vendorid){
    /* command modifying the Orders, OrderDetails tables. */
}
```
