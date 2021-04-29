use pubs

select * from authors
select * from titleauthor
select * from sales
--1) Create procedure that will take the author first name and last name and print all the books the author sold
create proc proc_author(@fname varchar(20),@lname varchar(20))
as
begin 
 select title_id,title from titles where title_id in 
(select title_id from titleauthor where au_id in 
(select au_id from authors where au_fname=@fname and au_lname=@lname))
end  
drop proc proc_author
exec proc_author 'Charlene','Locksley'
--2) Print the Author name, Publisher name and the sale amount for every book(use joins)
select * from authors
select * from publishers
select * from sales
select * from titles
--select au_fname from authors a join titleauthor t
--on
--a.au_id=t.au_id 
--select price,pub_name from titles join publishers
on p.pub_id=t.pub_id group by pub_name,au_fname,au_lname,price
    declare @pname varchar(20), @title varchar(30) ,@author varchar(30),@pubdate varchar(50),@qty varchar(10)
	declare cur_pub cursor for
	select pub_name,title,au_fname,pubdate,qty from authors,sales,publishers p join titles t
	on
	p.pub_id=t.pub_id
	open cur_pub
	fetch next from cur_pub
	into @pname,@title,@author,@pubdate,@qty
	print 'publishers'
	while @@FETCH_STATUS=0
	begin
	 print 'publisher name: '+@pname
	 print 'title name: '+@title
	  print 'author name: ' +cast(@author as varchar(20))
	   print 'published date:' +cast(@pubdate as varchar(50))
	   print 'quantity sold : ' +cast(@qty as varchar(10))
	 print'______________'
	 fetch next from cur_pub
	into @pname,@title,@author,@pubdate,@qty
	end
	close cur_pub
	deallocate cur_pub
--4) Create a account table
	 update accounte set balance=balance-200 where acc_num='acc1'
	 update accounte set balance=balance+200 where acc_num='acc2'
        if(select balance from accounte where acc_num='acc1' )<1500
	      rollback
	else
	    commit
create trigger trgtrans
on trans
after update
as
begin
    if (select remarks  from inserted)= 'void'
	begin
    update accounte set balance = balance + (select amount from trans where tran_id = (select tran_id from inserted))
	  where acc_num=(select from_account from trans where tran_id = (select tran_id from inserted))
	  update accounte set balance = balance - (select amount from trans where tran_id = (select tran_id from inserted))
	  where acc_num=(select to_account from trans where tran_id = (select tran_id from inserted))
	end
end
update trans set remarks = 'void' where tran_id ='t001'
drop trigger trgtrans
select * from trans
select * from accounte