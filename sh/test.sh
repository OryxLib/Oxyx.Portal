#!/bin/bash
#echo "Hello World !"
#testN='hhhhh'
#echo testN
#hosts=`sed -n '/^[^#]/p' hostlist`
#echo hosts
for host in `sed -n '/^[^#]/p' hostlist`;do
	echo $host
	sshpass -p 'Adminqwer!@#$' ssh "root@"$host < install.sh
done 
