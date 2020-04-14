#select * from Categories where Id in (select CategoriesId from Tags where Name ='恋爱')
#delete from FileEntry where ContentEntryId in ( select Id from ContentEntry where CategoryId in (select CategoriesId from Tags where Name ='恋爱'));
#delete from ContentEntry where CategoryId in (select CategoriesId from Tags where Name ='恋爱');
#delete from FileEntry where ActualPath like '%i.php%';
#select count(1) from Tags where Name = '恋爱';