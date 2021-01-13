create database dbFarmerScheme3
use dbFarmerScheme3

create table tblUser
( 
	EmailId varchar(50) constraint pk_User primary key ,
	Password varchar(20) not null,
	UserType varchar(20) check(usertype in ('farmer','bidder','admin')),
	fId int constraint fk_uf foreign key references tblFarmer(fId) on delete set null,
	bId int constraint fk_ub foreign key references tblBidder(bId) on delete set null,	
)

/*sp_helpconstraint tbluser
alter table tbluser add constraint pk_user primary key(EmailId)
alter table tbluser add fId int 
alter table tbluser add constraint fk_uf foreign key(fId) references tblfarmer(fId)
alter table tbluser add bId int 
alter table tbluser add constraint fk_ub foreign key(bId) references tblBidder(bId)*/

create table tblFarmer
(
	fId int identity(1,1) constraint pk_Farmer primary key,
	fUserName varchar(50) not null,
	fContactNo varchar(10) not null,
	fEmailId varchar(50) unique not null,
	fAddress varchar(100) not null,
	fCity varchar(20) not null,
	fState varchar(20) not null,
	fPincode varchar(6) not null,
	fLandArea float not null,
	fLandAddress varchar(100) not null,
	fLandPincode varchar(6) not null,
	fAccountNo varchar(16) not null,
	fIFSCcode varchar(20) not null,
	fAadhar varchar(200) not null,
	fPan varchar(200) not null,
	fCertificate varchar(200) not null,
	fPassword varchar(20) not null,
	StatusOfFarmerDocx varchar(20) default 'pending' check(statusOfFarmerDocx in ('pending','verified')),
	ApprovedBy varchar(100),
	ApprovedDate date
)
/*alter table tblfarmer alter column approvedby varchar(100)
alter table tblFarmer drop constraint fk_fu
alter table tblFarmer drop column fEmailId
alter table tblFarmer add fEmailId varchar(50) unique not null
select * from tblFarmer*/


create table tblBidder
(
	bId int identity(1,1) constraint pk_Bidder primary key,
	bUserName varchar(50) not null,
	bContactNo varchar(10) not null,
	bEmailId varchar(50) unique not null,
	bAddress varchar(100) not null,
	bCity varchar(20) not null,
	bState varchar(20) not null,
	bPincode varchar(6) not null,
	bAccountNo varchar(16) not null,
	bIFSCcode varchar(20) not null,
	bAadhar varchar(100) not null,
	bPan varchar(100) not null,
	bTraderLicense varchar(100) not null,
	bPassword varchar(20) not null,
	StatusOfBidderDocx varchar(20) default 'pending' check(statusOfBidderDocx in ('pending','verified')),
	ApprovedBy varchar(100),
	ApprovedDate date
)
/*alter table tblBidder alter column approvedby varchar(100)
alter table tblBidder drop constraint fk_bu
alter table tblBidder drop column bEmailId
alter table tblBidder add bEmailId varchar(50) unique not null*/





create table tblCropForSale
(
	CropId int identity(100,1) constraint pk_CropForSale primary key,
	FarmerId int constraint fk_fc foreign key references tblFarmer(fId) on delete set null,
	CropType varchar(20) check (croptype in ('rabi','kharif','horticultural')),
	CropName varchar(20) not null,
	Quantity float not null,
	FertilizerType varchar(20) not null,
	SoilPhCertificate varchar(200) not null,
	DateOfSoldCrop date,
	MSP money,
	SoldPrice money ,/*max bidamount from tblBiddingCrops*/ 
	TotalPrice money ,
	StatusOfCropSaleReq varchar(20) default 'pending' check (statusOfCropSaleReq in ('approved','pending'))
)
/*alter table tblcropforsale alter column msp money
alter table tblcropforsale alter column soldprice money
alter table tblcropforsale alter column totalprice money*/


/*procedure for marketplace
Marketplace takes croptype and crop name from tblCropForSelling and base price from tblCropDetails and bids from tblBiddingCrops*/


create table tblBids 
(
	bId int identity(800,1) constraint pk_Bids primary key, 
	CropId int constraint fk_cb foreign key references tblCropForSale(CropId) on delete set null,
	BidderId int constraint fk_bb foreign key references tblBidder(bId) on delete set null,
	BidAmount money not null,
	DateOfBid date not null,
	BidStatus varchar(20) default 'pending' check (BidStatus in ('approved','pending','rejected'))
)


create table tblBidCrops
(
    Id int identity(400,1) constraint pk_BidCrops primary key,
    bidId int constraint fk_bbb foreign key references tblBids(bId) on delete set null,
	FarmerId int constraint fk_bbf foreign key references tblFarmer(fId) on delete set null,
	CropId int constraint fk_cbc foreign key references tblCropForSale(CropId) on delete set null,
	BidderId int constraint fk_bbbid foreign key references tblBidder(bId) on delete set null,
	BasePriceAsMSP money,
	FinalBidAmount money,
	BidStatus varchar(20) default 'active' check (BidStatus in ('active','sold'))
	/*current bid is max of bidamount*/
)
/*alter table tblBidCrops alter column basepriceasMsp money
alter table tblBidCrops alter column finalbidamount money

alter table tblBidcrops add constraint ckid default 'active' for bidstatus*/




create table tblCropDetails
(
	Id int identity(1,1) constraint pk_CropDetails primary key,
	CropId int constraint fk_cc foreign key references tblCropForSale(CropId) on delete set null,
	CropType varchar(20) check(CropType in ('kharif','rabi','horticultural')),
	CropName varchar(20) unique not null,
	MspPerQuintal money not null,
	ActuarialRatePercentage float not null,
	YeildPerHectareTons float not null
)


insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('kharif','paddy',1815,4.06,4.5)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('kharif','jowar',2550,52.54,1)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('kharif','groundnut',5090,31.54,1.4)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('kharif','maize',1760,19.24,4)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('kharif','sesame',6485,22.99,0.5)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('rabi','wheat',1840,5,1.4)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('rabi','mustard',4200,10,0.4)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('rabi','gram(bengal gram)',4620,12,0.5)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('rabi','masur(lentil)',4475,15,0.4)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('rabi','barley',1440,2,3.6)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('horticultural','sugarcane',285,5,42)
insert into tblCropDetails(CropType,CropName,MspPerQuintal,ActuarialRatePercentage,YeildPerHectareTons) values('horticultural','cotton',5550,18,0.8)

select * from tblCropDetails


create table tblInsurance
(
	InsuranceApplicationId bigint identity(250000,1) constraint pk_IApp primary key,
	FarmerId int constraint fk_fi foreign key references tblFarmer(fId) on delete set null,
	CropType varchar(10) check(CropType in ('Kharif','Rabi','Horticultural')),
	Year varchar(10) not null,
	CropName varchar(10) not null,
	Area float not null,
	SumInsuredPerHectare money
)
/*alter table tblinsurance drop column suminsured
alter table tblinsurance add SumInsuredPerHectare money
alter table tblinsurance drop constraint UQ__tblInsur__FE7DE0C5EC4D0202
alter table tblinsurance drop column suminsuredperhectare*/



create table tblInsuranceClaim
(
	InsuranceId int identity(1000,1) constraint pk_InsuranceClaim primary key,
	Policyno bigint constraint fk_ii foreign key references tblInsurance(InsuranceApplicationId) on delete set null,
	InsuranceComp varchar(30) default 'AGRICULTURE INSURANCE COMPANY',
	InsureeName varchar(20) not null,
	SumInsured money not null,
	DateOfLoss date not null,
	CauseOfLoss varchar(100) not null,
	ClaimStatus varchar(30) default 'pending' check (ClaimStatus in ('pending','claimed'))
)



