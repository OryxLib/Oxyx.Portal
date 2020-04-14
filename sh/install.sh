groupadd mysql
useradd mysql -g mysql
wget -P /usr/local/src http://mysql.mirror.kangaroot.net/Downloads/MySQL-Cluster-7.6/mysql-cluster-gpl-7.6.5-linux-glibc2.12-x86_64.tar.gz
cd /usr/local/src
tar -zxf mysql-cluster-gpl-7.5.10-linux-glibc2.12-x86_64.tar.gz
mv mysql-cluster-gpl-7.5.10-linux-glibc2.12-x86_64 mysql
yum install libaio
chown -R mysql:mysql mysql
su - mysql
echo '[ndb_mgmd default]
# Directory for MGM node log files
DataDir=/var/lib/mysql-cluster
 
[ndb_mgmd]
#Management Node db1
HostName=47.100.232.68
 
[ndbd default]
NoOfReplicas=2      # Number of replicas
DataMemory=256M     # Memory allocate for data storage
IndexMemory=128M    # Memory allocate for index storage
#Directory for Data Node
DataDir=/var/lib/mysql-cluster
 
[ndbd]
#Data Node db2
HostName=106.15.183.61
DataDir=/usr/local/mysql/data
 
[ndbd]
#Data Node db3
HostName=47.100.181.213
DataDir=/usr/local/mysql/data


[mysqld]
#SQL Node db4
HostName=47.100.245.61
 
[mysqld]
#SQL Node db5
HostName=47.100.62.184' >  /usr/local/mysql/mysql-cluster/config.ini
scripts/mysql_install_db --user=mysql --datadir=/usr/local/mysql/data
mkdir /var/lib/mysql-cluster
cd /var/lib/mysql-cluster
sudo ./ndbmtd --ndb-connectstring=172.19.16.170:1186 --ndb-nodeid=1 --initial-start --foreground

sudo ./mysqld --defaults-file=/usr/local/mysql/mysql-cluster/my.cnf

