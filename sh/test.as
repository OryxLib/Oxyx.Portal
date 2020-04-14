[ndb_mgmd default]
datadir=/usr/local/mysql/ndbdata
 
[ndbd default]
NoOfReplicas=2      # Number of replicas
DataMemory=256M     # Memory allocate for data storage
IndexMemory=128M    # Memory allocate for index storage
#Directory for Data Node
DataDir=/var/lib/mysql-cluster

[ndb_mgmd]
#Management Node db1
NodeId=1
HostName=47.100.232.68


[ndbd]
#Data Node db2
NodeId=2
HostName=106.15.183.61
DataDir=/usr/local/mysql/data
 
[ndbd]
#Data Node db3
NodeId=3
HostName=47.100.181.213
DataDir=/usr/local/mysql/data
 
[mysqld]
#SQL Node db4
NodeId=4
HostName=47.100.245.61
 
[mysqld]
#SQL Node db5
NodeId=5
HostName=47.100.62.184