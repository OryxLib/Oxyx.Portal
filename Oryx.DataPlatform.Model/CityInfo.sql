#select count(1) from CityInfos where level =2 ;

#select count(1) from StoreMapInfos;
select province ,city , district , count(1) from StoreMapInfos group by district order by province;# where IdCode ='B000AA0Q1P';# like '%金博智慧%';
#select * from StoreMapInfos where district = '青山湖区';
#select distinct district from StoreMapInfos ;
#select count (1) from (select distinct district from StoreMapInfos ) as smi ;