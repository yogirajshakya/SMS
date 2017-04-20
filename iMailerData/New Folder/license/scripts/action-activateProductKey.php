<?php
// header("location: ../customerRegistration.php");

include("DBConfig.php");
// Variable declaration

$email			= $_POST["email"];
$wstname		= $_POST["wstname"];

$strQuery1 = "select count(*) as wstactive from RegisteredWorkstations where WorkstationName = '$wstname' and PrimaryEmailId = '$email'";
echo $strQuery1."<br/>";
$rsrcResult1 = mysql_query($strQuery1);

while($arrayRow1 = mysql_fetch_assoc($rsrcResult1))
{
	$wstactive = $arrayRow1["wstactive"];
}

$strQuery2 = "select PurchasedLicense, ActivatedLicense from CustomerInfo where PrimaryEmailId = '$email'";
echo $strQuery2."<br/>";
$rsrcResult2 = mysql_query($strQuery2);

while($arrayRow2 = mysql_fetch_assoc($rsrcResult2))
{
	$plicense = $arrayRow2["PurchasedLicense"];
	$alicense = $arrayRow2["ActivatedLicense"];
}

echo "Active Workstations : "	.$wstactive		."<br/>";
echo "Purchased Licenses : "	.$plicense		."<br/>";
echo "ActivatedLicense : "		.$alicense		."<br/>";

if($alicense<$plicense and $wstactive==0)
{
	$alicense = $alicense + 1;
	$strUpdQuery1 = "update CustomerInfo set ActivatedLicense = $alicense where PrimaryEmailId = '$email';";
	$strUpdQuery2 = "insert into RegisteredWorkstations(PrimaryEmailId,WorkstationName, IsActive) values ('$email','$wstname','Y');";
	
	mysql_query($strUpdQuery1)or die('Application encountered error, update query failed');
	mysql_query($strUpdQuery2)or die('Application encountered error, insert query failed');
}
mysql_close($connect);

/*
mssql_query($strQuery)or die('Application encountered error, insert query failed');
*/
?>