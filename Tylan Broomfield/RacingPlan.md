# Racing Page Plan

## UI Control

### Registration

- **Page_Load** - on the initial load of the page, employees will be presented with an `<asp:Calendar />` control

- **Date_Click** - upon clicking a date on the calendar, The BLL method `public int UseSelectedDate()` will be called in order to return the required id for a population method.
    - This will fill the `public List<ScheduleItem> ListDailySchedule(int RaceID)` method in order to fill the schedule section based of the returned value.

- **View_Click** - Clicking *View* on the Schedule section will pop another list containing The roster of the selected race, this will be done by accessing the `public List<RosterItem> Racers (int RaceDetailID)` BLL method which will fill the list with the required data regarding a specific race.

- **Edit_Click** - upon clicking the edit button, the BLL Method `public void UpdateAlbum(UpdateDriverInfo update)` will be called in order update the driver info.

- **Add_Click** - upon clicking the add button, the BLL method `public void AddDriver (AddDriverDetails driver)` is called and the neccesary info is updated. if certain fields are missing a validator will run and a UserMessage will display the required fields.

- **Record Race Times_Click** - this will take the user to the race results page for the given roster.

### Race Results

- **Save_Click** - Upon clicking the save button, the user will be met with a message saying "Confrim Times?". if the user slects yes, the BLL method `public void UpdateRaceTimes (RecordRaceTimes times)` will be called in order to update race times.

## BLL

```C#
public int UseSelectedDate() {/* query the Races table, and return a RaceID*/}
```

```C#
public List<ScheduleItem> ListDailySchedule(int RaceID) {/* will query from the RaceDetails, Races and CarClasses table */}
```

```C#
public List<RosterItem> Racers (int RaceDetailID) { /* will query from the Members, RaceFees, and CarClasses table*/}
```

```C#
public void UpdateDriver(UpdateDriverInfo update) 
{
    /* Run a command on the RaceDetails, Races, and CarClasses table*/
}
```

```C#
public void AddDriver (AddDriverDetails driver) 
{
    /* will Add the required information to the RaceDetails, Races, RaceFee, and CarClasses table*/
}
```

```C#
public void UpdateRaceTimes (RecordRaceTimes times)
{
    /* will add the required info into the RaceDetails and RacePenalties tables*/ 
}
```
## View Models
```C#
public class SelectDate
{
    public int RaceID {get; set;}

    public DateTime RaceDate {get; set;}

    public IEnumerable<ScheduleItem> SelectionItem {get; set;}
}
```

```C#
public class ScheduleItem
{
    public int RaceDetailID {get; set;}

    public time StartTime {get; set;}

    public string Competition {get; set;}

    public char Run {get; set;}

    public int DriverCount {get; set;}

}
```

```c#
public class RosterItem 
{
    public string Name {get; set;}

    public decimal RaceFee {get; set;}

    public decimal RenatlFee {get; set;}

    public bool Refund {get; set;} // even though it is listed as a bit, a boolean seems to better fit the context of the application
}
```

```C#
public class RaceResult
{
    public string Name {get; set;}

    public TimeSpan Time {get; set;}

    public string Penalties {get; set;}

    public string Placement {get; set;} // Will utilize the Humanizr nuget package.
}
```

## Commands

```C#
public class AddDriverDetails 
{
    public int MemberID {get; set;}

    public string Name {get; set;}

    public Double RaceFee {get; set;}

    public string CarClass {get; set;}

    public double RentalFee {get; set;}

    public string SerialNumber {get; set;}
}
```

```C#
public class UpdateDriverInfo
{
    public string Name {get; set;}

    public double RaceFee {get; set;}

    public IEnumerable<UpdateItems> Edit {get; set;}
}
```

```C#
public class UpdateItems
{
    public string Comment {get; set;}

    public string Reason {get; set;}

    public bool Refund {get; set;}

    public string CarDescription {get; set;}

    public double RentalFee {get; set;}

    public string SerialNumber {get; set;}
}
```

```C#
public class RecordRaceTimes
{
    public string Name {get; set;}

    public TimeSpan RaceTime {get; set;}

    public string Penalties {get; set;}

    public string Placement {get; set;} 
}
```