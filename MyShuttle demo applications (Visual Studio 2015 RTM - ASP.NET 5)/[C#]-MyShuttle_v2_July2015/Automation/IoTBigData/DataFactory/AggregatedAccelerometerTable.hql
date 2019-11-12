USE default;

DROP TABLE IF EXISTS accelerometereventsinput;
CREATE EXTERNAL TABLE accelerometereventsinput(eventdatetime timestamp, deviceid string, driverid int, x double, y double, z double)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION '${hiveconf:INPUT}/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS  accelerometeraggregateoutput;

CREATE TABLE IF NOT EXISTS accelerometeraggregateoutput(eventdate string,
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
STORED AS ORC;


INSERT OVERWRITE TABLE accelerometeraggregateoutput
    SELECT EventDate
        , accelerometereventsinput.driverid
        , if(max(x) <= 0, 0, max(x)) as XMaxPositive
        , XAVGPositive
        , if(max(x) <= 0, 0, sqrt(((sum(pow(if(x > 0, x - XAVGPositive, 0), 2)) / sum(if(x > 0, 1, 0)))))) as XTypDevPositive
        , if(min(x) >= 0, 0, min(x)) as XMaxNegative
        , XAVGNegative
        , if(min(x) >= 0, 0, sqrt(((sum(pow(if(x < 0, x - XAVGNegative, 0), 2)) / sum(if(x < 0, 1, 0)))))) as XTypDevNegative
        , if(max(y) <= 0, 0, max(y)) as YMaxPositive
        , YAVGPositive
        , if(max(y) <= 0, 0, sqrt(((sum(pow(if(y > 0, y - YAVGPositive, 0), 2)) / sum(if(y > 0, 1, 0)))))) as YTypDevPositive
        , if(min(y) >= 0, 0, min(y)) as YMaxNegative
        , YAVGNegative
        , if(min(y) >= 0, 0, sqrt(((sum(pow(if(y < 0, y - YAVGNegative, 0), 2)) / sum(if(y < 0, 1, 0)))))) as YTypDevNegative 
        , if(max(z) <= 0, 0, max(z)) as ZMaxPositive
        , ZAVGPositive
        , if(max(z) <= 0, 0, sqrt(((sum(pow(if(z > 0, z - ZAVGPositive, 0), 2)) / sum(if(z > 0, 1, 0)))))) as ZTypDevPositive
        , if(min(z) >= 0, 0, min(z)) as ZMaxNegative
        , ZAVGNegative
        , if(min(z) >= 0, 0, sqrt(((sum(pow(if(z < 0, z - ZAVGNegative, 0), 2)) / sum(if(z < 0, 1, 0)))))) as ZTypDevNegative 
    FROM accelerometereventsinput 
        JOIN 
            (   
             SELECT to_date(eventdatetime) AS EventDate
                , driverid
                , if(max(x) <= 0, 0, (sum(if(x > 0, x, 0)) / sum(if(x > 0, 1, 0)))) as XAVGPositive
                , if(min(x) >= 0, 0, (sum(if(x < 0, x, 0)) / sum(if(x < 0, 1, 0)))) as XAVGNegative 
                , if(max(y) <= 0, 0, (sum(if(y > 0, y, 0)) / sum(if(y > 0, 1, 0)))) as YAVGPositive
                , if(min(y) >= 0, 0, (sum(if(y < 0, y, 0)) / sum(if(y < 0, 1, 0)))) as YAVGNegative 
                , if(max(z) <= 0, 0, (sum(if(z > 0, z, 0)) / sum(if(z > 0, 1, 0)))) as ZAVGPositive
                , if(min(z) >= 0, 0, (sum(if(z < 0, z, 0)) / sum(if(z < 0, 1, 0)))) as ZAVGNegative 
            FROM accelerometereventsinput
            GROUP BY to_date(eventdatetime), driverid
        ) averages 
            ON (to_date(accelerometereventsinput.eventdatetime) = averages.EventDate AND accelerometereventsinput.driverid = averages.driverid)
        WHERE TO_DATE(EventDate) >= TO_DATE(from_unixtime(unix_timestamp()-1*60*60*24, 'yyyy-MM-dd HH:mm:ss'))
    GROUP BY EventDate, accelerometereventsinput.driverid, XAVGPositive, XAVGNegative, YAVGPositive, YAVGNegative, ZAVGPositive, ZAVGNegative;