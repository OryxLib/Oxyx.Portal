
    groupadd mysql
    useradd mysql -g mysql
    wget -P /usr/local/src https://dev.mysql.com/get/Downloads/MySQL-Cluster-7.5/mysql-cluster-gpl-7.5.10-linux-glibc2.12-x86_64.tar.gz
    tar -zxf mysql-cluster-gpl-7.5.4.tar.gz
    mv mysql-cluster-gpl-7.5.4 mysql
    chown -R mysql:mysql mysql
    su - mysql
    [writeConfig]
    scripts/mysql_install_db --user=mysql --datadir=/usr/local/mysql/data
    mkdir /var/lib/mysql-cluster
    cd /var/lib/mysql-cluster