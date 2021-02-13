# Sales Implementation planning
##UI Interactions 
- **Page Load** - Two dropdown list, one for the Category, one for the products, One editable box to enter number of the product.
 
  - _Add() - Add number of the product into the display list
  - _RefreshButton - Refresh the calculation of the amount,subtotal,tax,total
  - _DeleteButton - Delete the product from the display list 
  - _PaymentButton - Add all the infomation into the database
  - _ClearButton - clear all the information
- **Refund** -
  - _lookupButton - search the Invoice detail using Invoice number 
  - _Refund - update all the information into the database
  - _ClearButton - clear all the information
  
## ViewModels
```csharp
    public class SaleCartDisplay
    {
        public int EmployeeID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
    
       public class Products
    {
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public int ReOrderLevel { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityOnOrder { get; set; }
        public string OrderUnitType { get; set; }
    }
    
     public class RefundDetail
    {
        public string Product { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal RestockCharge { get; set; }        
        public string Reason { get; set; }
    }
    
```


## BLL
```csharp
public List<Category> Categories_List()
 {
  /* return the category to list */
 }
public int Get_CategoryID(string product)
 {
  /* get category using product */
 }
public void Add_ProductToSaleCart(SalesCartItem item)
 {
  /* add the product into the display cart */
 }
public void Product_UpdateQty(SalesCartItem item)
 {
  /* update the product quantity using the refresh button */
 }
 public int Product_DeleteFromCart(SalesCartItem item)
 {
  /* delete the product form the display cart */
 }
public List<SaleCartDisplay> SalesCartItems_List(int empID)
 {
   /* select the information from database into the display cart */
 }
public int Check_Refunded(int originalInvoiceID, int productID)
 {
   /* check if the product already refund or not */
 }
public Invoice Get_Invoice(int id)
 {
  /* get the invoice detail from the database if exist */
 }

```
