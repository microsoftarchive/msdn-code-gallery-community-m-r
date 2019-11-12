
DROP TABLE IF EXISTS tripdata;

CREATE EXTERNAL TABLE tripdata(license int, hack_license int, vendor_id string, rate_code int, store_and_fwd_flag string,
    pickup_datetime date, dropoff_datetime date, passenger_count int, trip_time_in_secs double, trip_distance double, 
    pickup_longitude double, pickup_latitude double, dropoff_longitude double, dropoff_latitude double)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/tripdata/'
tblproperties ("skip.header.line.count"="1");


DROP TABLE IF EXISTS accelerometerevents;

CREATE EXTERNAL TABLE accelerometerevents(eventdatetime timestamp, deviceid string, driverid int, x double, y double, z double)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/accelerometer/'
tblproperties ("skip.header.line.count"="1");


DROP TABLE IF EXISTS compassevents;

CREATE EXTERNAL TABLE compassevents(eventdatetime timestamp, deviceid string, driverid int, headingdegrees double)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/compass/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS odbevents;

CREATE EXTERNAL TABLE odbevents(eventdatetime timestamp, deviceid string, driverid int, code string)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/OBD/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS odbsecuritybeltevents;

CREATE EXTERNAL TABLE odbsecuritybeltevents(winstarttime string, winendtime string, driverId int, licenseplate string, name string, securityBeltWarnings int)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/OBD-SecurityBelt/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS rfidevents;

CREATE EXTERNAL TABLE rfidevents(eventdatetime timestamp, deviceid string, driverid int)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION 'wasb://myshuttledata@myshuttledemosto8.blob.core.windows.net/rfid/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS  accelerometeraggregate;

CREATE TABLE IF NOT EXISTS default.accelerometeraggregate(eventdate string,
       driverid int,
       XMaxPositive float,
       XAVGPositive float,
       XTypDevPositive float,
       XMaxNegative float,
       XAVGNegative float,
       XTypDevNegative float,
       YMaxPositive float,
       YAVGPositive float,
       YTypDevPositive float,
       YMaxNegative float,
       YAVGNegative float,
       YTypDevNegative float,
       ZMaxPositive float,
       ZAVGPositive float,
       ZTypDevPositive float,
       ZMaxNegative float,
       ZAVGNegative float,
       ZTypDevNegative float)
ROW FORMAT DELIMITED
        FIELDS TERMINATED BY '\001'
        COLLECTION ITEMS TERMINATED BY '\002'
        MAP KEYS TERMINATED BY '\003'
STORED AS ORC







