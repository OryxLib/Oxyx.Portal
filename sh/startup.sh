#机器启动 需要执行的脚本

#1. 启动数据库
 service mariadb start
 service mysql start
#2. 启动Redis 
 redis-server /etc/redis.conf

#3. 启动RebbitMQ
 systemctl start rabbitmq-server
 #systemctl enable rabbitmq-server

#4. 启动Nginx
nginx
#5. 启动supervisor
supervisor 

#查看端口占用

lsof -i tcp: 80
netstat -ntlp
