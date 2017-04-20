<?php
header("location: ../index.php");

include("DBConfig.php");
// Variable declaration
$salRepNo=$_POST["salRepNo"];
$stylegrp=$_POST["stylegrp"];
$compct=$_POST["compct"];

echo $salRepNo;
echo $stylegrp;
echo $compct;

$strQuery = 'UPDATE REPCOMPF SET COMPCT='.$compct.'WHERE SALREP='.$salRepNo.'AND STYGP='.'\''.$stylegrp.'\'';
mssql_query($strQuery)or die('Application encountered error, insert query failed');
mssql_close($db);
?>