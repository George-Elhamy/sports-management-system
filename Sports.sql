--2.1a
create procedure createAllTables
as 

create table Stadium(
id int primary key identity(1,1), 
status bit , 
name varchar(20) unique ,
capacity int, 
location varchar(20)) ; 

create table Club(
id int primary key identity(1,1), 
location varchar(20), 
name varchar(20) unique) ;



Create TABLE System_admin(
id int   IDENTITY(1,1),
name varchar(20),
username varchar(20) unique, 
password varchar(20),
CONSTRAINT PK_SA Primary key(id));


CREATE TABLE Sports_association_manager(
id int IDENTITY(1,1), 
name varchar(20),
username varchar(20) unique ,
password varchar(20),
CONSTRAINT PK_SAM Primary key(id));


CREATE TABLE Fan(
national_id varchar(20) , 
birth_date date , 
phone_no int,
name varchar(20),
address varchar(20),
status bit,
username varchar(20) unique ,
password varchar(20),
CONSTRAINT PK_F Primary key(national_id));


create table Match(
id int primary key identity(1,1), 
start_time datetime,
end_time datetime, 
host_id int foreign key references Club(id) on delete cascade ,
guest_id int foreign key references Club(id) on delete no action,
stadium_id int foreign key references Stadium(id) on delete no action) ;

create table Ticket( 
id int primary key identity(1,1) ,
status bit , 
fan_id varchar(20),
foreign key (fan_id) references Fan(national_id) on update cascade,
match_id int foreign key references Match(id) on delete cascade ); 


CREATE TABLE Club_representative(
id int IDENTITY(1,1),
name varchar(20),
username varchar(20) unique , 
password varchar(20),
CONSTRAINT PK_CR Primary key(id),
club_id int FOREIGN KEY REFERENCES Club(id) ON DELETE cascade unique) ;


Create TABLE Stadium_manager(
id int   IDENTITY(1,1),
name varchar(20),
username varchar(20) unique ,
password varchar(20),
CONSTRAINT PK_SM Primary key(id),
stadium_id int FOREIGN KEY REFERENCES Stadium(id) ON DELETE cascade unique ) ;

create table Host_request(
id int primary key identity(1,1) , 
status varchar(20)  , 
match_id int , 
club_representative_id int,
stadium_manger_id int, 
foreign key (club_representative_id) references Club_representative(id) , 
foreign key (stadium_manger_id) references Stadium_manager(id) 
) ;

go;


--2.1.b
create procedure dropAllTables
as
drop table Host_request;
drop table Stadium_manager;
drop table Club_representative;
drop table Ticket;
drop table Match;
drop table Fan;
drop table System_admin;
drop table Sports_association_manager;
drop table Stadium;
drop table Club;
go;




--2.1.c
create procedure dropAllProceduresFunctionsViews
as 
drop procedure createAllTables ,dropAllTables, clearAllTables,addAssociationManager,addNewMatch,deleteMatch,
			   deleteMatchesOnStadium,addClub,addTicket,deleteClub,addStadium,deleteStadium,blockFan,unblockFan,
			   addRepresentative,addHostRequest,addStadiumManager,acceptRequest,rejectRequest,addFan,purchaseTicket,
			   updateMatchHost;
drop view allAssocManagers,allClubRepresentatives, allStadiumManagers,allFans,allMatches, allTickets,allCLubs,
		  allStadiums,allRequests,clubsWithNoMatches,matchesPerTeam,clubsNeverMatched;
drop function viewAvailableStadiumsOn,allUnassignedMatches,allPendingRequests,upcomingMatchesOfClub,
			  availableMatchesToAttend,clubsNeverPlayed,matchWithHighestAttendance,matchesRankedByAttendance,
			  requestsFromClub;

go; 


--2.1d

create procedure clearAllTables
as
delete from Host_request;
delete from Stadium_manager;
delete from Club_representative;
delete from Ticket;
delete from Match;
delete from Fan;
delete from System_admin;
delete from Sports_association_manager;
delete from Stadium;
delete from Club;



go;
--2.2a
create view allAssocManagers as
select a.username ,a.password, a.name
from Sports_association_manager a ; 

--2.2b



go;
create view allClubRepresentatives as
select cr.username ,cr.password, cr.name as crname , c.name
from Club_representative cr inner join Club c on cr.club_id=c.id;

go;

--2.2c
create view allStadiumManagers as 
select sm.username ,sm.password, sm.name as stadium_manager_name , s.name
from Stadium_manager sm inner join Stadium s on sm.stadium_id=s.id;

go;
--2.2d
create view allFans as
select f.name, f.username,f.password, f.national_id , f.birth_date ,f.status 
from Fan f ; 

go;
--2.2e
create view  allMatches as
select  h.name as Host , g.name as Guest , m.start_time , m.end_time
from  Club h inner join Match m on h.id=m.host_id 
inner join Club g on m.guest_id=g.id ; 

go;
--2.2f 
create view allTickets as 
select h.name as Host , g.name as Guest, s.name as Stadium_name , m.start_time 
from Ticket t inner join Match m on t.match_id=m.id 
left outer join Stadium s  on s.id=m.stadium_id 
left outer join  Club h  on h.id=m.host_id 
left outer join Club g on m.guest_id=g.id  ; 
go;
--2.2g
create view allClubs as 
select c.name , c.location
from Club c  ;

go;
--2.2h
create view allStadiums as 
select  s.name , s.location , s.capacity , s.status 
from Stadium s ;

go; 
--2.2i

create view allRequests as 
select c.username as Club_representative_username , s.username as Stadium_manager_username , hr.status  
from Club_representative c inner join Host_request hr on c.id=hr.club_representative_id 
						   left outer join Stadium_manager s on s.id=hr.stadium_manger_id; 
						   
go;
--2.3i 
create procedure addAssociationManager
@name varchar(20), @username varchar(20) , @password varchar(20) 
as
insert into Sports_association_manager values (@name , @username ,@password) ;


--2.3ii
go;
create procedure addNewMatch
@host varchar(20), @guest varchar(20), @dateStart datetime, @dateEnd datetime
as
insert into Match(start_time,end_time,guest_id,host_id) values (@dateStart, @dateEnd ,
(select g.id from club g where g.name=@guest ),
(select h.id from club h where h.name=@host))
go;

--2.3iii

create view clubsWithNoMatches as 
select c.name
from Club c 
where c.id not in ( select m.guest_id from Match m) and c.id not in (select m2.host_id from Match m2) ;  

go;


--2.3iv

create procedure deleteMatch
@host varchar(20), @guest varchar(20)
as
delete from Ticket where match_id in (select m.id from Match m inner join Club h on h.id=m.host_id inner join Club g on g.id=m.guest_id
where h.name=@host and g.name=@guest )
delete from Match where( Match.host_id=(select c1.id from Club c1 where c1.name=@host) and Match.guest_id=
(Select c2.id from Club c2 where c2.name=@guest));
go;
--2.3v

create procedure deleteMatchesOnStadium
@stadium_name varchar(20)
as
delete from Ticket where match_id in (select m.id from Match m inner join Stadium s on s.id=m.stadium_id 
where s.name=@stadium_name and  m.start_time>CURRENT_TIMESTAMP  )
delete from Match where (id in (select m.id from Stadium s inner join Match m on s.id=m.stadium_id 
where s.name=@stadium_name and  m.start_time>CURRENT_TIMESTAMP)
)
go;
--2.3vi
create procedure addClub
@name varchar(20),@location varchar(20)
as
insert into Club values (@location,@name) 

go;
--2.3vii
create procedure addTicket
@host varchar(20),@guest varchar(20),@starting_time datetime
as
insert into Ticket (match_id,status) values ((select m.id from Match m inner join club g  on (g.id=m.guest_id )
inner join club h on h.id=m.host_id where h.name=@host and g.name=@guest and m.start_time=@starting_time),'1'); 
go;
--2.3viii

create procedure deleteClub 
@name varchar(20)
as
delete from Ticket where match_id=(select m.id from Match m inner join Club c on c.id=m.host_id or c.id=m.guest_id where c.name=@name)
delete from Match where host_id=(select c.id from Club c where c.name=@name) or 
guest_id=(select c.id from Club c where c.name=@name)

delete from Club where club.name=@name

go;
---2.3ix

create procedure addStadium
@name varchar(20),@location varchar(20),@capacity int
as 
insert into Stadium (name,location,capacity,status) values (@name,@location,@capacity,'1')
go;
---2.3x

create procedure deleteStadium
@name varchar(20)
as
delete from Host_request where stadium_manger_id=(select sm.id from Stadium_manager sm inner join stadium s on s.id=sm.stadium_id where s.name=@name)
delete from Stadium_manager where stadium_id=(select s.id from Stadium s where s.name=@name) 
delete from Ticket where match_id in (select m.id from Match m inner join Stadium s on s.id=m.stadium_id 
where s.name=@name)
delete from Match where id in (select m.id from Stadium s inner join Match m on s.id=m.stadium_id 
where s.name=@name)
delete from Stadium where name=@name 

go;
----2.3xi 
create procedure blockFan
@national_id varchar(20)
as
update  Fan set status='0' where national_id=@national_id 



go; 
--2.3xii
create procedure unblockFan
@national_id varchar(20)
as
update  Fan set status='1' where national_id=@national_id 


go; 
--2.3xiii
create procedure addRepresentative 
@name varchar(20) , @club_name varchar(20) , @username varchar(20) , @password varchar(20) 
as
insert into Club_representative values (@name , @username , @password ,
(select c.id from CLub c where c.name=@club_name)) 


go;
--2.3xiv  

create function viewAvailableStadiumsOn
(@date datetime)  
returns table 
as 
return ( select distinct s.name ,s.location ,s.capacity from Stadium s 
where s.name not in (select distinct s1.name from Stadium s1 inner join match m1 on  s1.id=m1.stadium_id
where  @date >=m1.start_time and @date<=m1.end_time ) ) 

go;
--2.3xv 
create procedure addHostRequest
@clubname varchar(20) , @stadium_name varchar(20) , @starting_time datetime
as 
insert into Host_request(status,match_id,club_representative_id,stadium_manger_id) values('unhandled',(select top(1) m.id from Match m inner join Club h on h.id=m.host_id   
where m.start_time=@starting_time and h.name=@club ),
(select top(1) cr.id from Club c inner join Club_representative cr on c.id=cr.club_id
where c.name=@clubname),(select top(1) sm.id from Stadium S inner join Stadium_manager sm on s.id=sm.stadium_id
where s.name=@stadium_name))  


go;
--2.3xvi 
create function allUnassignedMatches
(@club_name varchar(20)) 
returns table 
as 
return ( select g.name as Guest_name , m.start_time as Start_time from Club h inner join Match m on h.id=m.host_id inner join Club g on g.id=m.guest_id 
where m.stadium_id is null and h.name= @club_name) 


go;
--2.3xvii
create procedure addStadiumManager
@name varchar(20) , @stadium_name varchar(20), @username varchar(20), @password varchar(20)
as 
insert into Stadium_manager values (@name , @username , @password ,
(select s.id from Stadium s where s.name=@stadium_name)) 


go;
--2.3xviii  

create function allPendingRequests
(@stadium_manager_username varchar(20))
returns table   
as 
return (select cr.name as Club_representative_name,g.name as Guest_name, m.start_time , m.end_time , hr.status from  Stadium_manager sm inner join Host_request hr on sm.id=hr.stadium_manger_id 
inner join Club_representative cr on hr.club_representative_id=cr.id inner join Club h on h.id=cr.club_id
inner join Match m on m.host_id=h.id inner join Club g on g.id=m.guest_id inner join Stadium s on s.id=sm.stadium_id
where hr.status='unhandled' and sm.username=@stadium_manager_username )

go;
--2.3xix

create procedure acceptRequest
@stadium_manager_username varchar(20) , @host_name varchar(20) , @guest_name varchar(20) , @start datetime 
as  
update hr
set hr.status='accepted' 
from  Stadium_manager sm inner join Host_request hr on sm.id=hr.stadium_manger_id 
inner join Club_representative cr on hr.club_representative_id=cr.id inner join Match m on m.id=hr.match_id 
inner join Club g on m.guest_id=g.id inner join Club h on m.host_id=h.id
where hr.status='unhandled'  and sm.username=@stadium_manager_username and h.name=@host_name and g.name=@guest_name
and m.start_time=@start;
update m
set m.stadium_id=(select s.id from stadium s inner join Stadium_manager sm on sm.stadium_id=s.id
where sm.username=@stadium_manager_username) 
from  Stadium_manager sm inner join Host_request hr on sm.id=hr.stadium_manger_id 
inner join Club_representative cr on hr.club_representative_id=cr.id inner join Match m on m.id=hr.match_id 
inner join Club g on m.guest_id=g.id inner join Club h on m.host_id=h.id
where hr.status='accepted' and sm.username=@stadium_manager_username and h.name=@host_name and g.name=@guest_name
and m.start_time=@start;

DECLARE @i INT =1;
WHILE @i <= (select s.capacity from stadium s inner join Stadium_manager sm on sm.stadium_id=s.id
where sm.username=@stadium_manager_username) 
	BEGIN
		exec addTicket @host=@host_name , @guest=@guest_name , @starting_time=@start
		SET @i = @i + 1;
	END;

go;
--2.3xx
create procedure rejectRequest 
@stadium_manager_username varchar(20) , @host_name varchar(20) , @guest_name varchar(20) , @start datetime 
as 
update hr
set hr.status='rejected'
from  Stadium_manager sm inner join Host_request hr on sm.id=hr.stadium_manger_id 
inner join Club_representative cr on hr.club_representative_id=cr.id inner join Match m on m.id=hr.match_id 
inner join Club g on m.guest_id=g.id inner join Club h on m.host_id=h.id
where hr.status='unhandled' and sm.username=@stadium_manager_username and h.name=@host_name and g.name=@guest_name
and m.start_time=@start;
go;
--2.3xxi
create procedure addFan 
@name varchar(20) ,@username varchar(20), @password varchar(20), @national_id varchar(20) , @birthdate date , @address varchar(20) , @phone int 
as 
insert into Fan(national_id,birth_date, phone_no,name,address,status,username ,password) 
values(@national_id, @birthdate,@phone, @name , @address ,'1',@username, @password )

go;
--2.3xxii

create function upcomingMatchesOfClub
(@club_name varchar(20) )
returns table 
as
return (select h.name as Host , g.name as Guest, s.name as Stadium
from Match m inner join Club h on m.host_id=h.id 
inner join Club g on m.guest_id=g.id 
left outer join Stadium s on m.stadium_id=s.id
where (h.name=@club_name or g.name=@club_name) and m.start_time>CURRENT_TIMESTAMP);


go;
--2.3xxiii

create function availableMatchesToAttend
(@date datetime) 
returns table 
as 
return (select h.name Host , g.name Guest, s.name Stadium ,s.location , m.start_time 
from Ticket t inner join Match m on t.match_id=m.id 
inner join Club h on m.host_id=h.id 
inner join Club g on m.guest_id=g.id
left outer join Stadium s on s.id=m.stadium_id
where t.status='1' and m.start_time>=@date);

go;
--2.3xxiv 

create procedure purchaseTicket
@national_id varchar(20) , @host varchar(20) , @guest varchar(20) , @starting_time varchar(20) 
as 
update Ticket set status='0',fan_id=@national_id 
where id in (select  top(1) t.id   
from Ticket t inner join Match m on t.match_id=m.id
inner join club h on m.host_id=h.id inner join club g on m.guest_id=g.id 
where  t.status<>'0' and h.name=@host and g.name=@guest and m.start_time=@starting_time );


go;
--2.3xxv

create procedure updateMatchHost
@host varchar(20) , @guest varchar(20) , @start datetime 
as
update Match set guest_id=(select c.id from club c where c.name=@host)
where id= (select m.id from Match m inner join Club h on m.host_id=h.id 
		   inner join Club g on m.guest_id=g.id 
		   where m.start_time=@start and g.name=@guest and h.name =@host);

update Match set host_id=(select c.id from club c where c.name=@guest)
where id= (select m.id from Match m inner join Club h on m.host_id=h.id 
		   inner join Club g on m.guest_id=g.id 
		   where m.start_time=@start and g.name=@host and h.name =@host);


go;
--2.3xxvi
create view matchesPerTeam 
as 
select c.name , count(c.id) as number_of_matches
from Club c inner join Match m on c.id=m.guest_id or c.id=m.host_id
where m.start_time<CURRENT_TIMESTAMP
group by c.name;


go;
--2.3xxvii

create view clubsNeverMatched 
as 
select c1.name Club1 , c2.name Club2
from Club c1 , CLub c2 
where c1.name<>c2.name and c1.id>c2.id and not exists (select *
					from Match m 
					where  (m.guest_id=c1.id and m.host_id=c2.id)  or  (m.host_id=c1.id and m.guest_id=c2.id))


go;
--2.3xxviii
create function clubsNeverPlayed
(@club_name varchar(20))
RETURNs TABLE
AS
return (select c.name
from club c
where c.name<> @club_name and not exists  (select * 
											from club c2 inner join match m on m.host_id=c2.id
											inner join club c3 on c3.id=m.guest_id
											where (c2.name=@club_name and c3.name=c.name)  or 
											(c2.name=c.name and c3.name=@club_name)))

go;
--2.3 xxix 

create function matchWithHighestAttendance
()
returns table 
as 
return (select m.id ,h.name Host ,g.name Guest , count(*) AS NUMBER_OF_TICKETS
		from Club h inner join Match m on h.id=m.host_id inner join Club g on g.id=m.guest_id inner join
		Ticket t on m.id=t.match_id
		where t.status='0'
		group by m.id ,h.name,g.name 
		HAVING COUNT(*)>= ALL (select  count(*) 
		from Match m inner join Ticket t on m.id=t.match_id
		where t.status='0'
		group by m.id));




go;
--2.3xxx 

create function matchesRankedByAttendance
 ()
 returns table
 as 
 return (select rank() over (order by count(*) desc) as Rank ,m.id ,h.name Host ,g.name Guest, count(*) AS NUMBER_OF_SOLD_TICKETS
		from Club h inner join Match m on h.id=m.host_id inner join Club g on g.id=m.guest_id left outer join
		Ticket t on m.id=t.match_id
		where t.status='0'
		group by m.id ,h.name,g.name)

go;
--2.3xxxi
create function requestsFromClub
(@stadium_name varchar(20) , @club_name varchar(20)) 
returns table 
as
return (select h.name Host , g.name Guest 
		from Match m inner join Club h on h.id=m.host_id inner join Club g on g.id=m.guest_id
		inner join Stadium s on m.stadium_id=s.id inner join Host_request hr on m.id=hr.match_id inner join 
		Club_representative cr on cr.id=hr.club_representative_id
		where s.name=@stadium_name and cr.club_id=h.id and h.name=@club_name)


go;
