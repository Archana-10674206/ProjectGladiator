insert into tblUser(EmailId,password,usertype,fid) values('archanareddypalli@gmail.com','archana123','farmer',3)
insert into tblUser(EmailId,password,usertype,bid) values('shashikumargundala@gmail.com','shashi123','Bidder',1)
insert into tblUser(EmailId,password,usertype) values('ujjawalsinghi97@gmail.com','ujjawal123','admin')



insert into tblFarmer(fUserName,fContactNo,fEmailId,fAddress,fCity,fState,fPincode,fLandArea,fLandAddress,
fLandPincode,fAccountNo,fIFSCcode,fAadhar,fPan,fCertificate,fPassword,ApprovedBy,ApprovedDate) 
values('Archana','9876543212','archanareddypalli@gmail.com','h.no-1-7-937/1,ashok colony',
'Hyderabad','Telangana','506119',2,'Nilayam','506119','1234123412341234','DWUP123678','123412356712',
'ghfs1234','certificate','archana123','ujjawalsinghi97@gmail.com','06-01-2021')


insert into tblBidder(bUserName,bContactNo,bEmailId,bAddress,bCity,bState,bPincode,bAccountNo,bIFSCcode,bAadhar,bPan,bTraderLicense,bPassword,ApprovedBy,ApprovedDate) 
values('shashi','9876543211','shashikumargundala@gmail.com','h.no-1-7-997/1,ashok colony',
'Hyderabad','Telangana','506019','1234123412341231','DWUP123671','123412356711',
'ghfs12341','certificatebidder','shashi123','ujjawalsinghi97@gmail.com','06-01-2021')

/*select * from tbluser
update tblfarmer set ApprovedBy='ujjawalsinghi97@gmail.com'
update tblBidder set ApprovedBy='ujjawalsinghi97@gmail.com'*/

insert into tblcropforsale(farmerid,croptype,cropname,quantity,FertilizerType,SoilPhCertificate,DateOfSoldCrop) 
values(3,'kharif','paddy','10','type1','certificatesoil','06-01-2021')

insert into tblBids(CropId,BidderId,BidAmount,DateOfBid) values(100,1,30000,'6-01-2021')

insert into tblBidCrops(bidId,FarmerId,CropId,BidderId) values (800,3,100,1)

insert into tblInsurance(FarmerId,CropType,Year,CropName,Area) values (3,'kharif','2021','Paddy',2,40000)

insert into tblInsuranceClaim(Policyno,InsureeName,SumInsured,DateOfLoss,CauseOfLoss) values(250000,'Archana',40000,'6-01-2020','earthquake')