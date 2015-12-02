# Note
? Uncertain Feature
+ New feature/function
- Remove feature/function
* Comment

################	FEATURE NOTE		################
? (Item) could have price for make it more realistic
? (Employee) could have salaries, specialist, variable of skill's level

################	RELEASE NOTE		################

(2015-12-02)
+ Controller: Order & Items Management

(2015-12-01)
+ SQL: Database procedure

(2015-12-30)
+ SQL: Create Database & Tables

################	INSTRUCTION NOTE    ################
# First step of application when start up, application will initiate (Customer) object for put in the work line
# Application looking for (Customer)'s (Order) and listing (OrderLine), validate available (Item) in stock
# If (Item) are available in stock the system will put deliverDate from NULL to actual Date
# If (Item) are not available in stock, the system will waiting on (Machine) to produce the (Item)
# If (Machine) is not ACTIVE the system will looking the available (Employee) to initiate the (Machine)'s activation
# Normally the system will make work schedules for employees to set production in motion, but system have threshold for (Item) in stock, too slow down the production process.
# Certain threshold must be set as ENUM in 3 levels {Active, Passive, Stop},   Active < Passive < Stop
* Application can build in to complex calculation the finance flow, make dynamic work schedule after the need, dynamic notification the need of (Machine) or (Employee)

# The Application is act like a server to serve and control work processing and production
# General object MUST have data in database and relying data 
# Applying Thread Timer as Production of item's quantities
# Manage Controller and Item and Machine to co-operate