#!/bin/sh
 
# store
socks_found="socks.ok"
 
# ports to scan
ports="1080"

nmap -sS --min-parallelism 700 -n -PN -p"1080" --max-retries 1 --script socks-open-proxy -T 4 -iL ranges.txt 2>/dev/null | grep -B 4 -A 2 "socks-open-proxy:"|tee -a "socks.ok"

nmap -p"8080" -iL ranges.txt --script socks-open-proxy  2> "socks.ok"

nmap -p"1080" -iL ranges.txt --script http-open-proxy 1>"socks.ok"
nmap -p"80" -iL ranges.txt --script http-open-proxy 1>"socks1.ok"
nmap -p"8080" -iL ranges.txt --script http-open-proxy 1>"socks2.ok"

# jquery for pxory page
# var txt = ''
# $("#freelist tr").each(function(){
# txt+=$(this).children().eq(0).text()+":"+$(this).children().eq(1).text()+'\n'
# })
# txt