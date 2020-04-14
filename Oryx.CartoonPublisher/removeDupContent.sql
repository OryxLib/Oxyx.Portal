#select count( ContentEntryId) from (select count(1) as num,ContentEntryId from FileEntry group by ActualPath ) as countT where num >1
#select count(ContentEntryId) from FileEntry where Name ='tmpimg'
#remove dup content


#delete from FileEntry where ContentEntryId in (select ContentEntryId from (select count(1) as num,ContentEntryId from FileEntry group by ActualPath ) as countT where num >1)

#create dup file table
drop table tmpDupFileEntry;
create table tmpDupFileEntry (select Id from FileEntry where ContentEntryId in (select ContentEntryId from (select count(1) as num,ContentEntryId from FileEntry group by ActualPath ) as countT where num >1)
group by FileEntry.ActualPath
);


#delete from FileEntry where Id in (select Id from tmpDupFileEntry);