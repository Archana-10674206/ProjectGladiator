--===Procedures for bidder
--==selling data
drop proc sp_getCropData
create proc sp_getCropData1
as
begin
select * from tblCropForSale where StatusOfCropSaleReq='approved'
end

exec sp_getCropData1

alter proc sp_newBid(@cropid int,@bidderid int,@bidamt money,@dateofbid date)
as
begin
insert into tblBids(CropId,BidderId,BidAmount,DateOfBid) values(@cropid,@bidderid,@bidamt,@dateofbid)
end

--============================================
create proc sp_InsertintoBidCrops(@bidId int,@FarmerId int ,@CropId int ,@BidderId int)
as
begin
insert into tblBidCrops(bidId,FarmerId,CropId,BidderId) values (@bidId,@FarmerId,@CropId,@BidderId)
end

--===To get Cuuremax bid

alter proc sp_GetMAxBidAmount(@cropid int,@bidderid int)
as
begin
select MAX(BidAmount) from tblBids where CropId=@cropid and bidderid=@bidderid
end

exec sp_GetMAxBidAmount 102



--==>procedures for admin
create proc sp_getPendingSaleData
as 
begin
select * from tblCropForSale where StatusOfCropSaleReq='pending' 
end
exec sp_getPendingSaleData



--==>pending bidder users
create proc sp_getPendingBidders
as
begin
select * from tblBidder where StatusOfBidderDocx='pending'
end
exec sp_getPendingBidders
--==pending farmer users
create proc sp_getPendingFarmers
as 
begin
select * from tblFarmer where StatusOfFarmerDocx='pending'
end
exec sp_getPendingFarmers
--==>pending new bids
create proc sp_getpendingbids1
as 
begin
select
b.bid,a.cropId,b.BidderId,b.bidAmount,b.dateofbid from tblcropforsale a join tblbids b on a.cropid=b.cropid where a.StatusOfCropSaleReq!='sold'

end
exec sp_getpendingbids1

sp_rename 'tblCropForSale.DateOfSoldCrop', 'DateOfRequestForSell', 'COLUMN'
alter table tblCropForSale add DateOfSoldCrop date
select * from tblcropforsale
select * from tblFarmer
--===============Procedure for Admin approvals
alter proc sp_approveFarmer(@fid int,@admin varchar(100),@aDate date,@pass varchar(20),@email varchar(50))
as
begin
update tblFarmer set StatusOfFarmerDocx='verified',ApprovedBy=@admin,ApprovedDate=@aDate where fId=@fid;
insert into tblUser(EmailId,Password,UserType,fid)values(@email,@pass,'Farmer',@fid)
end

exec sp_approveFarmer 1,'shashi','2021-01-09','archana123','archanareddypalli@gmail.com'
--==bidder approval
alter proc sp_approveBidder(@bid int,@admin varchar(100),@adate date,@pass varchar(20),@email varchar(50))
as
begin
update tblBidder set StatusOfBidderDocx='verified',ApprovedBy=@admin,ApprovedDate=@aDate where bId=@bid
insert into tblUser(EmailId,Password,UserType,bid)values(@email,@pass,'Farmer',@bid)
end
--==sale data approval
alter proc sp_approvesale(@cropid int,@cropType varchar(20),@cropname  varchar(20))
as
begin
declare @msp money
set @msp = (select MspPerQuintal from tblcropDetails where CropName=@cropname and CropType=@cropType)
update tblCropForSale set MSP=@msp,StatusOfCropSaleReq='approved' where CropId=@cropid
end

---====bid approval


alter  proc sp_approveBid(@bid int,@cropid int)
as 
begin
update tblCropforsale set SoldPrice=(select max(bidamount) from tblBids where cropid = @cropid) where cropid=@cropid
update tblcropforsale set totalprice=quantity*SoldPrice,StatusOfCropSaleReq='sold',DateOfSoldCrop=GETDATE() where CropId=@cropid
update tblBids set BidStatus='approved' where bId=@bid
end

exec sp_approveBid 




exec  sp_approveBid 803
exec sp_approvesale 102,'kharif','paddy'

select * from tblUser
delete from tblUser where EmailId='archanareddypalli@gmail.com'

exec sp_approveBid 810,102


alter table tblcropforsale drop constraint CK__tblCropFo__Statu__1CF15040

select * from tblinsurance

select * from tblInsuranceClaim


--====Insurance Procedures
alter proc sp_getClaimData
as
begin
select a.InsuranceApplicationId,a.croptype,a.cropname,a.SumInsuredPerHectare,a.dateofapplication,c.dateofloss,a.area,c.causeofloss from tblInsurance a join tblInsuranceClaim c on 
a.InsuranceApplicationId=c.Policyno where c.ClaimStatus='pending';
end


--===approving the cliam
alter procedure proc_checkexpiredateofclaim(@policy int,@dateofinsuranceapp date,@dateofloss date,@fid int,@croptype varchar(30))
as
begin
	declare @y int
	if(@croptype='kharif')
	begin
		set @y=DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)
		if(@y<=92)
			update tblInsuranceClaim set ClaimStatus='claimed' where Policyno=@Policy
		else
			update tblInsuranceClaim set ClaimStatus='rejected' where Policyno=@Policy
	end
	else if(@croptype='rabi')
	begin
		set @y= DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)

		if(@y<=183)
			update tblInsuranceClaim set ClaimStatus='claimed' where Policyno=@Policy
		else
			update tblInsuranceClaim set ClaimStatus='rejected' where Policyno=@Policy
	end
	else if(@croptype='horticultural')
	begin
		set @y= DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)
		if(@y<=92)
		update tblInsuranceClaim set ClaimStatus='claimed' where Policyno=@Policy
		else
		update tblInsuranceClaim set ClaimStatus='rejected' where Policyno=@Policy
	end
	
end

update tblInsuranceClaim set ClaimStatus='pending'
select * from tblInsuranceClaim
exec proc_checkexpiredateofclaim 250005,'06-01-2018','09-01-2019',3,'kharif'


create procedure sp_validateClaim(@dateofinsuranceapp date,@dateofloss date,@fid int,@croptype varchar(30))
as
begin
declare @flag int
declare @y int

if(@croptype='kharif')
begin
set @y=DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)
if(@y<=92)
set @flag=1
else
set @flag=0
end
else if(@croptype='rabi')
begin
set @y= DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)
if(@y<=183)
set @flag=1
else
set @flag=0
end
else if(@croptype='horticultural')
begin
set @y= DATEDiff(DAY,@dateofinsuranceapp,@dateofloss)
if(@y<=92)
set @flag=1
else
set @flag=0
end

select @flag as flag
end

exec sp_validateClaim '06-01-2018','09-01-2019',3,'kharif'

create proc sp_updateClaim(@policy bigint,@flag int)
as
begin
if(@flag = 1)
	update tblInsuranceClaim set  ClaimStatus='claimed' where Policyno=@Policy;
else
	update tblInsuranceClaim set ClaimStatus='rejected' where Policyno=@Policy;
end

exec sp_updateClaim 250000,1


create proc sp_wonBids(@bidderid int)
as 
begin 
select * from tblBids where BidderId=@bidderid and BidStatus='approved'
end

exec sp_wonBids 1

create proc sp_lostBids(@bidderId int)
as 
begin
select * from tblBids where BidderId=@bidderId and BidStatus='pending' and CropId in ( select cropid from tblCropForSale where StatusOfCropSaleReq='sold')
end

exec sp_lostBids 1


create proc sp_getClaimInfo
as
begin
select
a.insuranceApplicationId,a.farmerid,a.croptype,a.cropname,a.suminsured,a.dateofapplication,c.dateofloss,a.area,c.causeofloss from tblinsurance a join tblinsuranceclaim c on a.InsuranceApplicationId=c.Policyno where c.ClaimStatus='pending'
end
