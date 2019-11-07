

DROP TABLE IF EXISTS odbsecuritybelteventsinput;

CREATE EXTERNAL TABLE odbsecuritybelteventsinput(winstarttime string, winendtime string, driverId int, licenseplate string, name string, securityBeltWarnings int)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION '${hiveconf:INPUT}/'
tblproperties ("skip.header.line.count"="1");

DROP TABLE IF EXISTS odbsecuritybelteventsoutput;

CREATE EXTERNAL TABLE odbsecuritybelteventsoutput(winstarttime string, winendtime string, driverId int, licenseplate string, name string, securityBeltWarnings int)
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' 
STORED AS TEXTFILE LOCATION '${hiveconf:RESULTOUTPUT}/${hiveconf:Year}_${hiveconf:Month}_${hiveconf:Day}';

INSERT OVERWRITE TABLE odbsecuritybelteventsoutput
    select winstarttime,winendtime,driverId, licenseplate, name, securityBeltWarnings As NumWarnings from odbsecuritybelteventsinput
