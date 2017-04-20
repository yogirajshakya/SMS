<?php
// header("location: ../customerRegistration.php");

include('DBConfig.php');

// Variable declaration
$uname		= $_POST["uname"];
$passwd		= $_POST["passwd"];

$strQuery1 = 'select count(*) as validUser from CustomerInfo where LoginId = \'$uname\' and Password = \'$passwd\'';
$rsrcResult1 = mysql_query($strQuery1);

while($arrayRow1 = mysql_fetch_assoc($rsrcResult1))
{
	$validUser = $arrayRow1["validUser"];
}

echo "Valid User : "		.$validUser		."<br/>";

mysql_close($connect);

/*
mssql_query($strQuery)or die('Application encountered error, insert query failed');
*/
?>